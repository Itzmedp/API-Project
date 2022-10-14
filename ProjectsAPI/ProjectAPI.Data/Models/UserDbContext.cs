using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Data.EFModels;
using System.Reflection.Emit;

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

        public virtual DbSet<Registration> Registration { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<object> TeamTypes { get; set; } = null!;
        public virtual DbSet<object> UserTeams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=49.249.56.102;Database=ProjectAPI;User ID=sql;Password=Optisol@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Registration>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Registration");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Address)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            builder.Entity<Role>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Roles)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Roles_Registration");
            });

            builder.Entity<TeamType>(entity =>
            {
                entity.HasKey(e => e.TeamTypeId);

                entity.ToTable("TeamType");

                entity.Property(e => e.TeamTypeId).HasColumnName("TeamType_Id");

                entity.Property(e => e.TeamType1).HasMaxLength(200);
            });

            builder.Entity<UserTeam>(entity =>
            {
                entity.HasKey(e => e.UserTeamId);

                entity.ToTable("UserTeam");

                entity.HasIndex(e => e.TeamTypeId, "IX_UserTeam_TeamType_Id");

                entity.Property(e => e.UserTeamId).HasColumnName("UserTeam_Id");

                entity.Property(e => e.AssignedUser).HasMaxLength(200);

                entity.Property(e => e.TeamTypeId).HasColumnName("TeamType_Id");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.TeamType)
                    .WithMany(p => p.UserTeams)
                    .HasForeignKey(d => d.TeamTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTeam_TeamType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTeams)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTeam_User");
            });

            OnModelCreatingPartial(builder);
        }





        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
