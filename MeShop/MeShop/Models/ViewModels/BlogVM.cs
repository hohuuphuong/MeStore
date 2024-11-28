namespace MeShop.Models.ViewModels
{
    public class BlogVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime? Created_At { get; set; }
    }
}
