using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Route;
using PKS.Models.DTO.Stop;
using PKS.Models.DTO.Ticket;
using PKS.Services;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {

        public readonly PKSContext pks;
        private readonly IPKSModelValidator validator;
        public TicketController(PKSContext pks, IPKSModelValidator pKSModelValidator)
        {
            this.pks = pks;
            this.validator = pKSModelValidator;
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

        [HttpGet("{idTicket}")]
        public async Task<IActionResult> GetTicketById(int idTicket)
        {
            if (idTicket < 0)
            {
                return BadRequest("Ticket id cannot be less than 0");
            }
            var ticket = await pks.Ticket.FirstOrDefaultAsync(b => b.idTicket == idTicket);
            if (ticket is null)
            {
                return NotFound($"Ticket with id: {idTicket} does not exists");
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

        [HttpPost]
        public async Task<IActionResult> AddTicket(TicketAddDTO ticketAdd)
        {
            var error = validator.ValidateTicketAddDTO(ticketAdd);
            if (error != null)
            {
                return BadRequest(error);
            }
            else
            {
                int idTicket = await pks.Ticket.CountAsync() > 0 ? await pks.Ticket.MaxAsync(t => t.idTicket) + 1 : 1;
                var ticket = new Ticket()
                {
                    idTicket= idTicket,
                    idBus = ticketAdd.idBus,
                    idDiscount = ticketAdd.idDiscount,
                    idRoute = ticketAdd.idRoute,
                    idPassenger = ticketAdd.idPassenger,
                    ValidFrom = ticketAdd.ValidFrom,
                    ValidTo = ticketAdd.ValidTo,
                    Validated = ticketAdd.Validated,
                    SeatNumber = ticketAdd.SeatNumber
                };
                await pks.Ticket.AddAsync(ticket);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Ticket added");
            }
        }

        [HttpDelete("{idTicket}")]
        public async Task<IActionResult> DeleteTicket(int idTicket)
        {
            if (!await pks.Ticket.AnyAsync(t => t.idTicket == idTicket))
            {
                return BadRequest($"Ticket with id: {idTicket} doesn't exist");
            } 
            else
            {
                var ticket = await pks.Ticket.FirstOrDefaultAsync(t => t.idTicket == idTicket);
                pks.Ticket.Remove(ticket);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok($"Ticket with id: {idTicket} was deleted");
            }
        }

        [HttpPut("{idTicket}")]
        public async Task<IActionResult> UpdateTicket(int idTicket, TicketAddDTO ticketUpdate)
        {
            var error = validator.ValidateTicketAddDTO(ticketUpdate);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if (!await pks.Ticket.AnyAsync(t => t.idTicket == idTicket))
            {
                return BadRequest($"Ticket with id: {idTicket} doesn't exist");
            }
            else
            {
                var ticket = await pks.Ticket.FirstOrDefaultAsync(t => t.idTicket == idTicket);
                ticket.idBus = ticketUpdate.idBus;
                ticket.idDiscount = ticketUpdate.idDiscount;
                ticket.idRoute = ticketUpdate.idRoute;
                ticket.idPassenger = ticketUpdate.idPassenger;
                ticket.ValidFrom = ticketUpdate.ValidFrom;
                ticket.ValidTo = ticketUpdate.ValidTo;
                ticket.Validated = ticketUpdate.Validated;
                ticket.SeatNumber = ticketUpdate.SeatNumber;
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Ticket updated");
            }
        }

    }
}
