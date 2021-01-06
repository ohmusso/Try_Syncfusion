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

### test account

ID:Administrator@Email
password:Passw0rd!

## Debug

Run(Ctrl + Shift + D) on vscode

## Tools
