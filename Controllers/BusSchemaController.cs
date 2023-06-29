using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Bus;
using PKS.Models.DTO.BusSchema;
using PKS.Models.DTO.BusType;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/busschema")]
    public class BusSchemaController : ControllerBase
    {
        private readonly PKSContext pks;
        public BusSchemaController(PKSContext pks)
        {
            this.pks = pks;
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

        [HttpGet("{schemaId}")]
        public async Task<IActionResult> GetBusSchemaById(int schemaId)
        {
            if (schemaId < 0)
            {
                return BadRequest("Schema id cannot be less than 0");
            }
            var schema = await pks.BusSchema.FirstOrDefaultAsync(b => b.idBusSchema == schemaId);
            if (schema is null)
            {
                return NotFound($"Schema with id: {schemaId} does not exists");
            }
            var schemaReturn = new BusSchemaSelectDTO()
            {
                Filename= schema.Filename,
            };
            return Ok(schemaReturn);
        }
    }
}
