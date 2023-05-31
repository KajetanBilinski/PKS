using Microsoft.AspNetCore.Mvc;
using PKS.Models.DBModels;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {

        public readonly PKSContext pks;
        public TicketController(PKSContext pks)
        {
            this.pks = pks;
            
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(pks.BusType.ToList());
        }
    }
}
