using Microsoft.AspNetCore.Mvc;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/discount")]
    public class DiscountController : ControllerBase
    {
        private readonly PKSContext pks;
        public DiscountController(PKSContext pks)
        {
            this.pks = pks;
        }
    }
}
