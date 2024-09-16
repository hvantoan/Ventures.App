using System.ComponentModel;

namespace CB.Domain.Enums;

public enum ETransactionType {

    [Description("Lợi nhuận")]
    Profit = 1,

    [Description("Nộp")]
    Deposit = 2,

    [Description("Rút")]
    Withdrawal = 3,
}
