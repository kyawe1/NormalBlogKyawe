using BlogProject.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models.ViewModels
{
    public class CustomBlogViewModel
    {
        [Required]
        public string header { set; get; }
        [Required]
        public string body { set; get; }
        public string image { set; get; }
        //public ICollection<Category> categories;
    }
}
