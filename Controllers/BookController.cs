// Controllers/BookController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Libary.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var books = _context.Books.Include(b => b.MainCategory).Include(b => b.SubCategory).Include(b => b.BookState);
        return View(await books.ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "CategoryId", "CategoryName");
        ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryName");
        ViewData["BookStateId"] = new SelectList(_context.BookStates, "StatusId", "StatusName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BookId,Title,Author,Publisher,Type,Availability,NumberOfCopies,Description,isShow,MainCategoryId,SubCategoryId,BookStateId")] Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "CategoryId", "CategoryName", book.MainCategoryId);
        ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryName", book.SubCategoryId);
        ViewData["BookStateId"] = new SelectList(_context.BookStates, "StatusId", "StatusName", book.BookStateId);
        return View(book);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "CategoryId", "CategoryName", book.MainCategoryId);
        ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryName", book.SubCategoryId);
        ViewData["BookStateId"] = new SelectList(_context.BookStates, "StatusId", "StatusName", book.BookStateId);
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Author,Publisher,Type,Availability,NumberOfCopies,Description,isShow,MainCategoryId,SubCategoryId,BookStateId")] Book book)
    {
        if (id != book.BookId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookId))
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
        ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "CategoryId", "CategoryName", book.MainCategoryId);
        ViewData["SubCategoryId"] = new SelectList(_context.SubCategories, "SubcategoryId", "SubcategoryName", book.SubCategoryId);
        ViewData["BookStateId"] = new SelectList(_context.BookStates, "StatusId", "StatusName", book.BookStateId);
        return View(book);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .Include(b => b.MainCategory)
            .Include(b => b.SubCategory)
            .Include(b => b.BookState)
            .FirstOrDefaultAsync(m => m.BookId == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _context.Books.FindAsync(id);
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.BookId == id);
    }
}
