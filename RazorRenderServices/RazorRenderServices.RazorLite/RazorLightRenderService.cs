using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using NJsonSchema.Validation;
using RazorLight;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RazorRenderServices.RazorLite
{
    /// <summary>
    /// Razor rendering and schema validation services that makes use of RazorLightEngine and NJsonSchema
    /// </summary>
    public class RazorLightRenderService : IRazorRenderService
    {
        private readonly RazorLightEngine _razorEngine;
        private readonly JsonSchemaValidator _validator;

        public RazorLightRenderService(RazorLightRenderServiceOptions options)
        {
            _razorEngine = new RazorLightEngineBuilder()
                .SetOperatingAssembly(Assembly.GetExecutingAssembly())
                .UseEmbeddedResourcesProject(options.UseEmbeddedResourcesProject)
                .UseMemoryCachingProvider()
                .Build();

            _validator = new JsonSchemaValidator();
        }

        /// <summary>
        /// Render the given <paramref name="template"/> and <paramref name="templateModel"/> using <see cref="RazorLightEngine"/>.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="templateModel"></param>
        /// <returns></returns>
        public Task<string> RenderTemplateAsync(IRazorTemplate template, object templateModel)
        {
            var properties = templateModel;

            if (templateModel is JObject jObject)
                properties = jObject.ToObject<ExpandoObject>();

            return _razorEngine.CompileRenderStringAsync(template.Key, template.Content, properties);
        }

        /// <summary>
        /// Validate the given <paramref name="templateModel"/> against the provided <paramref name="jsonSchema"/> using <see cref="JsonSchemaValidator"/> by NJson
        /// </summary>
        /// <param name="templateModel"></param>
        /// <param name="jsonSchema"></param>
        /// <returns></returns>
        public async Task<RazorValidationResult> ValidateTemplateModelAsync(object templateModel, string jsonSchema)
        {
            var schema = await JsonSchema.FromJsonAsync(jsonSchema);

            return ValidateTemplateModel(templateModel, schema);
        }

        private RazorValidationResult ValidateTemplateModel(object templateModel, JsonSchema jsonSchema)
        {
            var jsonProperties = JsonConvert.SerializeObject(templateModel ?? new object());

            var errors = _validator.Validate(jsonProperties, jsonSchema);

            var results = new RazorValidationResult();

            if (!errors.Any())
                return results;

            foreach (var validationError in errors)
            {
                results.AddError(validationError.Property, validationError.ToString());
            }

            return results;
        }
    }
}