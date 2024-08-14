using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<Transaction> Transactions = new List<Transaction>();
}