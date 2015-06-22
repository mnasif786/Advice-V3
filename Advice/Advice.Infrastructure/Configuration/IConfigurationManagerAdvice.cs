namespace Advice.Infrastructure.Configuration
{
    public interface IConfigurationManagerAdvice
    {
        string GetApplicationSettingValueFromKey(string key);
    }
}
