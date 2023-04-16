using DealerInventory.Data;
using DealerInventory.Data.DealerModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DealerInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        private readonly MasterContext _context;

        public VehicleTypeController(MasterContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetDealer()
        {
            return await _context.VehicleTypes.ToListAsync();
        }

        // GET: api/vehicleType
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleType>> GetDealer(int id)
        {
            //get the id of vehicleType
            var dealer = await _context.VehicleTypes.FindAsync(id);

            if (dealer == null)
            {
                return NotFound();
            }

            return dealer;
        }

        // PUT: api/vehicleType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "RegisteredUser")]
        public async Task<IActionResult> PutDealer(int id, VehicleType vehicleType)
        {
            if (id != vehicleType.VehicleTypeID)
            {
                return BadRequest();
            }

            _context.Entry(vehicleType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/vehicleType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "RegisteredUser")]
        public async Task<ActionResult<VehicleType>> PostDealer(VehicleType vehicleType)
        {
            _context.VehicleTypes.Add(vehicleType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDealer", new { id = vehicleType.VehicleTypeID }, vehicleType);
        }

        // DELETE: api/vehicleType
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteDealer(int id)
        {
            var vehicle = await _context.VehicleTypes.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.VehicleTypes.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(int id)
        {
            return _context.VehicleTypes.Any(e => e.VehicleTypeID == id);
        }
    }
}

