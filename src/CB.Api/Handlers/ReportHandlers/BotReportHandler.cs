using CB.Domain.Enums;
using CB.Domain.Extentions;

namespace CB.Api.Handlers.ReportHandlers;

public class BotReportQuery : Common.Request<List<BotReport>> {
    public int Month { get; set; }
    public int Year { get; set; }
}

internal class BotReportHandler(IServiceProvider serviceProvider) : Common.BaseHandler<BotReportQuery, List<BotReport>>(serviceProvider) {

    public override async Task<List<BotReport>> Handle(BotReportQuery request, CancellationToken cancellationToken) {
        var now = DateTimeOffset.Now;
        if (now.Month == request.Month) {
            var startDate = new DateTimeOffset(request.Year, request.Month, 1, 0, 0, 0, now.Offset); // get next month
            var endDate = new DateTimeOffset(request.Year, request.Month + 1, 1, 0, 0, 0, now.Offset).AddDays(-1);

            var transactions = await this.db.Transactions.Include(o => o.UserBot).
                Where(o => o.TransactionAt >= startDate && o.TransactionAt <= endDate).ToListAsync(cancellationToken);
            var userBotIds = transactions.Select(o => o.UserBotId).Distinct().ToList();
            var userBotBalance = await this.db.UserBots.Where(o => userBotIds.Contains(o.Id)).GroupBy(o => o.BotId)
                .ToDictionaryAsync(k => k.Key, v => v.Sum(o => o.Balance), cancellationToken);
            var botIds = await this.db.UserBots.Where(o => userBotIds.Contains(o.Id)).Select(o => o.BotId).Distinct().ToListAsync(cancellationToken);
            var bots = await this.db.Bots.Where(o => botIds.Contains(o.Id)).ToDictionaryAsync(k => k.Id, cancellationToken);

            var reports = transactions.GroupBy(o => new { o.TransactionAt.Year, o.TransactionAt.Month, o.UserBot!.BotId }).Select(o => {
                var banlance = userBotBalance.GetValueOrDefault(o.Key.BotId);
                var profit = o.Where(t => t.TransactionType == ETransactionType.Profit).Sum(t => t.Amount);
                return new BotReport {
                    Id = NGuidHelper.New(),
                    BotId = o.Key.BotId,
                    Month = o.Key.Month,
                    Year = o.Key.Year,
                    Balance = banlance,
                    Profit = profit,
                    MerchantId = request.MerchantId,
                    ProfitPercent = banlance == 0 ? banlance : profit / banlance,
                    Bot = bots.GetValueOrDefault(o.Key.BotId),
                };
            }).ToList();

            return reports;
        }

        return await this.db.BotReports.Include(o => o.Bot)
            .Where(o => o.Month == request.Month && o.Year == request.Year).ToListAsync(cancellationToken);
    }
}
