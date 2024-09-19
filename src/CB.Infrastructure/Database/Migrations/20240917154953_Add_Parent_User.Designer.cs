﻿// <auto-generated />
using System;
using CB.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CB.Infrastructure.Database.Migrations
{
    [DbContext(typeof(CBContext))]
    [Migration("20240917154953_Add_Parent_User")]
    partial class Add_Parent_User
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CB.Domain.Entities.BankCard", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("CardBranch")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("bigint");

                    b.Property<string>("Cvv")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("ExpirationDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("character varying(32)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BankCard", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.Bot", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.Property<long>("CreatedAt")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("SearchName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Bot", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.BotReport", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<string>("BotId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<decimal>("Profit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ProfitPercent")
                        .HasColumnType("numeric");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BotId");

                    b.HasIndex("Month", "Year", "BotId")
                        .IsUnique();

                    b.ToTable("BotReport", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.Contact", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("Address")
                        .HasMaxLength(2147483647)
                        .HasColumnType("text");

                    b.Property<string>("BankCardId")
                        .IsRequired()
                        .HasColumnType("character varying(32)");

                    b.Property<long>("CreateAt")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("IdentityCard")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("SearchName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("character varying(32)");

                    b.HasKey("Id");

                    b.HasIndex("BankCardId");

                    b.HasIndex("UserId");

                    b.ToTable("Contact", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.Feature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PricingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PricingId");

                    b.ToTable("Feature", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.ItemImage", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .HasColumnType("character varying(8000)");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<int>("ItemType")
                        .HasMaxLength(20)
                        .HasColumnType("integer");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId", "ItemType");

                    b.ToTable("ItemImage", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.Merchant", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ApiSecret")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long?>("At")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Commune")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("bigint");

                    b.Property<string>("District")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<long>("ExpiredDate")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("Province")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("SearchName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Merchant", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.Permission", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("ClaimName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsClaim")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("boolean");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("integer");

                    b.Property<string>("ParentId")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<int>("Type")
                        .HasMaxLength(20)
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Permission", "public");

                    b.HasData(
                        new
                        {
                            Id = "ec0f270b424249438540a16e9157c0c8",
                            ClaimName = "BO",
                            DisplayName = "Trang quản lý",
                            IsActive = true,
                            IsClaim = false,
                            IsDefault = true,
                            OrderIndex = 0,
                            Type = 1
                        },
                        new
                        {
                            Id = "b47bbb68c29e4880bb3a230620ce4e6e",
                            ClaimName = "BO.Dashboard",
                            DisplayName = "Tổng quan",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = true,
                            OrderIndex = 1,
                            ParentId = "ec0f270b424249438540a16e9157c0c8",
                            Type = 1
                        },
                        new
                        {
                            Id = "b47ccc68c29e4880bb3a230620ce4e7e",
                            ClaimName = "BO.Contact",
                            DisplayName = "Liên hệ",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = true,
                            OrderIndex = 2,
                            ParentId = "ec0f270b424249438540a16e9157c0c8",
                            Type = 1
                        },
                        new
                        {
                            Id = "b47ccc68c29e4990bb3a230620ce4e6e",
                            ClaimName = "BO.Service",
                            DisplayName = "Dịch vụ",
                            IsActive = true,
                            IsClaim = false,
                            IsDefault = false,
                            OrderIndex = 3,
                            ParentId = "ec0f270b424249438540a16e9157c0c8",
                            Type = 1
                        },
                        new
                        {
                            Id = "b47ccc68c29e4990aa3a230620dd4e7e",
                            ClaimName = "BO.Transaction",
                            DisplayName = "Danh sách giao dịch",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = false,
                            OrderIndex = 4,
                            ParentId = "b47ccc68c29e4990bb3a230620ce4e6e",
                            Type = 1
                        },
                        new
                        {
                            Id = "b47ccc68c29e4990bb3a230620dd4e7e",
                            ClaimName = "BO.Server",
                            DisplayName = "",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = false,
                            OrderIndex = 5,
                            ParentId = "b47ccc68c29e4990bb3a230620ce4e6e",
                            Type = 1
                        },
                        new
                        {
                            Id = "b47ccc68c29e4990bb3a230620ce4e7e",
                            ClaimName = "BO.Category",
                            DisplayName = "Danh mục",
                            IsActive = true,
                            IsClaim = false,
                            IsDefault = false,
                            OrderIndex = 6,
                            ParentId = "ec0f270b424249438540a16e9157c0c8",
                            Type = 1
                        },
                        new
                        {
                            Id = "b47ccc68c29e4990aa3a230620ce4e7e",
                            ClaimName = "BO.Bot",
                            DisplayName = "Bot",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = false,
                            OrderIndex = 7,
                            ParentId = "b47ccc68c29e4990bb3a230620ce4e7e",
                            Type = 1
                        },
                        new
                        {
                            Id = "dc1c2ce584d74428b4e5241a5502787d",
                            ClaimName = "BO.Setting",
                            DisplayName = "Cài đặt",
                            IsActive = true,
                            IsClaim = false,
                            IsDefault = false,
                            OrderIndex = 8,
                            ParentId = "ec0f270b424249438540a16e9157c0c8",
                            Type = 1
                        },
                        new
                        {
                            Id = "b35cc06a567e420f8d0bda3426091048",
                            ClaimName = "BO.General",
                            DisplayName = "Cài đặt chung",
                            IsActive = true,
                            IsClaim = false,
                            IsDefault = false,
                            OrderIndex = 9,
                            ParentId = "dc1c2ce584d74428b4e5241a5502787d",
                            Type = 1
                        },
                        new
                        {
                            Id = "721bb6697d4c4579abc649ed838443cd",
                            ClaimName = "BO.General.Api",
                            DisplayName = "Cài đặt nâng cao",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = false,
                            OrderIndex = 10,
                            ParentId = "b35cc06a567e420f8d0bda3426091048",
                            Type = 1
                        },
                        new
                        {
                            Id = "296285809bac481890a454ea8aed6af4",
                            ClaimName = "BO.User",
                            DisplayName = "Người dùng",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = false,
                            OrderIndex = 11,
                            ParentId = "dc1c2ce584d74428b4e5241a5502787d",
                            Type = 1
                        },
                        new
                        {
                            Id = "98873832ebcb4d9fb12e9b21a187f12c",
                            ClaimName = "BO.User.Reset",
                            DisplayName = "Đặt lại mật khẩu",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = false,
                            OrderIndex = 12,
                            ParentId = "296285809bac481890a454ea8aed6af4",
                            Type = 1
                        },
                        new
                        {
                            Id = "cb26c94262ab4863baa6c516edfde134",
                            ClaimName = "BO.Role",
                            DisplayName = "Phân quyền",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = false,
                            OrderIndex = 13,
                            ParentId = "dc1c2ce584d74428b4e5241a5502787d",
                            Type = 1
                        });
                });

            modelBuilder.Entity("CB.Domain.Entities.Pricing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Interval")
                        .HasColumnType("integer");

                    b.Property<string>("MonetaryUnit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Pricing", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("SearchName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Role", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.RolePermission", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("PermissionId")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("boolean");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.ServerReport", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("AffterAsset")
                        .HasColumnType("numeric");

                    b.Property<decimal>("AffterBalance")
                        .HasColumnType("numeric");

                    b.Property<decimal>("BeforeAsset")
                        .HasColumnType("numeric");

                    b.Property<decimal>("BeforeBalance")
                        .HasColumnType("numeric");

                    b.Property<int>("Commission")
                        .HasColumnType("integer");

                    b.Property<decimal>("Deposit")
                        .HasColumnType("numeric");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<decimal>("Profit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ProfitActual")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ProfitPercent")
                        .HasColumnType("numeric");

                    b.Property<string>("UserBotId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Withdrawal")
                        .HasColumnType("numeric");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserBotId");

                    b.ToTable("ServerReports", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<decimal>("AfterBalance")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("BeforeBalance")
                        .HasColumnType("numeric");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TransactionAt")
                        .HasColumnType("bigint");

                    b.Property<int>("TransactionType")
                        .HasColumnType("integer");

                    b.Property<string>("UserBotId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("UserBotId");

                    b.ToTable("Transaction", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Commune")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("District")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("IdentityCard")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("boolean");

                    b.Property<long>("LastSession")
                        .HasColumnType("bigint");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("ParentId")
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Provider")
                        .HasColumnType("integer");

                    b.Property<string>("Province")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("RoleId")
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.Property<string>("SearchName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId");

                    b.HasIndex("RoleId");

                    b.HasIndex("MerchantId", "Username")
                        .IsUnique();

                    b.ToTable("User", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.UserBot", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<string>("BotId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.Property<string>("BrokerServer")
                        .HasColumnType("text");

                    b.Property<long>("CreatAt")
                        .HasColumnType("bigint");

                    b.Property<long>("EV")
                        .HasColumnType("bigint");

                    b.Property<long>("ID_MT4")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PassView")
                        .HasColumnType("text");

                    b.Property<string>("PassWeb")
                        .HasColumnType("text");

                    b.Property<long>("Ref")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character(32)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("BotId");

                    b.HasIndex("UserId", "BotId");

                    b.ToTable("UserBot", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.BankCard", b =>
                {
                    b.HasOne("CB.Domain.Entities.User", "User")
                        .WithMany("BankCards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CB.Domain.Entities.BotReport", b =>
                {
                    b.HasOne("CB.Domain.Entities.Bot", "Bot")
                        .WithMany("BotReports")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bot");
                });

            modelBuilder.Entity("CB.Domain.Entities.Contact", b =>
                {
                    b.HasOne("CB.Domain.Entities.BankCard", "BankCard")
                        .WithMany()
                        .HasForeignKey("BankCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CB.Domain.Entities.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserId");

                    b.Navigation("BankCard");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CB.Domain.Entities.Feature", b =>
                {
                    b.HasOne("CB.Domain.Entities.Pricing", "Pricing")
                        .WithMany("Features")
                        .HasForeignKey("PricingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pricing");
                });

            modelBuilder.Entity("CB.Domain.Entities.RolePermission", b =>
                {
                    b.HasOne("CB.Domain.Entities.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CB.Domain.Entities.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CB.Domain.Entities.ServerReport", b =>
                {
                    b.HasOne("CB.Domain.Entities.UserBot", "UserBot")
                        .WithMany()
                        .HasForeignKey("UserBotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserBot");
                });

            modelBuilder.Entity("CB.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("CB.Domain.Entities.UserBot", "UserBot")
                        .WithMany("Transactions")
                        .HasForeignKey("UserBotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserBot");
                });

            modelBuilder.Entity("CB.Domain.Entities.User", b =>
                {
                    b.HasOne("CB.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CB.Domain.Entities.UserBot", b =>
                {
                    b.HasOne("CB.Domain.Entities.Bot", "Bot")
                        .WithMany("UserBots")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CB.Domain.Entities.User", "User")
                        .WithMany("UserBots")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CB.Domain.Entities.Bot", b =>
                {
                    b.Navigation("BotReports");

                    b.Navigation("UserBots");
                });

            modelBuilder.Entity("CB.Domain.Entities.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("CB.Domain.Entities.Pricing", b =>
                {
                    b.Navigation("Features");
                });

            modelBuilder.Entity("CB.Domain.Entities.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CB.Domain.Entities.User", b =>
                {
                    b.Navigation("BankCards");

                    b.Navigation("Contacts");

                    b.Navigation("UserBots");
                });

            modelBuilder.Entity("CB.Domain.Entities.UserBot", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
