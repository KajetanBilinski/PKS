using PKS.Models.DBModels;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;
using PKS.Models.DTO.Ticket;

namespace PKS.Models.DTO.Bus
{
    public class BusAddDTO
    {
        public int Capacity { get; set; }
        public string Registration { get; set; }

        public BusSchemaAddDTO Schema { get; set; }   
        public BusTypeAddDTO Type { get; set; }

    }
}
