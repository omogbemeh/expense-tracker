using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Data;

namespace ExpenseTracker.Core.Entities;

public class Transaction : BaseEntity
{
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public TransactionType TransactionType { get; set; }
   
    public string CreatedBy { get; set; }
    [ForeignKey(nameof(CreatedBy))]
    public ApplicationUser ApplicationUser { get; set; }
   
    public Guid TransactionCategoryId { get; set; }
    public TransactionCategory TransactionCategory { get; set; }
    
}