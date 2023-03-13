using Microsoft.AspNetCore.Mvc;

namespace DealerInventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //change name of class 
    public class WeatherForecastController : ControllerBase
    {
        private static double[] Price = new[]
        {
        1000.00, 1000.00, 1000.00, 1000.00, 1000.00,1000.00, 1000.00, 1000.00, 1000.00, 1000.00
    };
        private static  string[] Make = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private static  string[] Model = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

       /* private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }*/

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 13).Select(index => new WeatherForecast
            {
                /*Make = Make.R
                Model = Model[Random.Shared.Next(Model.Length)],
                Price = Double.Conca[Random.Shared.Next(Price.Length)],*/
            });
           

        }
    }
}