using System.Text.Json.Serialization;

namespace PKS.Models.DBModels
{
    public class BusSchema
    {
        public BusSchema()
        {
            NavigationBuses = new HashSet<Bus>();
        }
        public int idBusSchema { get; set; }
        public string Filename { get; set; }
      
        public virtual ICollection<Bus> NavigationBuses { get; set;}
    }
}
