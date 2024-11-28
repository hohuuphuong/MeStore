namespace MeShop.Models
{
    public class Cart_Item
    {
        public int Cart_Id { get; set; }
        public int ProductSize_Id { get; set; }

        public int quantity { get; set; }
        public bool IsSelection { get; set; }

        public Cart? Cart { get; set; }// cart_id

        public Product_Size? Product_Size { get; set; } // product size 
    }
}
