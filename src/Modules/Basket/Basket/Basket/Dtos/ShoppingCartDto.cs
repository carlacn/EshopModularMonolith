namespace Basket.Basket.Dtos;

public record ShoppingCartDto(Guid Id, string UserName, decimal TotalPrice, List<ShoppingCartItemDto> Items);

