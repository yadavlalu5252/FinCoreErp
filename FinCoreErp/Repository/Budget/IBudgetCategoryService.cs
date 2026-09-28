using FinCoreErp.DTO.Budget;

namespace FinCoreErp.Repository.Budget
{
    public interface IBudgetCategoryService
    {
        Task<List<BudgetCategoryDto>> GetAll();
        Task<BudgetCategoryDto> GetById(int BudgetCategoryId);
        Task<bool> CreateBudgetCategory(BudgetCategoryDto dto, int UserId);
        Task<bool> UpdateBudgetCategory(BudgetCategoryDto dto, int UserId);
        Task<bool> DeleteBudgetCategory(int BudgetCategoryId);
    }
}
