using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models.ViewModels.IdentityViewModel
{
    public class RegisterViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { set; get; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { set; get; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password",ErrorMessage ="Password and Confirm Password must be the same...")]
        public string ConfirmPassword { set; get; }
    }
}
