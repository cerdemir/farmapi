using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace farmapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// I am here to show what is happening in exception
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            throw new Exception("I am an exception");
            return new string[] { "value1", "value2" };
        }
    }
}