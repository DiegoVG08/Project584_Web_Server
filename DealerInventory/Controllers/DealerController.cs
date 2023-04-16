using DealerInventory.Data;
using DealerInventory.Data.DealerModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DealerInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {


        private readonly MasterContext _context;

        public DealerController(MasterContext context)
        {
            _context = context;
        }

       
        // GET: api/Dealer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDealership>>> GetCarDealership()
        {
            return await _context.carDealership.ToListAsync();
        }

        // GET: api/CarDealership
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDealership>> GetDealer(int id)
        {
            //get the id of dealerships 
            var dealer = await _context.carDealership.FindAsync(id);

            if (dealer == null)
            {
                return NotFound();
            }

            return dealer;
        }

        // PUT: api/CarDealership
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "RegisteredUser")]
        public async Task<IActionResult> PutDealer(int id, CarDealership dealership)
        {
            if (id != dealership.DealershipID)
            {
                return BadRequest();
            }

            _context.Entry(dealership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DealerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CarDealership
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "RegisteredUser")]
        public async Task<ActionResult<CarDealership>> PostDealer(CarDealership dealership)
        {
            _context.carDealership.Add(dealership);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDealer", new { id = dealership.DealershipID }, dealership);
        }

        // DELETE: api/Dealer
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteDealer(int id)
        {
            var dealer = await _context.carDealership.FindAsync(id);
            if (dealer == null)
            {
                return NotFound();
            }

            _context.carDealership.Remove(dealer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DealerExists(int id)
        {
            return _context.carDealership.Any(e => e.DealershipID == id);
        }
    }


}
