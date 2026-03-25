using Catalog.Products.Exceptions;

namespace Catalog.Products.Features.GetProductById;

//public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

//public record GetProductByIdResult(ProductDto Product);

public class GetProductByIdHandler(CatalogDbContext catalogDbContext) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await catalogDbContext.Products
            .AsNoTracking()
            .Where(p => p.Id == query.Id)
            .Select(product => new ProductDto(
                product.Id,
                product.Name,
                product.Category,
                product.Description,
                product.ImageFile,
                product.Price
            ))
            .FirstOrDefaultAsync(cancellationToken) ?? throw new ProductNotFoundException(query.Id);

        return new GetProductByIdResult(product);
    }
}
