using Microsoft.AspNetCore.Mvc;
using Orleans;
using OrleansTest.Interfaces;

namespace OrleansTest.Controllers
{
    [Route("/api/v1/{controller}")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly IGrainFactory _grainFactory;

        public BetController(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        [HttpPost("/getBet/{key}")]
        public async Task<ActionResult> Hello(Guid key, [FromBody] string greetings)
        {
            var betGrain = _grainFactory.GetGrain<IBetGrain>(key);
            var result = await betGrain.SayHello(greetings);
            return Ok(result);  
        }
        [HttpPost("/setAmount/{key}")]
        public async Task<ActionResult> SetBetAmountAsync(Guid key, [FromBody] decimal amount)
        {
            var betGrain = _grainFactory.GetGrain<IBetGrain>(key);
            var result = await betGrain.SetAmountAsync(amount);
            return Ok(result);
        }
    }
}
