// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Kmd.Logic.FileSecurity.Client
{
    using Microsoft.Rest;
    using Models;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// </summary>
    internal partial interface IInternalClient : System.IDisposable
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }

        /// <summary>
        /// Subscription credentials which uniquely identify client
        /// subscription.
        /// </summary>
        ServiceClientCredentials Credentials { get; }


        /// <summary>
        /// Get all certificates managed by the subscription.
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the certificate.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<IList<CertificateListResponse>>> GetAllCertificatesWithHttpMessagesAsync(System.Guid subscriptionId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates/Uploads a certificate for a specific subscription.
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the certificate.
        /// </param>
        /// <param name='name'>
        /// </param>
        /// <param name='certificate'>
        /// </param>
        /// <param name='certificatePassword'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<CertificateResponse>> CreateCertificatesWithHttpMessagesAsync(System.Guid subscriptionId, string name, Stream certificate, string certificatePassword = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get details of the requested certificate managed by a subscrition.
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the certificate.
        /// </param>
        /// <param name='certificateId'>
        /// Identifier of the certificate to fetch.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<CertificateResponse>> GetCertificatesWithHttpMessagesAsync(System.Guid subscriptionId, System.Guid certificateId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update certificate and details
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the certificate
        /// </param>
        /// <param name='certificateId'>
        /// Identifier of the certificate to update.
        /// </param>
        /// <param name='name'>
        /// </param>
        /// <param name='certificate'>
        /// </param>
        /// <param name='certificatePassword'>
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<CertificateResponse>> UpdateCertificatesWithHttpMessagesAsync(System.Guid subscriptionId, System.Guid certificateId, string name, Stream certificate = default(Stream), string certificatePassword = default(string), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete requested certificate managed by a subscrition.
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the certificate.
        /// </param>
        /// <param name='certificateId'>
        /// Identifier of the certificate to delete.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> DeleteCertificatesWithHttpMessagesAsync(System.Guid subscriptionId, System.Guid certificateId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get all signconfiguration managed by the subscription.
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the configurations.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<IList<SignConfigurationListResponse>>> GetAllSignConfigurationsWithHttpMessagesAsync(System.Guid subscriptionId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get owner password of the requested sign configuration.
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the configurations.
        /// </param>
        /// <param name='signConfigurationId'>
        /// Identifier of sign configuration to be used.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<string>> GetSignConfigurationOwnerPasswordWithHttpMessagesAsync(System.Guid subscriptionId, System.Guid signConfigurationId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Creates a signconfiguration for pdf document type
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the configurations.
        /// </param>
        /// <param name='request'>
        /// The details of the pdf document privileges and other configuration
        /// details
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<SignConfigurationPdfResponse>> CreatePdfSignConfigurationWithHttpMessagesAsync(System.Guid subscriptionId, PdfPrivilegeModelSignConfigurationCreateRequest request = default(PdfPrivilegeModelSignConfigurationCreateRequest), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get details of the requested sign configuration for pdf document
        /// type
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the configurations.
        /// </param>
        /// <param name='signConfigurationId'>
        /// Identifier of sign configuration to be used.
        /// </param>
        /// <param name='requireCertificate'>
        /// If true, will return the certificate
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<SignConfigurationPdfResponse>> GetPdfSignConfigurationWithHttpMessagesAsync(System.Guid subscriptionId, System.Guid signConfigurationId, bool? requireCertificate = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates a sign configuration for pdf document type
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the configurations.
        /// </param>
        /// <param name='signConfigurationId'>
        /// Identifier of the sign configuration.
        /// </param>
        /// <param name='request'>
        /// The details of the pdf document privileges and other configuration
        /// details
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<SignConfigurationPdfResponse>> UpdatePdfSignConfigurationWithHttpMessagesAsync(System.Guid subscriptionId, System.Guid signConfigurationId, PdfPrivilegeModelSignConfigurationUpdateRequest request = default(PdfPrivilegeModelSignConfigurationUpdateRequest), Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete requested sign configuration managed by a subscription.
        /// </summary>
        /// <param name='subscriptionId'>
        /// The subscription that owns the configurations.
        /// </param>
        /// <param name='signConfigurationId'>
        /// Identifier of the sign configuration to delete.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse> DeleteSignConfigurationPdfWithHttpMessagesAsync(System.Guid subscriptionId, System.Guid signConfigurationId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

    }
}
