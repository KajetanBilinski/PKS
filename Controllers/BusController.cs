using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Bus;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;
using PKS.Models.DTO.Ticket;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/bus")]
    public class BusController : ControllerBase
    {
        private readonly PKSContext pks;
        public BusController(PKSContext pks)
        {
            this.pks = pks;
        }

        [HttpGet]
        public async Task<IActionResult> GetBuses()
        {
            List<BusSelectDTO> buses = new List<BusSelectDTO>();
            foreach(Bus bus in await pks.Bus.ToListAsync())
            {
                buses.Add(new BusSelectDTO()
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
                    }
                });
            }
            return Ok(buses);
        }
        [HttpGet("{idBus}")]
        public async Task<IActionResult> GetBusById(int idBus)
        {
            if(idBus < 0)
            {
                return BadRequest("Bus id cannot be less than 0");
            }
            var bus = await pks.Bus.FirstOrDefaultAsync(b=>b.idBus== idBus);
            if(bus is null)
            {
                return NotFound($"Bus with id: {idBus} does not exists");
            }
            var busReturn = new BusSelectDTO()
            {
                Registration = bus.Registration,
                Capacity = bus.Capacity,
                Schema = new BusSchemaSelectDTO()
                {
                    Filename = bus.NavigationBusSchema.Filename
                },
                Type = new BusTypeSelectDTO()
                {
                    Engine = bus.NavigationBusType.Engine,
                    Year = bus.NavigationBusType.Year,
                    Version = bus.NavigationBusType.Version,
                    Made = bus.NavigationBusType.Made,
                }
            };
            return Ok(busReturn);
        }
    }
}
