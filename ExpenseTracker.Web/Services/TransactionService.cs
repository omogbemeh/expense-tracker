using ExpenseTracker.Core.Entities;
using ExpenseTracker.Data;
using ExpenseTracker.Web.Data;
using ExpenseTracker.Web.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Services;

public class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _context;

    public TransactionService(ApplicationDbContext _context)
    {
        this._context = _context;
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
        return transactions.Count <= 0 ? 0m : transactions.Sum(t => t.TransactionType == TransactionTypeEnum.Credit ? t.Amount : - t.Amount );
    }
}