using CB.Domain.Enums;
using CB.Domain.Extentions;

namespace CB.Application.Handlers.ReportHandlers;

public class BotReportQuery : Request<List<BotReport>> {
    public int Month { get; set; }
    public int Year { get; set; }
}

internal class BotReportHandler(IServiceProvider serviceProvider) : BaseHandler<BotReportQuery, List<BotReport>>(serviceProvider) {

    public override async Task<List<BotReport>> Handle(BotReportQuery request, CancellationToken cancellationToken) {
        var now = DateTimeOffset.Now;
        if (now.Month == request.Month) {
            var startDate = new DateTimeOffset(request.Year, request.Month, 1, 0, 0, 0, now.Offset); // get next month
            var endDate = new DateTimeOffset(request.Year, request.Month + 1, 1, 0, 0, 0, now.Offset).AddDays(-1);

            var transactions = await this.db.Transactions.Where(o => o.TransactionAt >= startDate && o.TransactionAt <= endDate).ToListAsync(cancellationToken);
            var userBotIds = transactions.Select(o => o.UserBotId).Distinct().ToList();
            var userBotBalance = await this.db.UserBots.Where(o => userBotIds.Contains(o.Id)).ToDictionaryAsync(k => k.Id, v => v.Balance, cancellationToken);

            var reports = transactions.GroupBy(o => new { o.TransactionAt.Year, o.TransactionAt.Month, o.UserBotId }).Select(o => {
                var banlance = userBotBalance.GetValueOrDefault(o.Key.UserBotId);
                var profit = o.Where(t => t.TransactionType == ETransactionType.Profit).Sum(t => t.Amount);
                return new BotReport {
                    Id = NGuidHelper.New(),
                    BotId = o.Key.UserBotId,
                    Month = o.Key.Month,
                    Year = o.Key.Year,
                    Balance = banlance,
                    Profit = profit,
                    ProfitPercent = profit / banlance
                };
            }).ToList();

            return reports;
        }

        return await this.db.BotReports.Where(o => o.Month == request.Month && o.Year == request.Year).ToListAsync(cancellationToken);
    }
}
