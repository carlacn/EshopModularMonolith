using Catalog.Products.Exceptions;

namespace Catalog.Products.Features.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductHandler(CatalogDbContext catalogDbContext) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{

    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await catalogDbContext.Products
        .FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken) ?? throw new ProductNotFoundException(command.Id);

        
        catalogDbContext.Products.Remove(product);

        await catalogDbContext.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
