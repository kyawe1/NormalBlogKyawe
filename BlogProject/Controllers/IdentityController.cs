using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BlogProject.Models.ViewModels.IdentityViewModel;
using BlogProject.Models.Entity;
using Microsoft.Extensions.Logging;


namespace BlogProject.Controllers
{
    [Authorize]
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<IdentityController> _logger;
        public IdentityController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,ILogger<IdentityController> logger)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager=signInManager;
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost,AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind(include:new string[] {
            "Email",
            "Password",
            "RememberMe"
})] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result=await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        [HttpPost,AllowAnonymous]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Register([Bind(include:new string[] {
            "Email",
            "Password",
            "ConfirmPassword"
})] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var temp = new IdentityUser();
                var user = new ApplicationUser()
                {
                    Id=temp.Id,
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result=await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var result1=await _userManager.AddToRoleAsync(user, "Normal");
                    if (result1.Succeeded)
                    {
                        _logger.LogInformation(Convert.ToString(DateTime.UtcNow)+"This Account is created");
                        TempData["success"] = "Your Account Is Created";
                        return RedirectToAction("Login");
                    } 
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [ActionName("AdminRegister"),AllowAnonymous]
        [Route("admin/register")]
        public IActionResult RegisterAdmin()
        {
            return View();
        }
        [HttpPost, AllowAnonymous,ActionName("AdminResgister")]
        [Route("admin/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin([Bind(include:new string[] {
            "Email",
            "Password",
            "ConfirmPassword"
})] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var temp = new IdentityUser();
                var user = new ApplicationUser()
                {
                    Id = temp.Id,
                    UserName = model.Email,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var result1 = await _userManager.AddToRoleAsync(user, "Adminstrator");
                    if (result1.Succeeded)
                    {
                        _logger.LogInformation(Convert.ToString(DateTime.UtcNow) + "This Admin Account is created");
                        TempData["success"] = "Your Account Is Created";
                        return RedirectToAction("Login");
                    }
                }
            }
            return View(model);
        }
    }

}
