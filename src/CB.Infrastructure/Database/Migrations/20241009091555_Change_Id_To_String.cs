using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Change_Id_To_String : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feature_Pricing_PricingId",
                schema: "public",
                table: "Feature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pricing",
                schema: "public",
                table: "Pricing");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "public",
                table: "Pricing");

            migrationBuilder.AddColumn<string>(
                name: "IdAdd",
                schema: "public",
                table: "Pricing",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PricingId",
                schema: "public",
                table: "Feature",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pricing",
                schema: "public",
                table: "Pricing",
                column: "IdAdd");

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_Pricing_PricingId",
                schema: "public",
                table: "Feature",
                column: "PricingId",
                principalSchema: "public",
                principalTable: "Pricing",
                principalColumn: "IdAdd",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feature_Pricing_PricingId",
                schema: "public",
                table: "Feature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pricing",
                schema: "public",
                table: "Pricing");

            migrationBuilder.DropColumn(
                name: "IdAdd",
                schema: "public",
                table: "Pricing");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "public",
                table: "Pricing",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PricingId",
                schema: "public",
                table: "Feature",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(32)",
                oldFixedLength: true,
                oldMaxLength: 32);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pricing",
                schema: "public",
                table: "Pricing",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feature_Pricing_PricingId",
                schema: "public",
                table: "Feature",
                column: "PricingId",
                principalSchema: "public",
                principalTable: "Pricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
