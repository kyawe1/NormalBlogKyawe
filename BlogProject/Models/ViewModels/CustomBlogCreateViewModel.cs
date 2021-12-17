using System.ComponentModel.DataAnnotations;
namespace BlogProject.Models.ViewModels
{
    public class CustomBlogCreateViewModel
    {
        
            [Required]
            public string header { set; get; }
            [Required]
            public string body { set; get; }
            public IFormFile image { set; get; }
            //public ICollection<Category> categories;
        
    }
}
