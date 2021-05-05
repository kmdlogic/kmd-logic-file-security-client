using System;

namespace Kmd.Logic.FileSecurity.Client.ServiceMessages
{
    public class CreateCertificateResponseDetails
    {
        public string Name { get; set; }

        public Guid CertificateId { get; set; }
    }
}
