using ApiRepository.Interfaces;
using DB.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using static Financial_Balance_API.ApiSettings;

namespace Financial_Balance_API.Middlewares
{
    public class ApiSettingsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiSettings _apiSettings;

        public ApiSettingsMiddleware(RequestDelegate next
            , IOptions<ApiSettings> options)
        {
            _next = next;
            _apiSettings = options.Value;
        }

        public async Task Invoke(HttpContext httpContext
            , IRepository<Privilege> repository
            , IRepository<User> userRepository)
        {
            var apiKeyProvided = GetRequestApiKey(httpContext);

            if (apiKeyProvided == StringValues.Empty)
            {
                httpContext.Response.StatusCode = 401;

                await httpContext.Response.WriteAsync("Key was not provided. ");

                return;
            }

            var typeOfKey = GetTypeOfKey(apiKeyProvided.ToString());

            if (typeOfKey == KeyType.None)
            {
                httpContext.Response.StatusCode = 401;

                await httpContext.Response.WriteAsync("Unauthorized. Key was not found. ");

                return;
            }

            httpContext.Items["TypeClient"] = typeOfKey;

            await _next(httpContext);
        }

        public KeyType GetTypeOfKey(string apiKey)
        {
            if (apiKey == _apiSettings.ApiKey)
            {
                return KeyType.Admin;
            }

            if (apiKey == _apiSettings.ApiKeyPremium)
            {
                return KeyType.Premium;
            }

            if (apiKey == _apiSettings.ApiKeyStandard)
            {
                return KeyType.Standard;
            }

            return KeyType.None;
        }

        private StringValues GetRequestApiKey(HttpContext context)
        {
            try
            {
                return context.Request
                    .Headers
                    .Where(x => x.Key == ApiSettings.APIKEY)
                    .FirstOrDefault()
                    .Value;
            }
            catch (Exception ex)
            {
                return StringValues.Empty;
            }
        }

        private StringValues GetRequestUserName(HttpContext context)
        {
            try
            {
                return context.Request
                    .Headers
                    .Where(x => x.Key == ApiSettings.USER_NAME)
                    .FirstOrDefault()
                    .Value;
            }
            catch (Exception ex)
            {
                return StringValues.Empty;
            }
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ApiSettingsMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiSettingsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiSettingsMiddleware>();
        }
    }
}
