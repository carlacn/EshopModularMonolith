using Basket.Basket.Exceptions;

namespace Basket.Basket.Features.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required");
    }
}

public class DeleteBasketHandler(BasketDbContext basketDbContext) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        var shoppingCart = await basketDbContext.ShoppingCarts
            .FirstOrDefaultAsync(x => x.UserName == command.UserName, cancellationToken) ?? throw new ShoppingCartNotFoundException(command.UserName);

        basketDbContext.ShoppingCarts.Remove(shoppingCart);

        await basketDbContext.SaveChangesAsync(cancellationToken);

        return new DeleteBasketResult(true);
    }
}
