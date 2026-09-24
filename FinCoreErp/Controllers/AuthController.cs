using Microsoft.AspNetCore.Mvc;

namespace FinCoreErp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
