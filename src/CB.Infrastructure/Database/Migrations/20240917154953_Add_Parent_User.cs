using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Parent_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "public",
                table: "User",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                schema: "public",
                table: "User",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "public",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "public",
                table: "User",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character(32)",
                oldFixedLength: true,
                oldMaxLength: 32,
                oldNullable: true);
        }
    }
}
