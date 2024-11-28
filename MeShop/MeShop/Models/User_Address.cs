namespace MeShop.Models
{
    public class User_Address
    {
        public int Id { get; set; }
        public string Address_City { get; set; } = String.Empty;
        public string Address_District { get; set; } = String.Empty ;
        public string Address_Ward { get; set; } = String.Empty;
        public string Address_Detail { get; set; } = String.Empty;
        public bool IsDefault { get; set; }

        public int? User_Id { get; set; }
        public User? User { get; set; }
    }
}
