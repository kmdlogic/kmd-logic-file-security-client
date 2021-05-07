using Serilog;
using System;

namespace Kmd.Logic.FileSecurity.Client.ConfigurationSample
{
    /// <summary>
    /// Class to validate configuration details.
    /// </summary>
    internal class ConfigurationValidator
    {
        private readonly AppConfiguration _configuration;

        public ConfigurationValidator(AppConfiguration configuration)
        {
            this._configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        internal bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this._configuration.TokenProvider?.ClientId)
                || string.IsNullOrWhiteSpace(this._configuration.TokenProvider?.ClientSecret)
                || string.IsNullOrWhiteSpace(this._configuration.TokenProvider?.AuthorizationScope))
            {
                Log.Error(
                    "Invalid configuration. Please provide proper information to `appsettings.json`. Current data is: {@Settings}",
                    this._configuration);

                return false;
            }

            if (this._configuration.FileSecurityOptions?.SubscriptionId == null)
            {
                Log.Error(
                    "Invalid configuration. FileSecurity must have a configured SubscriptionId in `appsettings.json`. Current data is: {@Settings}",
                    this._configuration);

                return false;
            }

            if (this._configuration.CertificateDetails?.CertificateId == null)
            {
                Log.Error(
                    "Invalid configuration. FileSecurity must have a configured CertificateId in `appsettings.json`. Current data is: {@Settings}",
                    this._configuration);

                return false;
            }

            return true;
        }
    }
}