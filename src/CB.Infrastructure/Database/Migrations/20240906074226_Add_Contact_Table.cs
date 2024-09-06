using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Contact_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "296285809bac481890a454ea8aed6af4",
                column: "OrderIndex",
                value: 6);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "721bb6697d4c4579abc649ed838443cd",
                columns: new[] { "ClaimName", "OrderIndex" },
                values: new object[] { "BO.General.Api", 5 });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "98873832ebcb4d9fb12e9b21a187f12c",
                column: "OrderIndex",
                value: 7);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b35cc06a567e420f8d0bda3426091048",
                column: "OrderIndex",
                value: 4);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "cb26c94262ab4863baa6c516edfde134",
                column: "OrderIndex",
                value: 8);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "dc1c2ce584d74428b4e5241a5502787d",
                column: "OrderIndex",
                value: 3);

            migrationBuilder.InsertData(
                schema: "public",
                table: "Permission",
                columns: new[] { "Id", "ClaimName", "DisplayName", "IsActive", "IsClaim", "IsDefault", "OrderIndex", "ParentId", "Type" },
                values: new object[] { "b47ccc68c29e4880bb3a230620ce4e7e", "BO.Contact", "Tổng quan", true, true, true, 2, "ec0f270b424249438540a16e9157c0c8", 1 });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact",
                schema: "public");

            migrationBuilder.DropTable(
                name: "BankCard",
                schema: "public");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4880bb3a230620ce4e7e");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "296285809bac481890a454ea8aed6af4",
                column: "OrderIndex",
                value: 5);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "721bb6697d4c4579abc649ed838443cd",
                columns: new[] { "ClaimName", "OrderIndex" },
                values: new object[] { "BO.General.Advanced", 4 });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "98873832ebcb4d9fb12e9b21a187f12c",
                column: "OrderIndex",
                value: 6);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b35cc06a567e420f8d0bda3426091048",
                column: "OrderIndex",
                value: 3);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "cb26c94262ab4863baa6c516edfde134",
                column: "OrderIndex",
                value: 7);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "dc1c2ce584d74428b4e5241a5502787d",
                column: "OrderIndex",
                value: 2);
        }
    }
}
