using PKS.Models.DTO.Bus;

namespace PKS.Services
{
    public class PKSModelValidator : IPKSModelValidator
    {
        public string ValidateBusAddDTO(BusAddDTO bus)
        {
            if (bus == null)
                return "Bus is null";
            else if (bus.Capacity <= 0)
                return "Bus capacity is equal or less than 0";
            else if (bus.Type.Year > DateTime.Now.Year)
                return "Year cannot be greater than current year";
            else if (bus.Type.Year < 1900)
                return "Year cannot be less than 1900";
            return null;
        }
    }
}
