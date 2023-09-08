using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;

namespace PKS.Models.DTO.Bus
{
    public class BusSelectDTO
    {
        public int Capacity { get; set; }
        public string Registration { get; set; }

        public BusSchemaSelectDTO Schema { get; set; }
        public BusTypeSelectDTO Type { get; set; }

    }
}
