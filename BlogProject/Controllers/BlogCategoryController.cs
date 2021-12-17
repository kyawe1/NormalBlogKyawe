using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data;
using BlogProject.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace BlogProject.Controllers
{
    [Authorize(Roles ="Adminstrator")]
    public class BlogCategoryController : Controller
    {
        private DefaultDbContext _context;
        public BlogCategoryController(DefaultDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(_context.blogCategories.ToList());
        }
        [Authorize]
        public IActionResult create()
        {
            return View();
        }
        [Authorize]
        public IActionResult update(int id)
        {
            var model = _context.blogCategories.FirstOrDefault(p => p.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> delete(int id)
        {
            var model = _context.blogCategories.FirstOrDefault(p => p.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            _context.blogCategories.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> update(int id, [Bind(new string[] {
            "BlogId","CategoryId"
})]BlogCategory blogCategory)
        {
            var model = _context.blogCategories.FirstOrDefault(p => p.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(blogCategory);
            }
            blogCategory.Id = id;
            _context.blogCategories.Update(blogCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create([Bind(new string[] {
            "BlogId","CategoryId"
})]BlogCategory blogCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(blogCategory);
            }

            _context.blogCategories.Add(blogCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
