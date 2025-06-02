using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DiaryAppDbContext
{
    public partial class KeeplyDbContext : DbContext
    {
        public KeeplyDbContext()
        {
        }

        public KeeplyDbContext(DbContextOptions<KeeplyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DailyNote> DailyNotes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<DailyNote>(entity =>
            {
                entity.ToTable("daily-notes");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "UserId_idx");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(5000);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DailyNotes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "Username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.NameSurname).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(45);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
