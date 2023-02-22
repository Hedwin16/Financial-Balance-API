using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Financial_Balance_API.Attributes;

[AttributeUsage(validOn: AttributeTargets.Method)]
public class AdminKeyAttribute : Attribute, IAsyncActionFilter
{
    private ApiSettings _apiSettings;

    public async Task OnActionExecutionAsync(ActionExecutingContext context
        , ActionExecutionDelegate next)
    {
        var adminKey = GetKey(context);

        if (adminKey == StringValues.Empty)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Key was not Provided."
            };

            return;
        }

        var appSettings = context.HttpContext.RequestServices
            .GetRequiredService<IOptions<ApiSettings>>();

        _apiSettings = appSettings.Value;

        if (_apiSettings.ApiKey != adminKey)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Unauthorized Client."
            };

            return;
        }

        await next();
    }

    public StringValues GetKey(ActionExecutingContext context)
    {
        return context.HttpContext.Request
            .Headers
            .Where(k => k.Key == ApiSettings.APIKEY)
            .FirstOrDefault()
            .Value;
    }
}
