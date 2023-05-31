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
    }
}
