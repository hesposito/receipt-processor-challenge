using ReceiptProcessorChallenge.DTOs;

namespace ReceiptProcessorChallenge.Services;

public interface IReceiptService
{
    Task<Response<Guid>> CreateReceipt(ReceiptDto receipt);
    Task<Response<long>> GetPointsByReceiptId(Guid receiptId);
}