using CB.Domain.Common.Attributes;
using CB.Domain.Entities;
using CB.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Infrastructure.Database.Configurations;

internal class PermissionConfig : IEntityTypeConfiguration<Permission> {

    public void Configure(EntityTypeBuilder<Permission> builder) {
        builder.ToTable(nameof(Permission));

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasMaxLength(32);
        builder.Property(o => o.ParentId).HasMaxLength(32);

        builder.Property(o => o.ClaimName).HasMaxLength(50).IsRequired();
        builder.Property(o => o.DisplayName).HasMaxLength(50).IsRequired();
        builder.Property(o => o.Type).HasMaxLength(20).IsRequired();

        // fk
        builder.HasMany(o => o.RolePermissions).WithOne(o => o.Permission).HasForeignKey(o => o.PermissionId);

        // seed data
        var index = 0;

        builder.HasData(new Permission {
            Id = "ec0f270b424249438540a16e9157c0c8",
            ClaimName = CbClaim.NoClaim.BO,
            DisplayName = "Trang quản lý",
            IsDefault = true,
            IsActive = true,
            IsClaim = false,
            OrderIndex = index++,
            Type = EPermission.Web,
        });

        builder.HasData(new Permission {
            Id = "b47bbb68c29e4880bb3a230620ce4e6e",
            ParentId = "ec0f270b424249438540a16e9157c0c8",
            ClaimName = CbClaim.Web.Dashboard,
            DisplayName = "Tổng quan",
            IsDefault = true,
            IsActive = true,
            IsClaim = true,
            OrderIndex = index++,
            Type = EPermission.Web,
        });

        builder.HasData(new Permission {
            Id = "b47ccc68c29e4880bb3a230620ce4e7e",
            ParentId = "ec0f270b424249438540a16e9157c0c8",
            ClaimName = CbClaim.Web.Contact,
            DisplayName = "Liên hệ",
            IsDefault = true,
            IsActive = true,
            IsClaim = true,
            OrderIndex = index++,
            Type = EPermission.Web,
        });

        builder.HasData(new Permission {
            Id = "b47ccc68c29e4990bb3a230620ce4e6e",
            ParentId = "ec0f270b424249438540a16e9157c0c8",
            ClaimName = CbClaim.NoClaim.Service,
            DisplayName = "Dịch vụ",
            IsDefault = false,
            IsActive = true,
            IsClaim = false,
            OrderIndex = index++,
            Type = EPermission.Web,
        }, new Permission {
            Id = "b47ccc68c29e4990aa3a230620dd4e7e",
            ParentId = "b47ccc68c29e4990bb3a230620ce4e6e",
            ClaimName = CbClaim.Web.Service_Transaction,
            DisplayName = "Danh sách giao dịch",
            IsDefault = false,
            IsActive = true,
            IsClaim = true,
            OrderIndex = index++,
            Type = EPermission.Web,
        }, new Permission {
            Id = "b47ccc68c29e4990bb3a230620dd4e7e",
            ParentId = "b47ccc68c29e4990bb3a230620ce4e6e",
            ClaimName = CbClaim.Web.Service_Server,
            DisplayName = "",
            IsDefault = false,
            IsActive = true,
            IsClaim = true,
            OrderIndex = index++,
            Type = EPermission.Web,
        });

        builder.HasData(new Permission {
            Id = "b47ccc68c29e4990bb3a230620ce4e7e",
            ParentId = "ec0f270b424249438540a16e9157c0c8",
            ClaimName = CbClaim.NoClaim.Category,
            DisplayName = "Danh mục",
            IsDefault = false,
            IsActive = true,
            IsClaim = false,
            OrderIndex = index++,
            Type = EPermission.Web,
        }, new Permission {
            Id = "b47ccc68c29e4990aa3a230620ce4e7e",
            ParentId = "b47ccc68c29e4990bb3a230620ce4e7e",
            ClaimName = CbClaim.Web.Category_Bot,
            DisplayName = "Bot",
            IsDefault = false,
            IsActive = true,
            IsClaim = true,
            OrderIndex = index++,
            Type = EPermission.Web,
        });

        builder.HasData(new Permission {
            Id = "dc1c2ce584d74428b4e5241a5502787d",
            ParentId = "ec0f270b424249438540a16e9157c0c8",
            ClaimName = CbClaim.NoClaim.Setting,
            DisplayName = "Cài đặt",
            IsDefault = false,
            IsActive = true,
            IsClaim = false,
            OrderIndex = index++,
            Type = EPermission.Web,
        });

        builder.HasData(new Permission {
            Id = "b35cc06a567e420f8d0bda3426091048",
            ParentId = "dc1c2ce584d74428b4e5241a5502787d",
            ClaimName = CbClaim.NoClaim.General,
            DisplayName = "Cài đặt chung",
            IsDefault = false,
            IsActive = true,
            IsClaim = false,
            OrderIndex = index++,
            Type = EPermission.Web,
        });

        builder.HasData(new Permission {
            Id = "721bb6697d4c4579abc649ed838443cd",
            ParentId = "b35cc06a567e420f8d0bda3426091048",
            ClaimName = CbClaim.Web.Setting_General_Api,
            DisplayName = "Cài đặt nâng cao",
            IsDefault = false,
            IsActive = true,
            IsClaim = true,
            OrderIndex = index++,
            Type = EPermission.Web,
        });

        builder.HasData(new Permission {
            Id = "296285809bac481890a454ea8aed6af4",
            ParentId = "dc1c2ce584d74428b4e5241a5502787d",
            ClaimName = CbClaim.Web.Setting_User,
            DisplayName = "Người dùng",
            IsDefault = false,
            IsActive = true,
            IsClaim = true,
            OrderIndex = index++,
            Type = EPermission.Web,
        }, new Permission {
            Id = "98873832ebcb4d9fb12e9b21a187f12c",
            ParentId = "296285809bac481890a454ea8aed6af4",
            ClaimName = CbClaim.Web.Setting_User_Reset,
            DisplayName = "Đặt lại mật khẩu",
            IsDefault = false,
            IsActive = true,
            IsClaim = true,
            OrderIndex = index++,
            Type = EPermission.Web,
        });

        builder.HasData(new Permission {
            Id = "cb26c94262ab4863baa6c516edfde134",
            ParentId = "dc1c2ce584d74428b4e5241a5502787d",
            ClaimName = CbClaim.Web.Setting_Role,
            DisplayName = "Phân quyền",
            IsDefault = false,
            IsActive = true,
            IsClaim = true,
            OrderIndex = index++,
            Type = EPermission.Web,
        });
    }
}
