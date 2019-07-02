using Microsoft.AspNetCore.Mvc;

namespace farmapi.Controllers
{
    public class BaseApiController : ControllerBase
    {
        [NonAction]
        public int GetUserId()
        {
            if (User == null || !User.Identity.IsAuthenticated)
            {
                return -1;
            }
            return int.Parse(User.Identity.Name);
        }
    }
}