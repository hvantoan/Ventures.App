using System.ComponentModel;

namespace CB.Domain.Enums;

public enum EItemImage {
    [Description("merchant")]
    MerchantLogo = 1,

    [Description("account")]
    UserAvatar = 2,

    [Description("account")]
    UserFontIdentityCard = 3,

    [Description("account")]
    UserBackIdentityCard = 4,

    [Description("account")]
    UserFontBankCard = 5,

    [Description("account")]
    UserBackBankCard = 6,

    [Description("merchant")]
    BotAvatar = 7,
}
