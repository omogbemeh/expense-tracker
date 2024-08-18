using ExpenseTracker.Core.Entities;
using ExpenseTracker.Web.Data;
using ExpenseTracker.Web.Models;
using ExpenseTracker.Web.Models.TransactionCategory;

namespace ExpenseTracker.Web.Services;

public class TransactionCategoryService : ITransactionCategoryService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TransactionCategoryService> _logger;

    public TransactionCategoryService(ApplicationDbContext context, ILogger<TransactionCategoryService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task Create(CreateTransactionCategoryViewModel createTransactionCategoryViewModel)
    {
        try
        {
            TransactionCategory transactionCategory = new TransactionCategory
            {
                Name = createTransactionCategoryViewModel.Name,
                Emoji = createTransactionCategoryViewModel.Emoji,
                Description = createTransactionCategoryViewModel.Description
            };
            await _context.TransactionCategories.AddAsync(transactionCategory);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Something went wrong while creating a transaction service");
            throw;
        }
        
    }
}