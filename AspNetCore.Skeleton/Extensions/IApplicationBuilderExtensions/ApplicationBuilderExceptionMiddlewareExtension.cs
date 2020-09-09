using AspNetCore.Skeleton.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Skeleton.Extensions.IApplicationBuilderExtensions
{
    /// <summary>
    /// Exception Middleware extension for the application builder
    /// </summary>
    public static class ApplicationBuilderExceptionMiddlewareExtension
    {
        /// <summary>
        /// Name of the application writed in every error
        /// </summary>
        public static string AppName { get; private set; }
        /// <summary>
        /// ConfigureExceptionHandler
        /// </summary>
        /// <param name="app"></param>
        /// <param name="appName"></param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, string appName)
        {
            AppName = appName;
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    var problemDetails = new CustomProblemDetails();

                    switch (exception)
                    {
                        case BadHttpRequestException badHttpRequestException:
                            problemDetails.Title = "Invalid request";
                            problemDetails.Status = badHttpRequestException.StatusCode;
                            problemDetails.Detail = badHttpRequestException.Message;
                            break;
                        default:
                            problemDetails.Title = "An unexpected error occurred!";
                            problemDetails.Status = StatusCodes.Status500InternalServerError;
                            problemDetails.Detail = exception.Message;
                            break;
                    }

                    string jsonError =  problemDetails.ToJson();

                    context.Response.StatusCode = problemDetails.Status.Value;
                    context.Response.ContentType = "application/json";

                    //Log.Error(exception, jsonError);

                    await context.Response.WriteAsync(jsonError);
                });
            });
        }
    }
}
