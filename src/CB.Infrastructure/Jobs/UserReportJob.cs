using CB.Domain.Entities;
using CB.Domain.Enums;
using CB.Domain.Extentions;
using CB.Infrastructure.Database;
using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Infrastructure.Jobs;

public class ServerReportJob(IServiceProvider serviceProvider) : IInvocable {
    private readonly CBContext db = serviceProvider.GetRequiredService<CBContext>();

    public async Task Invoke() {
        var now = DateTimeOffset.Now;

        var lastReport = await this.db.ServerReports.OrderByDescending(o => o.Year).ThenBy(o => o.Month).FirstOrDefaultAsync();
        if (lastReport != null && lastReport.Year == now.Year && lastReport.Month == (now.Month - 1)) return; // return, if pre report has exist

        var startDate = lastReport == null ? DateTimeOffset.MinValue : new DateTimeOffset(lastReport.Year, lastReport.Month + 1, 1, 0, 0, 0, now.Offset); // get next month
        var endDate = new DateTimeOffset(now.Year, now.Month, 1, 0, 0, 0, now.Offset).AddDays(-1);

        var transactions = await this.db.Transactions.Include(o => o.UserBot).Where(o => o.TransactionAt >= startDate && o.TransactionAt <= endDate).ToListAsync();
        var reports = transactions.GroupBy(o => new { o.MerchantId, o.TransactionAt.Year, o.TransactionAt.Month, o.UserBotId }).Select(o => {
            var beforeBalance = o.OrderBy(t => t.TransactionAt).First().BeforeBalance;
            var deposit = o.Where(t => t.TransactionType == ETransactionType.Deposit).Sum(t => t.Amount);
            var affterBalance = beforeBalance + deposit;

            var profit = o.Where(t => t.TransactionType == ETransactionType.Profit).Sum(t => t.Amount);

            var beforeAsset = affterBalance + profit;
            var withdrawal = o.Where(o => o.TransactionType == ETransactionType.Withdrawal).Sum(o => o.Amount);
            var affterAsset = beforeAsset - withdrawal;
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

                Commission = 30
            };
        }).ToList();

        await this.db.ServerReports.AddRangeAsync(reports);
        await this.db.SaveChangesAsync();
    }
}
