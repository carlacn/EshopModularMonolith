namespace Basket.Basket.Features.UpdateItemPriceInBasket;

public record UpdateItemPriceInBasketCommand(Guid ProductId, decimal NewPrice) : ICommand<UpdateItemPriceInBasketResult>;

public record UpdateItemPriceInBasketResult(bool IsSuccess);

public class UpdateItemPriceInBasketValidator : AbstractValidator<UpdateItemPriceInBasketCommand>
{
    public UpdateItemPriceInBasketValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
        RuleFor(x => x.NewPrice).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

public class UpdateItemPriceInBasketHandler(BasketDbContext basketDbContext) : ICommandHandler<UpdateItemPriceInBasketCommand, UpdateItemPriceInBasketResult>
{
    public async Task<UpdateItemPriceInBasketResult> Handle(UpdateItemPriceInBasketCommand command, CancellationToken cancellationToken)
    {
        //Find shoppingCartItems with a given product Id
        //Iterate items and udpate prices of every item with incoming command.price
        //save to database
        //return result

        var shoppingCarts = await basketDbContext.ShoppingCarts
            .Include(x => x.Items)
            .Where(x => x.Items.Any(i => i.ProductId == command.ProductId))
            .ToListAsync(cancellationToken);

        foreach (var shoppingCart in shoppingCarts)
            shoppingCart.UpdatePriceInItems(command.ProductId, command.NewPrice);

        await basketDbContext.SaveChangesAsync(cancellationToken);

        return new UpdateItemPriceInBasketResult(true);
    }
}