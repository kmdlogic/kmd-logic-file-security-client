﻿# KMD Logic File Security Api Client

A dotnet client library for using File Security module in KMD Logic platform through API.

## The purpose of the File Security API

To allow products using the Logic platform to use File Security in order to create,update,read ceritificates


## Getting started in ASP.NET Core

To use this library in an ASP.NET Core application, 
add a reference to the [Kmd.Logic.Identity.FileSecurity](https://www.nuget.org/packages/Kmd.Logic.FileSecurity.Client/1.0.0) nuget package, 
and add a reference to the [Kmd.Logic.Identity.Authorization](https://www.nuget.org/packages/Kmd.Logic.Identity.Authorization) nuget package.


## FileSecurityClient certificate

The Logic FileSecurityClient provides APIs for:

* Get a certificate;
* Create a certificate;
* Update a certificate;
* Delete a certificate;

## How to configure the File Security client

Perhaps the easiest way to configure the File Security client is though Application Settings.

```json
{
  "TokenProvider": {
    "ClientId": "",
    "ClientSecret": "",
    "AuthorizationScope": ""
  },
  "FileSecurity": {
    "SubscriptionId": ""
  }
}
```

To get started:

1. Create a subscription in [Logic Console](https://console.kmdlogic.io). This will provide you the `SubscriptionId`.
2. Request a client credential. Once issued you can view the `ClientId`, `ClientSecret` and `AuthorizationScope` in [Logic Console](https://console.kmdlogic.io).

## Sample applications

Sample console application is included to demonstrate how to call the Logic File Security API. You will need to provide the settings described above in their `appsettings.json`.
