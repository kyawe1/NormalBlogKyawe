using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogProject.Services.Interfaces;
using BlogProject.Services.Worker;
using Microsoft.AspNetCore.Authorization;
using BlogProject.Models.ViewModels;
using BlogProject.Models.Entity;
using BlogProject.Data;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IPictureStore _blogPictureStore;
        private readonly DefaultDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IPictureStore blogPictureStore, DefaultDbContext context,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _blogPictureStore = blogPictureStore;
            _context = context;
        }
        public string Index()
        {
            return "Myanmar";
        }
        [HttpGet, Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet, Authorize, ActionName("UserUpdateBlog")]
        [Route("[controller]/[action]/blog")]
        public IActionResult Update(int id)
        {
            var temp = _context.blogs.FirstOrDefault(p => p.Id == id);
            if (temp != null)
            {
                var list = _context.pictures.Where(p => p.BlogId == temp.Id).ToList();
                var c = new CustomBlogViewModel()
                {
                    body = temp.body,
                    header = temp.header,
                    image = Convert.ToBase64String(list[0].Image)
                };
                return View(c);
            }
            return NotFound();
        }
        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomBlogCreateViewModel blog)
        {
            var Id=_userManager.GetUserId(User);
            if (!ModelState.IsValid)
            {
                return View(blog);
            }
            if (blog.image != null)
            {
                byte[]? k = _blogPictureStore.store(blog.image);
                Blog b = new Blog()
                {
                    
                    body = blog.body,
                    header = blog.header,
                    User_id = Id,
                    pictures=new List<BlogPictures>()
                    {
                        new BlogPictures()
                        {
                            Image=k
                        }
                    }
                };
                _context.Add(b);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Blog? blog=_context.blogs.FirstOrDefault(p => p.Id == id);
            if (blog != null)
            {
                _context.blogs.Remove(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Detail", "Home");
        }
        [AllowAnonymous]
        public IActionResult all()
        {
            return View(_context.blogs.Include(p => p.pictures).ToList());
        }
        [AllowAnonymous]
        public IActionResult detail(int id)
        {
            return View(_context.blogs.Include(p=>p.comments).Include(p => p.pictures).FirstOrDefault(p=>p.Id==id));
        }
        [HttpPost,Authorize]
        public async Task<IActionResult> UserCreateComment(int id,[FromForm] string CommentBody)
        {
            Comment m = new Comment()
            {
                BlogId = id,
                CommentBody = CommentBody,
                Name = User.Identity.Name,
                UserId =_userManager.GetUserId(User)
            };
            _context.Add(m);
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", new { id=id});
        }
    }
}
