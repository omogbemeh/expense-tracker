using ExpenseTracker.Core.Entities;
using ExpenseTracker.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext _context)
    {
        this._context = _context;
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        try
        {
            return await _context.Categories.ToListAsync();
        }
        catch (Exception e)
        {
            return new List<Category>();
            throw;
        }
    }
}