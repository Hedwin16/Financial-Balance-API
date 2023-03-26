using static Financial_Balance_API.ApiSettings;

namespace Financial_Balance_API.Common;

public class ApiSession
{
    public static KeyType GetCurrentKeyType(HttpRequest request)
    {
        try
        {
            return (KeyType)request.HttpContext.Items["TypeClient"];
        }
        catch (Exception)
        {
            return KeyType.None;
        }
    }
}
