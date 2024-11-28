namespace MeShop.Models.ViewModels
{
    public class CartVM
    {
        public int Id { get; set; }
        public List<CartItemVM> Items { get; set; } = new List<CartItemVM>();
        public int Quantity { get; set; } = 0;
        public decimal Total { get; set; }
    }
}
