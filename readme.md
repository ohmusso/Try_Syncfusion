# Try "The Help Desk Application" by Syncfusion

<https://www.syncfusion.com/ebooks/blazor-succinctly>

## Project

### Create solution and project

``` dotnet
dotnet new blazorwasm -o HelpDeskApp --hosted --auth Individual
#--use-local-db
```

* HelpDeskApp.sln
  * Client
    * HelpDeskApp.Client.csproj
  * Server
    * HelpDeskApp.Server.csproj
  * Shared
    * HelpDeskApp.Shared.csproj

## Run

``` dotnet
dotnet run --project "server"
```

or

``` dotnet
dotnet watch run --project "server"
# changes are reflected and restart
```

### test accounts

See Server/appsettings.json
"TestUsers"

## Debug

Run(Ctrl + Shift + D) on vscode

## Tools

### syncfusion blazor platform

``` dotnet
dotnet add Client package Syncfusion.Blazor.Inputs -v 18.4.0.30
dotnet add Client package Syncfusion.Blazor.Popups -v 18.4.0.30
dotnet add Client package Syncfusion.Blazor.Data -v 18.4.0.30
dotnet add Client package Syncfusion.Blazor.DropDowns -v 18.4.0.30
dotnet add Client package Syncfusion.Blazor.Layouts -v 18.4.0.30
dotnet add Client package Syncfusion.Blazor.Calendars -v 18.4.0.30
dotnet add Client package Syncfusion.Blazor.Navigations -v 18.4.0.30
dotnet add Client package Syncfusion.Blazor.Lists -v 18.4.0.30
dotnet add Client package Syncfusion.Blazor.Grid -v 18.4.0.30
dotnet add Client package Syncfusion.Blazor.Buttons -v 18.4.0.30
dotnet add Client package Syncfusion.Blazor.Notifications -v 18.4.0.30
```

#### Add license key

see below url
<https://help.syncfusion.com/common/essential-studio/licensing/license-key?_ga=2.240940611.1192356537.1609715894-565617065.1609279460#how-to-generate-syncfusion-license-key>

Register lisence key

```csharp
//Client/Program.cs
public static async Task Main(string[] args)
{
  ....

  //Register Syncfusion license 
  Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("<KEY>");
```

## vscode extention

### yuml

write uml on markdown

<https://github.com/jaime-olivares/yuml-diagram/wiki>

```yuml
// {type:activity}
// {generate:true}

(start)-><a>[kettle empty]->(Fill Kettle)->|b|
<a>[kettle full]->|b|->(Boil Kettle)->|c|
|b|->(Add Tea Bag)->(Add Milk)->|c|->(Pour Water)
(Pour Water)->(end)


```
