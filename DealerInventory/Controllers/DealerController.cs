using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DealerInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {

        private List<Dealer> GetDealers()
        {
            return new List<Dealer>()
        {
            new Dealer()
            {

                Make= "Nissan",
                Model = "Altima",
                Price = 1000.00
            },
            new Dealer()
            {
                Make= "Nissan",
                Model = "Sentra",
                Price = 1000.00
            },
            new Dealer()
            {
                Make= "Fiat",
                Model = "500c",
                Price = 1000.00
            },
            new Dealer()
            {
                Make= "Toyota",
                Model = "Rav4",
                Price = 1000.00
            },
            new Dealer()
            {
                Make= "Toyota",
                Model = "Prius",
                Price = 1000.00
            }
        };
       }
    /*    private static double[] Price = new[]
     {
        1000.00, 1000.00, 1000.00, 1000.00, 1000.00,1000.00, 1000.00, 1000.00, 1000.00, 1000.00
    };
        private static string[] Make = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private static string[] Model = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };*/

        // GET: api/<DealerController>
        [HttpGet]
        public IEnumerable<Dealer> Get()
        {

            return GetDealers();


        }

        // GET api/<DealerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DealerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DealerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DealerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
