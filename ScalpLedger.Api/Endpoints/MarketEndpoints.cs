using MediatR;
using ScalpLedger.Api.Endpoints.Constants;
using ScalpLedger.Application.Candles.Fetch;

namespace ScalpLedger.Api.Endpoints;

public static class MarketEndpoints
{
    public static void MapMarketsEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup(EndpointConsts.MARKET_ROUTE).WithTags("market");

        group.MapPost("/", Create);
        // group.MapPut("/{id:long}", Update);
        // group.MapGet("/", Search);
        // group.MapGet("/{id:long}", Find);
        // group.MapGet("/external/sign", SignTest);
    }

    private static async Task<IResult> Create([FromServices] ISender sender, [FromBody] FetchCandleRequest request)
    {
        var response = await sender.Send(request);

        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
    }
}