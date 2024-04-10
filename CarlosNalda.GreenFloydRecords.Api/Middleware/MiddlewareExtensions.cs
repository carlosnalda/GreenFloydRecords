namespace CarlosNalda.GreenFloydRecords.Api.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandlerForDev(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddlewareForDev>();
        }

        public static IApplicationBuilder UseCustomExceptionHandlerForProd(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddlewareForProd>();
        }
    }
}
