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

    [Display(Name = "Date")]
    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public List<Core.Entities.TransactionCategory> Categories { get; set; } = new List<Core.Entities.TransactionCategory>();
    [Display(Name = "Notes")] 
    public string Description { get; set; } = string.Empty;
}