namespace ScalpLedger.Infrastructure.Exchanges.Bitunix.Models;

public sealed class BitunixBaseResponse<TResult> where TResult : class
{
    public int Code { get; set; }
    public string Msg { get; set; } = null!;
    public TResult Data { get; set; } = null!;
    public bool Success { get; set; }
}