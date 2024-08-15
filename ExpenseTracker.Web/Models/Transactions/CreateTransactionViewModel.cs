using System.ComponentModel.DataAnnotations;
using System.Net;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Web.Models;

public class CreateTransactionViewModel
{
    [Required]
    public decimal Amount { get; set; }
    public TransportType TransportType { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Category> Categories { get; set; } = new List<Category>();
    [Display(Name = "Notes")] 
    public string Description { get; set; } = string.Empty;
}