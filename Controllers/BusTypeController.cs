using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/bustype")]
    public class BusTypeController : ControllerBase
    {
        private readonly PKSContext pks;
        public BusTypeController(PKSContext pks)
        {
            this.pks = pks;
        }

        [HttpGet]
        public async Task<IActionResult> GetBusTypes()
        {
            List<BusTypeSelectDTO> busTypes = new List<BusTypeSelectDTO>();
            foreach (BusType busType in await pks.BusType.ToListAsync())
            {
                busTypes.Add(new BusTypeSelectDTO()
                {
                    Engine = busType.Engine,
                    Version= busType.Version,
                    Made= busType.Made,
                    Year= busType.Year,
                });
            }
            return Ok(busTypes);
        }
        [HttpGet("{typeId}")]
        public async Task<IActionResult> GetBusTypeById(int typeId)
        {
            if (typeId < 0)
            {
                return BadRequest("BusType id cannot be less than 0");
            }
            var type = await pks.BusType.FirstOrDefaultAsync(b => b.idBusType == typeId);
            if (type is null)
            {
                return NotFound($"BusType with id: {typeId} does not exists");
            }
            var typeReturn = new BusTypeSelectDTO()
            {
                Version= type.Version,
                Engine= type.Engine,
                Year= type.Year,
                Made= type.Made,
            };
            return Ok(typeReturn);
        }
    }
}
