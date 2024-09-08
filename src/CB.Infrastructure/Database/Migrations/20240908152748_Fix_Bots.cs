using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Bots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Account_AccountId",
                schema: "public",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "AccountBot",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "public");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                schema: "public",
                table: "Transaction",
                newName: "UserBotId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_AccountId",
                schema: "public",
                table: "Transaction",
                newName: "IX_Transaction_UserBotId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_UserBot_UserBotId",
                schema: "public",
                table: "Transaction",
                column: "UserBotId",
                principalSchema: "public",
                principalTable: "UserBot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_UserBot_UserBotId",
                schema: "public",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "UserBot",
                schema: "public");

            migrationBuilder.RenameColumn(
                name: "UserBotId",
                schema: "public",
                table: "Transaction",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_UserBotId",
                schema: "public",
                table: "Transaction",
                newName: "IX_Transaction_AccountId");

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    UserId = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    BrokerServer = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    ID_MT4 = table.Column<long>(type: "bigint", nullable: false),
                    PassView = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PassWeb = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Account_AccountId",
                schema: "public",
                table: "Transaction",
                column: "AccountId",
                principalSchema: "public",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
