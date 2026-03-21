namespace CorporateITAssetManagement.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // Admin veya User
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
