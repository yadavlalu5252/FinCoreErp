using FinCoreErp.DTO.Authentication;
using FinCoreErp.Filter;
using FinCoreErp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinCoreErp.Controllers.Authentication
{
    //[SessionAuthorize]
    //[RoleAuthorize("Administrator")]
    public class RoleController : Controller
    {
        private readonly IRoleService service;

        public RoleController(IRoleService service)
        {
            this.service = service;
        }

     
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetAll()
        {
            var data = await service.GetAllRole();

            return Json(data);
        }


        public async Task<IActionResult> GetById(int id)
        {
            var data = await service.GetRoleById(id);

            return Json(data);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(RoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Please enter valid details."
                });
            }

            int userId = HttpContext.Session.GetInt32("UserId").Value;

            await service.CreateRole(dto, userId);

            return Json(new
            {
                success = true,
                message = "Role created successfully."
            });
        }

        
        [HttpPost]
        public async Task<IActionResult> Update(RoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Please enter valid details."
                });
            }

            int userId = HttpContext.Session.GetInt32("UserId").Value;

            await service.UpdateRole(dto, userId);

            return Json(new
            {
                success = true,
                message = "Role updated successfully."
            });
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;

            await service.DeleteRole(id, userId);

            return Json(new
            {
                success = true,
                message = "Role deleted successfully."
            });
        }
    }
}