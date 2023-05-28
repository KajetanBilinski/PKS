using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PKS.Models.DBModels;

public class BusType
{
    public BusType() 
    {
        NavigationBuses = new HashSet<Bus>();
    }
    public int idBusType { get; set; }
    public string Made { get; set; }
    public string Version { get; set; }
    public string Engine { get; set; }
    public int Year { get; set; }
   
    public virtual ICollection<Bus> NavigationBuses { get; set; }
}
