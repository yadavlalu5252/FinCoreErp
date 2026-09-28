using FinCoreErp.DTO.Budget;
using FinCoreErp.Repository.Budget;
using Microsoft.AspNetCore.Mvc;

namespace FinCoreErp.Controllers.Budget
{
    public class BudgetController : Controller
    {
        private readonly IBudgetService service;

        public BudgetController(IBudgetService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await service.GetAllBudgets();
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BudgetDto dto)
        {
            if (!ModelState.IsValid)
            {
                var data = await service.GetAllBudgets();
                ViewBag.OpenModal = "create";
                ViewBag.FormData = dto;
                return View("Index", data);
            }

            int userId = 1;
            await service.CreateBudget(dto, userId);

            TempData["Success"] = "Budget created successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BudgetDto dto)
        {
            if (!ModelState.IsValid)
            {
                var data = await service.GetAllBudgets();
                ViewBag.OpenModal = "edit";
                ViewBag.FormData = dto;
                return View("Index", data);
            }

            int userId = 1;
            await service.UpdateBudget(dto, userId);

            TempData["Success"] = "Budget updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await service.DeleteBudget(id);

            if (!result.Success)
                TempData["Error"] = result.Message;
            else
                TempData["Success"] = result.Message;

            return RedirectToAction("Index");
        }
    }
}