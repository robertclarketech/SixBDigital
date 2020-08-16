# 6BDigital Technical Test

## Prerequisites

-   Please ensure you have sqlite installed on your machine
    -   On Linux this will likely be available in your systems repository
    -   On Windows, the easiest way is through [chocolatey](https://chocolatey.org) or [scoop](https://scoop.sh/)
-   Ensure you have the [dotnet cli](https://docs.microsoft.com/en-us/dotnet/core/tools/) installed, which is included with the .NET Core SDK

## Building

-   Navigate to `/SixBDigital.Web`
-   (If you have `dotnet-ef` installed globally, you can skip this step) Run `dotnet tool restore`
-   Run `dotnet ef database migrate`
-   This should create `database.sqlite` in the Web Project folder, and populate it with seed data
-   Run `dotnet build` to build the project and `dotnet run` to run it

## Notes

-   The database will be populated with a single admin user that can be used to access the Admin area:
    -   Username: admin
    -   Password: SHzMrX5QgHPhnGnl
-   There is currently no way to add additional admin users from the UI
