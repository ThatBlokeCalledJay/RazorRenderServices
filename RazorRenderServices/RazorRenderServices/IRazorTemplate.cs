namespace RazorRenderServices
{
    public interface IRazorTemplate
    {
        /// <summary>
        /// Razor template content
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// Razor template key
        /// </summary>
        string Key { get; }
    }
}