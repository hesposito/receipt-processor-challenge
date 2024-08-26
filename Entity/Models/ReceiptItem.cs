namespace ReceiptProcessorChallenge.Entity.Models;

public class ReceiptItem
{
    public Guid Id { get; set; }
    public string ShortDescription { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Receipt Receipt { get; set; } = new();
}