using FinCoreErp.Data;
using FinCoreErp.DTO.Budget;
using FinCoreErp.Filter;
using FinCoreErp.Repository.Budget;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Controllers.Budget
{

    public class BudgetCategoryController : Controller
    {
        private readonly IBudgetCategoryService service;
        private readonly AppDbContext db;

        public BudgetCategoryController(IBudgetCategoryService service, AppDbContext db)
        {
            this.service = service;
            this.db = db;
        }

        private async Task LoadDropdowns()
        {
            ViewBag.Departments = await db.Departments.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<IActionResult> Index()
        {
            var data = await service.GetAll();
            await LoadDropdowns();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BudgetCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                var data = await service.GetAll();
                await LoadDropdowns();
                ViewBag.OpenModal = "create";
                ViewBag.FormData = dto;
                return View("Index", data);
            }

            int userId = 1;
            await service.CreateBudgetCategory(dto, userId);

            TempData["Success"] = "Budget category created successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BudgetCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                var data = await service.GetAll();
                await LoadDropdowns();
                ViewBag.OpenModal = "edit";
                ViewBag.FormData = dto;
                return View("Index", data);
            }

            int userId = 1;
            await service.UpdateBudgetCategory(dto, userId);

            TempData["Success"] = "Budget category updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await service.GetById(id);
            if (dto == null)
            {
                return NotFound();
            }

            await service.DeleteBudgetCategory(id);
            TempData["Success"] = "Budget category deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}