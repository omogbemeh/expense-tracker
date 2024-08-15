using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers;

public class TransactionsController: Controller
{
    public IActionResult Create()
    {
        return View();
    }
}