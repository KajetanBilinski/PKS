using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKS.Models.DBModels;

namespace PKS.Configurations
{
    public class RouteStopEfConfiguration : IEntityTypeConfiguration<RouteStop>
    {
        public void Configure(EntityTypeBuilder<RouteStop> builder)
        {
            builder.HasKey(e => e.idRouteStop).HasName("RouteStop_pk");
            builder.Property(e => e.ArriveTime).HasColumnType("datetime");
            builder.Property(e => e.DepartueTime).HasColumnType("datetime");
            builder.HasOne(e => e.NavigationStop)
                .WithMany(e => e.NavigationRouteStops)
                .HasForeignKey(e => e.idStop);
            builder.HasOne(e => e.NavigationRoute)
                .WithMany(e => e.NavigationRouteStops)
                .HasForeignKey(e => e.idRoute);
        }
    }
}
