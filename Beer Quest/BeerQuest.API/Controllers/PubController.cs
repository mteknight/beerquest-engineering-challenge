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

        public async Task<IActionResult> Get([FromQuery] string name)
        {
            var pub = await this.service.Get(name);

            return this.Ok(pub);
        }
    }
}
