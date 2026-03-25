namespace Basket.Infrastructure.Configurations;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);

        builder.HasIndex(x => x.UserName).IsUnique();

        builder.HasMany(x => x.Items).WithOne().HasForeignKey(si => si.ShoppingCartId);   
    }
}
