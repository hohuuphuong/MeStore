namespace MeShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address_City { get; set; } = string.Empty;
        public string Address_District { get; set; } = string.Empty;
        public string Address_Ward { get; set; } = string.Empty;
        public string Address_Detail { get; set; } = string.Empty;
        public string? Oder_Note { get; set; } = string.Empty;
        public decimal Total { get; set; } = 0;
        public DateTime Created_At { get; set; }
        public DateTime? Modified_At { get; set; }
        public string Payment_Method {  get; set; } = string.Empty; 

        public List<Order_Detail> Orders_Detail { get; set; } = new List<Order_Detail>();

        public int? User_Id { get; set; }
        public User? User { get; set; }
    }
}
