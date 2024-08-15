using ExpenseTracker.Core.Entities;
using ExpenseTracker.Web.Models;
using ExpenseTracker.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers;

public class TransactionsController: Controller
{
    private ICategoryService _categoryService;
    private readonly ILogger<TransactionsController> _logger;

    public TransactionsController(ICategoryService categoryService, ILogger<TransactionsController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public async Task<IActionResult> Create()
    {
        try
        {
            List<TransactionCategory> categories = await _categoryService.GetCategoriesAsync();
            CreateTransactionViewModel createTransactionViewModel = new CreateTransactionViewModel
            {
                Categories = categories,
            };
            return View(createTransactionViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong fetching Categories");
            return View(new CreateTransactionViewModel { Categories = new List<TransactionCategory>() });
        }
    }
}