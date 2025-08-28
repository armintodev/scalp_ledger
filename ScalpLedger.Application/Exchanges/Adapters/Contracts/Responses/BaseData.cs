namespace ScalpLedger.Application.Exchanges.Adapters.Contracts.Responses;

public record BaseData
{
    public Dictionary<string, object>? Additional { get; set; }
}