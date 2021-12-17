using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Models.Entity
{
    public class Blog
    {
        [Key]
        public int Id { set; get; }
        [StringLength(350,ErrorMessage ="The title is not greater than 350 characters")]
        [Required]
        public string header { set; get; }
        [StringLength(1000)]
        [Required]
        public string body { set; get; }
        [ForeignKey("User")]
        public string? User_id { set; get; }
        public virtual ApplicationUser? User { set; get; }
        public virtual ICollection<BlogCategory>? blogCategories { set; get; }
        public virtual ICollection<BlogPictures>? pictures { set; get; }
        public virtual ICollection<Comment>? comments { set; get; }
    }
}
