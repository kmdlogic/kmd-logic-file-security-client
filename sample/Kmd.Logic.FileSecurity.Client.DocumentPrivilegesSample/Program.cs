using System;
using System.IO;
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
    class Program
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
            var signConfigurationRequest = BuildSignConfigurationRequest();
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
            string dataDir = GetDataDir_AsposePdf_SecuritySignatures();
            using (Document document = new Document(dataDir + "input.pdf"))
            {
                DocumentPrivilege documentPrivilege = FillPrivileges(signConfigurationResult.PdfPrivilege);
                document.Encrypt("user", "owner", documentPrivilege, CryptoAlgorithm.AESx128, false);
                document.Save(dataDir + "SetPrivileges_out.pdf");
                Log.Information("Document with configured privileges generated successfully...");
            }
        }

        private static DocumentPrivilege FillPrivileges(PdfPrivilegeModel pdfPrivilege)
        {
            DocumentPrivilege privileges = DocumentPrivilege.ForbidAll;

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

        private static string GetDataDir_AsposePdf_SecuritySignatures()
        {
            return Path.GetFullPath(GetDataDir_Data() + "Pdf/");
        }

        private static string GetDataDir_Data()
        {
            var parent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            string startDirectory = null;
            if (parent != null)
            {
                var directoryInfo = parent.Parent;
                if (directoryInfo != null)
                {
                    startDirectory = directoryInfo.FullName;
                }
            }
            else
            {
                startDirectory = parent.FullName;
            }

            return Path.Combine(startDirectory, "Data\\");
        }

        private static SignConfigurationPdfRequestDetails BuildSignConfigurationRequest()
        {
            var pdfPrivilege = new PdfPrivilegeModel(
                copyAllowLevel: 1,
                changeAllowLevel: 1,
                allowAssembly: true,
                allowScreenReaders: true,
                allowFillIn: true,
                allowModifyAnnotations: true,
                allowCopy: true,
                allowModifyContents: true,
                allowDegradedPrinting: true,
                allowPrint: true,
                printAllowLevel: 1,
                allowAll: true,
                forbidAll: false);
            return new SignConfigurationPdfRequestDetails(
                  signConfigurationId: Guid.Empty,
                  name: "TestSignConfiguration",
                  ownerPassword: "TestPwd",
                  certificateId: Guid.Empty,
                  subscriptionId: Guid.Empty,
                  pdfPrivilege: pdfPrivilege
                );
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
