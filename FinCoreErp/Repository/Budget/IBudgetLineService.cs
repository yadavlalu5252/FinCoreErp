using FinCoreErp.DTO.Budget;

namespace FinCoreErp.Repository.Budget
{
    public interface IBudgetLineService
    {
        Task<List<BudgetLineDto>> GetAll();
        Task<BudgetLineDto> GetById(int BudgetLineId);
        Task<bool> CreateBudgetLine(BudgetLineDto dto, int UserId);
        Task<bool> UpdateBudgetLine(BudgetLineDto dto, int UserId);
        Task<bool> DeleteBudgetLine(int BudgetLineId);
    }
}
