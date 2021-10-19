using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace BeerQuest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PubController : ControllerBase
    {
        public async Task<IActionResult> Get([FromQuery] string name)
        {
            throw new NotImplementedException();
        }
    }
}
