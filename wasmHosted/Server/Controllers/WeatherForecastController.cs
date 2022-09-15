using MediatR;
using Microsoft.AspNetCore.Mvc;
using wasmHosted.Shared;

namespace wasmHosted.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            return await mediator.Send(new ForecastQuery());
        }

        [HttpGet("GetFilteredAsync")]
        public async Task<IEnumerable<WeatherForecast>> GetFilteredAsync()
        {
            return await mediator.Send(new ForecastQuery());
        }

    }
}