namespace UM.REST.Infrastructure.Middleware
{
    public static class UrlResolverPipelineExtenstion
    {
        public static IApplicationBuilder UseUrlResolverPipeline(
            this IApplicationBuilder app)
        {
            app.UseMiddleware<UrlResolverPipeline>();
            return app;
        }
    }
}