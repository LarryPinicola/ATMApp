using Microsoft.AspNetCore.Mvc;

namespace ATMApp.Controllers
{
    public class AtmController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
