using System;
using Kmd.Logic.FileSecurity.Client.Models;

namespace Kmd.Logic.FileSecurity.Client.ServiceMessages
{
    /// <summary>
    /// This class will be used as signconfiguration pdf document type data to be created using FileSecurityClient.cs.
    /// </summary>
    public class SignConfigurationPdfRequestDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SignConfigurationPdfRequestDetails"/> class.
        /// </summary>
        /// <param name="signConfigurationId">Sign configuration Id.</param>
        /// <param name="name">Name of the configuration.</param>
        /// <param name="ownerPassword">Owner password.</param>
        /// <param name="certificateId">Certificate Id.</param>
        /// <param name="subscriptionId">Subscription Id.</param>
        /// <param name="pdfPrivilege">Pdf privileges.</param>
        public SignConfigurationPdfRequestDetails(
            Guid signConfigurationId,
            string name,
            string ownerPassword,
            Guid? certificateId,
            Guid subscriptionId,
            PdfPrivilegeModel pdfPrivilege)
        {
            this.SignConfigurationId = signConfigurationId;
            this.Name = name;
            this.OwnerPassword = ownerPassword;
            this.CertificateId = certificateId;
            this.SubscriptionId = subscriptionId;
            this.PdfPrivilege = pdfPrivilege;
        }

        /// <summary>
        /// Gets the signconfiguration id.
        /// </summary>
        public Guid SignConfigurationId { get; }

        /// <summary>
        /// Gets the name of the configuration.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the owner password.
        /// </summary>
        public string OwnerPassword { get; }

        /// <summary>
        /// Gets the certificate id.
        /// </summary>
        public Guid? CertificateId { get; }

        /// <summary>
        /// Gets the subscription id.
        /// </summary>
        public Guid SubscriptionId { get; }

        /// <summary>
        /// Gets the pdf priileges.
        /// </summary>
        public PdfPrivilegeModel PdfPrivilege { get; }
    }
}
