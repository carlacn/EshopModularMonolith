namespace Basket.Basket.Features.AddItemToBasket;

public record AddItemToBasketRequest(ShoppingCartItemDto ShoppingCartItemDto);

public record AddItemToBasketResponse(Guid Id);

public class AddItemToBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    { 
        app.MapPost("/baskets/{userName}/items", async(string userName, AddItemToBasketRequest request, ISender sender) =>
        {
            var command = new AddItemToBasketCommand(userName, request.ShoppingCartItemDto);

            var result = await sender.Send(command);

            var response = new AddItemToBasketResponse(result.Id);

            return Results.Created($"/baskets/items/{response.Id}", response);
        })
        .WithName("AddItemToBasket")
        .Produces<AddItemToBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Add Item To Basket")
        .WithDescription("Add Item To Basket for documentation");
    }
}
