using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKS.Models.DBModels;

namespace PKS.Configurations
{
    public class DiscountEfConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(e => e.IdDiscount).HasName("Discount_pk");
            builder.Property(e => e.DiscountValue).HasColumnType("decimal");
            builder.Property(e => e.Name).HasColumnType("string");
        }
    }
}
