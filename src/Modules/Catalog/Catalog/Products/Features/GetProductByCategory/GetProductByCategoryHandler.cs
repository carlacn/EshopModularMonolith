namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResponse>;

public record GetProductByCategoryResponse(IEnumerable<ProductDto> Products);

public class GetProductByCategoryHandler(CatalogDbContext catalogDbContext) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResponse>
{
    public async Task<GetProductByCategoryResponse> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await catalogDbContext.Products
            .AsNoTracking()
            .Where(p => p.Category.Contains(query.Category))
            .OrderBy(p => p.Name)
            .Select(product => new ProductDto(
                product.Id,
                product.Name,
                product.Category,
                product.Description,
                product.ImageFile,
                product.Price
            ))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResponse(products);
    }
}
