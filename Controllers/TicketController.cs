using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Stop;
using PKS.Models.DTO.Ticket;
using System.Net.Sockets;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {

        public readonly PKSContext pks;
        public TicketController(PKSContext pks)
        {
            this.pks = pks;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            List<TicketSelectDTO> tickets = new List<TicketSelectDTO>();
            foreach(var t in await pks.Ticket.ToListAsync()) 
            {
                tickets.Add(new TicketSelectDTO
                {
                    Cost = t.NavigationRoute.Cost - ((t.NavigationDiscount.DiscountValue/100) * t.NavigationRoute.Cost),
                    ValidFrom = t.ValidFrom,
                    ValidTo = t.ValidTo,
                    Validated = t.Validated,
                    SeatNumber = t.SeatNumber,
                    RouteName = t.NavigationRoute.RouteName,
                    DiscountName = t.NavigationDiscount is null ? "Brak" : t.NavigationDiscount.Name,
                    Distance = t.NavigationRoute.Distance
                });
            }
            return Ok(tickets);
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicketById(int ticketId)
        {
            if (ticketId < 0)
            {
                return BadRequest("Ticket id cannot be less than 0");
            }
            var ticket = await pks.Ticket.FirstOrDefaultAsync(b => b.idTicket == ticketId);
            if (ticket is null)
            {
                return NotFound($"Ticket with id: {ticketId} does not exists");
            }
            var ticketReturn = new TicketSelectDTO()
            {
                Cost = ticket.NavigationRoute.Cost,
                ValidFrom = ticket.ValidFrom,
                ValidTo = ticket.ValidTo,
                Validated = ticket.Validated,
                SeatNumber = ticket.SeatNumber,
                DiscountName = ticket.NavigationDiscount.Name,
                Distance = ticket.NavigationRoute.Distance
            };
            return Ok(ticketReturn);
        }
    }
}
