using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Web.Interfaces;

public interface ITransactionService
{
    Task<List<Transaction>> GetTransactionsByUserIdAsync(string userId, int days = 7);
    decimal SumTransactions(List<Transaction> transactions);

}