using System.ComponentModel;

namespace CB.Domain.Enums;

public enum ETransactionType {

    [Description("Lợi nhuận")]
    Profit = 1,

    [Description("Nộp")]
    Income = 2,

    [Description("Rút")]
    Outcome = 3,
}
