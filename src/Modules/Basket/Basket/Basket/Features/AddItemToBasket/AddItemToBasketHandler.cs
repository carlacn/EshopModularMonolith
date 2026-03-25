using Basket.Basket.Exceptions;
using Catalog.Contracts.Products.Features.GetProductById;

namespace Basket.Basket.Features.AddItemToBasket;

public record AddItemToBasketCommand(string UserName, ShoppingCartItemDto ShoppingCartItemDto) : ICommand<AddItemToBasketResult>;

public record AddItemToBasketResult(Guid Id);

public class AddItemToBasketCommandValidator : AbstractValidator<AddItemToBasketCommand>
{
    public AddItemToBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(x => x.ShoppingCartItemDto.ProductId).NotEmpty().WithMessage("ProductId is required");
        RuleFor(x => x.ShoppingCartItemDto.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}

public class AddItemToBasketHandler(BasketDbContext basketDbContext, ISender sender) : ICommandHandler<AddItemToBasketCommand, AddItemToBasketResult>
{
    public async Task<AddItemToBasketResult> Handle(AddItemToBasketCommand command, CancellationToken cancellationToken)
    {
        //Add shoppingCartItem into ShoppingCart
        var shoppingCart = await basketDbContext.ShoppingCarts
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserName.Equals(command.UserName), cancellationToken) ?? throw new ShoppingCartNotFoundException(command.UserName);

        //Get product info from catalog.contracts module
        var getProductByIdQuery = new GetProductByIdQuery(command.ShoppingCartItemDto.ProductId);

        var productDto = await sender.Send(getProductByIdQuery, cancellationToken);

        shoppingCart.AddItem(
            command.ShoppingCartItemDto.ProductId,
            command.ShoppingCartItemDto.Quantity,
            command.ShoppingCartItemDto.Color,
            productDto.Product.Price,
            productDto.Product.Name);

            //command.ShoppingCartItemDto.Price,
            //command.ShoppingCartItemDto.ProductName);
            

        await basketDbContext.SaveChangesAsync(cancellationToken);

        return new AddItemToBasketResult(shoppingCart.Id);
    }
}
