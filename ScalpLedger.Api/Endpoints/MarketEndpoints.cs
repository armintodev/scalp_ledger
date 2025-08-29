using MediatR;
using ScalpLedger.Api.Endpoints.Constants;
using ScalpLedger.Application.Candles.Fetch;
using ScalpLedger.Application.Candles.Indicator;

namespace ScalpLedger.Api.Endpoints;

public static class MarketEndpoints
{
    public static void MapMarketsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup(EndpointConsts.MARKET_ROUTE).WithTags("market");

        group.MapPost("/", Create);
        group.MapGet("/indicators", Indicators);
    }

    private static async Task<IResult> Create([FromServices] ISender sender, [FromBody] FetchCandleRequest request)
    {
        var response = await sender.Send(request);

        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> Indicators([FromServices] ISender sender)
    {
        var response = await sender.Send(new DoIndicatorRequest());

        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
    }
}