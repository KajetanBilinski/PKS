using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;

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
