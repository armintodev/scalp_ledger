using ScalpLedger.Domain.CandleSticks;
using ScalpLedger.Infrastructure.Persistence.Configurations;
using ScalpLedger.Infrastructure.Persistence.Extensions;

namespace ScalpLedger.Infrastructure.Persistence;

public class LedgerDbContext : DbContext
{
    public LedgerDbContext(DbContextOptions options) : base(options)
    {
    }

    public required DbSet<Candle> Candles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyDeletedQueryFilters();

        //Configurations
        modelBuilder.ApplyConfiguration(new CandleConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();

        base.OnConfiguring(optionsBuilder);
    }
}