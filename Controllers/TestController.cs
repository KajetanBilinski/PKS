using Microsoft.AspNetCore.Mvc;
using PKS.Models.DBModels;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api")]
    public class TestController : ControllerBase
    {

        public readonly PKSContext pks;
        public TestController(PKSContext pks)
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
