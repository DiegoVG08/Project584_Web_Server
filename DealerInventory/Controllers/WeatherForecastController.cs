using Microsoft.AspNetCore.Mvc;

namespace DealerInventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //change name of class 
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Make = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Model = Random.Shared.Next(-20, 55),
                Price = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}