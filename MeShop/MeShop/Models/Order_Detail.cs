namespace MeShop.Models
{
    public class Order_Detail
    {
        public int Id { get; set; }
        public decimal Unit_Price { get; set; }
        public int Quantity { get; set; }
        public int Discount_Percent { get; set; }

        public int Product_Size_Id { get; set; }
        public Product_Size? Product_Size { get; set; }

        public int Order_Id { get; set; }
        public Order? Order { get; set; }

    }
}
