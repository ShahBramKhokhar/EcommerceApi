using WebRexErpAPI.Services.Hangfire.QBHangfire;

namespace WebRexErpAPI.Services
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<QBJobService>();
        }
    }
}
