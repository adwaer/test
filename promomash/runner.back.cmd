cd backend
dotnet restore
cd PM.Identity.WebApi
set ASPNETCORE_ENVIRONMENT=Development
dotnet run --server.urls=http://localhost:5000/