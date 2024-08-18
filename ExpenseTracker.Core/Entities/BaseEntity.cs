using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Core.Entities;

public abstract class BaseEntity
{
    [Required] 
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;    
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}