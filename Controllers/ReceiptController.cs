using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using ReceiptProcessorChallenge.DTOs;
using ReceiptProcessorChallenge.Filters;
using ReceiptProcessorChallenge.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace ReceiptProcessorChallenge.Controllers;

[ApiController]
[Route("receipts")]
public partial class ReceiptsController(IReceiptService receiptService) : ControllerBase
{
    private readonly IReceiptService _receiptService = receiptService;

    [HttpPost("process")]
    [Consumes("application/json")]
    [SwaggerOperation(
        Summary = "Submits a receipt for processing",
        Description = "Submits a receipt for processing"
    )]
    [SwaggerResponse(400, "The receipt is invalid")]
    [SwaggerOperationFilter(typeof(ProcessReceiptFilter))]
    public async Task<IActionResult> ProcessReceipt([FromBody, SwaggerRequestBody(Required = true)] ReceiptDto receipt)
    {
        if (!ModelState.IsValid)
            return BadRequest("The receipt is invalid");

        var response = await _receiptService.CreateReceipt(receipt);

        if (!response.StatusCode.Equals(StatusCodes.Status200OK))
            return BadRequest(response.Message);

        return Ok(
        new
        {
            Id = response.Item.ToString()
        });
    }

    [HttpGet("{id}/points")]
    [SwaggerOperation(
        Summary = "Returns the points awarded for the receipt",
        Description = "Returns the points awarded for the receipt"
    )]
    [SwaggerResponse(404, "No receipt found for that id")]
    [SwaggerOperationFilter(typeof(GetPointsByReceiptIdFilter))]
    public async Task<IActionResult> GetPointsByReceiptId([FromRoute, SwaggerParameter("The ID of the receipt", Required = true)] Guid id)
    {
        if (!IdRegex().IsMatch(id.ToString()))
            return NotFound("No receipt found for that id");

        var response = await _receiptService.GetPointsByReceiptId(id);

        if (!response.StatusCode.Equals(StatusCodes.Status200OK))
            return NotFound(response.Message);

        return Ok(
        new
        {
            points = response.Item
        });
    }

    [GeneratedRegex("^\\S+$")]
    public static partial Regex IdRegex();
}
