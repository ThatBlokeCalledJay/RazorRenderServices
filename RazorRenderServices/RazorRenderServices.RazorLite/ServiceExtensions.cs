using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace RazorRenderServices.RazorLite
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRazorLightRenderService(this IServiceCollection services)
        {
            return services.AddSingleton<IRazorRenderService, RazorLightRenderService>(context =>
                new RazorLightRenderService(context.GetRequiredService<IOptions<RazorLightRenderServiceOptions>>().Value));
        }

        public static IServiceCollection AddRazorLightRenderService(this IServiceCollection services, Action<RazorLightRenderServiceOptions> options)
        {
            return services.AddRazorLightRenderService().Configure(options);
        }
    }
}