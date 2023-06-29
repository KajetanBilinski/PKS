using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DTO.Discount;
using PKS.Models.DTO.Stop;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/stop")]
    public class StopController : ControllerBase
    {
        private readonly PKSContext pks;
        public StopController(PKSContext pks)
        {
            this.pks = pks;
        }
        [HttpGet]
        public async Task<IActionResult> GetStops()
        {
            List<StopSelectDTO> stops = new List<StopSelectDTO>();
            foreach(var s in await pks.Stop.ToListAsync())
            {
                stops.Add(new StopSelectDTO
                {
                    StopName = s.StopName
                });
            }
            return Ok(stops);
        }

        [HttpGet("{stopId}")]
        public async Task<IActionResult> GetStopById(int stopId)
        {
            if (stopId < 0)
            {
                return BadRequest("Stop id cannot be less than 0");
            }
            var stop = await pks.Stop.FirstOrDefaultAsync(b => b.idStop == stopId);
            if (stop is null)
            {
                return NotFound($"Stop with id: {stopId} does not exists");
            }
            var stopReturn = new StopSelectDTO()
            {
                StopName= stop.StopName
            };
            return Ok(stopReturn);
        }
    }
}
