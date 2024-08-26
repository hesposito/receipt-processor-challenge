using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace ReceiptProcessorChallenge.DTOs;

[SwaggerSchema(Title = "Receipt")]
public class ReceiptDto
{
    /// <summary>
    /// The name of the retailer or store the receipt is from.
    /// </summary>
    /// <example>M&amp;M Corner Market</example>
    [Required]
    [RegularExpression(@"^[\w\s\-&]+$")]
    public required string Retailer { get; set; }
    /// <summary>
    /// The date of the purchase printed on the receipt.
    /// </summary>
    /// <example>2022-01-01</example>
    [Required]
    [DataType(DataType.Date)]
    public required string PurchaseDate { get; set; }
    /// <summary>
    /// The time of the purchase printed on the receipt. 24-hour time expected.
    /// </summary>
    /// <example>13:01</example>   
    [Required]
    [DataType(DataType.Time)]
    public required string PurchaseTime { get; set; }
    /// <summary>
    /// The total amount paid on the receipt.
    /// </summary>
    /// <example>6.49</example>
    [Required]
    [RegularExpression(@"^\d+\.\d{2}$")]
    public required string Total { get; set; }

    [Required]
    [MinLength(1)]
    public required IEnumerable<ReceiptItemDto> Items { get; set; } = [];
}