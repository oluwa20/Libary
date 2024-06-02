
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Libary.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ReservationController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReservationController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var reservations = _context.Reservations.Include(r => r.Book).Include(r => r.User);
        return View(await reservations.ToListAsync());
    }

    public IActionResult Create()
    {
        ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ReservationId,UserId,BookId,ReservedDate,ReturnDate,StatusId")] Reservation reservation)
    {
        if (ModelState.IsValid)
        {
            _context.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", reservation.BookId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", reservation.UserId);
        return View(reservation);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null)
        {
            return NotFound();
        }
        ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", reservation.BookId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", reservation.UserId);
        return View(reservation);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ReservationId,UserId,BookId,ReservedDate,ReturnDate,StatusId")] Reservation reservation)
    {
        if (id != reservation.ReservationId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(reservation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(reservation.ReservationId))
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
        ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", reservation.BookId);
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Username", reservation.UserId);
        return View(reservation);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var reservation = await _context.Reservations
            .Include(r => r.Book)
            .Include(r => r.User)
            .FirstOrDefaultAsync(m => m.ReservationId == id);
        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);
        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ReservationExists(int id)
    {
        return _context.Reservations.Any(e => e.ReservationId == id);
    }
}
