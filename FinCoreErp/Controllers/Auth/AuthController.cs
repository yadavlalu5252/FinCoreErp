using FinCoreErp.DTO.Authentication;
using FinCoreErp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinCoreErp.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly IAuthService service;

        public AuthController(IAuthService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
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

            try
            {
                await service.Register(dto);

                TempData["Success"] = "Registration successful.";

                return RedirectToAction("Login");
            }
            catch (Exception ex)    
            {
                ModelState.AddModelError("", ex.Message);

                return View(dto);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var user = await service.Login(dto);

            if (user == null)
            {
                TempData["LoginError"] = "Invalid email or password.";

                return View(dto);
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetInt32("RoleId", user.RoleId);

            HttpContext.Session.SetString("Name", user.FullName);
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("RoleName", user.Role.RoleName);


            TempData["LoginSuccess"] = "Login Successful.";
            // Role-based redirect
            switch (user.Role.RoleName)
            {
                case "Administrator":
                    return RedirectToAction("Index", "Admin");

                case "Finance Manager":
                    return RedirectToAction("Index", "Finance");

                case "Procurement Manager":
                    return RedirectToAction("Index", "Procurement");

                case "Department Head":
                    return RedirectToAction("Index", "Department");

                case "Employee":
                    return RedirectToAction("Index", "Auth");

                case "Auditor":
                    return RedirectToAction("Index", "Auditor");

                case "CFO":
                    return RedirectToAction("Index", "CFO");

                default:
                    TempData["RoleError"] = "Role is not configured.";
                    HttpContext.Session.Clear();

                    return RedirectToAction("Login");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["LogoutSuccess"] = "Logout successful.";
            return RedirectToAction("Login", "Auth");
        }
    }
}