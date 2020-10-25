using System.Threading.Tasks;

namespace RazorRenderServices
{
    public interface IRazorRenderService
    {
        /// <summary>
        /// Render the given <paramref name="template"/> using the provided <paramref name="templateModel"/>.
        /// </summary>
        /// <param name="template"></param>
        /// <param name="templateModel"></param>
        /// <returns></returns>
        Task<string> RenderTemplateAsync(IRazorTemplate template, object templateModel);

        /// <summary>
        /// Validate the given <paramref name="templateModel"/> using the provided <paramref name="jsonSchema"/>.
        /// </summary>
        /// <param name="templateModel"></param>
        /// <param name="jsonSchema"></param>
        /// <returns></returns>
        Task<RazorValidationResult> ValidateTemplateModelAsync(object templateModel, string jsonSchema);
    }
}