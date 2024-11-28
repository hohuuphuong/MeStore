namespace MeShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
        public string Desc { get; set; } = string.Empty;
        public float Rating { get; set; } = 5f;
        public bool Active { get; set; } = true;
        public DateTime Created_At { get; set; }
        public DateTime? Modified_At { get; set;} 
        public DateTime? Deleted_At { get; set; } 

        public List<Product_Size> Product_Sizes { get; set; } = new List<Product_Size>();

        public List<Image> Images { get; set; } = new List<Image>();

        public int? ProductCategory_Id;
        public Product_Category? Product_Category { get; set; }

        public int? Discount_Id {  get; set; }
        public Discount? Discount { get; set; }
    }
}
