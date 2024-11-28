namespace MeShop.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Product_Size> Product_Sizes { get; set; }  = new List<Product_Size>();
    }
}
