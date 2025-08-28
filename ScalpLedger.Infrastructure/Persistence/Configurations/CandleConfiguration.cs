using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScalpLedger.Domain.CandleSticks;
using ScalpLedger.Infrastructure.Constants;
using ScalpLedger.Infrastructure.Extensions;

namespace ScalpLedger.Infrastructure.Persistence.Configurations;

internal class CandleConfiguration : IEntityTypeConfiguration<Candle>
{
    public void Configure(EntityTypeBuilder<Candle> builder)
    {
        builder.ToTable(nameof(Candle).ToSnakeCase(), DatabaseConsts.MARKET_TABLE_SCHEMA.ToSnakeCase());

        builder.Property(x => x.Symbol).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Timeframe).HasMaxLength(10).IsRequired();

        builder.Property(x => x.Open).HasPrecision(30, 10).IsRequired();
        builder.Property(x => x.Close).HasPrecision(30, 10).IsRequired();
        builder.Property(x => x.High).HasPrecision(30, 10).IsRequired();
        builder.Property(x => x.Low).HasPrecision(30, 10).IsRequired();

        builder.Property(x => x.Volume).HasPrecision(38, 18).IsRequired();

        builder.HasIndex(
            i => new
            {
                i.Symbol,
                i.Timeframe,
                i.Timestamp
            }
        ).IsUnique();
    }
}