using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PKS.Models.DBModels;
using PKS.Models.DTO.Bus;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;
using PKS.Models.DTO.Passenger;
using PKS.Models.DTO.Ticket;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/passenger")]
    public class PassengerController : ControllerBase
    {
        private readonly PKSContext pks;
        public PassengerController(PKSContext pks) 
        {
            this.pks= pks; 
        }


        [HttpGet]
        public async Task<IActionResult> GetPassengers()
        {
            List<PassengerSelectDTO> passengers = new List<PassengerSelectDTO>(); 
            foreach(Passenger passenger in await pks.Passenger.ToListAsync())
            {
                List<TicketSelectDTO> tickets = new List<TicketSelectDTO>();
                foreach (Ticket ticket in passenger.NavigationTickets!)
                {
                    dynamic cost = 0;
                    if (ticket.NavigationDiscount is not null)
                        cost = ticket.NavigationRoute.Cost - ticket.NavigationRoute.Cost * (ticket.NavigationDiscount.DiscountValue / 100);
                    tickets.Add(new TicketSelectDTO()
                    {
                        Cost = cost,
                        Validated = ticket.Validated,
                        ValidFrom = ticket.ValidFrom,
                        ValidTo = ticket.ValidTo,
                        SeatNumber = ticket.SeatNumber,
                        RouteName = ticket.NavigationRoute.RouteName,
                        Distance = ticket.NavigationRoute.Distance,
                        DiscountName = ticket.NavigationDiscount is null?"Brak":ticket.NavigationDiscount.Name
                    }) ;
                }
                passengers.Add(new PassengerSelectDTO()
                {
                    PhoneNumber = passenger.PhoneNumber,
                    Firstname = passenger.Firstname,
                    LastName = passenger.LastName,
                    Email = passenger.Email,
                    Age = passenger.Age,
                    Tickets = tickets
                });               
            }
            return Ok(passengers);
        }
    }
}
