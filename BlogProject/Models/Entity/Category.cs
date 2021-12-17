using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; } 
        public string CategoryName { set; get; }
        public string Description { set; get; }
        public virtual ICollection<BlogCategory>? blogCategories { set; get; }
    }
}
