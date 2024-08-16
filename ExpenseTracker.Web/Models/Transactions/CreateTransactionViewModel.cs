using System.ComponentModel.DataAnnotations;
using System.Net;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Data;
using Newtonsoft.Json;

namespace ExpenseTracker.Web.Models;

public class CreateTransactionViewModel
{
    [Required]
    public decimal Amount { get; set; }
    
    [Required]
    [Display(Name = "Debit or Credit")]
    public TransactionType TransactionType{ get; set; }

    [Required]
    [Display(Name = "Transaction Category")]
    public Guid TransactionCategoryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<TransactionCategory> Categories { get; set; } = new List<TransactionCategory>();
    [Display(Name = "Notes")] 
    public string Description { get; set; } = string.Empty;
}