using PKS.Models.DBModels;
using PKS.Models.DTO.Bus;

namespace PKS.Services
{
    public interface IPKSModelValidator
    {
        public string ValidateBusAddDTO(BusAddDTO bus);
    }
}