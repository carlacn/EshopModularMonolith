namespace Basket.Basket.Features.CreateBasket;

public record CreateBasketCommand(ShoppingCartDto ShoppingCartDto) : ICommand<CreateBasketResult>;

public record CreateBasketResult(Guid Id);

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCartDto.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class CreateBasketHandler(BasketDbContext basketDbContext) : ICommandHandler<CreateBasketCommand, CreateBasketResult>
{
    public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
    {
        //Create ShoppingCart Entity from Command
        //Save to database
        //Return result

        var shoppingCart = CreateNewBasket(command.ShoppingCartDto);

        basketDbContext.ShoppingCarts.Add(shoppingCart);
        await basketDbContext.SaveChangesAsync(cancellationToken);

        return new CreateBasketResult(shoppingCart.Id);
    }

    private ShoppingCart CreateNewBasket(ShoppingCartDto shoppingCartDto)
    {
        var shoppingCart = ShoppingCart.Create(
            Guid.NewGuid(),
            shoppingCartDto.UserName);

        return shoppingCart;

    }
}
