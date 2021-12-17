using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Models.Entity;

    [Table("users")]
    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser() : base()
        {

        }
        public virtual ICollection<Blog>? blogs { set; get; }
        public virtual ICollection<Comment>? comments { set; get; }
    
    }

