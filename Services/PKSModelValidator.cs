using PKS.Models.DBModels;
using PKS.Models.DTO.Bus;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;
using PKS.Models.DTO.Discount;
using PKS.Models.DTO.Passenger;

namespace PKS.Services
{
    public class PKSModelValidator : IPKSModelValidator
    {
        public string? ValidateBusAddDTO(BusAddDTO bus)
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

        public string? ValidateBusSchemaAddDTO(BusSchemaAddDTO busSchema)
        {
            if (busSchema is null)
                return "BusSchema is null";
            else if (string.IsNullOrEmpty(busSchema.Filename))
                return "BusSchema filename is null or empty";
            return null;
        }

        public string? ValidateBusTypeAddDTO(BusTypeAddDTO busType)
        {
            if (busType is null)
                return "BusType is null";
            else if (string.IsNullOrWhiteSpace(busType.Made))
                return "Made is null or whitespace";
            else if (string.IsNullOrWhiteSpace(busType.Version))
                return "Version is null or whitespace";
            else if (string.IsNullOrWhiteSpace(busType.Engine))
                return "Engine is null or whitespace";
            else if (busType.Year > DateTime.Now.Year)
                return "Year cannot be greater than current year";
            else if (busType.Year < 1900)
                return "Year cannot be less than 1900";
            return null;
        }

        public string? ValidateDiscountAddDTO(DiscountAddDTO discount)
        {
            if (discount is null)
                return "Discount is null";
            else if (string.IsNullOrWhiteSpace(discount.Name))
                return "Discount name is null or whitespace";
            else if (discount.DiscountValue <= 0)
                return "Discount value cannot be less than 0";
            else if (discount.DiscountValue > 100)
                return "Discount value cannot be greater than 100";
            return null;
        }

        public string? ValidatePassengerAddDTO(PassengerAddDTO passenger)
        {
            if (passenger is null)
                return "Passenger is null";
            else if (string.IsNullOrWhiteSpace(passenger.Firstname))
                return "Firstname is null or whitespace";
            else if (passenger.Firstname.Any(char.IsDigit))
                return "Firstname cannot contain digit";
            else if (string.IsNullOrWhiteSpace(passenger.LastName))
                return "LastName is null or whitespace";
            else if (passenger.LastName.Any(char.IsDigit))
                return "LastName cannot contain digit";
            else if (passenger.Age < 0 || passenger.Age > 130)
                return "Age cannot be less than 0 or greater than 130";
            else if (passenger.PhoneNumber.Any(char.IsLetter))
                return "Phone number cannot contain letter";
            else if (passenger.Email.Contains("@"))
                return "Email doesn't have @";
            return null;
        }
    }
}
