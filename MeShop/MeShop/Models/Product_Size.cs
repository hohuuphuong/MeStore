namespace MeShop.Models
{
    public class Product_Size
    {
        public int Id { get; set; }

        public int? Product_Id { get; set; }
        public Product? Product { get; set; }

        public int? Size_Id { get; set; }
        public Size? Size { get; set; } 

        public int? ProductInventory_Id { get; set; }
        public Product_Inventory? Product_Inventory { get; set; }

        public List<Cart_Item> Cart_Items = new List<Cart_Item>();
        public List<Order_Detail> Orders_Detail = new List<Order_Detail>();
    }
}
