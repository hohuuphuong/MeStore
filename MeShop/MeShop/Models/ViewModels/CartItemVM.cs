namespace MeShop.Models.ViewModels
{
    public class CartItemVM
    {
        public int? Cart_Id { get; set; }
        public int ProductSize_Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int quantity { get; set; }
        public string SizeName { get; set; } = string.Empty;

        public int? Discount_Percent { get; set; }
        public bool? Discount_Active { get; set; }

        public decimal Price { get; set; }
        public decimal Price_AfterDiscount
        {
            get
            {
                if (Discount_Percent != null && Discount_Active != null && Discount_Active != false)
                    return Price * (100 - (decimal)Discount_Percent) / 100;
                else
                    return Price;
            }
        }

        public string? PathImage { get; set; } = String.Empty;

    }
}
