using Shared.Pagination;

namespace Catalog.Products.Features.GetProducts;

public record GetProductsQuery(PaginationRequest PaginationRequest) : IQuery<GetProductsResult>;

public record GetProductsResult(PaginatedResult<ProductDto> Products);

public class GetProductsHandler(CatalogDbContext catalogDbContext) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        //Get products using dbContext
        //Return result

        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;
        var totalCount = await catalogDbContext.Products.LongCountAsync(cancellationToken);

        var productDtos = await catalogDbContext.Products
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Select(product => new ProductDto(
                product.Id,
                product.Name,
                product.Category,
                product.Description,
                product.ImageFile,
                product.Price
                ))
            .ToListAsync(cancellationToken);

        //var productsDto = new List<ProductDto>();
        //foreach (var product in products)
        //{
        //var productDto = new ProductDto(
        //    product.Id,
        //    product.Name,
        //    product.Category,
        //    product.Description,
        //    product.ImageFile,
        //    product.Price
        //    );
        //    productsDto.Add( productDto );  
        //}

        return new GetProductsResult(
            new PaginatedResult<ProductDto>(
                pageIndex,
                pageSize,
                totalCount,
                productDtos
            )
        );
    }
}
