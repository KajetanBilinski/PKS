using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Discount;
using PKS.Models.DTO.Route;
using PKS.Models.DTO.Stop;
using PKS.Services;
using System.ComponentModel.DataAnnotations;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/stop")]
    public class StopController : ControllerBase
    {
        private readonly PKSContext pks;
        private readonly IPKSModelValidator validator;
        public StopController(PKSContext pks, IPKSModelValidator pKSModelValidator)
        {
            this.pks = pks;
            this.validator = pKSModelValidator;
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

        [HttpPost]
        public async Task<IActionResult> AddStop(StopAddDTO stopAdd)
        {
            var error = validator.ValidateStopAddDTO(stopAdd);
            if (error != null)
            {
                return BadRequest(error);
            }
            else
            {
                int idStop = await pks.Stop.CountAsync() > 0 ? await pks.Stop.MaxAsync(s => s.idStop) + 1 : 1;
                var stop = new Stop()
                {
                    idStop= idStop,
                    StopName= stopAdd.StopName
                };
                await pks.Stop.AddAsync(stop);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Stop added");
            }
        }

        [HttpDelete("{idStop}")]
        public async Task<IActionResult> DeleteStop(int idStop)
        {
            if (!await pks.Route.AnyAsync(r => r.idRoute == idStop))
            {
                return BadRequest($"Stop with id: {idStop} doesn't exist");
            }
            else if (await pks.RouteStop.AnyAsync(rs => rs.idStop == idStop))
            {
                return BadRequest($"Cannot remove Stop due to connection with one or more tickets");
            }
            else
            {
                var stop = await pks.Stop.FirstOrDefaultAsync(s => s.idStop == idStop);
                pks.Stop.Remove(stop);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok($"Stop with id: {idStop} was deleted");
            }
        }

        [HttpPut("{idStop}")]
        public async Task<IActionResult> UpdateStop(int idStop, StopAddDTO stopUpdate)
        {
            var error = validator.ValidateStopAddDTO(stopUpdate);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if (!await pks.Stop.AnyAsync(S => S.idStop == idStop))
            {
                return BadRequest($"Stop with id: {idStop} doesn't exist");
            }
            else
            {
                var stop = await pks.Stop.FirstOrDefaultAsync(s => s.idStop == idStop);
                stop.StopName = stopUpdate.StopName;
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Stop updated");
            }
        }
    }
}
