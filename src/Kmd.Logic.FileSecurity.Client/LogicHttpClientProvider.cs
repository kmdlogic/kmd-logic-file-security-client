﻿using Kmd.Logic.FileSecurity.Client.Types;
using Kmd.Logic.Identity.Authorization;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Kmd.Logic.FileSecurity.Client
{
    public class LogicHttpClientProvider
    {
        private readonly FileSecurityOptions _options;
        private readonly LogicTokenProviderFactory _tokenProviderFactory;
        private InternalClient _internalClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicHttpClientProvider"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use. The caller is expected to manage this resource and it will not be disposed.</param>
        /// <param name="tokenProviderFactory">The Logic access token provider factory.</param>
        /// <param name="options">The required configuration options.</param>
        protected LogicHttpClientProvider(HttpClient httpClient, LogicTokenProviderFactory tokenProviderFactory, FileSecurityOptions options)
        {
            this.HttpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this._options = options ?? throw new ArgumentNullException(nameof(options));
            this._tokenProviderFactory = tokenProviderFactory ?? throw new ArgumentNullException(nameof(tokenProviderFactory));
        }

        internal InternalClient Client => this.CreateClient();

        internal HttpClient HttpClient { get; }

        private static readonly object ClientLocker = new object();

        internal InternalClient CreateClient()
        {
            lock (ClientLocker)
            {
                if (this._internalClient != null)
                {
                    return this._internalClient;
                }

                var tokenProvider = this._tokenProviderFactory.GetProvider(this.HttpClient);

                this._internalClient = new InternalClient(new TokenCredentials(tokenProvider))
                {
                    BaseUri = this._options.FileSecurityServiceUri ?? new Uri("https://gateway.kmdlogic.io/file-security/v2"),
                };

                return this._internalClient;
            }
        }

        protected Guid ResolveSubscriptionId()
        {
            var resolvedSubscriptionId = this._options.SubscriptionId;
            if (resolvedSubscriptionId == null)
            {
                throw new FileSecurityValidationException("No subscription id provided", new Dictionary<string, IList<string>>());
            }

            return resolvedSubscriptionId.Value;
        }
    }
}
