using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

public record GetProductsResponse(PaginatedResult<ProductDto> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var query = new GetProductsQuery(request);

            var result = await sender.Send(query);

            var response = new GetProductsResponse(result.Products);

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get products for documentation");
    }
}
