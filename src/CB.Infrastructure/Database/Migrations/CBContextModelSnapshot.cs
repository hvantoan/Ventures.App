﻿// <auto-generated />
using System;
using CB.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CB.Infrastructure.Database.Migrations
{
    [DbContext(typeof(CBContext))]
    partial class CBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CB.Domain.Entities.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("CreateAt")
                        .HasColumnType("bigint");

                    b.Property<Guid>("CreateBy")
                        .HasColumnType("uuid");

                    b.Property<long?>("DeleteAt")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("DeleteBy")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<long?>("UpdateAt")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("UpdateBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Type", "ParentId");

                    b.ToTable("Attachment", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("uuid");

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

                    b.Property<Guid?>("ParentId")
                        .HasMaxLength(32)
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Permission", "public");

                    b.HasData(
                        new
                        {
                            Id = new Guid("01f543fd-521f-41c7-83b3-00253996dd69"),
                            ClaimName = "CB.Kaban",
                            DisplayName = "Quản lý dự án",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = true,
                            OrderIndex = 0
                        },
                        new
                        {
                            Id = new Guid("4ff1aa66-fc29-4e06-becb-6e307e6aa09a"),
                            ClaimName = "CB.DevTools",
                            DisplayName = "Công cụ",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = true,
                            OrderIndex = 1
                        },
                        new
                        {
                            Id = new Guid("cc91c9c4-5845-407d-867b-0c1453f2b852"),
                            ClaimName = "CB.User",
                            DisplayName = "Quản lý người dùng",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = false,
                            OrderIndex = 2
                        },
                        new
                        {
                            Id = new Guid("31f07c51-7067-4e96-9f44-de6a02818513"),
                            ClaimName = "CB.User.Password",
                            DisplayName = "Quản lý mật khẩu",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = false,
                            OrderIndex = 3,
                            ParentId = new Guid("de5ffa57-021d-4768-b361-894828259350")
                        },
                        new
                        {
                            Id = new Guid("8ad5baf8-b7f6-433c-94e7-87ca45728945"),
                            ClaimName = "CB.User.Edit",
                            DisplayName = "Cập nhật người dùng",
                            IsActive = true,
                            IsClaim = true,
                            IsDefault = false,
                            OrderIndex = 4,
                            ParentId = new Guid("cc91c9c4-5845-407d-867b-0c1453f2b852")
                        });
                });

            modelBuilder.Entity("CB.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32)
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("CreatedDate")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

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
                    b.Property<Guid>("RoleId")
                        .HasMaxLength(32)
                        .HasColumnType("uuid");

                    b.Property<Guid>("PermissionId")
                        .HasMaxLength(32)
                        .HasColumnType("uuid");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("boolean");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission", "public");
                });

            modelBuilder.Entity("CB.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Provider")
                        .HasColumnType("integer");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User", "public");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dec5aee5-12e1-4b61-8d3f-ad5d5235e6cd"),
                            Address = "Thanh An, Hớn Quản, Bình Phước",
                            IsActive = true,
                            IsAdmin = true,
                            IsDeleted = false,
                            IsSystem = true,
                            Name = "Admin",
                            Password = "Wgkm5WCLFQbdzCjqx8AC3oZ0YU+hQET+Lpm+MfDusm2mCP9SlsPtzsSr9ohzF6XFMa1IaJacF7LHNh0/G68Uqg==",
                            Phone = "",
                            Provider = 0,
                            Username = "admin"
                        });
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

            modelBuilder.Entity("CB.Domain.Entities.User", b =>
                {
                    b.HasOne("CB.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CB.Domain.Entities.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("CB.Domain.Entities.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
