using FinCoreErp.Data;
using FinCoreErp.DTO.Budget;
using FinCoreErp.Models;
using FinCoreErp.Repository.Budget;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Service.Budget
{
    public class BudgetCategoryService : IBudgetCategoryService
    {
        private readonly AppDbContext db;
        public BudgetCategoryService(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<bool> CreateBudgetCategory(BudgetCategoryDto dto, int UserId)
        {
            var category = new BudgetCategory
            {
                CategoryName = dto.CategoryName,
                DepartmentId = dto.DepartmentId,
                IsActive = 1,
                CreatedAt = DateTime.Now,
                CreatedBy = UserId,
                ModifiedBy = UserId
            };

            db.BudgetCategories.Add(category);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBudgetCategory(int budgetCategoryId)
        {
            var category = await db.BudgetCategories.FindAsync(budgetCategoryId);
            if (category == null)
            {
                return false;
            }

            db.BudgetCategories.Remove(category);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<List<BudgetCategoryDto>> GetAll()
        {
            var data = await db.BudgetCategories
                .Include(x => x.Department)
                .Select(x => new BudgetCategoryDto
                {
                    BudgetCategoryId = x.BudgetCategoryId,
                    CategoryName = x.CategoryName,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.Department.DepartmentName,
                    IsActive = x.IsActive,
                    CreatedAt = x.CreatedAt
                }).ToListAsync();
            
            return data;
        }

        public async Task<BudgetCategoryDto> GetById(int BudgetCategoryId)
        {
            var category = await db.BudgetCategories.FindAsync(BudgetCategoryId);
            if (category == null)
            {
                return null;
            }

            var dto = new BudgetCategoryDto
            {
                BudgetCategoryId = category.BudgetCategoryId,
                CategoryName = category.CategoryName,
                DepartmentId = category.DepartmentId,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt
            };
            return dto;
        }

        public async Task<bool> UpdateBudgetCategory(BudgetCategoryDto dto, int UserId)
        {
            var category = await db.BudgetCategories.FindAsync(dto.BudgetCategoryId);
            if (category == null)
            {
                return false;
            }

            category.CategoryName = dto.CategoryName;
            category.DepartmentId = dto.DepartmentId;
            category.IsActive = dto.IsActive;
            category.ModifiedAt = DateTime.Now;
            category.ModifiedBy = UserId;

            await db.SaveChangesAsync();
            return true;
        }
    }
}
