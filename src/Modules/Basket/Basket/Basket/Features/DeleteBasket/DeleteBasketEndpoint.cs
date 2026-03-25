namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/baskets/{userName}", async (string userName, ISender sender) =>
        {
            var command = new DeleteBasketCommand(userName);

            var result = await sender.Send(command);

            var response = new DeleteBasketResponse(result.IsSuccess);

            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete basket")
        .WithDescription("Delete basket by username");
    }
}