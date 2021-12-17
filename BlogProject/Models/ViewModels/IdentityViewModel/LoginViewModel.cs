using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models.ViewModels.IdentityViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Required]
        [StringLength(50,MinimumLength =6)]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        public bool RememberMe { set; get; } = false;
    }
}
