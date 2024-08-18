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
    public async Task<IActionResult> Create(TransactionCategoryCreateViewModel transactionCategoryCreateViewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await this._transactionCategoryService.CreateAsync(transactionCategoryCreateViewModel);
                TempData["Success"] = $"${{Successfully created new Category: {transactionCategoryCreateViewModel.Name}}}";
                return View(new TransactionCategoryCreateViewModel());
            }

            return View(transactionCategoryCreateViewModel);
        }
        catch (Exception e)
        {
            TempData["Error"] = $"${{Something went wrong created new Category: {transactionCategoryCreateViewModel.Name}}}";
            return View();
        }
        
    }
}