using System;

namespace Kmd.Logic.FileSecurity.Client
{
    /// <summary>
    /// Provide the configuration options for using the filesecurity service.
    /// </summary>
    public sealed class FileSecurityOptions
    {
        /// <summary>
        /// Gets or sets the Logic FileSecurity service.
        /// </summary>
        /// <remarks>
        /// This option should not be overridden except for testing purposes.
        /// </remarks>
        public Uri FileSecurityServiceUri { get; set; } = new Uri("https://gateway.kmdlogic.io/file-security/v1");

        /// <summary>
        /// Gets or sets the Logic Subscription.
        /// </summary>
        public Guid SubscriptionId { get; set; }
    }
}
