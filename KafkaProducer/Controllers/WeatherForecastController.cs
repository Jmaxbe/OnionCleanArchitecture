using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using KafkaProducer.Models;
using KafkaProducer.Services;
using Microsoft.AspNetCore.Mvc;

namespace KafkaProducer.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IProducerService _producerService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IProducerService producerService)
    {
        _logger = logger;
        _producerService = producerService;
    }

    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        try
        {
            var data = new TestMessage
            {
                Text = $"Message: {Guid.NewGuid()}"
            };

            await _producerService.PublishAsync("some-trigger", Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
        }
        catch (Exception e)
        {
            return Conflict();
        }

        return Ok();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}