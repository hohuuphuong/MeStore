namespace MeShop.Models.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Price_AfterDiscount {
            get
            {
                if (Discount_Percent != null && Discount_Active != null && Discount_Active != false)
                    return Price * (100 - (decimal)Discount_Percent) / 100;
                else
                    return Price;
            }
        }
        public string Desc { get; set; } = string.Empty;
        public bool Active { get; set; }
        public float Rating { get; set; } = 5f;
        public DateTime? Created_At { get; set; }
 
        public int? ProductCategory_Id { get; set; }
        public string? ProductCategory_Name { get; set; }

        public int? Discount_Id { get; set; }
        public int? Discount_Percent { get; set; } 
        public bool? Discount_Active { get; set; }

        public int Quantity_Sold { get; set; } = 0;

        public List<Product_Size> Product_Sizes { get; set; } = new List<Product_Size>();

        public List<IFormFile> ImageFiles { get; set; } = new List<IFormFile>();
        public List<Image> Images { get; set; } = new List<Image>();
        public List<string> existingPathImage { get; set; } = new List<string>();
        public List<string> existingNameImage { get; set; } = new List<string>();

        public List<ProductVM> newest_products {get; set; } = new List<ProductVM>();
        public List<ProductVM> popular_products {get; set; } = new List<ProductVM>();
        public List<ProductVM> bestseller_products {get; set; } = new List<ProductVM>();
    }
}
