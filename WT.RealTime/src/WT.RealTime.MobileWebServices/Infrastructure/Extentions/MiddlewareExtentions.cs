using Microsoft.AspNetCore.Builder;
using WT.RealTime.MobileWebServices.Infrastructure.Middlewares;

namespace WT.RealTime.MobileWebServices.Infrastructure.Extentions
{
    public static class MiddlewareExtentions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
