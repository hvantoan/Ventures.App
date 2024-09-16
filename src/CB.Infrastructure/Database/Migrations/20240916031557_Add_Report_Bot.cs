using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Report_Bot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "UserBot",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "Transaction",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BotReport",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    BotId = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    MerchantId = table.Column<string>(type: "character(32)", fixedLength: true, maxLength: 32, nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    Profit = table.Column<decimal>(type: "numeric", nullable: false),
                    ProfitPercent = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BotReport_Bot_BotId",
                        column: x => x.BotId,
                        principalSchema: "public",
                        principalTable: "Bot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotReport_BotId",
                schema: "public",
                table: "BotReport",
                column: "BotId");

            migrationBuilder.CreateIndex(
                name: "IX_BotReport_Month_Year_BotId",
                schema: "public",
                table: "BotReport",
                columns: new[] { "Month", "Year", "BotId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotReport",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                schema: "public",
                table: "UserBot");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                schema: "public",
                table: "Transaction");
        }
    }
}
