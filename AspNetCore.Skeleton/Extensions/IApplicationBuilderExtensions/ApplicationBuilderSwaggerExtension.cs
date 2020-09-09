using Microsoft.AspNetCore.Builder;

namespace AspNetCore.Skeleton.Extensions.IApplicationBuilderExtensions
{
    /// <summary>
    /// Swagger extension methodes for IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderSwaggerExtension
    {
        /// <summary>
        /// Do application builder swagger configuration
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            if (app is null)
            {
                throw new System.ArgumentNullException(nameof(app));
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
                c.RoutePrefix = "swagger";
            });
        }
    }
}
