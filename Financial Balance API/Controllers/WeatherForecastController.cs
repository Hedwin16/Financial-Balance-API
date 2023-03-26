using ApiRepository.Interfaces;
using DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Financial_Balance_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ApiContext _context;
        private readonly IRepository<Currency> _repository;
        private readonly IRepository<User> _repository1;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , IRepository<Currency> repository
            , IRepository<User> repository1)
        {
            _logger = logger;
            _repository= repository;
            _repository1 = repository1;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var result = await _repository1.GetAll();

            //if (result.Success)
            //{
            //    return null;
            //}

            //if (result.Data)
            //{

            //}

            //var data = await _repository.GetAll();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}