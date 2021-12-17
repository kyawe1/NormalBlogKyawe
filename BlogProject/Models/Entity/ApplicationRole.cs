using Microsoft.AspNetCore.Identity;
namespace BlogProject.Models.Entity
{
    public class ApplicationRole:IdentityRole<int>
    {
        public ApplicationRole() : base()
        {
            
        }
    }
}
