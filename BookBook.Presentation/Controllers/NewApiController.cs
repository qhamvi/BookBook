using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BookBook.Presentation.Controllers
{
    [ServiceFilter(typeof(ControllerFilterExample), Order = 2)]
    [ApiController]
    [Route("[controller]")]
    public class NewApiController : ControllerBase
    {
        [HttpGet]
        [ServiceFilter(typeof(ActionFilterExample), Order = 1)]
        public IActionResult Get()
        {
            return Ok(new string[] { "example", "data" });
        }
    }
}