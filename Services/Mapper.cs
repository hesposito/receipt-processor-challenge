using System.Globalization;
using ReceiptProcessorChallenge.DTOs;
using ReceiptProcessorChallenge.Entity.Models;

namespace ReceiptProcessorChallenge.Services;

public static class Mapper
{
    public static ReceiptDto ReceiptEntityToDTO(Receipt receipt) =>
        new()
        {
            Retailer = receipt.Retailer,
            PurchaseDate = receipt.PurchaseDate.ToString("yyyy-MM-dd"),
            PurchaseTime = receipt.PurchaseTime.ToString("HH:mm"),
            Total = receipt.Total.ToString(),
            Items = receipt.Items.Select(x => ItemEntityToDTO(x))
        };
    
    public static Receipt DTOToReceiptEntity(ReceiptDto receiptDto) =>
        new()
        {
            Retailer = receiptDto.Retailer,
            PurchaseDate = DateTime.ParseExact(receiptDto.PurchaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture),
            PurchaseTime = TimeOnly.ParseExact(receiptDto.PurchaseTime, "HH:mm", CultureInfo.InvariantCulture),
            Total = decimal.Parse(receiptDto.Total),
            Items = receiptDto.Items.Select(x => DTOToItemEntity(x)).ToList()
        };

    private static ReceiptItemDto ItemEntityToDTO(ReceiptItem receiptItem) =>
       new()
       {
           ShortDescription = receiptItem.ShortDescription,
           Price = receiptItem.Price.ToString()
       };
    
    private static ReceiptItem DTOToItemEntity(ReceiptItemDto itemDto) =>
        new()
        {
           ShortDescription = itemDto.ShortDescription,
           Price = decimal.Parse(itemDto.Price)
        };    
}