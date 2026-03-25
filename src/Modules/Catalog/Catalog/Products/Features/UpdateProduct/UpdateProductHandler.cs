using Catalog.Products.Exceptions;
using FluentValidation;

namespace Catalog.Products.Features.UpdateProduct;

public record UpdateProductCommand(Guid Id, ProductDto ProductDto) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductDto.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.ProductDto.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ProductDto.ImageFile).NotEmpty().WithMessage("Iamge File is required");
        RuleFor(x => x.ProductDto.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}


public class UpdateProductHandler(CatalogDbContext catalogDbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await catalogDbContext.Products
            .FindAsync([command.Id], cancellationToken) ?? throw new ProductNotFoundException(command.Id);

        product.Update(
            command.ProductDto.Name,
            command.ProductDto.Category,
            command.ProductDto.Description,
            command.ProductDto.ImageFile,
            command.ProductDto.Price);

        catalogDbContext.Products.Update(product);
        await catalogDbContext.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
