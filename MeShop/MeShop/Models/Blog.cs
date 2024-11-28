namespace MeShop.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? Created_At { get; set; }

        public int? Image_Id { get; set; }
        public Image? Image { get; set; }
    }
}
