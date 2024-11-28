namespace MeShop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Create_at { get; set; } = DateTime.Now;

        public int Cart_Id { get; set; }
        public Cart Cart { get; set; } = new Cart();

        public List<User_Address> Addresses { get; set; } = new List<User_Address>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
