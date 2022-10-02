using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UM.Sdk.Enum;
using UM.Sdk.Model;
using UM.ServiceProvider.InternalService.Authentication;

namespace UM.REST.Infrastructure.Middleware
{
    public class ApiAuthentication
    {
        private readonly RequestDelegate _next;

        public ApiAuthentication(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, 
            IJwtAuthenticationService authenticateService)
        {
            if (IsInExcludedUrls(context.Request) || context.Request.Path == "/")
            {
                await _next.Invoke(context);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(await authenticateService
                    .ValidateTokenAsync(context.Request.Headers["Security-Token"])))
                {
                    await _next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json; charset=utf-8";
                    var serializerSetting = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(
                        BaseResponseCollection.GetBaseResponse(RequestResult.InvalidJwt), serializerSetting));
                }
            }
        }



        private static readonly string[] ExcludedList = {
            "/api/identity/account/login",
            "/api/identity/account/register"
        };

        private static bool IsInExcludedUrls(HttpRequest request)
        {
            return ExcludedList.Any(path =>
                request.Path.ToString().StartsWith(path, StringComparison.OrdinalIgnoreCase));
        }
    }

    public static class ApiAuthenticationExtention
    {
        public static IApplicationBuilder UseApiAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiAuthentication>();
        }
    }
}
