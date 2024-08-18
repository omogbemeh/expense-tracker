using ExpenseTracker.Web.Models;
using ExpenseTracker.Web.Models.TransactionCategory;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers;

public class TransactionCategoryController : Controller
{
    private readonly ITransactionCategoryService _transactionCategoryService;

    public TransactionCategoryController(ITransactionCategoryService transactionCategoryService)
    {
        _transactionCategoryService = transactionCategoryService;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionCategoryViewModel createTransactionCategoryViewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await this._transactionCategoryService.Create(createTransactionCategoryViewModel);
                TempData["Success"] = $"${{Successfully created new Category: {createTransactionCategoryViewModel.Name}}}";
                return View(new CreateTransactionCategoryViewModel());
            }

            return View(createTransactionCategoryViewModel);
        }
        catch (Exception e)
        {
            TempData["Error"] = $"${{Something went wrong created new Category: {createTransactionCategoryViewModel.Name}}}";
            return View();
        }
        
    }
}