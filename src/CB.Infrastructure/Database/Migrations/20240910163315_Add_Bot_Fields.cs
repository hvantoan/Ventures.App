using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Bot_Fields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "Bot",
                keyColumn: "Id",
                keyValue: "ec0f270b424249438540a16e9157c0c8");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "Bot",
                keyColumn: "Id",
                keyValue: "ec0f272b424249938540a16e9157c0c8");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                schema: "public",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "public",
                table: "BankCard");

            migrationBuilder.AddColumn<long>(
                name: "CreateAt",
                schema: "public",
                table: "Contact",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "SearchName",
                schema: "public",
                table: "Contact",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "Bot",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                schema: "public",
                table: "Bot",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                schema: "public",
                table: "Bot",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "Bot",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SearchName",
                schema: "public",
                table: "Bot",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "CreatedAt",
                schema: "public",
                table: "BankCard",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "296285809bac481890a454ea8aed6af4",
                column: "OrderIndex",
                value: 8);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "721bb6697d4c4579abc649ed838443cd",
                column: "OrderIndex",
                value: 7);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "98873832ebcb4d9fb12e9b21a187f12c",
                column: "OrderIndex",
                value: 9);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b35cc06a567e420f8d0bda3426091048",
                column: "OrderIndex",
                value: 6);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "cb26c94262ab4863baa6c516edfde134",
                column: "OrderIndex",
                value: 10);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "dc1c2ce584d74428b4e5241a5502787d",
                column: "OrderIndex",
                value: 5);

            migrationBuilder.InsertData(
                schema: "public",
                table: "Permission",
                columns: new[] { "Id", "ClaimName", "DisplayName", "IsActive", "IsClaim", "IsDefault", "OrderIndex", "ParentId", "Type" },
                values: new object[,]
                {
                    { "b47ccc68c29e4990aa3a230620ce4e7e", "BO.Bot", "Bot", true, true, false, 4, "b47ccc68c29e4990bb3a230620ce4e7e", 1 },
                    { "b47ccc68c29e4990bb3a230620ce4e7e", "BO.Category", "Danh mục", true, false, false, 3, "ec0f270b424249438540a16e9157c0c8", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4990aa3a230620ce4e7e");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4990bb3a230620ce4e7e");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                schema: "public",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "SearchName",
                schema: "public",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "public",
                table: "Bot");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                schema: "public",
                table: "Bot");

            migrationBuilder.DropColumn(
                name: "MerchantId",
                schema: "public",
                table: "Bot");

            migrationBuilder.DropColumn(
                name: "SearchName",
                schema: "public",
                table: "Bot");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "public",
                table: "BankCard");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                schema: "public",
                table: "Contact",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "Bot",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(32)",
                oldFixedLength: true,
                oldMaxLength: 32);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedDate",
                schema: "public",
                table: "BankCard",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

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
                keyValue: "296285809bac481890a454ea8aed6af4",
                column: "OrderIndex",
                value: 6);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "721bb6697d4c4579abc649ed838443cd",
                column: "OrderIndex",
                value: 5);

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
        }
    }
}
