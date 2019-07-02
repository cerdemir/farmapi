using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using farmapi.Models;
using farmapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace farmapi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Authenticates user 
        /// </summary>
        /// <param name="usermodel">user credentials</param>

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public ActionResult<ApiResponseModel<UserAuthResultModel>> Authenticate([FromBody] UserAuthModel usermodel)
        {
            var authResult = _userService.Authenticate(usermodel.Username, usermodel.Password);

            if (!authResult.Authenticated)
            {
                return BadRequest(new ApiResponseModel<UserAuthResultModel> { Success = false, Data = authResult });
            }

            return Ok(new ApiResponseModel<UserAuthResultModel> { Data = authResult });
        }
        /// <summary>
        /// Register user 
        /// </summary>
        /// <param name="registermodel">User registeration details</param>
        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult<ApiResponseModel<Entities.User>> RegisterUser([FromBody] UserRegisterModel registermodel)
        {
            var user = _userService.Register(registermodel);

            if (user == null)
            {
                return BadRequest(new ApiResponseModel<Entities.User> { Success = false });
            }

            return Ok(new ApiResponseModel<Entities.User> { Data = user });
        }
    }
}