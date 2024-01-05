using ATMApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ATMApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(AtmDataModel reqModel)
        {
            var atm = await _context.AtmData.FirstOrDefaultAsync(a => a.CardNum == reqModel.CardNum && a.CardPin == reqModel.CardPin);
            if (atm is null)
            {
                return RedirectToAction("Index");
            }
            HttpContext.Session.SetString("LoginData", JsonConvert.SerializeObject(reqModel));
            return Redirect("/");
        }
    }
}
