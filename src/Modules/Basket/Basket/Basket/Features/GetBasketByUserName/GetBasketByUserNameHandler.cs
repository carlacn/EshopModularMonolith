using Basket.Basket.Exceptions;

namespace Basket.Basket.Features.GetBasketByUserName;

public record GetBasketByUserNameQuery(string UserName) : IQuery<GetBasketByUserNameResult>;

public record GetBasketByUserNameResult(ShoppingCartDto ShoppingCartDto);

public class GetBasketByUserNameHandler(BasketDbContext basketDbContext)
    : IQueryHandler<GetBasketByUserNameQuery, GetBasketByUserNameResult>
{
    public async Task<GetBasketByUserNameResult> Handle(GetBasketByUserNameQuery query, CancellationToken cancellationToken)
    {
        var shoppingCart = await basketDbContext.ShoppingCarts
            .AsNoTracking()
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.UserName == query.UserName, cancellationToken) ?? throw new ShoppingCartNotFoundException(query.UserName);

            var shoppingCartDto = new ShoppingCartDto(
                shoppingCart.Id,
                shoppingCart.UserName,
                shoppingCart.TotalPrice,
                shoppingCart.Items.Select(x => new ShoppingCartItemDto(
                    x.Id,
                    x.ShoppingCartId,
                    x.ProductId,
                    x.Quantity,
                    x.Color,
                    x.Price,
                    x.ProductName
                )).ToList()
            );

            return new GetBasketByUserNameResult(shoppingCartDto);
    }
}
