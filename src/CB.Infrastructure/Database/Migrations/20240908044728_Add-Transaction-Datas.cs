using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionDatas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    UserId = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    BrokerServer = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ID_MT4 = table.Column<long>(type: "bigint", nullable: false),
                    PassView = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PassWeb = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Transaction",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    AccountId = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionType = table.Column<int>(type: "integer", nullable: false),
                    TransactionDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "public",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountBot",
                schema: "public",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    BotId = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    EV = table.Column<long>(type: "bigint", nullable: false),
                    Ref = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBot", x => new { x.AccountId, x.BotId });
                    table.ForeignKey(
                        name: "FK_AccountBot_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "public",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountBot_Bot_BotId",
                        column: x => x.BotId,
                        principalSchema: "public",
                        principalTable: "Bot",
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

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4880bb3a230620ce4e7e",
                column: "DisplayName",
                value: "Liên hệ");

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                schema: "public",
                table: "Account",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBot_BotId",
                schema: "public",
                table: "AccountBot",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountId",
                schema: "public",
                table: "Transaction",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBot",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Transaction",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Bot",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "public");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4880bb3a230620ce4e7e",
                column: "DisplayName",
                value: "Tổng quan");
        }
    }
}
