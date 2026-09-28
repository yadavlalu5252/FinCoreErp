using FinCoreErp.Data;
using FinCoreErp.DTO.Budget;
using FinCoreErp.Models;
using FinCoreErp.Repository.Budget;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Service.Budget
{
    public class BudgetLineService : IBudgetLineService
    {
        private readonly AppDbContext db;
        public BudgetLineService(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> CreateBudgetLine(BudgetLineDto dto, int UserId)
        {
            var line = new BudgetLine
            {
                BudgetId = dto.BudgetId,
                BudgetCategoryId = dto.BudgetCategoryId,
                AllocatedAmount = dto.AllocatedAmount,
                UtilizedAmount = 0,
                IsActive = 1,
                CreatedAt = DateTime.Now,   
                CreatedBy = UserId,
                ModifiedBy = UserId
            };

            db.BudgetLines.Add(line);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBudgetLine(int BudgetLineId)
        {
            var line = await db.BudgetLines.FindAsync(BudgetLineId);
            if (line == null)
            {
                return false;
            }   

            db.BudgetLines.Remove(line);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<BudgetLineDto>> GetAll()
        {
            var data = await db.BudgetLines
                .Include(x => x.Budget)
                .Include(x => x.BudgetCategory)
                .Select(x => new BudgetLineDto
                {
                    BudgetLineId = x.BudgetLineId,
                    BudgetId = x.BudgetId,
                    BudgetName = x.Budget.BudgetName,
                    BudgetCategoryId = x.BudgetCategoryId,
                    CategoryName = x.BudgetCategory.CategoryName,
                    AllocatedAmount = x.AllocatedAmount,
                    UtilizedAmount = x.UtilizedAmount,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt
                }).ToListAsync();

            return data;
        }

        public async Task<BudgetLineDto> GetById(int BudgetLineId)
        {
            var line = await db.BudgetLines
                .Include(x => x.Budget)
                .Include(x => x.BudgetCategory)
                .FirstOrDefaultAsync(x => x.BudgetLineId == BudgetLineId);

            if (line == null)
            {
                return null;
            }

            return new BudgetLineDto
            {
                BudgetLineId = line.BudgetLineId,
                BudgetId = line.BudgetId,
                BudgetName = line.Budget.BudgetName,
                BudgetCategoryId = line.BudgetCategoryId,
                CategoryName = line.BudgetCategory.CategoryName,
                AllocatedAmount = line.AllocatedAmount,
                UtilizedAmount = line.UtilizedAmount,
                IsActive = line.IsActive,
                CreatedAt = line.CreatedAt
            };
        }

        public async Task<bool> UpdateBudgetLine(BudgetLineDto dto, int UserId)
        {
            var line = await db.BudgetLines.FindAsync(dto.BudgetLineId);
            if (line == null)
            {
                return false;
            }

            line.BudgetId = dto.BudgetId;
            line.BudgetCategoryId = dto.BudgetCategoryId;
            line.AllocatedAmount = dto.AllocatedAmount;
            line.IsActive = dto.IsActive;
            line.ModifiedAt = DateTime.Now;
            line.ModifiedBy = UserId;

            await db.SaveChangesAsync();
            return true;
        }
    }
}
