using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Bus;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;
using PKS.Models.DTO.Ticket;
using PKS.Services;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/bus")]
    public class BusController : ControllerBase
    {
        private readonly PKSContext pks;
        private readonly IPKSModelValidator validator;
        public BusController(PKSContext pks,IPKSModelValidator pKSModelValidator)
        {
            this.pks = pks;
            this.validator = pKSModelValidator;
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
        [HttpPost]
        public async Task<IActionResult> AddBus(BusAddDTO busAdd)
        {
            var error = validator.ValidateBusAddDTO(busAdd);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if(await pks.Bus.AnyAsync(b=>b.Registration == busAdd.Registration))
            {
                return BadRequest($"Bus with this registration {busAdd.Registration} already exists");
            }
            else
            {
                bool addedBusType = false;
                bool addedBusSchema = false;
                BusType busType = await pks.BusType.FirstOrDefaultAsync(bt =>
                busAdd.Type.Engine == bt.Engine &&
                busAdd.Type.Year == bt.Year &&
                busAdd.Type.Version == bt.Version &&
                busAdd.Type.Made == bt.Made);
                if (busType is null)
                {
                    busType = new BusType()
                    {
                        idBusType = await pks.BusType.MaxAsync(bt => bt.idBusType)+1, // may produce null pointer exception if table is empty
                        Made = busAdd.Type.Made,
                        Year = busAdd.Type.Year,
                        Engine = busAdd.Type.Engine,
                        Version = busAdd.Type.Version,
                    };
                    await pks.BusType.AddAsync(busType);
                    if (await pks.SaveChangesAsync() <= 0)
                        return StatusCode(505);
                    addedBusType = true;
                }

                BusSchema busSchema = await pks.BusSchema.FirstOrDefaultAsync(bs => bs.Filename == busAdd.Schema.Filename);
                if(busSchema is null)
                {
                    busSchema = new BusSchema()
                    {
                        idBusSchema = await pks.BusSchema.MaxAsync(bs => bs.idBusSchema) + 1, // may produce null pointer exception if table is empty
                        Filename = busAdd.Schema.Filename
                    };
                    await pks.BusSchema.AddAsync(busSchema);
                    if (await pks.SaveChangesAsync() <= 0)
                        return StatusCode(505);
                    addedBusSchema = true;
                }
                
                Bus bus = new Bus()
                {
                    idBus = await pks.Bus.MaxAsync(bs => bs.idBus) + 1, // may produce null pointer exception if table is empty
                    idBusSchema = busSchema.idBusSchema,
                    idBusType = busType.idBusType,
                    Capacity = busAdd.Capacity,
                    Registration = busAdd.Registration
                };
                await pks.Bus.AddAsync(bus);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Bus added "+(addedBusSchema ? ", BusSchema added ":"")+(addedBusType?", BusType added":""));
            }
        }
    }
}
