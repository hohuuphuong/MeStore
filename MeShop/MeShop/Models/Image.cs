namespace MeShop.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
     
        public int? Product_Id { get; set; }
        public Product? Product { get; set; }

        public Blog? Blog { get; set; }
    }
}
