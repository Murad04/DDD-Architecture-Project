using Application.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Nodes;

namespace Web.Common
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExcepitonAsync(context, ex);
            }
        }

        public Task HandleExcepitonAsync(HttpContext context,Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch(ex)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result=JsonConvert.SerializeObject(validationException.Failures);
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            
            if(string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new { error = ex.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
