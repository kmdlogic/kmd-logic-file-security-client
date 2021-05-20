using Serilog;

namespace Kmd.Logic.FileSecurity.Client.DocumentPrivilegesSample
{
    /// <summary>
    /// Class to validate configuration details.
    /// </summary>
    internal class ConfigurationValidator
    {
        private readonly AppConfiguration configuration;

        public ConfigurationValidator(AppConfiguration configuration)
        {
            this.configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        internal bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this.configuration.TokenProvider?.ClientId)
                || string.IsNullOrWhiteSpace(this.configuration.TokenProvider?.ClientSecret)
                || string.IsNullOrWhiteSpace(this.configuration.TokenProvider?.AuthorizationScope))
            {
                Log.Error(
                    "Invalid configuration. Please provide proper information to `appsettings.json`. Current data is: {@Settings}",
                    this.configuration);

                return false;
            }

            if (this.configuration.FileSecurityOptions?.SubscriptionId == null)
            {
                Log.Error(
                    "Invalid configuration. FileSecurity must have a configured SubscriptionId in `appsettings.json`. Current data is: {@Settings}",
                    this.configuration);

                return false;
            }

            return true;
        }
    }
}