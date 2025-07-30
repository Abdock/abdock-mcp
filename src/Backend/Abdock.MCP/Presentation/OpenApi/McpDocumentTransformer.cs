using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;

namespace Presentation.OpenApi;

public class McpDocumentTransformer : IOpenApiDocumentTransformer
{
    private readonly IHttpContextAccessor _accessor;

    public McpDocumentTransformer(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info = new OpenApiInfo
        {
            Title = "MCP get weather data",
            Version = "1.0.0",
            Description = "A simple MCP server to get weather data."
        };
        document.Servers =
        [
            new OpenApiServer
            {
                Url = _accessor.HttpContext != null
                    ? $"{_accessor.HttpContext.Request.Scheme}://{_accessor.HttpContext.Request.Host}/"
                    : "http://localhost:8080/"
            }
        ];
        var pathItem = new OpenApiPathItem();
        pathItem.AddOperation(OperationType.Post, new OpenApiOperation
        {
            Summary = "Weather data",
            Extensions = new Dictionary<string, IOpenApiExtension>
            {
                ["x-ms-agentic-protocol"] = new OpenApiString("mcp-streamable-1.0")
            },
            OperationId = "InvokeMCP",
            Responses = new OpenApiResponses
            {
                ["200"] = new OpenApiResponse
                {
                    Description = "Success",
                }
            }
        });

        document.Paths ??= [];
        document.Paths.Add("/mcp", pathItem);

        return Task.CompletedTask;
    }
}