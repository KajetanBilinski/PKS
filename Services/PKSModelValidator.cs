using PKS.Models.DTO.Bus;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;
using PKS.Models.DTO.Discount;
using PKS.Models.DTO.Passenger;
using PKS.Models.DTO.Route;
using PKS.Models.DTO.RouteStop;
using PKS.Models.DTO.Stop;
using PKS.Models.DTO.Ticket;

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

        public string? ValidateRouteAddDTO(RouteAddDTO route)
        {
            if (route is null)
                return "Route is null";
            else if (route.Cost < 0)
                return "Route cost cannot be less than zero";
            else if (route.Distance <= 0)
                return "Route distance cannot be less or equal to zero";
            else if (string.IsNullOrEmpty(route.RouteName))
                return "Route name cannot be null or empty";
            return null;
        }

        public string? ValidateRouteStopAddDTO(RouteStopAddDTO routeStopAdd)
        {
            if (routeStopAdd is null)
                return "RouteStop is null";
            else if (routeStopAdd.ArriveTime>routeStopAdd.DepartueTime)
                return "Arrive time cannot be after departure time";
            return null;
        }

        public string? ValidateStopAddDTO(StopAddDTO stop)
        {
            if (stop is null)
                return "Stop is null";
            else if (string.IsNullOrEmpty(stop.StopName))
                return "Stop name cannot be null or empty";
            return null;
        }

        public string? ValidateTicketAddDTO(TicketAddDTO ticket)
        {
            if (ticket is null)
                return "Stop is null";
            else if(string.IsNullOrEmpty(ticket.RouteName))
                return "Route name cannot be null or empty";
            else if (string.IsNullOrEmpty(ticket.SeatNumber))
                return "Seat number cannot be null or empty";
            else if (ticket.Cost<0)
                return "Ticket cost cannot be less than zero";
            else if (ticket.Distance <= 0)
                return "Ticket distance cannot be less or equal to zero";
            return null;
        }
    }
}
