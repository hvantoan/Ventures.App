using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CB.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Update_Table_Config : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_User_UserId",
                schema: "public",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerReports_UserBot_UserBotId",
                schema: "public",
                table: "ServerReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServerReports",
                schema: "public",
                table: "ServerReports");

            migrationBuilder.RenameTable(
                name: "ServerReports",
                schema: "public",
                newName: "ServerReport",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "EV",
                schema: "public",
                table: "UserBot",
                newName: "Ev");

            migrationBuilder.RenameColumn(
                name: "ID_MT4",
                schema: "public",
                table: "UserBot",
                newName: "IdMt4");

            migrationBuilder.RenameColumn(
                name: "CreatAt",
                schema: "public",
                table: "UserBot",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "AffterBalance",
                schema: "public",
                table: "ServerReport",
                newName: "AfterBalance");

            migrationBuilder.RenameColumn(
                name: "AffterAsset",
                schema: "public",
                table: "ServerReport",
                newName: "AfterAsset");

            migrationBuilder.RenameIndex(
                name: "IX_ServerReports_UserBotId",
                schema: "public",
                table: "ServerReport",
                newName: "IX_ServerReport_UserBotId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "public",
                table: "UserBot",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(32)",
                oldFixedLength: true,
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "PassWeb",
                schema: "public",
                table: "UserBot",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PassView",
                schema: "public",
                table: "UserBot",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "UserBot",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BrokerServer",
                schema: "public",
                table: "UserBot",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "UserBot",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "public",
                table: "User",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "public",
                table: "User",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "public",
                table: "User",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCard",
                schema: "public",
                table: "User",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "public",
                table: "User",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "Transaction",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "Role",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "public",
                table: "Role",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "public",
                table: "Merchant",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "public",
                table: "Merchant",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "public",
                table: "Feature",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "Feature",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "public",
                table: "Contact",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SearchName",
                schema: "public",
                table: "Contact",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCard",
                schema: "public",
                table: "Contact",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "BankCardId",
                schema: "public",
                table: "Contact",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "Contact",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "SearchName",
                schema: "public",
                table: "Bot",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ExpirationDate",
                schema: "public",
                table: "BankCard",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserBotId",
                schema: "public",
                table: "ServerReport",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "ServerReport",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "ServerReport",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServerReport",
                schema: "public",
                table: "ServerReport",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Landing",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    MerchantId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", maxLength: 2147483647, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landing", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Landing_MerchantId_Type",
                schema: "public",
                table: "Landing",
                columns: new[] { "MerchantId", "Type" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_User_UserId",
                schema: "public",
                table: "Contact",
                column: "UserId",
                principalSchema: "public",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerReport_UserBot_UserBotId",
                schema: "public",
                table: "ServerReport",
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
                name: "FK_Contact_User_UserId",
                schema: "public",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerReport_UserBot_UserBotId",
                schema: "public",
                table: "ServerReport");

            migrationBuilder.DropTable(
                name: "Landing",
                schema: "public");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServerReport",
                schema: "public",
                table: "ServerReport");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "public",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "ServerReport",
                schema: "public",
                newName: "ServerReports",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "Ev",
                schema: "public",
                table: "UserBot",
                newName: "EV");

            migrationBuilder.RenameColumn(
                name: "IdMt4",
                schema: "public",
                table: "UserBot",
                newName: "ID_MT4");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                schema: "public",
                table: "UserBot",
                newName: "CreatAt");

            migrationBuilder.RenameColumn(
                name: "AfterBalance",
                schema: "public",
                table: "ServerReports",
                newName: "AffterBalance");

            migrationBuilder.RenameColumn(
                name: "AfterAsset",
                schema: "public",
                table: "ServerReports",
                newName: "AffterAsset");

            migrationBuilder.RenameIndex(
                name: "IX_ServerReport_UserBotId",
                schema: "public",
                table: "ServerReports",
                newName: "IX_ServerReports_UserBotId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "public",
                table: "UserBot",
                type: "character(32)",
                fixedLength: true,
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "PassWeb",
                schema: "public",
                table: "UserBot",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PassView",
                schema: "public",
                table: "UserBot",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "UserBot",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "BrokerServer",
                schema: "public",
                table: "UserBot",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "UserBot",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "public",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "public",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                schema: "public",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCard",
                schema: "public",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "public",
                table: "User",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "Transaction",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(32)",
                oldFixedLength: true,
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "Role",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(32)",
                oldFixedLength: true,
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "public",
                table: "Merchant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "public",
                table: "Merchant",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "public",
                table: "Feature",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "public",
                table: "Feature",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(32)",
                oldFixedLength: true,
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "public",
                table: "Contact",
                type: "character varying(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character(32)",
                oldFixedLength: true,
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "SearchName",
                schema: "public",
                table: "Contact",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityCard",
                schema: "public",
                table: "Contact",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "BankCardId",
                schema: "public",
                table: "Contact",
                type: "character varying(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(32)",
                oldFixedLength: true,
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "Contact",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character(32)",
                oldFixedLength: true,
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "SearchName",
                schema: "public",
                table: "Bot",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ExpirationDate",
                schema: "public",
                table: "BankCard",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "UserBotId",
                schema: "public",
                table: "ServerReports",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "MerchantId",
                schema: "public",
                table: "ServerReports",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "public",
                table: "ServerReports",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(32)",
                oldMaxLength: 32);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServerReports",
                schema: "public",
                table: "ServerReports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_User_UserId",
                schema: "public",
                table: "Contact",
                column: "UserId",
                principalSchema: "public",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServerReports_UserBot_UserBotId",
                schema: "public",
                table: "ServerReports",
                column: "UserBotId",
                principalSchema: "public",
                principalTable: "UserBot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
