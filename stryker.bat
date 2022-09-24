dotnet restore
dotnet build --no-restore
dotnet stryker -tp tests/PowerUtils.Results.Tests/PowerUtils.Results.Tests.csproj --reporter cleartext --reporter html -o