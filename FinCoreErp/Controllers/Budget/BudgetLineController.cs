using FinCoreErp.Data;
using FinCoreErp.DTO.Budget;
using FinCoreErp.Repository.Budget;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Controllers.Budget
{
    public class BudgetLineController : Controller
    {
        private readonly IBudgetLineService service;
        private readonly AppDbContext db;

        public BudgetLineController(IBudgetLineService service, AppDbContext db)
        {
            this.service = service;
            this.db = db;
        }

        private async Task LoadDropdowns()
        {
            ViewBag.Budgets = await db.Budgets.Where(x => x.IsActive == 1).ToListAsync();
            ViewBag.Categories = await db.BudgetCategories.Where(x => x.IsActive == 1).ToListAsync();
        }

        public async Task<IActionResult> Index()
        {
            var data = await service.GetAll();
            await LoadDropdowns();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BudgetLineDto dto)
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
            await service.CreateBudgetLine(dto, userId);

            TempData["Success"] = "Budget line created successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BudgetLineDto dto)
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
            await service.UpdateBudgetLine(dto, userId);

            TempData["Success"] = "Budget line updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await service.DeleteBudgetLine(id);
            if (!deleted)
            {
                return NotFound();
            }

            TempData["Success"] = "Budget line deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}