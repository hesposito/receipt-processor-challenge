using Microsoft.EntityFrameworkCore;
using ReceiptProcessorChallenge.DTOs;
using ReceiptProcessorChallenge.Entity;
using ReceiptProcessorChallenge.Entity.Models;

namespace ReceiptProcessorChallenge.Services;

public class ReceiptService(ReceiptContext context, ILogger<ReceiptService> logger) : IReceiptService
{
    private readonly ReceiptContext _context = context;
    private readonly ILogger<ReceiptService> _logger = logger;

    public async Task<Response<Guid>> CreateReceipt(ReceiptDto receiptDto)
    {
        var receipt = Mapper.DTOToReceiptEntity(receiptDto);
        receipt.Points = CalculatePoints(receipt);

        try
        {
            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();

            return new() { Item = receipt.Id, StatusCode = StatusCodes.Status200OK };
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing receipt.");
            return new() 
            { 
                Message = $"An error occurred while processing receipt, details: {ex.Message}", 
                StatusCode = StatusCodes.Status400BadRequest 
            };
        }
    }

    public async Task<Response<long>> GetPointsByReceiptId(Guid receiptId)
    {
        var receipt = await _context.Receipts.FirstOrDefaultAsync(x => x.Id.Equals(receiptId));
        var points = receipt?.Points ?? -1;

        if (points == -1)
            return new() { Message = "No receipt found for that id", StatusCode = StatusCodes.Status404NotFound };

        return new() { Item = points, StatusCode = StatusCodes.Status200OK };
    }

    private static long CalculatePoints(Receipt receipt)
    {
        var points = 0M;

        // One point for every alphanumeric character in the retailer name
        points += receipt.Retailer.Count(x => char.IsLetterOrDigit(x));

        // 50 points if the total is a round dollar amount with no cents
        points += decimal.IsInteger(receipt.Total) ? 50 : 0;

        // 25 points if the total is a multiple of 0.25
        points += receipt.Total % 0.25M == 0 ? 25 : 0;

        // 5 points for every two items on the receipt
        points += Math.Floor(receipt.Items.Count() / 2M) * 5;

        // price * 0.2 points for every item w/ a trimmed
        // description length that is a multiple of 3
        points += receipt.Items.Select(x => 
            x.ShortDescription.Trim().Length % 3 == 0 ? 
                Math.Ceiling(x.Price * 0.2M) : 0)
            .Aggregate((x, y) => x + y);

        // 6 points if the day in the purchase date is odd
        points += receipt.PurchaseDate.Day % 2 == 1 ? 6 : 0;

        // 10 points if the time of purchase is after 2:00pm and before 4:00pm
        points += receipt.PurchaseTime.IsBetween(new TimeOnly(14, 00), new TimeOnly(16, 00)) ? 10 : 0;

        return decimal.ToInt64(points);
    }
}