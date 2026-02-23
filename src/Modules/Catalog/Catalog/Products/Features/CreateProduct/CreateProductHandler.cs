namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto ProductDto) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductHandler(CatalogDbContext catalogDbContext) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Create Product Entity from Command
        //Save to database
        //Return result

        var product = CreateNewProduct(command.ProductDto);
        
        catalogDbContext.Products.Add(product);        
        await catalogDbContext.SaveChangesAsync(cancellationToken);
        
        return new CreateProductResult(product.Id);
    }

    private Product CreateNewProduct(ProductDto productDto)
    {
        var product = Product.Create(
            Guid.NewGuid(),
            productDto.Name,
            productDto.Category,
            productDto.Description,
            productDto.ImageFile,
            productDto.Price);

        return product;
    }
}
