using System.IO;

namespace Kmd.Logic.FileSecurity.Client.ServiceMessages
{
    /// <summary>
    /// This class will be used as certificate data to be created using FileSecurityClient.cs
    /// </summary>
    public class CreateCertificateRequestDetails
    {
        /// <summary>
        /// Name of the certificate
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Memory stream of certificate to be uploaded
        /// </summary>
        public Stream Certificate { get; }

        /// <summary>
        /// Certificate password
        /// </summary>
        public string CertificatePassword { get; }

        public CreateCertificateRequestDetails(string name, Stream certificate, string certificatePassword)
        {
            Name = name;
            Certificate = certificate;
            CertificatePassword = certificatePassword;
        }
    }
}
