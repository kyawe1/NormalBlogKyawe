using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BlogProject.Models.Entity
{
    public class SaveBlog
    {
        [Key]
        public int Id { set; get; }
        [ForeignKey("user")]
        public string UserId { set; get; }
        [ForeignKey("blog")]
        public int BlogId { set; get; }
        public virtual Blog? blog { set; get; }
        public virtual ApplicationUser? user { set; get; }
        public DateTime created { set; get; }
        public SaveBlog()
        {
            created = DateTime.UtcNow;
        }
    }
}
