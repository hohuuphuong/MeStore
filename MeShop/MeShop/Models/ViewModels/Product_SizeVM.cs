namespace MeShop.Models.ViewModels
{
    public class Product_SizeVM
    {
        public int Id { get; set; }
        public int Product_Id {  get; set; }
        public string Product_Name { get; set; } = string.Empty;
        public int Size_Id { get; set; }
        public int Quantity { get; set; }
    }
}
