# KMD Logic File Security Api Client

A dotnet client library for using File Security module in KMD Logic platform through API.

## The purpose of the File Security API

To allow products using the Logic platform to use File Security in order to create,update,read ceritificates and sign configurations. Sign configurations will be having privileges and optionally a certificate which can be used to sign a document. Using the privileges the configuration owner can manage the access and different privileges in the document generated. As of now supported document type is Pdf.


## Getting started in ASP.NET Core

To use this library in an ASP.NET Core application, 
add a reference to the [Kmd.Logic.Identity.FileSecurity](https://www.nuget.org/packages/Kmd.Logic.FileSecurity.Client) nuget package, 
and add a reference to the [Kmd.Logic.Identity.Authorization](https://www.nuget.org/packages/Kmd.Logic.Identity.Authorization) nuget package.


## FileSecurityClient certificate

The Logic FileSecurityClient provides APIs for:

* Get a certificate;
* Get all certificates;
* Create a certificate;
* Update a certificate;
* Delete a certificate;
* Create a sign configuration;
* Get a sign configuration;
* Get all sign configuration;
* Update a sign configuration;
* Delete a sign configuration;

## How to configure the File Security client

Perhaps the easiest way to configure the File Security client is through Application Settings.

```json
{
  "TokenProvider": {
    "ClientId": "",
    "ClientSecret": "",
    "AuthorizationScope": ""
  },
  "FileSecurityOptions": {
    "SubscriptionId": ""
  },
  "CertificateDetails": {
    "CertificateId": ""
  },
  "SignConfigurationDetails": {
    "SignConfigurationId":  ""
  }
}
```

To get started:

1. Create a subscription in [Logic Console](https://console.kmdlogic.io). This will provide you the `SubscriptionId`.
2. Request a client credential. Once issued you can view the `ClientId`, `ClientSecret` and `AuthorizationScope` in [Logic Console](https://console.kmdlogic.io).

## Sample applications

1. Sample console application `Kmd.Logic.FileSecurity.Client.ConfigurationSample` is included to demonstrate how to call the Logic File Security API. You will need to provide the settings described above in their `appsettings.json`.
2. Sample console application `Kmd.Logic.FileSecurity.Client.DocumentPrivilegesSample` is included to demonstarte how to generate a pdf document using the configured privilges.
