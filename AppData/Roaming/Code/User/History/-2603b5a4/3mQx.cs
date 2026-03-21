using System.ComponentModel.DataAnnotations;

namespace CorporateITAssetManagement.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Equipment name is required")]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Serial number is required")]
        [StringLength(100)]
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Available";

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        
        public int? AssignedEmployeeId { get; set; }
        public Employee? AssignedEmployee { get; set; }
    }
}
