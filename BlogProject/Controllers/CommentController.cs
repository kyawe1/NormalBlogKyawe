using BlogProject.Data;
using BlogProject.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace BlogProject.Controllers
{
    [Authorize(Roles ="Adminstrator")]
    public class CommentController : Controller
    {
        private DefaultDbContext _context;
        private UserManager<ApplicationUser> _userManager { set; get; }
        public CommentController(DefaultDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager=userManager;
        }
        public IActionResult Index()
        {
            return View(_context.comments.Include(p => p.blog).ToList());
        }
        public IActionResult create()
        {
            ViewData["BlogId"] = new SelectList(_context.blogs, "Id", "body");
            return View();
        }
        public IActionResult update(int id)
        {
            var model = _context.comments.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_context.blogs, "Id", "header");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> delete(int id)
        {
            var model = _context.comments.FirstOrDefault(p => p.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            _context.comments.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create([Bind(new string[] {
            "Name",
            "BlogId",
            "CommentBody"
})]Comment model)
        {
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByNameAsync(User.Identity.Name);
                model.UserId = user.Id;
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.blogs, "Id", "header");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> update(int id, [Bind(new string[] {
            "Name",
            "BlogId",
            "CommentBody"
})]Comment comment)
        {
            var model = _context.comments.FirstOrDefault(x => x.Id == id);
            var user=await _userManager.FindByNameAsync(User.Identity.Name);
            if (model == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid && model.UserId==user.Id)
            {
                comment.Id = id;
                _context.Entry(comment).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if(ModelState.IsValid)
            {
                return BadRequest();
            }
            ViewData["BlogId"] = new SelectList(_context.blogs, "Id", "header");
            return View(model);
        }
    }
}
