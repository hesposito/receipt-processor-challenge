using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ReceiptProcessorChallenge.Filters;

public class ProcessReceiptFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses ??= [];

        operation.Responses.Add("200", new OpenApiResponse
        {
            Description = "Returns the ID assigned to the receipt",
            Content = new Dictionary<string, OpenApiMediaType>()
            {
                { 
                    "application/json", 
                    new() 
                    { 
                        Schema = new() 
                        { 
                            Type = "object", 
                            Required = new HashSet<string>() {"id"}, 
                            Properties = new Dictionary<string, OpenApiSchema>() 
                            { 
                                {
                                    "id", 
                                    new() 
                                    { 
                                        Type = "string", 
                                        Pattern = "^\\S+$", 
                                        Example = new OpenApiString("adb6b560-0eef-42bc-9d16-df48f30e89b2")
                                    } 
                                }
                            } 
                        }
                    }
                }
            }
        });
    }
}