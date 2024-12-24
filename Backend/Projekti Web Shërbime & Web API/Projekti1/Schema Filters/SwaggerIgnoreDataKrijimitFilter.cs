using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Projekti1.Receta.DTOs;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Projekti1.Schema
{
    public class SwaggerIgnoreDataKrijimitFilter : Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(DietaCreateDto) || context.Type == typeof(DietaUpdateDto))
            {
                // Remove the DataKrijimit property from the schema
                schema.Properties.Remove("DataKrijimit");
            }




            if (context.Type == typeof(UshqimiCreateDto) || context.Type == typeof(UshqimiUpdateDto))
            {
                // Remove the DataKrijimit property from the schema
                schema.Properties.Remove("DataKrijimit");
            }



            if (context.Type == typeof(RecetaCreateDto) || context.Type == typeof(RecetaUpdateDto))
            {
                // Remove the DataKrijimit property from the schema
                schema.Properties.Remove("DataKrijimit");
            }
        }


    }

}