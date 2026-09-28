using System.ComponentModel.DataAnnotations;

namespace FinCoreErp.DTO.Budget
{
    public class BudgetLineDto
    {
        public int BudgetLineId { get; set; }

        [Required]
        public int BudgetId { get; set; }

        public string? BudgetName { get; set; }

        [Required]
        public int BudgetCategoryId { get; set; }

        public string? CategoryName { get; set; }

        [Required]
        public decimal AllocatedAmount { get; set; }

        public decimal? UtilizedAmount { get; set; }

        public decimal? VarianceAmount => AllocatedAmount - (UtilizedAmount ?? 0);

        public byte IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
