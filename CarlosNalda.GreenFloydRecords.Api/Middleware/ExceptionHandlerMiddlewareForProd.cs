using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace CarlosNalda.GreenFloydRecords.Api.Middleware
{
    public class ExceptionHandlerMiddlewareForProd
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddlewareForProd(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.ValdationErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case NotFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case Exception:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (httpStatusCode != HttpStatusCode.InternalServerError && result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
