using System.ComponentModel;

namespace CB.Domain.Enums;

public enum ETransactionType {
    [Description("Nộp")]
    INCOME = 2,
    [Description("Rút")]
    OUTCOME = 3,
}
