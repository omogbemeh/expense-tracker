namespace ExpenseTracker.Core.Entities;

public class TransactionCategory : BaseEntity
{
    public TransactionCategory()
    {
        DisplayName = this.Name;
    }

    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? Emoji { get; set; }
}