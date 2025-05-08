using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using ProductManagement.Domain;

namespace ProductManagement.Persistence;

public partial class ProductManagementDbContext : DbContext
{
    public ProductManagementDbContext()
    {
    }

    public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MstConfig> MstConfigs { get; set; }

    public virtual DbSet<MstProduct> MstProducts { get; set; }

    public virtual DbSet<MstUser> MstUsers { get; set; }

    public virtual DbSet<TblLogTransac> TblLogTransacs { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("User ID=admin_db;Password=admin;Server=localhost;Port=5432;Database=product_management_db;Pooling=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MstConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_config_pkey");

            entity.ToTable("mst_config");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, 9999999L, true, null)
                .HasColumnName("id");
            entity.Property(e => e.ConfigKey)
                .HasMaxLength(500)
                .HasColumnName("config_key");
            entity.Property(e => e.ConfigValue).HasColumnName("config_value");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'-1'::integer")
                .HasColumnName("created_by");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_modified_at");
            entity.Property(e => e.LastModifiedBy)
                .HasDefaultValueSql("'-1'::integer")
                .HasColumnName("last_modified_by");
        });

        modelBuilder.Entity<MstProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_wallet_pkey");

            entity.ToTable("mst_product");

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, 9999999L, true, null)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'-1'::integer")
                .HasColumnName("created_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_modified_at");
            entity.Property(e => e.LastModifiedBy)
                .HasDefaultValueSql("'-1'::integer")
                .HasColumnName("last_modified_by");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
        });

        modelBuilder.Entity<MstUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mst_user_pkey");

            entity.ToTable("mst_user");

            entity.HasIndex(e => new { e.Email, e.Phone }, "unique").IsUnique();

            entity.Property(e => e.Id)
                .HasIdentityOptions(null, null, null, 9999999L, true, null)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("'-1'::integer")
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastModifiedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_modified_at");
            entity.Property(e => e.LastModifiedBy)
                .HasDefaultValueSql("'-1'::integer")
                .HasColumnName("last_modified_by");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(14)
                .HasColumnName("phone");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<TblLogTransac>(entity =>
        {
            entity.HasKey(e => e.LogTransacId).HasName("tbl_log_pkey");

            entity.ToTable("tbl_log_transac");

            entity.Property(e => e.LogTransacId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("log_transac_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.TransacAction)
                .HasMaxLength(200)
                .HasColumnName("transac_action");
            entity.Property(e => e.TransacBy).HasColumnName("transac_by");
            entity.Property(e => e.TransacFinishAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("transac_finish_at");
            entity.Property(e => e.TransacId).HasColumnName("transac_id");
            entity.Property(e => e.TransacNext).HasColumnName("transac_next");
            entity.Property(e => e.TransacParams).HasColumnName("transac_params");
            entity.Property(e => e.TransacStartAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("transac_start_at");
            entity.Property(e => e.TransacStep).HasColumnName("transac_step");
            entity.Property(e => e.TransacType)
                .HasMaxLength(200)
                .HasColumnName("transac_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
