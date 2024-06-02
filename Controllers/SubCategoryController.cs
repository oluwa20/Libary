using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Libary.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

public class SubCategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public SubCategoryController(ApplicationDbContext context)
    {
        _context = context;
    }



    public IActionResult Create()
    {
        ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "CategoryId", "CategoryName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([Bind("SubcategoryId,SubcategoryName,MainCategoryId")] SubCategory subCategory)
    {
        if (ModelState.IsValid)
        {
            _context.Add(subCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "CategoryId", "CategoryName", subCategory.MainCategoryId);
        return View(subCategory);
    }










    public async Task<IActionResult> Index()
    {
        var subCategories = _context.SubCategories.Include(s => s.MainCategory);
        return View(await subCategories.ToListAsync());
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subCategory = await _context.SubCategories.FindAsync(id);
        if (subCategory == null)
        {
            return NotFound();
        }
        ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "CategoryId", "CategoryName", subCategory.MainCategoryId);
        return View(subCategory);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("SubcategoryId,SubcategoryName,MainCategoryId")] SubCategory subCategory)
    {
        if (id != subCategory.SubcategoryId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(subCategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(subCategory.SubcategoryId))
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
        ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "CategoryId", "CategoryName", subCategory.MainCategoryId);
        return View(subCategory);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subCategory = await _context.SubCategories
            .Include(s => s.MainCategory)
            .FirstOrDefaultAsync(m => m.SubcategoryId == id);
        if (subCategory == null)
        {
            return NotFound();
        }

        return View(subCategory);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var subCategory = await _context.SubCategories.FindAsync(id);
        _context.SubCategories.Remove(subCategory);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SubCategoryExists(int id)
    {
        return _context.SubCategories.Any(e => e.SubcategoryId == id);
    }
}
