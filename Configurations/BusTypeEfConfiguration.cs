using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKS.Models.DBModels;

namespace PKS.Configurations
{
    public class BusTypeEfConfiguration : IEntityTypeConfiguration<BusType>
    {
        public void Configure(EntityTypeBuilder<BusType> builder)
        {
            builder.HasKey(e => e.idBusType).HasName("BusType_pk");
            builder.Property(e => e.idBusType).HasColumnType("int").UseIdentityColumn();
            builder.Property(e => e.Made).HasColumnType("string");
            builder.Property(e => e.Version).HasColumnType("string");
            builder.Property(e => e.Engine).HasColumnType("string");
            builder.Property(e => e.Year).HasColumnType("integer");
        }
    }
}
