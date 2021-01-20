using Microsoft.AspNetCore.Http;

namespace WT.RealTime.MobileWebServices.Infrastructure.Http
{
    public  static class AbsoluteUrl
    {
        public static string FromRequest(string relativeUrl, HttpRequest request)
        {
            return relativeUrl.Replace("~/", $@"{request.Scheme}://{request.Host}{request.PathBase}/");
        }
    }
}
