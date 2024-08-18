using System.Diagnostics;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Web.Models;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITransactionService _transactionService;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(
        ITransactionService transactionService, 
        ILogger<HomeController> logger, 
        UserManager<ApplicationUser> userManager, 
        ICategoryService categoryService)
    {
        _logger = logger;
        _transactionService = transactionService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            List<Transaction> transactions = await _transactionService.GetTransactionsByUserIdAsync(user.Id);
            Console.WriteLine("??****** {0}", transactions.Count);
            decimal sumOfTransactionsForTheWeek = _transactionService.SumTransactions(transactions);

            return View(new HomeViewModel
            { 
                CurrentWeeklyTransaction = sumOfTransactionsForTheWeek,
                Transactions = transactions.Count > 0 ? transactions : new List<Transaction>()
            });
        }
        catch (Exception e)
        {   
            _logger.LogError(e, "An error occured while retrieving transactions");
            TempData["ErrorMessage"] = "Something went wrong while retrieving transactions";
            return View(new HomeViewModel
            {
                CurrentWeeklyTransaction = 0m,
                Transactions = new List<Transaction>()
            });
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}