using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_User_Provider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Provider",
                schema: "public",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "public",
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("dec5aee5-12e1-4b61-8d3f-ad5d5235e6cd"),
                column: "Provider",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provider",
                schema: "public",
                table: "User");
        }
    }
}
