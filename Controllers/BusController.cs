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
        [HttpPost("/image")]
        public async Task<IActionResult> SendImage(IFormFile file)
        {
            return Ok(file.FileName);
        }

        [HttpGet]
        public async Task<IActionResult> GetBuses()
        {
            List<BusSelectDTO> buses = new List<BusSelectDTO>();
            foreach (Bus bus in await pks.Bus.ToListAsync())
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
                    int idBusType = await pks.BusType.CountAsync() > 0 ? await pks.BusType.MaxAsync(bs => bs.idBusType) + 1 : 1;
                    busType = new BusType()
                    {
                        idBusType = idBusType,
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
                    int idBusSchema = await pks.BusSchema.CountAsync() > 0 ? await pks.BusSchema.MaxAsync(bs => bs.idBusSchema) + 1 : 1;
                    busSchema = new BusSchema()
                    {
                        idBusSchema = idBusSchema,
                        Filename = busAdd.Schema.Filename
                    };
                    await pks.BusSchema.AddAsync(busSchema);
                    if (await pks.SaveChangesAsync() <= 0)
                        return StatusCode(505);
                    addedBusSchema = true;
                }
                int idBus = await pks.Bus.CountAsync() > 0 ? await pks.Bus.MaxAsync(bs => bs.idBus) + 1 : 1;
                Bus bus = new Bus()
                {
                    idBus = idBus,
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

        [HttpPut]
        public async Task<IActionResult> UpdateBus(BusAddDTO busAdd)
        {

            var error = validator.ValidateBusAddDTO(busAdd);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if (!await pks.Bus.AnyAsync(b => b.Registration == busAdd.Registration))
            {
                return BadRequest($"Bus with this registration {busAdd.Registration} doesn't exists");
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
                    int idBusType = await pks.BusType.CountAsync() > 0 ? await pks.BusType.MaxAsync(bs => bs.idBusType) + 1 : 1;
                    busType = new BusType()
                    {
                        idBusType = idBusType,
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
                if (busSchema is null)
                {
                    int idBusSchema = await pks.BusSchema.CountAsync() > 0 ? await pks.BusSchema.MaxAsync(bs => bs.idBusSchema) + 1 : 1;
                    busSchema = new BusSchema()
                    {
                        idBusSchema = idBusSchema,
                        Filename = busAdd.Schema.Filename
                    };
                    await pks.BusSchema.AddAsync(busSchema);
                    if (await pks.SaveChangesAsync() <= 0)
                        return StatusCode(505);
                    addedBusSchema = true;
                }
                var bus = await pks.Bus.FirstOrDefaultAsync(b => b.Registration == busAdd.Registration);
                bus.Capacity = busAdd.Capacity;
                bus.Registration = busAdd.Registration;
                bus.idBusSchema = busSchema.idBusSchema;
                bus.idBusType = busType.idBusType;
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Bus updated " + (addedBusSchema ? ", BusSchema added " : "") + (addedBusType ? ", BusType added" : ""));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBus(string registration)
        {
            if(string.IsNullOrWhiteSpace(registration))
            {
                return BadRequest("Registration is null or empty");
            }
            else
            {
                var bus = await pks.Bus.FirstOrDefaultAsync(b=>b.Registration == registration);
                if(bus is null)
                {
                    return NotFound($"Bus with registration: {registration} doesn't exist");
                }
                else
                {
                    pks.Bus.Remove(bus);
                    if (await pks.SaveChangesAsync() <= 0)
                        return StatusCode(505);
                    else
                        return Ok($"Bus with registration: {registration} was deleted");
                }
            }
        }
    }
}
