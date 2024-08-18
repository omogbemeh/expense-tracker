using ExpenseTracker.Core.Entities;
using ExpenseTracker.Web.Models;

namespace ExpenseTracker.Web.Interfaces;

public interface ITransactionService
{
    Task CreateAsync(string userId, TransactionCreateViewModel transactionCreateViewModel);
    Task<List<Transaction>> GetTransactionsByUserIdAsync(string userId, int days = 7);
    decimal SumTransactions(List<Transaction> transactions);
}