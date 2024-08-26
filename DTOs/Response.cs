namespace ReceiptProcessorChallenge.DTOs;

public class Response<T>
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public T? Item { get; set; }
}