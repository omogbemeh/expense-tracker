using System.Runtime.InteropServices.JavaScript;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Data;
using ExpenseTracker.Web.Data;
using ExpenseTracker.Web.Interfaces;
using ExpenseTracker.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Services;

public class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public TransactionService(ApplicationDbContext _context, UserManager<ApplicationUser> userManager)
    {
        this._context = _context;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task CreateTransaction(CreateTransactionViewModel createTransactionViewModel)
    {
        try
        {
            Transaction transaction = new Transaction
            {
                Amount = createTransactionViewModel.Amount,
                TransactionType = createTransactionViewModel.TransactionType,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Transaction>> GetTransactionsByUserIdAsync(string userId, int days = -7)
    {
        try
        {
            var currDayOfWeek = DateTime.UtcNow.DayOfWeek;
            var duration = DateTime.UtcNow.AddDays(- (int) currDayOfWeek);
            var transactions = await _context.Transactions.Where(t => t.CreatedBy == userId && t.CreatedAt >= duration).ToListAsync();
            return transactions;
        }
        catch (Exception e)
        {
            return new List<Transaction>();
        }
    }

    public decimal SumTransactions(List<Transaction> transactions)
    {
        return transactions.Count <= 0 ? 0m : transactions.Sum(t => t.TransactionType == TransactionType.Credit ? t.Amount : - t.Amount );
    }
}