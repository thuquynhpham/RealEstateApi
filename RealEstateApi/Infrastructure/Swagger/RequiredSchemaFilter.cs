using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RealEstate.Api.Infrastructure.Swagger
{
    public class RequiredSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null) 
                return;

            var notNullableProps = schema.Properties.Where(x => ! x.Value.Nullable && !schema.Required.Contains(x.Key));

            foreach ( var notNullableProp in notNullableProps)
            {
                schema.Required.Add(notNullableProp.Key);
            }
        }
    }
}
