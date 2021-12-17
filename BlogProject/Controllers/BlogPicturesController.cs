using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogProject.Data;
using BlogProject.Models.Entity;
using Microsoft.AspNetCore.Authorization;

namespace BlogProject.Controllers
{
    [Authorize(Roles = "Adminstrator")]
    public class BlogPicturesController : Controller
    {
        private readonly DefaultDbContext _context;

        public BlogPicturesController(DefaultDbContext context)
        {
            _context = context;
        }

        // GET: BlogPictures
        public async Task<IActionResult> Index()
        {
            var defaultDbContext = _context.pictures.Include(b => b.blog);
            return View(await defaultDbContext.ToListAsync());
        }

        // GET: BlogPictures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPictures = await _context.pictures
                .Include(b => b.blog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPictures == null)
            {
                return NotFound();
            }

            return View(blogPictures);
        }

        // GET: BlogPictures/Create
        public IActionResult Create()
        {
            ViewData["BlogId"] = new SelectList(_context.blogs, "Id", "header");
            return View();
        }

        // POST: BlogPictures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,BlogId")] BlogPictures blogPictures)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogPictures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.blogs, "Id", "body", blogPictures.BlogId);
            return View(blogPictures);
        }

        // GET: BlogPictures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPictures = await _context.pictures.FindAsync(id);
            if (blogPictures == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_context.blogs, "Id", "body", blogPictures.BlogId);
            return View(blogPictures);
        }

        // POST: BlogPictures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,BlogId")] BlogPictures blogPictures)
        {
            if (id != blogPictures.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogPictures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPicturesExists(blogPictures.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.blogs, "Id", "body", blogPictures.BlogId);
            return View(blogPictures);
        }

        // GET: BlogPictures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPictures = await _context.pictures
                .Include(b => b.blog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPictures == null)
            {
                return NotFound();
            }

            return View(blogPictures);
        }

        // POST: BlogPictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPictures = await _context.pictures.FindAsync(id);
            _context.pictures.Remove(blogPictures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPicturesExists(int id)
        {
            return _context.pictures.Any(e => e.Id == id);
        }
    }
}
