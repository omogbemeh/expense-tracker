using System.Diagnostics;
using ExpenseTracker.Core.Entities;
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
    private readonly TransactionService _transactionService;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ILogger<HomeController> logger, TransactionService transactionService, UserManager<ApplicationUser> userManager)
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
            
            var transactions = await _transactionService.GetTransactionsByUserIdAsync(user.Id);
            return View(transactions);
        }
        catch (Exception e)
        {   _logger.LogError(e, "An error occured while retrieving transactions");
            TempData["ErrorMessage"] = "Something went wrong while retrieving transactions";
            return View(new List<Transaction>());
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