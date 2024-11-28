namespace MeShop.Models
{
    public class Product_Inventory
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Modified_At { get; set;} = DateTime.Now;
        public DateTime Deleted_At { get; set; } = DateTime.Now;

        public Product_Size? Product_Size { get; set; }
    }
}
