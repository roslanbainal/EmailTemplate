using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EmailApp.Models;

#nullable disable

namespace EmailApp.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MstUser> MstUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("set by your self");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_trgm")
                .HasPostgresExtension("pgrouting")
                .HasPostgresExtension("postgis")
                .HasAnnotation("Relational:Collation", "English_Malaysia.1252");

            modelBuilder.Entity<MstUser>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("mst_users", "ca");

                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AccessFailedCount).HasColumnName("access_failed_count");

                entity.Property(e => e.Address1)
                    .HasMaxLength(500)
                    .HasColumnName("address1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(500)
                    .HasColumnName("address2");

                entity.Property(e => e.Address3)
                    .HasMaxLength(500)
                    .HasColumnName("address3");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.ConcurrencyStamp).HasMaxLength(500);

                entity.Property(e => e.Country).HasColumnName("country");

                entity.Property(e => e.CountryCode).HasColumnName("country_code");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasColumnType("character varying")
                    .HasColumnName("full_name");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.IndustryId).HasColumnName("industry_id");

                entity.Property(e => e.IndustryName)
                    .HasMaxLength(256)
                    .HasColumnName("industry_name");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsApprove)
                    .HasMaxLength(10)
                    .HasColumnName("is_approve");

                entity.Property(e => e.IsConfirmed).HasColumnName("is_confirmed");

                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");

                entity.Property(e => e.IsReject)
                    .HasMaxLength(10)
                    .HasColumnName("is_reject");

                entity.Property(e => e.LastLogin).HasColumnName("last_login");

                entity.Property(e => e.LastPasswordChanged).HasColumnName("last_password_changed");

                entity.Property(e => e.LockoutEnabled).HasColumnName("lockout_enabled");

                entity.Property(e => e.LockoutEndDate)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("lockout_end_date");

                entity.Property(e => e.MinistryId).HasColumnName("ministry_id");

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(25)
                    .HasColumnName("mobile_phone");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Password)
                    .HasMaxLength(500)
                    .HasColumnName("password");

                entity.Property(e => e.Position)
                    .HasColumnType("character varying")
                    .HasColumnName("position");

                entity.Property(e => e.Postcode)
                    .HasMaxLength(10)
                    .HasColumnName("postcode");

                entity.Property(e => e.RegisterDateUtc).HasColumnName("register_date_utc");

                entity.Property(e => e.SecurityStamp).HasMaxLength(500);

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.TitleId).HasColumnName("title_id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(256)
                    .HasColumnName("user_name");
            });

         

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
