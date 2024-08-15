using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Web.Models;

public class HomeViewModel
{
    public decimal CurrentWeeklyTransaction { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}