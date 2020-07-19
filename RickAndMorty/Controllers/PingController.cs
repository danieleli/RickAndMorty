namespace RickAndMorty.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using RickAndMorty.Controllers.Models.Ping;

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public sealed class PingController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(GetResponse), 200)]
        public IActionResult Get()
        {
            return this.Ok(new GetResponse
            {
                ApplicationName = "Rick and Morty Thing",
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            });
        }
    }
}