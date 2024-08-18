using ExpenseTracker.Web.Models.TransactionCategory;

namespace ExpenseTracker.Web.Services;

public interface ITransactionCategoryService
{
    Task Create(CreateTransactionCategoryViewModel createTransactionCategoryViewModel);
}