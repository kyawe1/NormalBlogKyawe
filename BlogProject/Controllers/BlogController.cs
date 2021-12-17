using BlogProject.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.Controllers;

[Authorize(Roles = "Adminstrator")]
public class BlogController : Controller
{
    private readonly DefaultDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public BlogController(DefaultDbContext context,UserManager<ApplicationUser> userManager)
    {
        this._context = context;
        _userManager= userManager;
    }
    public async Task<IActionResult> Index()
    {
        return View(await _context.blogs.ToListAsync());
    }
    public IActionResult Create()
    {
        return View();
    }
    public async Task<IActionResult> Update(int id)
    {
        var model = _context.blogs.FirstOrDefault(p => p.Id == id);
        var user=await _userManager.FindByNameAsync(User.Identity?.Name);
        if(model == null && user.Id!=model?.User_id)
        {
            return NotFound();
        }
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var p = _context.blogs.FirstOrDefault(p => p.Id == id);
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        if (p != null && user.Id==p?.User_id)
        {
            _context.blogs.Remove(p);
            TempData["success"] = "Delete Success";
            return RedirectToActionPermanent(nameof(Index));
        }
        TempData["error"] = "Delete Did Not Success";
        return RedirectToActionPermanent(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind(include:new string[] {
        "header",
        "body"
    })]Blog blog)
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        if (ModelState.IsValid)
        {
            blog.User_id = user.Id;
            _context.Add(blog);
            await _context.SaveChangesAsync();
            TempData["success"] = "Object Created";
            return RedirectToAction(nameof(Index));
        }
        return View(blog);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, [Bind(include:new string[] {
        "header",
        "body"
    })]Blog blog)
    {
        if (ModelState.IsValid)
        {
            var model=_context.blogs.FirstOrDefault(p => p.Id == id);
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);
            if (model != null && model?.User_id==user.Id)
            {
                blog.Id = id;
                _context.Update(blog);
                await _context.SaveChangesAsync();
                TempData["success"] = "Object Updated";
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        return View(blog);
    }
}

