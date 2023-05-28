using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PKS.Configurations;
using PKS.Models.DBModels;
using Route = PKS.Models.DBModels.Route;

namespace PKS;

public class PKSContext : DbContext
{
    public DbSet<BusType> BusType { get; set; }
    public DbSet<Passenger> Passenger { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<BusSchema> BusSchema { get; set; }
    public DbSet<Route> Route { get; set; }
    public DbSet<Stop> Stop { get; set; }
    public DbSet<RouteStop> RouteStop { get; set; }
    public DbSet<Bus> Bus { get; set; }
    public PKSContext(DbContextOptions opt)
        : base(opt)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BusEfConfiguration());
        modelBuilder.ApplyConfiguration(new BusSchemaEfConfiguration());
        modelBuilder.ApplyConfiguration(new BusTypeEfConfiguration());
        modelBuilder.ApplyConfiguration(new PassengerEfConfiguration());
        modelBuilder.ApplyConfiguration(new RouteEfConfiguration());
        modelBuilder.ApplyConfiguration(new RouteStopEfConfiguration());
        modelBuilder.ApplyConfiguration(new StopEfConfiguration());
        modelBuilder.ApplyConfiguration(new TicketEfConfiguration());
    }


}
