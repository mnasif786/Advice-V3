using System.Web.Configuration;

namespace Advice.Infrastructure.Configuration
{
    public class ConfigurationManagerAdvice : IConfigurationManagerAdvice
    {
        public string GetApplicationSettingValueFromKey(string key)
        {
             return WebConfigurationManager.AppSettings.Get(key);
        }
    }
}
