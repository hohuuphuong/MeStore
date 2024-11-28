namespace MeShop.Models
{
    public class Payment_Method
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty ;

        public List<Order> Orders{ get; set; } = new List<Order>();
    }
}
