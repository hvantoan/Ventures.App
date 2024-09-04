namespace CB.Domain.Constants;

public static class Messages {
    public const string User_NotFound = "Người dùng không tồn tại";
    public const string User_Inactive = "Người dùng đã bị vô hiệu hóa";
    public const string User_IncorrectPassword = "Tài khoản hoặc Mật khẩu không chính xác";
    public const string User_NoPermission = "Người dùng không có quyền truy cập";
    public const string User_IncorrentOldPassword = "Sai mật khẩu.";
    public const string User_NotDelete = "Chỉ xóa được người dùng ở trạng thái không cho phép hoạt động.";
    public const string User_Existed = "Người dùng đã tồn tại.";
    public const string User_NotInactive = "Không thể dừng hoạt động với người quản trị.";

    public const string User_NameIsRequire = "Tên người dùng không được để trống";

    public const string Role_NotFound = "Role không tồn tại.";
    public const string Role_NotEmpty = "Role không được để trống.";
    public const string Role_NotDeleted = "Không thể xóa role.";
    public const string Role_Existed = "Phân quyền đã tồn tại.";


    // Merchant
    public const string Request_Invalid = "Lỗi! Vui lòng kiểm tra lại thông tin.";
    public const string Merchant_Existed = "Khách hàng đã tồn tại.";
    public const string Merchant_NotFound = "Khách hàng không tồn tại.";
    public const string Merchant_Expired = "Khách hàng không tồn tại.";
    public const string Merchant_Inactive = "Khách hàng không hoạt động.";

}
