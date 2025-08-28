using ScalpLedger.Domain.Common;

namespace ScalpLedger.Domain.CandleSticks;

public class Candle : AuditableEntity, IAggregateRoot
{
    public Candle(string symbol, string timeframe, DateTime timestamp, decimal open, decimal high, decimal low,
        decimal close, decimal volume)
    {
        Symbol = symbol;
        Timeframe = timeframe;
        Timestamp = timestamp;
        Open = open;
        High = high;
        Low = low;
        Close = close;
        Volume = volume;
    }

    public string Symbol { get; private set; }

    public string Timeframe { get; private set; }

    public DateTime Timestamp { get; private set; } // stored as UTC

    public decimal Open { get; private set; }

    public decimal High { get; private set; }

    public decimal Low { get; private set; }

    public decimal Close { get; private set; }

    public decimal Volume { get; private set; }
}