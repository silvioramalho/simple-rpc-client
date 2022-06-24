using Microsoft.AspNetCore.Mvc;

namespace RPCLeo.RPCCLient.Controllers
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
        private readonly IThreadMessage _pendingMessages;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IThreadMessage pendingMessages)
        {
            _logger = logger;
            _pendingMessages = pendingMessages;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<string> Get()
        {
            string message = "cachorro";
            using var rpcClient = new RPCClient(_pendingMessages);

            return await rpcClient.SendAsync(message);

        }

        [HttpPost]
        public ActionResult Post(Payload payload)
        {
            var tcs = _pendingMessages.TryRemove(payload.CorrelationId);
            if (tcs != null)
                tcs.SetResult(payload.Message);

            return Ok();

        }
    }
}