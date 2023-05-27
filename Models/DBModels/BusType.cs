using System.ComponentModel.DataAnnotations;

namespace PKS.Models.DBModels;

public class BusType
{
    [Key]
    public int idBusType { get; set; }
    public string Made { get; set; }
    public string Version { get; set; }
    public string Engine { get; set; }
    public int Year { get; set; }
}
