# Overview
This is just a hacked up version of the console app here - https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli 

# Quickstart

1. After cloning, edit the appsettings.json and make sure the DB path points to some valid path on your system.
1. Install the global ef tools and let it create a new DB for you.
```
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```
1. Hack up the code in `Program.cs` `Main` method to do whatever you want and run the app.
```
dotnet run
```

