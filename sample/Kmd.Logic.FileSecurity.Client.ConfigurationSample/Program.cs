﻿using Kmd.Logic.FileSecurity.Client.Models;
using Kmd.Logic.FileSecurity.Client.ServiceMessages;
using Kmd.Logic.Identity.Authorization;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kmd.Logic.FileSecurity.Client.ConfigurationSample
{
    /// <summary>
    /// Sample class to use the client.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method to start the sample.
        /// </summary>
        /// <param name="args">Array of arguments.</param>
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

        private static void InitLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
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
            var certificateId = configuration.CertificateDetails.CertificateId;

            Log.Information("Fetching certificate details for certificate id {CertificateId} ", configuration.CertificateDetails.CertificateId);
            var result = await fileSecurityClient.GetCertificate(certificateId).ConfigureAwait(false);

            if (result == null)
            {
                Log.Error("Invalid certificate id {Id}", configuration.CertificateDetails.CertificateId);
                return;
            }

            Console.WriteLine("Certificate ID: {0} \nCertificate Name: {1}\nSubscription ID : {2}", result.CertificateId, result.Name, result.SubscriptionId);

            // Create a Sign Connfiguration 
            var signConfigurationRequest = BuildSignConfigurationRequest();
            Log.Information("Creating signconfiguration...");
            var signConfigurationResult = await fileSecurityClient.CreateSignConfigurationPdf(signConfigurationRequest).ConfigureAwait(false);
            if (signConfigurationResult == null)
            {
                Log.Error("Couldn't create signconfiguration");
                return;
            }

            Console.WriteLine(
                "Configuration created successfully. SignConfiguration ID: {0} \nSignConfiguration Name: {1}\nSubscription ID : {2}",
                signConfigurationResult.Id,
                signConfigurationResult.Name,
                signConfigurationResult.SubscriptionId);
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
    }
}