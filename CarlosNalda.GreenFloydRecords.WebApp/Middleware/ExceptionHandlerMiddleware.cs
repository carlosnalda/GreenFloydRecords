using CarlosNalda.GreenFloydRecords.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace CarlosNalda.GreenFloydRecords.WebApp.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
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
            switch (exception)
            {
                case ValidationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case Exception:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                context.Response.Redirect("/InvalidAction/PageNotFound");
                return Task.CompletedTask;
            }

            context.Response.Redirect("/InvalidAction/Error");
            return Task.CompletedTask;
        }

        //private Task ConvertException(HttpContext context, Exception exception)
        //{
        //    // context.TraceIdentifier = exception.Message;
        //    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //    context.Response.ContentType = "text/html; charset=utf-8";
        //    //context.Response.Headers.CacheControl.Append("no-cache,no-store");

        //    return Task.CompletedTask;
        //}
    }
}
