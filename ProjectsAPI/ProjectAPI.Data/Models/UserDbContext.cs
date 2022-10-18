using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectAPI.Data.EFModels;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ProjectAPI.Data.Models
{
    public partial class UserDbContext : IdentityDbContext
    {
        public UserDbContext()
        {
        }

        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Registration> Registration { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<TeamType> TeamType { get; set; }
        public virtual DbSet<UserDetail> UserDetail { get; set; }
        public virtual DbSet<UserTeam> UserTeam { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=49.249.56.102;Database=ProjectAPI;User ID=sql;Password=Optisol@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Registration)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Registration_Roles");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<TeamType>(entity =>
            {
                entity.HasKey(e => e.TeamTypeId);
                entity.Property(e => e.TeamTypeId).HasColumnName("Team_Type_Id");

                entity.Property(e => e.TeamType1)
                    .IsRequired()
                    .HasColumnName("Team_Type")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.UserDetailId);
                entity.Property(e => e.UserDetailId).HasColumnName("User_Detail_Id");

                entity.Property(e => e.CurrentOrganizationName)
                    .IsRequired()
                    .HasColumnName("Current_Organization_Name")
                    .HasMaxLength(150);

                entity.Property(e => e.DateOfJoining)
                    .HasColumnName("Date_of_Joining")
                    .HasColumnType("datetime");

                entity.Property(e => e.Experience)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PreviousOrganizationName)
                    .IsRequired()
                    .HasColumnName("Previous_Organization_Name")
                    .HasMaxLength(150);

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDetail)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetail_Registration");
            });

            modelBuilder.Entity<UserTeam>(entity =>
            {
                entity.HasKey(e => e.UserTeamId);
                entity.Property(e => e.UserTeamId)
                    .HasColumnName("User_Team_Id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AssignedUser)
                    .IsRequired()
                    .HasColumnName("Assigned_User")
                    .HasMaxLength(200);

                entity.Property(e => e.TeamTypeId).HasColumnName("Team_Type_Id");

                entity.Property(e => e.UserDetailId).HasColumnName("User_Detail_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
