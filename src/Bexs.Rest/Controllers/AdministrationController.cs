using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Bexs.Rest.Interfaces;

namespace Bexs.Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministrationController : ControllerBase
    {
        private readonly IAdministrationService _repo;
        public AdministrationController(IAdministrationService repo)
        {
            _repo = repo;
        }

        [HttpPost("file/input")]
        public async Task<IActionResult> CheapestRoute(IFormFile fileInput)
        {
            if (fileInput == null)
                return BadRequest();

            if (await _repo.UpdateDatabase(fileInput))
                return BadRequest();

            return Ok();
        }
    }
}
