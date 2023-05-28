using System.Text.Json.Serialization;

namespace PKS.Models.DBModels;

public class Route
{
    public int idRoute { get; set; }
    public string RouteName { get ; set; }  
    public decimal Distance { get; set; }

    public virtual ICollection<Ticket> NavigationTickets { get; set; }
    
    public virtual ICollection<RouteStop> NavigationRouteStops { get; set; }
}
