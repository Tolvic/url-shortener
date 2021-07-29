# URL Shortener
A simple URL shortene built using:
* .NET 5
* Entity Framework Core
* JQuery
* Bootstrap



## Local Setup
Clone this repo

Build the solution
* This can be done either through visual studio or by running the `dotnet build` command from within the UrlShortener directory
* Note: Client Side libraries are installed using [LibMan](https://docs.microsoft.com/en-us/aspnet/core/client-side/libman/?view=aspnetcore-5.0) and this should happen automatically on rebuild 

Set up a Databse
* Ensure that you have SQL Server installed
* Ensure that the connection string server name in appsettings.json matches your local sql server name
* Within in Visual Studio > Package Manager Console run `Update-Database`

Run the solution
* This can be done either through visual studio or using the `dotnet run` command


## Running Tests
C# code is tested using the Nunit framework with the Fluent Assertion library and Mocking is achieved using Moq. 

Javascript code is tested using Jasmine.

### C# Tests
C# Tests can be run using the Test Running in Visual Studio or by using the `dotnet test` command

### Javascript Tests
Javascript test can be run using a tool called Chutzpah.

**Visual Studio**

To run the tests in Visual Studio download the following two extensions by going to `Extensions > Manage Extensions > Online > Search for Chutzpah`

* Chutzpah Test Runner Context Menu Extensions
* Chutzpah Test Adapter for the Test Explorer

Javascript tests can then be run by right click on a Scripts folder or individual js test files and clicking either `Run JS Tests` or `Open in browser`

**Command Line**

From within the Unit test project directory, run the following command

`{your user folder}\.nuget\packages\chutzpah\4.4.11\tools\chutzpah.console.exe Scripts`

Individual test files can be specified rather than the entire scripts folder if you prefer
