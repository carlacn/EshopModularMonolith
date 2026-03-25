namespace Basket.Infrastructure.Configurations;

public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId).IsRequired();

        builder.Property(x => x.Quantity).IsRequired();

        builder.HasKey(x => x.Color);

        builder.Property(x => x.Price).IsRequired();

        builder.Property(x => x.ProductName).IsRequired();
    }
}
