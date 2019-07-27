using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DriverParser.Data.Models
{
    public partial class DriverParserContext : DbContext
    {
        public DriverParserContext()
        {
        }

        public DriverParserContext(DbContextOptions<DriverParserContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<CarDriver> CarDriver { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<LeaderBoardLine> LeaderBoardLine { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<ResultsLeaderBoardLines> ResultsLeaderBoardLines { get; set; }
        public virtual DbSet<SchemaVersions> SchemaVersions { get; set; }
        public virtual DbSet<Splits> Splits { get; set; }
        public virtual DbSet<Timing> Timing { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlite("Data Source=C:\\Users\\iostr\\Google Drive\\Development\\Repos\\DriverParser\\DriverParser.Console\\bin\\Debug\\netcoreapp2.2\\DriverParser.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("BIGINT")
                    .ValueGeneratedNever();

                entity.Property(e => e.CarId).HasColumnType("BIGINT");

                entity.Property(e => e.CarModel).HasColumnType("BIGINT");

                entity.Property(e => e.CupCategory).HasColumnType("BIGINT");

                entity.Property(e => e.CurrentDriverId).HasColumnType("BIGINT");

                entity.Property(e => e.RaceNumber).HasColumnType("BIGINT");

                entity.Property(e => e.TeamName).IsRequired();

                entity.HasOne(d => d.CurrentDriver)
                    .WithMany(p => p.Car)
                    .HasForeignKey(d => d.CurrentDriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CarDriver>(entity =>
            {
                entity.HasKey(e => new { e.CarId, e.DriverId });

                entity.Property(e => e.CarId).HasColumnType("BIGINT");

                entity.Property(e => e.DriverId).HasColumnType("BIGINT");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.CarDriver)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.CarDriver)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("BIGINT")
                    .ValueGeneratedNever();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.PlayerId).IsRequired();

                entity.Property(e => e.ShortName).IsRequired();
            });

            modelBuilder.Entity<LeaderBoardLine>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("BIGINT")
                    .ValueGeneratedNever();

                entity.Property(e => e.CarId).HasColumnType("BIGINT");

                entity.Property(e => e.CurrentDriverId).HasColumnType("BIGINT");

                entity.Property(e => e.CurrentDriverIndex).HasColumnType("BIGINT");

                entity.Property(e => e.TimingId).HasColumnType("BIGINT");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.LeaderBoardLine)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CurrentDriver)
                    .WithMany(p => p.LeaderBoardLine)
                    .HasForeignKey(d => d.CurrentDriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Timing)
                    .WithMany(p => p.LeaderBoardLine)
                    .HasForeignKey(d => d.TimingId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Result>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("BIGINT")
                    .ValueGeneratedNever();

                entity.Property(e => e.BestLap).HasColumnType("BIGINT");

                entity.Property(e => e.BestSplitsId).HasColumnType("BIGINT");

                entity.Property(e => e.IsWetSession)
                    .IsRequired()
                    .HasColumnType("BIT");

                entity.Property(e => e.Type).HasColumnType("BIGINT");

                entity.HasOne(d => d.BestSplits)
                    .WithMany(p => p.Result)
                    .HasForeignKey(d => d.BestSplitsId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ResultsLeaderBoardLines>(entity =>
            {
                entity.HasKey(e => new { e.ResultId, e.LeaderBoardLineId });

                entity.Property(e => e.ResultId).HasColumnType("BIGINT");

                entity.Property(e => e.LeaderBoardLineId).HasColumnType("BIGINT");

                entity.HasOne(d => d.LeaderBoardLine)
                    .WithMany(p => p.ResultsLeaderBoardLines)
                    .HasForeignKey(d => d.LeaderBoardLineId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.ResultsLeaderBoardLines)
                    .HasForeignKey(d => d.ResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SchemaVersions>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("BIGINT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Added)
                    .IsRequired()
                    .HasColumnType("DATETIME");

                entity.Property(e => e.ScriptName).IsRequired();
            });

            modelBuilder.Entity<Splits>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("BIGINT")
                    .ValueGeneratedNever();

                entity.Property(e => e.Value).HasColumnType("BIGINT");
            });

            modelBuilder.Entity<Timing>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("BIGINT")
                    .ValueGeneratedNever();

                entity.Property(e => e.BestLap).HasColumnType("BIGINT");

                entity.Property(e => e.BestSplitsId).HasColumnType("BIGINT");

                entity.Property(e => e.LapCount).HasColumnType("BIGINT");

                entity.Property(e => e.LastLap).HasColumnType("BIGINT");

                entity.Property(e => e.LastSplitId).HasColumnType("BIGINT");

                entity.Property(e => e.LastSplitsId).HasColumnType("BIGINT");

                entity.Property(e => e.TotalTime).HasColumnType("BIGINT");

                entity.HasOne(d => d.BestSplits)
                    .WithMany(p => p.TimingBestSplits)
                    .HasForeignKey(d => d.BestSplitsId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.LastSplits)
                    .WithMany(p => p.TimingLastSplits)
                    .HasForeignKey(d => d.LastSplitsId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
