namespace Erfa.PruductionManagement.Api.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {             
            builder.UseMiddleware<JwtHeaderMiddleware>();

            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
