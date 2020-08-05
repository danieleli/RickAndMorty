namespace RickAndMorty.Configuration
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using RickAndMorty.Utils;

    public static class ExceptionHandlerExtensions
    {
        public static void SetCustomExceptionHandler(this IApplicationBuilder application, bool isDevelopment)
        {
            application.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception                   = exceptionHandlerPathFeature.Error;

                    var result = new ExceptionResponse
                    {
                        StatusCode = context.Response.StatusCode,
                        Message    = exception.Message
                    };

                    if (isDevelopment)
                    {
                        result.StackTrace = exception.StackTrace;
                    }

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result.ToJson());
                });
            });
        }

        public class ExceptionResponse
        {
            public int StatusCode { get; set; }

            public string? Message { get; set; }

            public string? StackTrace { get; set; }
        }
    }
}
