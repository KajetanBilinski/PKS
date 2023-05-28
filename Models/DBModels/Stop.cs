using System.Text.Json.Serialization;

namespace PKS.Models.DBModels
{
    public class Stop
    {
        public int idStop { get; set; }
        public string StopName { get; set; }
        
        public virtual ICollection<RouteStop> NavigationRouteStops { get; set; }
    }
}
