using PKS.Models.DTO.Stop;

namespace PKS.Models.DTO.Route
{
    public class RouteSelectDTO
    {
        public string RouteName { get; set; }
        public decimal Distance { get; set; }

        public List<StopSelectDTO> stops { get; set; }

    }
}
