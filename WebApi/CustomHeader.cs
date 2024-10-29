using CGMWPF.Back.WebAPI;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi
{
    public class CustomHeader : IOperationFilter
    {
        private readonly IConfiguration _configuration;
        public CustomHeader(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Retrieve the existing parameters (if any)
            var existingParameters = operation.Parameters;
            var keyvalue = _configuration.GetValue<string>("APIToken");
            // Create the default header parameter
            var headerParameter = new OpenApiParameter
            {
                Name = "ApiToken",
                In = ParameterLocation.Header,
                Description = "Custom Header",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString(keyvalue)
                }
            };

            // Add the default header parameter to the existing parameters
            operation.Parameters = existingParameters ?? new List<OpenApiParameter>();
            operation.Parameters.Add(headerParameter);
        }
    }
}
