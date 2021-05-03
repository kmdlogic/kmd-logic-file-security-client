using System;
using System.Collections.Generic;

namespace Kmd.Logic.FileSecurity.Client.Types
{
    [Serializable]
    public class FileSecurityValidationException : Exception
    {
        public IDictionary<string, IList<string>> ValidationErrors { get; }

        public FileSecurityValidationException()
        {
        }

        public FileSecurityValidationException(string message, IDictionary<string, IList<string>> validationErrors)
           : base(message)
        {
            this.ValidationErrors = validationErrors;
        }
    }
}
