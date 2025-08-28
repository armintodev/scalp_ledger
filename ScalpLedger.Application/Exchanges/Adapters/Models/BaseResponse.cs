using System.Text.Json.Serialization;

namespace ScalpLedger.Application.Exchanges.Adapters.Models;

public class BaseResponse<TResult> where TResult : class
{
    [JsonPropertyOrder(0)]
    public bool Success { get; set; }

    [JsonPropertyOrder(3)]
    public TResult Data { get; set; } = null!;

    [JsonPropertyOrder(1)]
    public string Code { get; set; } = null!;

    [JsonPropertyOrder(2)]
    public string Message { get; set; } = null!;
}