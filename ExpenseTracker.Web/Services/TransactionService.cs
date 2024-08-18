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
    private readonly SignInManager<ApplicationUser> _signInManager;

    public TransactionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task CreateAsync(string userId, TransactionCreateViewModel transactionCreateViewModel)
    {
        try
        {
            Transaction transaction = new Transaction
            {
                Amount = transactionCreateViewModel.Amount,
                TransactionType = transactionCreateViewModel.TransactionType,
                TransactionCategoryId = transactionCreateViewModel.TransactionCategoryId,
                CreatedAt = transactionCreateViewModel.CreatedAt,
                CreatedBy = userId,
                Description = transactionCreateViewModel.Description ?? string.Empty,
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<List<Transaction>> GetTransactionsByUserIdAsync(string userId, int days = -7)
    {
        try
        {
            var currDayOfWeek = DateTime.Now.DayOfWeek;
            var startOfWeek = DateTime.Now.AddDays(- (int) currDayOfWeek).Date; // This will give you the start of the current week (Sunday)
            var transactions = await _context.Transactions
                .Where(t => t.CreatedBy == userId && t.CreatedAt >= startOfWeek)
                .ToListAsync();
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