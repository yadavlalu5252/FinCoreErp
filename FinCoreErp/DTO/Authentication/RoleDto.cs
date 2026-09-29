using System.ComponentModel.DataAnnotations;

namespace FinCoreErp.DTO.Authentication
{
    public class RoleDto
    {
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public string Description { get; set; }

        [Required]
        public byte IsActive { get; set; }
    }
}
