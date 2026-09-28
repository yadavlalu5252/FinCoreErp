using FinCoreErp.DTO.Budget;

namespace FinCoreErp.Repository.Budget
{
    public interface IBudgetService
    {
        Task<List<BudgetDto>> GetAllBudgets();
        Task<BudgetDto> GetBudgetById(int BudgetId);
        Task<bool> CreateBudget(BudgetDto dto, int userId);
        Task<bool> UpdateBudget(BudgetDto dto, int userId);
        Task<(bool Success, string Message)> DeleteBudget(int BudgetId);
    }
}
