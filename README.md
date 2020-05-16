# PostalCodeApi

## Table of content

  * [Description](#description)
  * [Pre-requisites](#pre-requisites)
  * [Installing](#installing)
  * [Running](#running)
  * [License](#license)


## Description

This project is a web API to perform postal code and city related tasks. It is developed using C# and .NET core 3.1. This version includes the following features:
* User Management (auth, roles, crud)
* Importing from [GeoNames](https://www.geonames.org/)
* Searching for a postal code
* Getting cities related to a postal code
* Swagger documentation

All the data is retrieved from [GeoNames](https://www.geonames.org/).

By default, two users are created:
* admin/123Aze@ (User with admin role)
* user/123Aze@ (User with basic user role)
 
## Pre-requisites

* [ASP.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)


## Getting started

* Clone or download the repository 
* Open the project in your favorite IDE
* Check the connection string in the appsettings.json
* If you are using Visual Studio, go to the Package Manager Console and run `Update-Database`
* If you are using another IDE, open a terminal at the root of the project and run `dotnet ef database update`
* Run the application and Swagger should appear 

## License

[MIT](https://github.com/malain96/PostalCodeApi/blob/master/LICENSE)