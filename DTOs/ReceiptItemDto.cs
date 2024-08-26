using System.ComponentModel.DataAnnotations;

namespace ReceiptProcessorChallenge.DTOs;

public class ReceiptItemDto
{
    /// <summary>
    /// The Short Product Description for the item.
    /// </summary>
    /// <example>Mountain Dew 12PK</example>
    [Required]
    [RegularExpression(@"^[\w\s\-]+$")]
    public string ShortDescription { get; set; } = string.Empty;
    /// <summary>
    /// The total price payed for this item.
    /// </summary>
    /// <example>6.49</example>
    [Required]
    [RegularExpression(@"^\d+\.\d{2}$")]
    public string Price { get; set; } = string.Empty;
}