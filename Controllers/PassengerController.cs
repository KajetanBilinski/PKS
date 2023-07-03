using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Passenger;
using PKS.Models.DTO.Ticket;
using PKS.Services;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/passenger")]
    public class PassengerController : ControllerBase
    {
        private readonly PKSContext pks;
        private readonly IPKSModelValidator validator;
        public PassengerController(PKSContext pks, IPKSModelValidator pKSModelValidator)
        {
            this.pks = pks;
            this.validator = pKSModelValidator;
        }


        [HttpGet]
        public async Task<IActionResult> GetPassengers()
        {
            List<PassengerSelectDTO> passengers = new List<PassengerSelectDTO>();
            foreach (Passenger passenger in await pks.Passenger.ToListAsync())
            {
                List<TicketSelectDTO> tickets = new List<TicketSelectDTO>();
                foreach (Ticket ticket in passenger.NavigationTickets!)
                {
                    dynamic cost = 0;
                    if (ticket.NavigationDiscount is not null)
                        cost = ticket.NavigationRoute.Cost - ticket.NavigationRoute.Cost * (ticket.NavigationDiscount.DiscountValue / 100);
                    tickets.Add(new TicketSelectDTO()
                    {
                        Cost = ticket.NavigationRoute.Cost - ((ticket.NavigationDiscount.DiscountValue / 100) * ticket.NavigationRoute.Cost),
                        Validated = ticket.Validated,
                        ValidFrom = ticket.ValidFrom,
                        ValidTo = ticket.ValidTo,
                        SeatNumber = ticket.SeatNumber,
                        RouteName = ticket.NavigationRoute.RouteName,
                        Distance = ticket.NavigationRoute.Distance,
                        DiscountName = ticket.NavigationDiscount is null ? "Brak" : ticket.NavigationDiscount.Name
                    });
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

        [HttpGet("{idPassenger}")]
        public async Task<IActionResult> GetPassengerById(int idPassenger)
        {
            if (idPassenger < 0)
            {
                return BadRequest("Passenger id cannot be less than 0");
            }
            var passenger = await pks.Passenger.FirstOrDefaultAsync(b => b.idPassenger == idPassenger);
            if (passenger is null)
            {
                return NotFound($"Passenger with id: {idPassenger} does not exists");
            }
            var passengerReturn = new PassengerNoTicketsSelectDTO()
            {
                PhoneNumber = passenger.PhoneNumber,
                Age = passenger.Age,
                Email = passenger.Email,
                Firstname = passenger.Firstname,
                LastName = passenger.LastName
            };
            return Ok(passengerReturn);
        }

        [HttpPost]
        public async Task<IActionResult> AddPassenger(PassengerAddDTO passengerAdd)
        {
            var error = validator.ValidatePassengerAddDTO(passengerAdd);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if (await pks.Passenger.AnyAsync(p => p.Firstname == passengerAdd.Firstname
            && p.LastName == passengerAdd.LastName && p.Email == passengerAdd.Email && p.Age == passengerAdd.Age))
            {
                return BadRequest($"Passenger already exist");
            }
            else
            {
                int idPassenger = await pks.Passenger.CountAsync() > 0 ? await pks.Passenger.MaxAsync(p => p.idPassenger) + 1 : 1;
                var passenger = new Passenger()
                {
                    idPassenger = idPassenger,
                    Firstname = passengerAdd.Firstname,
                    LastName = passengerAdd.LastName,
                    Email = passengerAdd.Email,
                    Age = passengerAdd.Age,
                    PhoneNumber = passengerAdd.PhoneNumber
                };
                await pks.Passenger.AddAsync(passenger);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Passenger added");
            }
        }
    }
}
