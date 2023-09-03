using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;
using PKS.Services;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/bustype")]
    public class BusTypeController : ControllerBase
    {
        private readonly PKSContext pks;
        private readonly IPKSModelValidator validator;
        public BusTypeController(PKSContext pks, IPKSModelValidator pKSModelValidator)
        {
            this.pks = pks;
            this.validator = pKSModelValidator;
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

        [HttpGet("{idBusType}")]
        public async Task<IActionResult> GetBusTypeById(int idBusType)
        {
            if (idBusType < 0)
            {
                return BadRequest("idBusType cannot be less than 0");
            }
            var type = await pks.BusType.FirstOrDefaultAsync(b => b.idBusType == idBusType);
            if (type is null)
            {
                return NotFound($"BusType with id: {idBusType} does not exists");
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

        [HttpPost]
        public async Task<IActionResult> AddBusType(BusTypeAddDTO busTypeAdd)
        {
            var error = validator.ValidateBusTypeAddDTO(busTypeAdd);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if (await pks.BusType.AnyAsync(bt => bt.Engine == busTypeAdd.Engine
            && bt.Version == busTypeAdd.Made && bt.Made == busTypeAdd.Made && bt.Year == busTypeAdd.Year))
            {
                return BadRequest($"BusType already exist");
            }
            else
            {
                int idBusType = await pks.BusType.CountAsync() > 0 ? await pks.BusType.MaxAsync(bt => bt.idBusType) + 1 : 1;
                var busType = new BusType()
                {
                    idBusType = idBusType,
                    Engine = busTypeAdd.Engine,
                    Year = busTypeAdd.Year,
                    Made = busTypeAdd.Made,
                    Version= busTypeAdd.Version
                };
                await pks.BusType.AddAsync(busType);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("BusType added");
            }
        }

        [HttpPut("{idBusType}")]
        public async Task<IActionResult> UpdateBusType(int idBusType, BusTypeAddDTO busTypeUpdate)
        {
            var error = validator.ValidateBusTypeAddDTO(busTypeUpdate);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if (!await pks.BusType.AnyAsync(bt => bt.idBusType == idBusType))
            {
                return BadRequest($"BusType with id: {idBusType} doesn't exist");
            }
            else
            {
                var busType = await pks.BusType.FirstOrDefaultAsync(bs => bs.idBusType == idBusType);
                busType.Version = busTypeUpdate.Version;
                busType.Engine = busTypeUpdate.Engine;
                busType.Made = busTypeUpdate.Made;
                busType.Year = busTypeUpdate.Year;
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("BusType updated");
            }
        }

        [HttpDelete("{idBusType}")]
        public async Task<IActionResult> DeleteBusType(int idBusType)
        {
            if (!await pks.BusType.AnyAsync(bs => bs.idBusType == idBusType))
            {
                return BadRequest($"BusSchema with id: {idBusType} doesn't exist");
            }
            else if (await pks.Bus.AnyAsync(b => b.idBusType == idBusType))
            {
                return BadRequest($"Cannot remove BusType due to connection with one or more buses");
            }
            else
            {
                var busType = await pks.BusType.FirstOrDefaultAsync(bs => bs.idBusType == idBusType);
                pks.BusType.Remove(busType);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok($"BusType with id: {idBusType} was deleted");
            }
        }
    }
}
