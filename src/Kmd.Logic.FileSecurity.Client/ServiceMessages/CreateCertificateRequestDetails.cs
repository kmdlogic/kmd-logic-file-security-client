using System.IO;

namespace Kmd.Logic.FileSecurity.Client.ServiceMessages
{
    /// <summary>
    /// This class will be used as certificate data to be created using FileSecurityClient.cs.
    /// </summary>
    public class CreateCertificateRequestDetails
    {
        /// <summary>
        /// Gets name of the certificate.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets memory stream of certificate to be uploaded.
        /// </summary>
        public Stream Certificate { get; }

        /// <summary>
        /// Gets certificate password.
        /// </summary>
        public string CertificatePassword { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCertificateRequestDetails"/> class.
        /// </summary>
        /// <param name="name">Name of certificate.</param>
        /// <param name="certificate">Stream of certificate file.</param>
        /// <param name="certificatePassword">Password to use the certificate.</param>
        public CreateCertificateRequestDetails(string name, Stream certificate, string certificatePassword)
        {
            this.Name = name;
            this.Certificate = certificate;
            this.CertificatePassword = certificatePassword;
        }
    }
}
