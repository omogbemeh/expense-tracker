using ExpenseTracker.Core.Entities;
using ExpenseTracker.Web.Interfaces;
using ExpenseTracker.Web.Models;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers;

public class TransactionsController: Controller
{
    private ICategoryService _categoryService;
    private readonly ITransactionService _transactionService;
    private readonly ILogger<TransactionsController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public TransactionsController(
        ICategoryService categoryService, 
        ITransactionService transactionService, 
        ILogger<TransactionsController> logger, 
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _categoryService = categoryService;
        _transactionService = transactionService;
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> CreateAsync()
    {
        try
        {
            List<TransactionCategory> categories = await _categoryService.GetCategoriesAsync();
            TransactionCreateViewModel transactionCreateViewModel = new TransactionCreateViewModel
            {
                Categories = categories,
            };
            return View(transactionCreateViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong fetching Categories");
            return View(new TransactionCreateViewModel { Categories = new List<TransactionCategory>() });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(TransactionCreateViewModel transactionCreateViewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                string userId = _signInManager.UserManager.GetUserId(User);

                if (!string.IsNullOrEmpty(userId))
                {
                    await _transactionService.CreateAsync(userId, transactionCreateViewModel);
                    TempData["Success"] = "Successfully created your transaction";
                    return RedirectToAction("Index", "Home");
                }
                
            }
            transactionCreateViewModel.Categories = await _categoryService.GetCategoriesAsync();
            return View(transactionCreateViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong fetching Categories");
            return View(new TransactionCreateViewModel { Categories = new List<TransactionCategory>() });
        }
    }
}