namespace Basket.Basket.Features.RemoveItemFromBasket;

public record RemoveItemFromBasketResponse(Guid Id);
public class RemoveItemFromBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets/{userName}/items/{productId}", async (string userName, Guid productId, ISender sender) =>
        {
            var command = new RemoveItemFromBasketCommand(userName, productId);

            var result = await sender.Send(command);

            var response = new RemoveItemFromBasketResponse(result.Id);

            return Results.Ok(response);
        })
        .WithName("RemoveItemFromBasket")
        .Produces<RemoveItemFromBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Remove item from basket")
        .WithDescription("Remove a product from the basket by username and productId");
    }
}
