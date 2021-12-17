using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Models.Entity
{
    public class BlogPictures
    {
        public int Id { get; set; }
        [Required]
        public byte[] Image { set; get; }
        [ForeignKey("blog")]
        public int BlogId { set; get; }
        public virtual Blog? blog { set; get; }

        public string getImage()
        {
            return "data:image/png;base64," + Convert.ToBase64String(Image);
        }
    }
}
