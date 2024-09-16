using CB.Domain.Enums;
using CB.Infrastructure.Database;
using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CB.Infrastructure.Jobs;

public class CalculatorTransactionJob(IServiceProvider serviceProvider) : IInvocable {
    private readonly CBContext db = serviceProvider.GetRequiredService<CBContext>();

    public async Task Invoke() {
        var userBotIds = await this.db.Transactions.Where(o => o.AfterBalance == 0 && o.BeforeBalance == 0).Select(o => o.UserBotId).Distinct().ToListAsync();
        if (userBotIds.Count == 0) return;
        var serverBalance = await this.db.UserBots.Where(o => userBotIds.Contains(o.Id)).ToDictionaryAsync(k => k.Id, v => v.Balance);
        var transactions = await this.db.Transactions.AsTracking().Where(o => userBotIds.Contains(o.UserBotId)).ToListAsync();

        var groups = transactions.GroupBy(o => o.UserBotId).ToDictionary(k => k.Key, v => v.ToList());

        foreach (var group in groups) {
            var balance = serverBalance.GetValueOrDefault(group.Key, 0);
            foreach (var transaction in group.Value.OrderByDescending(o => o.TransactionAt).ToList()) {
                var moneyChange = transaction.TransactionType switch {
                    ETransactionType.Deposit => -transaction.Amount,
                    ETransactionType.Withdrawal => transaction.Amount,
                    ETransactionType.Profit => 0,
                    _ => 0
                };

                transaction.AfterBalance = balance;
                balance += moneyChange;
                transaction.BeforeBalance = balance;
            }
        }
        await this.db.SaveChangesAsync();
    }
}
