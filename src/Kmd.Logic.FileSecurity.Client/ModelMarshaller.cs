using Kmd.Logic.FileSecurity.Client.Models;
using Kmd.Logic.FileSecurity.Client.ServiceMessages;
using Kmd.Logic.FileSecurity.Client.Types;
using Microsoft.Rest;
using System.Threading.Tasks;

namespace Kmd.Logic.FileSecurity.Client
{
    internal static class ModelMarshaller
    {
        internal static FileSecurityException FileSecurityThrow(
           this HttpOperationException httpOperationException,
           [System.Runtime.CompilerServices.CallerMemberName] string operation = "Unknown method")
        {
            var reason = httpOperationException.Response?.ReasonPhrase;
            var content = httpOperationException.Response?.Content;
            var message = $"{operation} Failed";
            if (!string.IsNullOrWhiteSpace(reason))
            {
                message += $": {reason}";
            }

            if (!string.IsNullOrWhiteSpace(content))
            {
                message += $": {content}";
            }

            return new FileSecurityException(message, httpOperationException);
        }

        internal static async Task<CreateCertificateResponseDetails> ToCreateCertificateResponseDetails(
            this Task<CreateCertificateResponse> createCertificateTask)
        {
            var response = await createCertificateTask.ConfigureAwait(false);
            return response?.ToCreateCertificateResponseDetails();
        }

        internal static CreateCertificateResponseDetails ToCreateCertificateResponseDetails(
            this CreateCertificateResponse createCertificate)
        {
            return new CreateCertificateResponseDetails
            {
                CertificateId = createCertificate.CertificateId ?? throw new FileSecurityException("CertificateId cannot be null"),
                Name = createCertificate.Name
            };
        }

        internal static async Task<T> ValidateBody<T>(
        this Task<HttpOperationResponse<T>> httpOperationResponseTask,
        [System.Runtime.CompilerServices.CallerMemberName] string operation = "Unknown method")
        {
            var httpOperationResponse = await httpOperationResponseTask.ConfigureAwait(false);
            return await httpOperationResponse.ValidateBody(operation).ConfigureAwait(false);
        }

        internal static async Task<T> ValidateBody<T>(
            this IHttpOperationResponse<T> httpOperationResponse,
            [System.Runtime.CompilerServices.CallerMemberName] string operation = "Unknown method")
        {
            if (httpOperationResponse != null && httpOperationResponse.Body != null)
            {
                return httpOperationResponse.Body;
            }

            if (httpOperationResponse?.Response == null)
            {
                throw new FileSecurityException($"{operation}: Failed.");
            }

            if (httpOperationResponse.Response.Content == null)
            {
                throw new FileSecurityException(
                    $"{operation}: {httpOperationResponse.Response.ReasonPhrase}");
            }

            var content = await httpOperationResponse.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new FileSecurityException(
                $"{operation}: {httpOperationResponse.Response.ReasonPhrase}: {content}");
        }
    }
}
