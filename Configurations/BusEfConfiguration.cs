using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKS.Models.DBModels;

namespace PKS.Configurations
{
    public class BusEfConfiguration : IEntityTypeConfiguration<Bus>
    {
        public void Configure(EntityTypeBuilder<Bus> builder)
        {
            builder.HasKey(e => e.idBus).HasName("Bus_pk");
            builder.Property(e => e.Capacity).HasColumnType("integer");
            builder.Property(e => e.Registration).HasColumnType("string");
            builder.HasOne(e => e.NavigationBusType)
                .WithMany(e => e.NavigationBuses)
                .HasForeignKey(e => e.idBusType);
            builder.HasOne(e => e.NavigationBusSchema)
                .WithMany(e => e.NavigationBuses)
                .HasForeignKey(e => e.idBusSchema);
        }

    }
}
