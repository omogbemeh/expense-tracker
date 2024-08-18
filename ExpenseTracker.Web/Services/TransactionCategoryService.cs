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
    public async Task CreateAsync(TransactionCategoryCreateViewModel transactionCategoryCreateViewModel)
    {
        try
        {
            TransactionCategory transactionCategory = new TransactionCategory
            {
                Name = transactionCategoryCreateViewModel.Name,
                Emoji = transactionCategoryCreateViewModel.Emoji,
                Description = transactionCategoryCreateViewModel.Description
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