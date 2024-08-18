using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Web.Models.TransactionCategory;

public class TransactionCategoryCreateViewModel
{
    [Required(ErrorMessage = "Name cannot be empty")]
    [StringLength(100, ErrorMessage = "Length cannot be more than 100 characters")]
    public string Name { get; set; } = string.Empty;

    public string Emoji { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}