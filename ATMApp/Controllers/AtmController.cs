using ATMApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult AtmIndex()
        {
            List<AtmDataModel> lst = _context.AtmData.ToList();
            return View("AtmIndex", lst);
        }

        [ActionName("Create")]
        public IActionResult AtmCreate()
        {
            return View("AtmCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> CardSave(AtmDataModel reqModel)
        {
            await _context.AtmData.AddAsync(reqModel);
            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? "Saving Successful" : "Saving Failed";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;

            return Redirect("/atm");
        }

        [ActionName("Withdrawl")]
        public IActionResult AtmWithdrawl()
        {
            return View("AtmWithdrawl");
        }

        [HttpPost]
        [ActionName("Withdrawl")]
        public async Task<IActionResult> AtmWithdrawl(AtmDataModel reqModel)
        {
            var atm = await _context.AtmData.FirstOrDefaultAsync(a => a.CardId == reqModel.CardId);
            if (atm is null || reqModel.Balance <= 0 || reqModel.Balance > atm.Balance)
            {
                TempData["Message"] = "Fail Withdrawl. Invalid resource and insufficient balance";
                TempData["IsSuccess"] = false;
            }
            return View("AtmWithdrawl");

            atm.Balance -= reqModel.Balance;
            _context.AtmData.Update(atm);

            var result = await _context.SaveChangesAsync();

            var message = result > 0 ? $"Successfully withdrawl. Remain balance: {atm.Balance}" : "Failed Withdrawl";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;
            return Redirect("/home");
        }

        [ActionName("Deposit")]
        public IActionResult AtmDeposit()
        {
            return View("AtmDeposit");
        }

        [HttpPost]
        [ActionName("Deposit")]
        public async Task<IActionResult> AtmDeposit(AtmDataModel reqModel)
        {
            var atm = await _context.AtmData.FirstOrDefaultAsync(a => a.CardNum == reqModel.CardNum && a.CardPin == reqModel.CardPin);
            if (atm is null)
            {
                TempData["Message"] = "Incorrect CardNumber or CardPin";
                TempData["IsSuccess"] = false;
                return View("AtmDeposit");
            }

            atm.Balance += reqModel.Balance;
            _context.AtmData.Update(atm);
            var result = await _context.SaveChangesAsync();
            var message = result > 0 ? $"Successfully Deposit. Current Balance: {atm.Balance}" : "Failed Deposit";
            TempData["Message"] = message;
            TempData["IsSuccess"] = result > 0;
            return Redirect("/home");
        }

        [ActionName("Check")]
        public IActionResult AtmCheck()
        {
            return View("AtmCheck");
        }

        [HttpGet]
        [ActionName("Check")]
        public async Task<IActionResult> AtmCheck(AtmDataModel reqModel)
        {
            var atm = await _context.AtmData.FirstOrDefaultAsync(a => a.CardNum == reqModel.CardNum && a.CardPin == reqModel.CardPin);
            if(atm is null)
            {
                return View("AtmResult", atm);
            }
            return View("AtmCheck");
        }
    }
}
