using System;

namespace Kmd.Logic.FileSecurity.Client.Types
{
    [Serializable]
    public class FileSecurityException : Exception
    {
        public string InnerMessage { get; }

        public FileSecurityException()
        {
        }

        public FileSecurityException(string message)
            : base(message)
        {
        }

        public FileSecurityException(string message, string innerMessage)
            : base(message)
        {
            this.InnerMessage = innerMessage;
        }

        public FileSecurityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
