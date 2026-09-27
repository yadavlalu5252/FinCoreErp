using System.ComponentModel.DataAnnotations;

namespace FinCoreErp.Models
{
    public class Dummy
    {
        [Key]
        public int DummyId { get; set; }
        public string? DummyName { get; set; }
    }
}
