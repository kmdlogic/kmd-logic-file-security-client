using System;

namespace Kmd.Logic.FileSecurity.Client.DocumentPrivilegesSample
{
    /// <summary>
    /// Gets or sets the configuration properties.
    /// </summary>
    public class SignConfigurationDetails
    {
        /// <summary>
        /// Gets or sets the preconfigured Sign Configuration Id.
        /// </summary>
        public Guid SignConfigurationId { get; set; }

        /// <summary>
        /// Gets or sets the preconfigured location where a sample pdf is located.
        /// An example pdf for reference is available at path /Data/Pdf/.
        /// </summary>
        public string PdfEmptySampleLocation { get; set; }

        /// <summary>
        /// Gets or sets the preconfigured location where the generated pdf can be saved.
        /// </summary>
        public string PdfGeneratedDocumentLocation { get; set; }
    }
}
