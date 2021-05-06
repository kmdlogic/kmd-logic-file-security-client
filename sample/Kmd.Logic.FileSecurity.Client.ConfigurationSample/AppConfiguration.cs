using Kmd.Logic.Identity.Authorization;

namespace Kmd.Logic.FileSecurity.Client.ConfigurationSample
{
    internal class AppConfiguration
    {
        public LogicTokenProviderOptions TokenProvider { get; set; } = new LogicTokenProviderOptions();

        public FileSecurityOptions FileSecurity { get; set; } = new FileSecurityOptions();

        public FileSecurityDetails FileSecurityDetails { get; set; } = new FileSecurityDetails();
    }
}