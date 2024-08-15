using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Web.Services;

public interface ICategoryService
{
    Task<List<TransactionCategory>> GetCategoriesAsync();
}