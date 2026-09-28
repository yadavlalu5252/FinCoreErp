using System.ComponentModel.DataAnnotations;

namespace FinCoreErp.DTO.Budget
{
    public class BudgetDto
    {
        public int BudgetId { get; set; }

        [Required]
        [StringLength(20)]
        public string BudgetCode { get; set; }

        [Required]
        [StringLength(30)]
        public string BudgetName { get; set; }

        [Required]
        [StringLength(20)]
        public string FinancialYear { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public decimal BudgetAmount { get; set; }

        public byte IsActive { get; set; } = 1;

        public DateTime? CreatedAt { get; set; }
    }
}
