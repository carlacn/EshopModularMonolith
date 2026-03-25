namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketRequest(ShoppingCartDto ShoppingCartDto);

public record CreateBasketResponse(Guid Id);

public class CreateBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/baskets", async (CreateBasketRequest request, ISender sender) =>
        {
            var command = new CreateBasketCommand(request.ShoppingCartDto);

            var result = await sender.Send(command);

            var response = new CreateBasketResponse(result.Id);

            return Results.Created($"/baskets/{response.Id}", response);
        })
        .WithName("CreateBasket")
        .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create basket")
        .WithDescription("Create basket for documentation");
    }
}
