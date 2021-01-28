cd back
dotnet restore
cd BN.Api
set ASPNETCORE_ENVIRONMENT=Development
dotnet build
dotnet run --migrate-db
dotnet run --server.urls=http://localhost:5000/