using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Service_Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "296285809bac481890a454ea8aed6af4",
                column: "OrderIndex",
                value: 11);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "721bb6697d4c4579abc649ed838443cd",
                column: "OrderIndex",
                value: 10);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "98873832ebcb4d9fb12e9b21a187f12c",
                column: "OrderIndex",
                value: 12);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b35cc06a567e420f8d0bda3426091048",
                column: "OrderIndex",
                value: 9);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4990aa3a230620ce4e7e",
                column: "OrderIndex",
                value: 7);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4990bb3a230620ce4e7e",
                column: "OrderIndex",
                value: 6);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "cb26c94262ab4863baa6c516edfde134",
                column: "OrderIndex",
                value: 13);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "dc1c2ce584d74428b4e5241a5502787d",
                column: "OrderIndex",
                value: 8);

            migrationBuilder.InsertData(
                schema: "public",
                table: "Permission",
                columns: new[] { "Id", "ClaimName", "DisplayName", "IsActive", "IsClaim", "IsDefault", "OrderIndex", "ParentId", "Type" },
                values: new object[,]
                {
                    { "b47ccc68c29e4990aa3a230620dd4e7e", "BO.Transaction", "Danh sách giao dịch", true, true, false, 4, "b47ccc68c29e4990bb3a230620ce4e6e", 1 },
                    { "b47ccc68c29e4990bb3a230620ce4e6e", "BO.Service", "Dịch vụ", true, false, false, 3, "ec0f270b424249438540a16e9157c0c8", 1 },
                    { "b47ccc68c29e4990bb3a230620dd4e7e", "BO.Server", "", true, true, false, 5, "b47ccc68c29e4990bb3a230620ce4e6e", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4990aa3a230620dd4e7e");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4990bb3a230620ce4e6e");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4990bb3a230620dd4e7e");

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
                keyValue: "b47ccc68c29e4990aa3a230620ce4e7e",
                column: "OrderIndex",
                value: 4);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b47ccc68c29e4990bb3a230620ce4e7e",
                column: "OrderIndex",
                value: 3);

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
        }
    }
}
