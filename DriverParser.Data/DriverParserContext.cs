using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<LeaderBoardLine> LeaderBoardLines { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<Timing> Timings { get; set; }

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
            base.OnModelCreating(modelBuilder);
        }

        /// <inheritdoc />
        /// <summary>
        /// Override of the SaveChanges method to add our RowVersion property
        /// </summary>
        /// <returns>ID of the changed record</returns>
        public override int SaveChanges()
        {
            _logger.LogDebug("SaveChanges: Override saving");
            SetUpdateState(DateTime.UtcNow);
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            _logger.LogDebug("SaveChangesAsync: Override saving async");
            SetUpdateState(DateTime.UtcNow);
            return base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// Sets the update state of the <see cref="IEntity"/> object - either added or modified
        /// </summary>
        /// <param name="updateTime">Update time, in UTC</param>
        private void SetUpdateState(DateTime updateTime)
        {
            _logger.LogDebug($"SetUpdateState: Setting RowVersion with {updateTime:yyyy-MM-dd hh:mm:ss}");
            var modifiedOrAddedEntites = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added)
                .Select(x => x.Entity).ToList();
            //foreach (var modifiedEntity in modifiedOrAddedEntites)
            //{
            //    if (modifiedEntity is IEntity entity)
            //    {
            //        entity.RowVersion = updateTime;
            //    }
            //}
        }
    }
}
