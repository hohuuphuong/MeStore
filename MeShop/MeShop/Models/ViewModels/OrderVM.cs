namespace MeShop.Models.ViewModels
{
    public class OrderVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address_City { get; set; } = string.Empty;
        public string Address_District { get; set; } = string.Empty;
        public string Address_Ward { get; set; } = string.Empty;
        public string Address_Detail { get; set; } = string.Empty;
        public string? Oder_Note { get; set; } = string.Empty;
        public string Payment_Method { get; set; } = string.Empty;
    }
}
