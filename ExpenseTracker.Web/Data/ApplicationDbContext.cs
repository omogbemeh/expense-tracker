using ExpenseTracker.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Web.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionCategory> TransactionCategories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<ApplicationUser>()
            .HasIndex(u => u.UserName)
            .IsUnique();
        
        builder.Entity<Transaction>()
            .Property(t => t.Amount)
            .HasPrecision(18, 2);

        builder.Entity<Transaction>()
            .HasOne<ApplicationUser>(t => t.ApplicationUser)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.CreatedBy)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Transaction>()
            .HasOne<TransactionCategory>(t => t.TransactionCategory)
            .WithMany(c => c.Transactions)
            .HasForeignKey(t => t.TransactionCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}