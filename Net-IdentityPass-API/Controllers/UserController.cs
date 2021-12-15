using Microsoft.AspNetCore.Mvc;

namespace Net_IdentityPass_API.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Settings()
        {
            return View();
        }
    }
}
