using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Discount;
using PKS.Services;

namespace PKS.Controllers
{
    [ApiController]
    [Route("api/discount")]
    public class DiscountController : ControllerBase
    {
        private readonly PKSContext pks;
        private readonly IPKSModelValidator validator;
        public DiscountController(PKSContext pks,IPKSModelValidator pKSModelValidator)
        {
            this.pks = pks;
            this.validator = pKSModelValidator;
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

        [HttpPost]
        public async Task<IActionResult> AddDiscount(DiscountAddDTO discountAdd)
        {
            var error = validator.ValidateDiscountAddDTO(discountAdd);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if (await pks.Discount.AnyAsync(d => d.Name == discountAdd.Name))
            {
                return BadRequest($"Discount with name: {discountAdd.Name} already exist");
            }
            else
            {
                int idDiscount = await pks.Discount.CountAsync() > 0 ? await pks.Discount.MaxAsync(bs => bs.IdDiscount) + 1 : 1;
                var discount = new Discount()
                {
                    IdDiscount = idDiscount,
                    DiscountValue= discountAdd.DiscountValue,
                    Name = discountAdd.Name
                };
                await pks.Discount.AddAsync(discount);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Discount added");
            }

        }

        [HttpPut("{idDiscount}")]
        public async Task<IActionResult> UpdateDiscount(int idDiscount,DiscountAddDTO discountUpdate)
        {
            var error = validator.ValidateDiscountAddDTO(discountUpdate);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if (!await pks.Discount.AnyAsync(d => d.IdDiscount == idDiscount))
            {
                return BadRequest($"Discount with id: {idDiscount} doesn't exist");
            }
            else
            {
                var discount = await pks.Discount.FirstOrDefaultAsync(d => d.IdDiscount == idDiscount);
                discount.DiscountValue=discountUpdate.DiscountValue;
                discount.Name=discountUpdate.Name;
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Discount updated");
            }
        }

        [HttpDelete("{idDiscount}")]
        public async Task<IActionResult> DeleteDiscount(int idDiscount)
        {
            if (!await pks.Discount.AnyAsync(d => d.IdDiscount == idDiscount))
            {
                return BadRequest($"BusSchema with id: {idDiscount} doesn't exist");
            }
            else if (await pks.Ticket.AnyAsync(b => b.idDiscount == idDiscount))
            {
                return BadRequest($"Cannot remove Discount due to connection with one or more tickets");
            }
            else
            {
                var discount = await pks.Discount.FirstOrDefaultAsync(bs => bs.IdDiscount == idDiscount);
                pks.Discount.Remove(discount);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok($"Discount with id: {idDiscount} was deleted");
            }
        }
    }
}
