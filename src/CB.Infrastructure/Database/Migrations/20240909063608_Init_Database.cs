using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Bot",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemImage",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    MerchantId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ItemId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ItemType = table.Column<int>(type: "integer", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchant",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SearchName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Province = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    District = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Commune = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ExpiredDate = table.Column<long>(type: "bigint", nullable: false),
                    ApiSecret = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    At = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ParentId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    ClaimName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsClaim = table.Column<bool>(type: "boolean", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pricing",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    MonetaryUnit = table.Column<string>(type: "text", nullable: false),
                    Interval = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pricing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    MerchantId = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SearchName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<long>(type: "bigint", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PricingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feature_Pricing_PricingId",
                        column: x => x.PricingId,
                        principalSchema: "public",
                        principalTable: "Pricing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "public",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    PermissionId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsEnable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "public",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    MerchantId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    RoleId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Provider = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SearchName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    Province = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    District = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Commune = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    LastSession = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankCard",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    UserId = table.Column<string>(type: "character varying(32)", nullable: false),
                    CardNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CardBranch = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Cvv = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    ExpirationDate = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCard_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBot",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    BotId = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    BrokerServer = table.Column<string>(type: "text", nullable: true),
                    ID_MT4 = table.Column<long>(type: "bigint", nullable: false),
                    PassView = table.Column<string>(type: "text", nullable: true),
                    PassWeb = table.Column<string>(type: "text", nullable: true),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    EV = table.Column<long>(type: "bigint", nullable: false),
                    Ref = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBot_Bot_BotId",
                        column: x => x.BotId,
                        principalSchema: "public",
                        principalTable: "Bot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBot_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    UserId = table.Column<string>(type: "character varying(32)", nullable: true),
                    BankCardId = table.Column<string>(type: "character varying(32)", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "text", maxLength: 2147483647, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_BankCard_BankCardId",
                        column: x => x.BankCardId,
                        principalSchema: "public",
                        principalTable: "BankCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contact_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    UserBotId = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionType = table.Column<int>(type: "integer", nullable: false),
                    TransactionDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_UserBot_UserBotId",
                        column: x => x.UserBotId,
                        principalSchema: "public",
                        principalTable: "UserBot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Bot",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { "ec0f270b424249438540a16e9157c0c8", "Bot giao dịch tự động Forex", "CBV_SynthFX" },
                    { "ec0f272b424249938540a16e9157c0c8", "Bot giao dịch cổ phiếu", "FX_Trader" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Permission",
                columns: new[] { "Id", "ClaimName", "DisplayName", "IsActive", "IsClaim", "IsDefault", "OrderIndex", "ParentId", "Type" },
                values: new object[,]
                {
                    { "296285809bac481890a454ea8aed6af4", "BO.User", "Người dùng", true, true, false, 6, "dc1c2ce584d74428b4e5241a5502787d", 1 },
                    { "721bb6697d4c4579abc649ed838443cd", "BO.General.Api", "Cài đặt nâng cao", true, true, false, 5, "b35cc06a567e420f8d0bda3426091048", 1 },
                    { "98873832ebcb4d9fb12e9b21a187f12c", "BO.User.Reset", "Đặt lại mật khẩu", true, true, false, 7, "296285809bac481890a454ea8aed6af4", 1 },
                    { "b35cc06a567e420f8d0bda3426091048", "BO.General", "Cài đặt chung", true, false, false, 4, "dc1c2ce584d74428b4e5241a5502787d", 1 },
                    { "b47bbb68c29e4880bb3a230620ce4e6e", "BO.Dashboard", "Tổng quan", true, true, true, 1, "ec0f270b424249438540a16e9157c0c8", 1 },
                    { "b47ccc68c29e4880bb3a230620ce4e7e", "BO.Contact", "Liên hệ", true, true, true, 2, "ec0f270b424249438540a16e9157c0c8", 1 },
                    { "cb26c94262ab4863baa6c516edfde134", "BO.Role", "Phân quyền", true, true, false, 8, "dc1c2ce584d74428b4e5241a5502787d", 1 },
                    { "dc1c2ce584d74428b4e5241a5502787d", "BO.Setting", "Cài đặt", true, false, false, 3, "ec0f270b424249438540a16e9157c0c8", 1 },
                    { "ec0f270b424249438540a16e9157c0c8", "BO", "Trang quản lý", true, false, true, 0, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankCard_UserId",
                schema: "public",
                table: "BankCard",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_BankCardId",
                schema: "public",
                table: "Contact",
                column: "BankCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserId",
                schema: "public",
                table: "Contact",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feature_PricingId",
                schema: "public",
                table: "Feature",
                column: "PricingId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImage_MerchantId_ItemType",
                schema: "public",
                table: "ItemImage",
                columns: new[] { "MerchantId", "ItemType" });

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_Code",
                schema: "public",
                table: "Merchant",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "public",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserBotId",
                schema: "public",
                table: "Transaction",
                column: "UserBotId");

            migrationBuilder.CreateIndex(
                name: "IX_User_MerchantId",
                schema: "public",
                table: "User",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_User_MerchantId_Username",
                schema: "public",
                table: "User",
                columns: new[] { "MerchantId", "Username" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "public",
                table: "User",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBot_BotId",
                schema: "public",
                table: "UserBot",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBot_UserId_BotId",
                schema: "public",
                table: "UserBot",
                columns: new[] { "UserId", "BotId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Feature",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ItemImage",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Merchant",
                schema: "public");

            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Transaction",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BankCard",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Pricing",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "public");

            migrationBuilder.DropTable(
                name: "UserBot",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Bot",
                schema: "public");

            migrationBuilder.DropTable(
                name: "User",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "public");
        }
    }
}
