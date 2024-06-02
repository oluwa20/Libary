using Libary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Libary.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
          
            return View();
        }



        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Username,Password,Mobile,Email,RoleId")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(manager);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(manager);
        }

        
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            var manager = _context.Manager.SingleOrDefault(m => m.Username == username && m.Password == password);
            if (manager != null)
            {
                
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }
    }
}
