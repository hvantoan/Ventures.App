using CB.Domain.Enums;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.ReportHandlers;

public class ServerReportQuery : Request<List<ServerReport>> {
    public int Month { get; set; }
    public int Year { get; set; }
}

internal class ServerReportHandler(IServiceProvider serviceProvider) : BaseHandler<ServerReportQuery, List<ServerReport>>(serviceProvider) {

    public override async Task<List<ServerReport>> Handle(ServerReportQuery request, CancellationToken cancellationToken) {
        var now = DateTimeOffset.Now;
        if (now.Month == request.Month) {
            var startDate = new DateTimeOffset(request.Year, request.Month, 1, 0, 0, 0, now.Offset); // get next month
            var endDate = new DateTimeOffset(request.Year, request.Month + 1, 1, 0, 0, 0, now.Offset).AddDays(-1);

            var transactions = await this.db.Transactions.Include(o => o.UserBot).Where(o => o.TransactionAt >= startDate && o.TransactionAt <= endDate).ToListAsync(cancellationToken);

            var userBotIds = transactions.Select(o => o.UserBotId).Distinct().ToList();
            var userBots = await this.db.UserBots.Where(o => userBotIds.Contains(o.Id)).ToDictionaryAsync(k => k.Id, cancellationToken);
            var userIds = userBots.Values.Select(o => o.UserId).Distinct().ToList();
            var users = await this.db.Users.Where(o => userIds.Contains(o.Id)).ToDictionaryAsync(k => k.Id, cancellationToken);

            var reports = transactions.GroupBy(o => new { o.MerchantId, o.TransactionAt.Year, o.TransactionAt.Month, o.UserBotId }).Select(o => {
                var beforeBalance = o.OrderBy(t => t.TransactionAt).First().BeforeBalance;
                var deposit = o.Where(t => t.TransactionType == ETransactionType.Deposit).Sum(t => t.Amount);
                var affterBalance = beforeBalance + deposit;

                var profit = o.Where(t => t.TransactionType == ETransactionType.Profit).Sum(t => t.Amount);

                var beforeAsset = affterBalance + profit;
                var withdrawal = o.Where(o => o.TransactionType == ETransactionType.Withdrawal).Sum(o => o.Amount);
                var affterAsset = beforeAsset - withdrawal;

                var userBot = userBots.GetValueOrDefault(o.Key.UserBotId);
                var user = users.GetValueOrDefault(userBot!.UserId);
                userBot.User = user;

                return new ServerReport {
                    Id = NGuidHelper.New(),
                    UserBotId = o.Key.UserBotId,
                    MerchantId = o.Key.MerchantId,

                    Month = o.Key.Month,
                    Year = o.Key.Year,
                    // Balance
                    BeforeBalance = beforeBalance,
                    Deposit = deposit,
                    AfterBalance = affterBalance,

                    Profit = profit,
                    ProfitPercent = affterBalance == 0 ? 0 : profit / affterBalance,
                    ProfitActual = profit * 0.3m,
                    BeforeAsset = beforeAsset,
                    Withdrawal = o.Where(o => o.TransactionType == ETransactionType.Withdrawal).Sum(o => o.Amount),
                    AfterAsset = affterAsset,

                    Commission = 30,
                    UserBot = userBot,
                };
            }).ToList();
            return reports;
        }

        return await this.db.ServerReports.Include(o => o.UserBot)
            .Where(o => o.Month == request.Month && o.Year == request.Year).ToListAsync(cancellationToken);
    }
}
