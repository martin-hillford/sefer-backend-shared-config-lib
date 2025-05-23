# Sefer.Backend.SharedConfig.Lib
This repository features a straightforward yet powerful C# library designed specifically for Sefer Backend projects, 
facilitating seamless configuration sharing across microservices.

## Overview
In modern microservices architectures, managing configuration settings can become complex, especially when services 
are distributed across different environments. This library simplifies the process by providing two flexible methods 
for sharing configuration data:

1. **Shared Configuration File:** 
If all microservices are deployed on the same system, you can utilize a shared configuration file located on the file 
system. This method ensures that all services can access and read the same configuration settings, promoting consistency 
and reducing the risk of configuration drift.

2. **Configuration Endpoint:** 
In scenarios where microservices are running on different systems or environments, the library offers an alternative 
approach through a dedicated configuration endpoint. This allows services to retrieve their configuration settings 
dynamically over the network, ensuring that each service can access the most up-to-date configuration without the need 
for manual updates. Currently, an Azure BlobContainer is required.

## Usage
To use this library please include the Sefer.Backend.SharedConfig.Lib NuGet package. Additional two configuration variables
are required. They can be set either through the appSettings.json file or through an Environment variable:

| AppSettings Variable | Environment Variable   | Values                                                            |
|----------------------|------------------------|-------------------------------------------------------------------|
| SharedConfigStore    | SHARED_CONFIG_STORE    | azure, local-file or endpoint                                     |
| SharedConfigLocation | SHARED_CONFIG_LOCATION | The url (for azure and endpoint) or the folder on the file system |

## License 
GPL-3.0-or-later