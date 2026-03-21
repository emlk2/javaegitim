namespace CorporateITAssetManagement.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // Laptop, Monitor, Phone, etc.
        public string Status { get; set; } = "Available"; // Available, Assigned, Broken
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        
        // Bir cihaz bir personele zimmetlendi
        public int? AssignedEmployeeId { get; set; }
        public Employee? AssignedEmployee { get; set; }
    }
}
