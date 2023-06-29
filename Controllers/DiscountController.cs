using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Discount;

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

        [HttpGet]
        public async Task<IActionResult> GetDiscounts()
        {
            List<DiscountSelectDTO> discounts = new List<DiscountSelectDTO>();
            foreach(var d in await pks.Discount.ToListAsync())
            {
                discounts.Add(new DiscountSelectDTO()
                {
                    DiscountValue = d.DiscountValue,
                    Name = d.Name,
                });
            }
            return Ok(discounts);
        }

        [HttpGet("{discountId}")]
        public async Task<IActionResult> GetDiscountById(int discountId)
        {
            if (discountId < 0)
            {
                return BadRequest("Discount id cannot be less than 0");
            }
            var discount = await pks.Discount.FirstOrDefaultAsync(b => b.IdDiscount == discountId);
            if (discount is null)
            {
                return NotFound($"Discount with id: {discountId} does not exists");
            }
            var discountReturn = new DiscountSelectDTO()
            {
                DiscountValue= discount.DiscountValue,
                Name = discount.Name,
            };
            return Ok(discountReturn);
        }
    }
}
