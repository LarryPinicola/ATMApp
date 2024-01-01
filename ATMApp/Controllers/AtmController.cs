using ATMApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ATMApp.Controllers
{
    public class AtmController : Controller
    {
        private readonly AppDbContext _context;

        public AtmController(AppDbContext context)
        {
            _context = context;
        }

        [ActionName("Index")]
        public IActionResult CardIndex()
        {
            return View();
        }
    }
}
