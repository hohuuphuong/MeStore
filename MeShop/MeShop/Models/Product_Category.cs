namespace MeShop.Models
{
    public class Product_Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Modified_At { get; set; } = DateTime.Now;
        public DateTime Deleted_At { get; set; } = DateTime.Now;

        public List<Product> Products { get; set;} = new List<Product>();
    }
}
