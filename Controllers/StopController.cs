using Microsoft.AspNetCore.Mvc;

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
    }
}
