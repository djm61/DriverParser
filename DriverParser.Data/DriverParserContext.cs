using DriverParser.Data.Entities;
using DriverParser.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DriverParser.Data
{
    public class DriverParserContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<DriverParserContext> _logger;
        private readonly DbContextOptions _options;

        public DriverParserContext()
        {
            _loggerFactory = new NullLoggerFactory();
            _logger = _loggerFactory.CreateLogger<DriverParserContext>();
            _options = null;
        }

        public DriverParserContext(ILoggerFactory loggerFactory, DbContextOptions options)
            : base(options.ThrowIfNull(nameof(options)))
        {
            _loggerFactory = loggerFactory.ThrowIfNull(nameof(loggerFactory));
            _logger = loggerFactory.ThrowIfNull(nameof(loggerFactory)).CreateLogger<DriverParserContext>();
            _options = options.ThrowIfNull(nameof(options));

            _logger.LogDebug("DriverParserContext created");
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<CarDriver> CarDriver { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<LeaderBoardLine> LeaderBoardLine { get; set; }
        public virtual DbSet<Result> Result { get; set; }
        public virtual DbSet<ResultsLeaderBoardLines> ResultsLeaderBoardLines { get; set; }
        public virtual DbSet<ResultsSplits> ResultsSplits { get; set; }
        public virtual DbSet<SchemaVersions> SchemaVersions { get; set; }
        public virtual DbSet<Splits> Splits { get; set; }
        public virtual DbSet<SplitsTimings> SplitsTimings { get; set; }
        public virtual DbSet<Timing> Timing { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);

            if (_options == null)
            {
                optionsBuilder
                    .UseSqlite("Data Source=DriverParser.db")
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _logger.LogDebug("OnModelCreating: EF model being created");

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

                entity.Property(e => e.IsWetSession)
                    .IsRequired()
                    .HasColumnType("BOOLEAN");

                entity.Property(e => e.Type).HasColumnType("BIGINT");
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

            modelBuilder.Entity<ResultsSplits>(entity =>
            {
                entity.HasKey(e => new { e.ResultId, e.SplitsId });

                entity.Property(e => e.ResultId).HasColumnType("BIGINT");

                entity.Property(e => e.SplitsId).HasColumnType("BIGINT");

                entity.HasOne(d => d.Result)
                    .WithMany(p => p.ResultsSplits)
                    .HasForeignKey(d => d.ResultId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Splits)
                    .WithMany(p => p.ResultsSplits)
                    .HasForeignKey(d => d.SplitsId)
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

            modelBuilder.Entity<SplitsTimings>(entity =>
            {
                entity.HasKey(e => new { e.SplitsId, e.TimingId });

                entity.Property(e => e.SplitsId).HasColumnType("BIGINT");

                entity.Property(e => e.TimingId).HasColumnType("BIGINT");

                entity.HasOne(d => d.Splits)
                    .WithMany(p => p.SplitsTimings)
                    .HasForeignKey(d => d.SplitsId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Timing)
                    .WithMany(p => p.SplitsTimings)
                    .HasForeignKey(d => d.TimingId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Timing>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("BIGINT")
                    .ValueGeneratedNever();

                entity.Property(e => e.BestLap).HasColumnType("BIGINT");

                entity.Property(e => e.LapCount).HasColumnType("BIGINT");

                entity.Property(e => e.LastLap).HasColumnType("BIGINT");

                entity.Property(e => e.LastSplitId).HasColumnType("BIGINT");

                entity.Property(e => e.TotalTime).HasColumnType("BIGINT");
            });

            _logger.LogDebug("OnModelCreating: EF model created");
        }

        /// <inheritdoc />
        /// <summary>
        /// Override of the SaveChanges method to add our RowVersion property
        /// </summary>
        /// <returns>ID of the changed record</returns>
        //public override int SaveChanges()
        //{
        //    _logger.LogDebug("SaveChanges: Override saving");
        //    SetUpdateState(DateTime.UtcNow);
        //    return base.SaveChanges();
        //}

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    _logger.LogDebug("SaveChangesAsync: Override saving async");
        //    SetUpdateState(DateTime.UtcNow);
        //    return base.SaveChangesAsync(cancellationToken);
        //}

        /// <summary>
        /// Sets the update state of the <see cref="IEntity"/> object - either added or modified
        /// </summary>
        /// <param name="updateTime">Update time, in UTC</param>
        //private void SetUpdateState(DateTime updateTime)
        //{
        //    _logger.LogDebug($"SetUpdateState: Setting RowVersion with {updateTime:yyyy-MM-dd hh:mm:ss}");
        //    var modifiedOrAddedEntites = ChangeTracker.Entries()
        //        .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added)
        //        .Select(x => x.Entity).ToList();
        //    //foreach (var modifiedEntity in modifiedOrAddedEntites)
        //    //{
        //    //    if (modifiedEntity is IEntity entity)
        //    //    {
        //    //        entity.RowVersion = updateTime;
        //    //    }
        //    //}
        //}
    }
}
