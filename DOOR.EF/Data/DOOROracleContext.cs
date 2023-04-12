using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DOOR.EF.Models;

namespace DOOR.EF.Data
{
    public partial class DOOROracleContext : DbContext
    {
        public DOOROracleContext()
        {
        }

        public DOOROracleContext(DbContextOptions<DOOROracleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<DeviceCode> DeviceCodes { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Key> Keys { get; set; } = null!;
        public virtual DbSet<OraTranslateMsg> OraTranslateMsgs { get; set; } = null!;
        public virtual DbSet<PersistedGrant> PersistedGrants { get; set; } = null!;
        public virtual DbSet<School> Schools { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DOOR_USER")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("ASP_NET_USER_ROLES");

                            j.HasIndex(new[] { "RoleId" }, "IX_ASP_NET_USER_ROLES_ROLE_ID");

                            j.IndexerProperty<string>("UserId").HasColumnName("USER_ID");

                            j.IndexerProperty<string>("RoleId").HasColumnName("ROLE_ID");
                        });
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => new { e.CourseNo, e.SchoolId })
                    .HasName("COURSE_PK");

                entity.Property(e => e.CourseNo).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("COURSE_FK2");

                entity.HasOne(d => d.PrerequisiteNavigation)
                    .WithMany(p => p.InversePrerequisiteNavigation)
                    .HasForeignKey(d => new { d.Prerequisite, d.PrerequisiteSchoolId })
                    .HasConstraintName("COURSE_FK1");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.SectionId, e.StudentId, e.SchoolId })
                    .HasName("ENROLLMENT_PK");

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENROLLMENT_FK3");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => new { d.SectionId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENROLLMENT_FK1");

                entity.HasOne(d => d.SNavigation)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => new { d.StudentId, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ENROLLMENT_FK2");
            });

            modelBuilder.Entity<OraTranslateMsg>(entity =>
            {
                entity.Property(e => e.OraTranslateMsgId).HasDefaultValueSql("sys_guid()");

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.Property(e => e.SchoolId).ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => new { e.SectionId, e.SchoolId })
                    .HasName("SECTION_PK");

                entity.Property(e => e.SectionId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SECTION_FK2");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => new { d.CourseNo, d.SchoolId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SECTION_FK1");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SchoolId })
                    .HasName("STUDENT_PK");

                entity.Property(e => e.StudentId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedBy).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("STUDENT_FK1");
            });

            modelBuilder.HasSequence("COURSE_SEQ");

            modelBuilder.HasSequence("SECTION_SEQ");

            modelBuilder.HasSequence("STUDENT_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
