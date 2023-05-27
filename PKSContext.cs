using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;

namespace PKS;

public class PKSContext : DbContext
{
    public DbSet<BusType> BusType { get; set; }
    public PKSContext(DbContextOptions opt)
        : base(opt)
    {
        
    }

}
