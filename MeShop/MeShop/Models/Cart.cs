namespace MeShop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Decimal Total {  get; set; }


        public User? User { get; set; }
        public List<Cart_Item> Cart_Items { get; set; } = new List<Cart_Item>();

    } 
}
