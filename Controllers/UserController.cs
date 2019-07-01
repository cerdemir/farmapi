using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace farmapi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get () {
            return new string[] { "value1", "value2" };
        }
    }
}