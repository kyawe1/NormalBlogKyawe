using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models.ViewModels
{
    public class AdminBlogViewModel
    {
        [Required]
        public string header { set; get; }
        [Required]
        public string body { set; get; }

    }
}
