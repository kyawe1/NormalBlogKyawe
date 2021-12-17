using Microsoft.AspNetCore.Mvc;
using BlogProject.Data;
using Microsoft.EntityFrameworkCore;
using BlogProject.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace BlogProject.Controllers
{
    [Authorize(Roles="Adminstrator")]
    public class CategoryController : Controller
    {
        private readonly DefaultDbContext _context;
        public CategoryController(DefaultDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.categories.ToList());
        }
        public IActionResult create()
        {
            return View();
        }
        public IActionResult update(int id)
        {
            var model=_context.categories.FirstOrDefault(q => q.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> delete(int id)
        {
            var model = _context.categories.FirstOrDefault(q => q.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            _context.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create([Bind(include: new string[] {
            "CategoryName",
            "Description"
        })]Category category)
        {
            if (ModelState.IsValid)
            {
                _context.categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["success"] = "Object Created..";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> update(int id,[Bind(include: new string[] {
            "CategoryName",
            "Description"
        })]Category category)
        {
            var model=_context.categories.FirstOrDefault(p => p.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                category.Id=model.Id;
                _context.Update(category);
                await _context.SaveChangesAsync();
                TempData["success"] = "Object Updated..";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
