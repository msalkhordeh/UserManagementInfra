using UM.DataAccess.DataContext;
using UM.DataAccess.Entity.Log;

namespace UM.REST.Infrastructure.Middleware
{
    public class UrlResolverPipeline
    {
        private readonly RequestDelegate _next;

        public UrlResolverPipeline(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context
            , EfCoreContext dbContext)
        {
            try
            {
                var now = DateTime.Now;
                var urlResolver = new UrlResolver
                {
                    Ip = context.Connection.RemoteIpAddress?.ToString()
                        ?? "::1",
                    Host = context.Request.Scheme + "://" + context.Request.Host,
                    Path = context.Request.Path,
                    Verb = context.Request.Method,
                    UserAgent = context.Request.Headers["User-Agent"],
                    ContentType = context.Request.ContentType ?? "UNKWON"
                };

                context.Request.EnableBuffering();
                urlResolver.BodyContent =
                    await new StreamReader(
                        context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                await _next.Invoke(context);

                urlResolver.ExecuteTime = DateTime.Now.Subtract(now).Milliseconds;
                urlResolver.StatusCode = context.Response.StatusCode;
                await dbContext.AddAsync(urlResolver);
                await dbContext.SaveChangesAsync();
            }
            catch
            {
                await _next.Invoke(context);
            }
        }
    }
}