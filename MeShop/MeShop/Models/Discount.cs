namespace MeShop.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public int Discount_Percent { get; set; }
        public bool Active { get; set; } = true;
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Modified_At { get; set; } = DateTime.Now;

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
