﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Kmd.Logic.FileSecurity.Client.Models;
using Kmd.Logic.FileSecurity.Client.ServiceMessages;
using Kmd.Logic.FileSecurity.Client.Types;
using Kmd.Logic.Identity.Authorization;
using Microsoft.Rest;

namespace Kmd.Logic.FileSecurity.Client
{
    /// <summary>
    /// Class to use the autogenerated client class to call APIs.
    /// </summary>
    public sealed class FileSecurityClient : IDisposable
    {
        private readonly HttpClient httpClient;
        private readonly FileSecurityOptions options;
        private readonly ITokenProviderFactory tokenProviderFactory;
        private InternalClient internalClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSecurityClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use. The caller is expected to manage this resource and it will not be disposed.</param>
        /// <param name="tokenProviderFactory">The Logic access token provider factory.</param>
        /// <param name="options">The required configuration options.</param>
        public FileSecurityClient(HttpClient httpClient, ITokenProviderFactory tokenProviderFactory, FileSecurityOptions options)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.tokenProviderFactory = tokenProviderFactory ?? throw new ArgumentNullException(nameof(tokenProviderFactory));
        }

        /// <summary>
        /// Creates certificate.
        /// </summary>
        /// <param name="createCertificateRequestDetails">Certificate details to be created.</param>
        /// <returns>CreateCertificateResponse.</returns>
        public async Task<CertificateResponse> CreateCertificate(CertificateRequestDetails createCertificateRequestDetails)
        {
            var client = this.CreateClient();

            using var certificateDetailsResponse = await client.CreateCertificatesWithHttpMessagesAsync(
                 this.options.SubscriptionId,
                 createCertificateRequestDetails.Name,
                 createCertificateRequestDetails.Certificate,
                 createCertificateRequestDetails.CertificatePassword).ConfigureAwait(false);
            switch (certificateDetailsResponse?.Response?.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return certificateDetailsResponse.Body;

                case System.Net.HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new FileSecurityException(certificateDetailsResponse?.Body?.ToString() ?? "Invalid configuration provided to access File Security service");
            }
        }

        /// <summary>
        /// Updates certificate.
        /// </summary>
        /// <param name="updateCertificateRequestDetails">Certficate details to be updated.</param>
        /// <returns>CertificateResponse.</returns>
        public async Task<CertificateResponse> UpdateCertificate(CertificateRequestDetails updateCertificateRequestDetails)
        {
            var client = this.CreateClient();

            using var certificateDetailsResponse = await client.UpdateCertificatesWithHttpMessagesAsync(
                 this.options.SubscriptionId,
                 updateCertificateRequestDetails.Id,
                 updateCertificateRequestDetails.Name,
                 updateCertificateRequestDetails.Certificate,
                 updateCertificateRequestDetails.CertificatePassword).ConfigureAwait(false);
            switch (certificateDetailsResponse?.Response?.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return certificateDetailsResponse.Body;

                case System.Net.HttpStatusCode.NotFound:
                    throw new FileSecurityException($"Certificate with Id {updateCertificateRequestDetails.Id} not found");

                default:
                    throw new FileSecurityException(certificateDetailsResponse?.Body?.ToString() ?? "Invalid configuration provided to access File Security service");
            }
        }

        /// <summary>
        /// Get certificate.
        /// </summary>
        /// <param name="certificateId">CertificateId.</param>
        /// <returns>CertificateResponse.</returns>
        public async Task<CertificateResponse> GetCertificate(Guid certificateId)
        {
            var client = this.CreateClient();

            using (var certificateDetailsResponse = await client.GetCertificatesWithHttpMessagesAsync(
                 this.options.SubscriptionId,
                 certificateId).ConfigureAwait(false))
            {
                switch (certificateDetailsResponse?.Response?.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return certificateDetailsResponse.Body;

                    case System.Net.HttpStatusCode.NotFound:
                        throw new FileSecurityException($"Certificate with Id {certificateId} not found");

                    default:
                        throw new FileSecurityException(certificateDetailsResponse?.Body?.ToString() ?? "Invalid configuration provided to access File Security service");
                }
            }
        }

        /// <summary>
        /// Get all certificates.
        /// </summary>
        /// <returns>List of Certificates.</returns>
        public async Task<IEnumerable<CertificateListResponse>> GetAllCertificate()
        {
            var client = this.CreateClient();

            using (var certificateDetailsResponse = await client.GetAllCertificatesWithHttpMessagesAsync(
                 this.options.SubscriptionId).ConfigureAwait(false))
            {
                switch (certificateDetailsResponse?.Response?.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return certificateDetailsResponse.Body;

                    default:
                        throw new FileSecurityException(certificateDetailsResponse?.Body?.ToString() ?? "Invalid configuration provided to access File Security service");
                }
            }
        }

        /// <summary>
        /// Creates sign configuration.
        /// </summary>
        /// <param name="createSignConfigurationPdfRequestDetails">Signconfiguration create request details.</param>
        /// <returns>SignConfigurationPdfResponse.</returns>
        public async Task<SignConfigurationPdfResponse> CreateSignConfigurationPdf(SignConfigurationPdfRequestDetails createSignConfigurationPdfRequestDetails)
        {
            var client = this.CreateClient();
            var request = new PdfPrivilegeModelSignConfigurationCreateRequest(
                            name: createSignConfigurationPdfRequestDetails.Name,
                            ownerPassword: createSignConfigurationPdfRequestDetails.OwnerPassword,
                            certificateId: createSignConfigurationPdfRequestDetails.CertificateId,
                            privileges: createSignConfigurationPdfRequestDetails.PdfPrivilege);

            using var signConfigurationDetailsResponse = await client.CreatePdfSignConfigurationWithHttpMessagesAsync(
                 this.options.SubscriptionId, request).ConfigureAwait(false);
            switch (signConfigurationDetailsResponse?.Response?.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return signConfigurationDetailsResponse.Body;

                case System.Net.HttpStatusCode.NotFound:
                    return null;

                default:
                    throw new FileSecurityException(signConfigurationDetailsResponse?.Body?.ToString() ?? "Invalid configuration provided to access File Security service");
            }
        }

        /// <summary>
        /// Delete certificate.
        /// </summary>
        /// <param name="certificateId">CertificateId.</param>
        /// <returns>HttpResponseMessage.</returns>
        public async Task<HttpResponseMessage> DeleteCertificate(Guid certificateId)
        {
            var client = this.CreateClient();

            using (var certificateDetailsResponse = await client.DeleteCertificatesWithHttpMessagesAsync(
                 this.options.SubscriptionId,
                 certificateId).ConfigureAwait(false))
            {
                return certificateDetailsResponse.Response;
            }
        }

        /// <summary>
        /// Get sign configuration.
        /// </summary>
        /// <param name="signConfigurationId">SignConfigurationID.</param>
        /// <returns>SignConfigurationPdfResponse.</returns>
        public async Task<SignConfigurationPdfResponse> GetPdfSignConfiguration(Guid signConfigurationId)
        {
            var client = this.CreateClient();

            using (var signConfigurationDetailsResponse = await client.GetPdfSignConfigurationWithHttpMessagesAsync(
                 this.options.SubscriptionId,
                 signConfigurationId).ConfigureAwait(false))
            {
                switch (signConfigurationDetailsResponse?.Response?.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return signConfigurationDetailsResponse.Body;

                    case System.Net.HttpStatusCode.NotFound:
                        throw new FileSecurityException($"Sign Configuration with Id {signConfigurationId} not found");

                    default:
                        throw new FileSecurityException(signConfigurationDetailsResponse?.Body?.ToString() ?? "Invalid sign configuration provided to access File Security service");
                }
            }
        }

        /// <summary>
        /// Disposing the rest of the classes.
        /// </summary>
        public void Dispose()
        {
            this.httpClient?.Dispose();
            this.tokenProviderFactory?.Dispose();
            this.internalClient?.Dispose();
        }

        /// <summary>
        /// Create internal client.
        /// </summary>
        /// <returns>InternalClient.</returns>
        internal InternalClient CreateClient()
        {
            if (this.internalClient != null)
            {
                return this.internalClient;
            }

            var tokenProvider = this.tokenProviderFactory.GetProvider(this.httpClient);

            this.internalClient = new InternalClient(new TokenCredentials(tokenProvider))
            {
                BaseUri = this.options.FileSecurityServiceUri,
            };

            return this.internalClient;
        }
    }
}