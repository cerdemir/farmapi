using Microsoft.AspNetCore.Mvc;

namespace farmapi.Controllers
{
    public class BaseApiController : ControllerBase
    {

        public int GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return 0;
            }
            return int.Parse(User.Identity.Name);
        }
    }
}