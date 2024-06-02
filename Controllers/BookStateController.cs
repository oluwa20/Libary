// Controllers/BookStateController.cs
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Libary.Models;

namespace Libary.Controllers
{
    public class BookStateController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookStateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BookState
        public async Task<IActionResult> Index()
        {
            return View(await _context.BookStates.ToListAsync());
        }

        // GET: BookState/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookState = await _context.BookStates
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (bookState == null)
            {
                return NotFound();
            }

            return View(bookState);
        }

        // GET: BookState/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookState/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,StatusName,CreateDate")] BookState bookState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookState);
        }

        // GET: BookState/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookState = await _context.BookStates.FindAsync(id);
            if (bookState == null)
            {
                return NotFound();
            }
            return View(bookState);
        }

        // POST: BookState/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,StatusName,CreateDate")] BookState bookState)
        {
            if (id != bookState.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookStateExists(bookState.StatusId))
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
            return View(bookState);
        }

        // GET: BookState/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookState = await _context.BookStates
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (bookState == null)
            {
                return NotFound();
            }

            return View(bookState);
        }

        // POST: BookState/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookState = await _context.BookStates.FindAsync(id);
            _context.BookStates.Remove(bookState);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookStateExists(int id)
        {
            return _context.BookStates.Any(e => e.StatusId == id);
        }
    }
}
