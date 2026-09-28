using System.ComponentModel.DataAnnotations;

namespace FinCoreErp.DTO.Budget
{
    public class BudgetCategoryDto
    {
        public int BudgetCategoryId { get; set; }

        [Required]
        [StringLength(20)]
        public string CategoryName { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public byte IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
