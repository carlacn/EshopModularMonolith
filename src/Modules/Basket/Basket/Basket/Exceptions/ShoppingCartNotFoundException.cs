using Shared.Exceptions;

namespace Basket.Basket.Exceptions;

public class ShoppingCartNotFoundException(string UserName) : NotFoundException("ShoppingCart", UserName);
