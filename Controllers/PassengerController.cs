using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PKS.Models.DTO.Bus;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;
using PKS.Models.DTO.Ticket;

namespace PKS.Controllers
{
    [ApiController]
    [Route("passenger")]
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
            var bus = pks.Bus.ToList()[0];
            List<TicketSelectDTO> tickets = new List<TicketSelectDTO>();
            foreach (var ticket in bus.NavigationTickets) 
            {
                tickets.Add(new TicketSelectDTO()
                {
                    Cost= ticket.Cost,
                    CreatedAt=ticket.CreatedAt,
                    SeatNumber=ticket.SeatNumber,
                    FirstName=ticket.NavigationPassenger.Firstname,
                    LastName=ticket.NavigationPassenger.LastName,
                    Email=ticket.NavigationPassenger.Email,
                });
            }
            var b = new BusSelectDTO()
            {
                Capacity = bus.Capacity,
                Registration = bus.Registration,
                Schema = new BusSchemaSelectDTO()
                {
                    Filename = bus.NavigationBusSchema.Filename
                },
                Type = new BusTypeSelectDTO()
                {
                    Made = bus.NavigationBusType.Made,
                    Version = bus.NavigationBusType.Version,
                    Engine = bus.NavigationBusType.Engine,
                    Year = bus.NavigationBusType.Year,
                },
                Tickets = tickets
            };
            return Ok(b);
        }
    }
}
