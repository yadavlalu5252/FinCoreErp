using FinCoreErp.Data;
using FinCoreErp.DTO.Budget;
using FinCoreErp.Repository.Budget;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Service.Budget
{
    public class BudgetService : IBudgetService
    {
        private readonly AppDbContext db;
        public BudgetService(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> CreateBudget(BudgetDto dto, int userId)
        {
            var budget = new Models.Budget
            {
                BudgetCode = dto.BudgetCode,
                BudgetName = dto.BudgetName,
                FinancialYear = dto.FinancialYear,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                BudgetAmount = dto.BudgetAmount,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.Now,
                CreatedBy = userId,
                ModifiedAt = DateTime.Now,
                ModifiedBy = userId
            };

            db.Budgets.Add(budget);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<(bool Success, string Message)> DeleteBudget(int budgetId)
        {
            var budget = await db.Budgets.FindAsync(budgetId);
            if (budget == null)
                return (false, "Budget not found.");

            bool hasLines = await db.BudgetLines.AnyAsync(l => l.BudgetId == budgetId);
            if (hasLines)
                return (false, "This budget has budget lines and cannot be deleted. Remove the lines first or deactivate the budget.");

            db.Budgets.Remove(budget);
            await db.SaveChangesAsync();
            return (true, "Budget deleted successfully.");
        }

        public async Task<List<BudgetDto>> GetAllBudgets()
        {
            var budgets = await db.Budgets
                .Select(x => new BudgetDto
                {
                    BudgetId = x.BudgetId,
                    BudgetCode = x.BudgetCode,
                    BudgetName = x.BudgetName,
                    FinancialYear = x.FinancialYear,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    BudgetAmount = x.BudgetAmount,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt
                }).ToListAsync();

            return budgets;
        }

        public async Task<BudgetDto> GetBudgetById(int BudgetId)
        {
            var budget = await db.Budgets.FindAsync(BudgetId);
            if (budget == null)
            {
                return null;
            }

            var dto = new BudgetDto
            {
                BudgetId = budget.BudgetId,
                BudgetCode = budget.BudgetCode,
                BudgetName = budget.BudgetName,
                FinancialYear = budget.FinancialYear,
                StartDate = budget.StartDate,
                EndDate = budget.EndDate,
                BudgetAmount = budget.BudgetAmount,
                IsActive = budget.IsActive,
                CreatedAt = budget.CreatedAt
            };
            return dto;
        }

        public async Task<bool> UpdateBudget(BudgetDto dto, int userId)
        {
            var budget = await db.Budgets.FindAsync(dto.BudgetId);
            if (budget == null)
            {
                return false;
            }

            budget.BudgetCode = dto.BudgetCode;
            budget.BudgetName = dto.BudgetName;
            budget.FinancialYear = dto.FinancialYear;
            budget.StartDate = dto.StartDate;
            budget.EndDate = dto.EndDate;
            budget.BudgetAmount = dto.BudgetAmount;
            budget.IsActive = dto.IsActive;
            budget.ModifiedAt = DateTime.Now;
            budget.ModifiedBy = userId;

            await db.SaveChangesAsync();
            return true;
        }
    }
}
