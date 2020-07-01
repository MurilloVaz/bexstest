using System.Threading.Tasks;
using Bexs.Rest.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bexs.Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _service;
        public RouteController(IRouteService service)
        {
            _service = service;
        }

        [HttpGet("from/{from}/to/{to}/cheapest")]
        public IActionResult CheapestRoute(string from, string to)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
                return BadRequest();

            return Ok(_service.CheapestRoute(from, to));
        }

        [HttpPost]
        public async Task<IActionResult> NewRoute([FromBody] Entities.DTO.BaseRoute newRoute)
        {
            if (newRoute == null || string.IsNullOrEmpty(newRoute.From) 
                || string.IsNullOrEmpty(newRoute.To) || newRoute.Price <= 0)
                return BadRequest();

            var result = await _service.InsertRoute(newRoute);

            if (result)
                return StatusCode(201);

            return BadRequest();
        }

    }
}
