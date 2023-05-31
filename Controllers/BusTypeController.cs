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
        public async Task<IActionResult> GetBusSchemas()
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
    }
}
