using FinCoreErp.DTO.Authentication;
using FinCoreErp.Repository;
using FinCoreErp.Service;
using Microsoft.AspNetCore.Mvc;

namespace FinCoreErp.Controllers
{
    public class AuthController : Controller
    {

        private readonly IAuthService service;
        public AuthController(IAuthService service)
        {
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            bool result = await service.Register(dto);

            if (!result)
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(dto);
            }

            TempData["Success"] = "Registration successful.";

            return RedirectToAction("Index");
        }
    }
}
