using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PKS.Models.DBModels;
using Route = PKS.Models.DBModels.Route;

namespace PKS.Configurations
{
    public class RouteEfConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.HasKey(e => e.idRoute).HasName("Route_pk");
            builder.Property(e => e.RouteName).HasColumnType("string");
            builder.Property(e => e.Distance).HasColumnType("decimal");
        }
    }
}
