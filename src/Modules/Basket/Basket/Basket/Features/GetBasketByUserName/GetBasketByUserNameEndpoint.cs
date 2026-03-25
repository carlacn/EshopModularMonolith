namespace Basket.Basket.Features.GetBasketByUserName;

public record GetBasketByUserNameResponse(ShoppingCartDto ShoppingCartDto);

public class GetBasketByUserNameEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/baskets/{userName}", async (string userName, ISender sender) =>
        {
            var query = new GetBasketByUserNameQuery(userName);

            var result = await sender.Send(query);

            var response = new GetBasketByUserNameResponse(result.ShoppingCartDto);

            return Results.Ok(response);
        })
        .WithName("GetBasketByUserName")
        .Produces<GetBasketByUserNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get basket by username")
        .WithDescription("Returns a shopping cart by username");
    }
}
