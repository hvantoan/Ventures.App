using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Server_Report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AfterBalance",
                schema: "public",
                table: "Transaction",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BeforeBalance",
                schema: "public",
                table: "Transaction",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ServerReports",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserBotId = table.Column<string>(type: "text", nullable: false),
                    MerchantId = table.Column<string>(type: "text", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    BeforeBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    Deposit = table.Column<decimal>(type: "numeric", nullable: false),
                    AffterBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    Profit = table.Column<decimal>(type: "numeric", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    ProfitActual = table.Column<decimal>(type: "numeric", nullable: false),
                    BeforeAsset = table.Column<decimal>(type: "numeric", nullable: false),
                    Withdrawal = table.Column<decimal>(type: "numeric", nullable: false),
                    AffterAsset = table.Column<decimal>(type: "numeric", nullable: false),
                    Commission = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerReports_UserBot_UserBotId",
                        column: x => x.UserBotId,
                        principalSchema: "public",
                        principalTable: "UserBot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerReports_UserBotId",
                schema: "public",
                table: "ServerReports",
                column: "UserBotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerReports",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "AfterBalance",
                schema: "public",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "BeforeBalance",
                schema: "public",
                table: "Transaction");
        }
    }
}
