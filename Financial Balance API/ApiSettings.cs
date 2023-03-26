namespace Financial_Balance_API;

public class ApiSettings
{
    public const string API_SETTINGS = "ApiSettings";
    public const string CONNECTION_STRING= "ApiContext";
    public const string APIKEY = "X-Api-Key";
    public const string USER_NAME = "X-User";

    public string? UserName { get; set; }
    public string? ApiKey { get; set; }
    public string? ApiKeyStandard { get; set; }
    public string? ApiKeyPremium { get; set; }

    public enum KeyType
    {
        Admin,
        Standard,
        Premium,
        None
    }
}
