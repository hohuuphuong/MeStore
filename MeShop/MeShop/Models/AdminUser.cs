using System.Data;

namespace MeShop.Models
{
    public class AdminUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime Created_at { get; set; } = DateTime.Now;
        public DateTime Modified_at { get; set; } = DateTime.Now;
        public DateTime Deleted_at { get; set; } = DateTime.Now;
    }
}
