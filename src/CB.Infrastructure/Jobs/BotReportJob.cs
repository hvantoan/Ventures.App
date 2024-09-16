using CB.Domain.Entities;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using CB.Infrastructure.Database;
using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Infrastructure.Jobs;

public class BotReportJob(IServiceProvider serviceProvider) : IInvocable {
    private readonly CBContext db = serviceProvider.GetRequiredService<CBContext>();

    public async Task Invoke() {
        var now = DateTimeOffset.Now;

        var lastReport = await this.db.BotReports.OrderByDescending(o => o.Year).ThenBy(o => o.Month).FirstOrDefaultAsync();
        if (lastReport != null && lastReport.Year == now.Year && lastReport.Month == (now.Month - 1)) return; // return, if pre report has exist

        var startDate = lastReport == null ? DateTimeOffset.MinValue : new DateTimeOffset(lastReport.Year, lastReport.Month + 1, 1, 0, 0, 0, now.Offset); // get next month
        var endDate = new DateTimeOffset(now.Year, now.Month, 1, 0, 0, 0, now.Offset).AddDays(-1);

        var transactions = await this.db.Transactions.Include(o => o.UserBot).Where(o => o.TransactionAt >= startDate && o.TransactionAt <= endDate).ToListAsync();
        var userBotIds = transactions.Select(o => o.UserBotId).Distinct().ToList();
        var userBotBalance = await this.db.UserBots.Where(o => userBotIds.Contains(o.Id)).GroupBy(o => o.BotId).ToDictionaryAsync(k => k.Key, v => v.Sum(o => o.Balance));

        var reports = transactions.GroupBy(o => new { o.MerchantId, o.TransactionAt.Year, o.TransactionAt.Month, o.UserBot!.BotId }).Select(o => {
            var banlance = userBotBalance.GetValueOrDefault(o.Key.BotId);
            var profit = o.Where(t => t.TransactionType == ETransactionType.Profit).Sum(t => t.Amount);
            return new BotReport {
                Id = NGuidHelper.New(),
                BotId = o.Key.BotId,
                MerchantId = o.Key.MerchantId,
                Month = o.Key.Month,
                Year = o.Key.Year,
                Balance = banlance,
                Profit = profit,
                ProfitPercent = banlance == 0 ? banlance : profit / banlance,
            };
        }).ToList();

        await this.db.BotReports.AddRangeAsync(reports);
        await this.db.SaveChangesAsync();
    }
}
