using PKS.Models.DBModels;
using PKS.Models.DTO.Bus;
using PKS.Models.DTO.BusSchema;

namespace PKS.Services
{
    public interface IPKSModelValidator
    {
        public string ValidateBusAddDTO(BusAddDTO bus);
        public string ValidateBusSchemaAddDTO(BusSchemaAddDTO busSchema);
    }
}