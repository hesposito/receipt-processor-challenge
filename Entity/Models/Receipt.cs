using System.ComponentModel.DataAnnotations;

namespace ReceiptProcessorChallenge.Entity.Models;

public class Receipt
{
    /// <example>
    /// adb6b560-0eef-42bc-9d16-df48f30e89b2
    /// </example>
    [Required]
    [DataType(DataType.Text)]
    [RegularExpression("^\\S+$")]
    public Guid Id { get; set; }
    public string Retailer { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public TimeOnly PurchaseTime { get; set; }
    public decimal Total { get; set; }
    public long Points { get; set; }

    public IEnumerable<ReceiptItem> Items { get; set; } = [];
}