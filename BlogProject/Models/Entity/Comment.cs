using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace BlogProject.Models.Entity;

public class Comment
{
    public int Id { set; get; }
    [Required]
    public string Name { set; get; }
    [Required]
    public string CommentBody { set;get; }
    [ForeignKey("blog")]
    public int BlogId { set; get; }
    [ForeignKey("User")]
    public string? UserId { set; get; }
    public virtual ApplicationUser? User { set; get; }
    public virtual Blog? blog { set; get; }

}
