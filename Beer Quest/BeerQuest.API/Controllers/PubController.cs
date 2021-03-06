using System.Threading.Tasks;

using BeerQuest.Domain.Services;

using Dawn;

using Microsoft.AspNetCore.Mvc;

namespace BeerQuest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PubController : ControllerBase
    {
        private readonly IPubService service;

        public PubController(IPubService service)
        {
            this.service = Guard.Argument(service, nameof(service)).NotNull().Value;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return this.BadRequest($"Query parameter '{nameof(name)}' is invalid.");
            }

            var pub = await this.service.Get(name);

            return this.Ok(pub);
        }
    }
}
