using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Bus;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;
using PKS.Services;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/busschema")]
    public class BusSchemaController : ControllerBase
    {
        private readonly PKSContext pks;
        private readonly IPKSModelValidator validator;
        public BusSchemaController(PKSContext pks, IPKSModelValidator pKSModelValidator)
        {
            this.pks = pks;
            this.validator = pKSModelValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBusSchemas()
        {
            List<BusSchemaSelectDTO> busSchemas = new List<BusSchemaSelectDTO>();
            foreach(BusSchema busSchema in await pks.BusSchema.ToListAsync())
            {
                busSchemas.Add(new BusSchemaSelectDTO()
                {
                    Filename = busSchema.Filename
                });
            }
            return Ok(busSchemas);
        }

        [HttpGet("{idBusSchema}")]
        public async Task<IActionResult> GetBusSchemaById(int idBusSchema)
        {
            if (idBusSchema < 0)
            {
                return BadRequest("Schema id cannot be less than 0");
            }
            var schema = await pks.BusSchema.FirstOrDefaultAsync(b => b.idBusSchema == idBusSchema);
            if (schema is null)
            {
                return NotFound($"Schema with id: {idBusSchema} does not exists");
            }
            var schemaReturn = new BusSchemaSelectDTO()
            {
                Filename= schema.Filename,
            };
            return Ok(schemaReturn);
        }

        [HttpPost]
        public async Task<IActionResult> AddBusSchema(BusSchemaAddDTO busSchemaAdd)
        {
            var error = validator.ValidateBusSchemaAddDTO(busSchemaAdd);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if(await pks.BusSchema.AnyAsync(bs=>bs.Filename==busSchemaAdd.Filename))
            {
                return BadRequest($"BusSchema with filename: {busSchemaAdd.Filename} already exist");
            }
            else
            {
                int idBusSchema = await pks.BusSchema.CountAsync() > 0 ? await pks.BusSchema.MaxAsync(bs => bs.idBusSchema) + 1 : 1;
                var busSchema = new BusSchema()
                {
                    idBusSchema = idBusSchema,
                    Filename = busSchemaAdd.Filename
                };
                await pks.BusSchema.AddAsync(busSchema);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("BusSchema added");
            }
        }

        [HttpPut("{idBusSchema}")]
        public async Task<IActionResult> UpdateBusSchema(int idBusSchema,BusSchemaAddDTO busSchemaUpdate)
        {
            var error = validator.ValidateBusSchemaAddDTO(busSchemaUpdate);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if (!await pks.BusSchema.AnyAsync(bs => bs.idBusSchema == idBusSchema))
            {
                return BadRequest($"BusSchema with id: {idBusSchema} doesn't exist");
            }
            else if(await pks.BusSchema.AnyAsync(bs => bs.Filename == busSchemaUpdate.Filename))
            {
                return BadRequest($"BusSchema with filename: {busSchemaUpdate.Filename} already exist");
            }
            else
            {
                var busSchema = await pks.BusSchema.FirstOrDefaultAsync(bs=>bs.idBusSchema == idBusSchema);
                busSchema.Filename = busSchemaUpdate.Filename;
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("BusSchema updated");
            }
        }

        [HttpDelete("{idBusSchema}")]
        public async Task<IActionResult> DeleteBusSchema(int idBusSchema)
        {
            if (!await pks.BusSchema.AnyAsync(bs => bs.idBusSchema == idBusSchema))
            {
                return BadRequest($"BusSchema with id: {idBusSchema} doesn't exist");
            }
            else if(await pks.Bus.AnyAsync(b=>b.idBusSchema == idBusSchema))
            {
                return BadRequest($"Cannot remove BusSchema due to connection with one or more buses");
            }
            else
            {
                var busSchema = await pks.BusSchema.FirstOrDefaultAsync(bs => bs.idBusSchema == idBusSchema);
                pks.BusSchema.Remove(busSchema);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok($"BusSchema with id: {idBusSchema} was deleted");
            }
        }
    }
}
