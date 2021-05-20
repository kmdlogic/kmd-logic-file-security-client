using System;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Kmd.Logic.FileSecurity.Client.Models;
using Kmd.Logic.FileSecurity.Client.ServiceMessages;
using Kmd.Logic.Identity.Authorization;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Kmd.Logic.FileSecurity.Client.DocumentPrivilegesSample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            InitLogger();

            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args)
                    .Build()
                    .Get<AppConfiguration>();

                await Run(config).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Caught a fatal unhandled exception");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static async Task Run(AppConfiguration configuration)
        {
            var validator = new ConfigurationValidator(configuration);
            if (!validator.Validate())
            {
                return;
            }

            using var httpClient = new HttpClient();
            using var tokenProviderFactory = new LogicTokenProviderFactory(configuration.TokenProvider);
            var fileSecurityClient = new FileSecurityClient(httpClient, tokenProviderFactory, configuration.FileSecurityOptions);

            // Get Sign Configuration
            Log.Information("Getting signconfiguration...");
            var signConfigurationResult = await fileSecurityClient.GetPdfSignConfiguration(configuration.SignConfigurationDetails.SignConfigurationId).ConfigureAwait(false);
            if (signConfigurationResult == null)
            {
                Log.Error("Couldn't get signconfiguration");
                return;
            }

            Console.WriteLine(
                "Configuration recieved successfully. SignConfiguration ID: {0} \nSignConfiguration Name: {1}\nSubscription ID : {2}",
                signConfigurationResult.Id,
                signConfigurationResult.Name,
                signConfigurationResult.SubscriptionId);

            // Generate document
            Log.Information("Generate document using privileges...");
            string pdfSample = configuration.SignConfigurationDetails.PdfEmptySampleLocation;
            using Document document = new Document(pdfSample);
            var documentPrivilege = FillPrivileges(signConfigurationResult.PdfPrivilege);
            document.Encrypt(string.Empty, "owner", documentPrivilege, CryptoAlgorithm.AESx128, false);
            document.Save(configuration.SignConfigurationDetails.PdfGeneratedDocumentLocation + "pdf_with_privileges.pdf");
            Log.Information(
                "Document with configured privileges generated successfully at {location}",
                configuration.SignConfigurationDetails.PdfGeneratedDocumentLocation + "pdf-with-privileges.pdf");
        }

        private static DocumentPrivilege FillPrivileges(PdfPrivilegeModel pdfPrivilege)
        {
            var privileges = DocumentPrivilege.ForbidAll;
            privileges.CopyAllowLevel = pdfPrivilege.CopyAllowLevel.Value;
            privileges.ChangeAllowLevel = pdfPrivilege.ChangeAllowLevel.Value;
            privileges.AllowAssembly = pdfPrivilege.AllowAssembly.Value;
            privileges.AllowScreenReaders = pdfPrivilege.AllowScreenReaders.Value;
            privileges.AllowFillIn = pdfPrivilege.AllowFillIn.Value;
            privileges.AllowModifyAnnotations = pdfPrivilege.AllowModifyAnnotations.Value;
            privileges.AllowCopy = pdfPrivilege.AllowCopy.Value;
            privileges.AllowModifyContents = pdfPrivilege.AllowModifyContents.Value;
            privileges.AllowDegradedPrinting = pdfPrivilege.AllowDegradedPrinting.Value;
            privileges.AllowPrint = pdfPrivilege.AllowPrint.Value;
            privileges.PrintAllowLevel = pdfPrivilege.PrintAllowLevel.Value;

            return privileges;
        }

        private static void InitLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
