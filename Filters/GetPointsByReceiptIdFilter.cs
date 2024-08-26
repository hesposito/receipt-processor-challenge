using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ReceiptProcessorChallenge.Filters;

public class GetPointsByReceiptIdFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses ??= [];

        operation.Responses.Add("200", new OpenApiResponse
        {
            Description = "The number of points awarded",
            Content = new Dictionary<string, OpenApiMediaType>()
            {
                { 
                    "application/json", 
                    new() 
                    { 
                        Schema = new() 
                        { 
                            Type = "object", 
                            Properties = new Dictionary<string, OpenApiSchema>() 
                            { 
                                {
                                    "points", 
                                    new() 
                                    { 
                                        Type = "integer", 
                                        Format = "int64",
                                        Example = new OpenApiInteger(100)
                                    } 
                                }
                            } 
                        }
                    }
                }
            }
        });

        if (operation.Parameters.Any(x => x.Name.Equals("id")))
        {
            operation.Parameters.Where(x => x.Name.Equals("id")).FirstOrDefault()!.Schema = new()
            {
                Type = "string",
                Pattern = "^\\S+$"
            };
        }
    }
}