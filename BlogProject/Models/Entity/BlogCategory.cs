using System.ComponentModel.DataAnnotations;
namespace BlogProject.Models.Entity
{
    public class BlogCategory
    {
        [Key]
        public int Id { get; set; }    
        [Required]
        public int CategoryId { set; get; }
        [Required]
        public int BlogId { set; get; }
        public virtual Category? Category { get; set; }  
        public virtual Blog? blog { set; get; }
    }
}
