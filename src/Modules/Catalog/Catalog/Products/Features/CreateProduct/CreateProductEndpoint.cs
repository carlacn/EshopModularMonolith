namespace Catalog.Products.Features.CreateProduct;

public record CreateProductRequest(ProductDto ProductDto);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = new CreateProductCommand(request.ProductDto);

            var result = await sender.Send(command);

            var response = new CreateProductResponse(result.Id);

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create product")
        .WithDescription("Create product for documentation");
    }
}
