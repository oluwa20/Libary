// Controllers/MainCategoryController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Libary.Models;
using System.Threading.Tasks;

public class MainCategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public MainCategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.MainCategories.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] MainCategory mainCategory)
    {
        if (ModelState.IsValid)
        {
            _context.Add(mainCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(mainCategory);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var mainCategory = await _context.MainCategories.FindAsync(id);
        if (mainCategory == null)
        {
            return NotFound();
        }
        return View(mainCategory);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName")] MainCategory mainCategory)
    {
        if (id != mainCategory.CategoryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(mainCategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainCategoryExists(mainCategory.CategoryId))
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
        return View(mainCategory);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var mainCategory = await _context.MainCategories
            .FirstOrDefaultAsync(m => m.CategoryId == id);
        if (mainCategory == null)
        {
            return NotFound();
        }

        return View(mainCategory);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var mainCategory = await _context.MainCategories.FindAsync(id);
        _context.MainCategories.Remove(mainCategory);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MainCategoryExists(int id)
    {
        return _context.MainCategories.Any(e => e.CategoryId == id);
    }
}
