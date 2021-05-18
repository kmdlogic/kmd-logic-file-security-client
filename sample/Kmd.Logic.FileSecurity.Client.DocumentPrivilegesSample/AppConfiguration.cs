﻿using Kmd.Logic.Identity.Authorization;

namespace Kmd.Logic.FileSecurity.Client.DocumentPrivilegesSample
{
    /// <summary>
    /// Class to get configuration details.
    /// </summary>
    internal class AppConfiguration
    {
        /// <summary>
        /// Gets or sets authorization token.
        /// </summary>
        public LogicTokenProviderOptions TokenProvider { get; set; } = new LogicTokenProviderOptions();

        /// <summary>
        /// Gets or sets filesecurity configuration options.
        /// </summary>
        public FileSecurityOptions FileSecurityOptions { get; set; } = new FileSecurityOptions();

        /// <summary>
        /// Gets or sets Sign Configuration details.
        /// </summary>
        public SignConfigurationDetails SignConfigurationDetails { get; set; } = new SignConfigurationDetails();
    }
}