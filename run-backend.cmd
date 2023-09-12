@echo off
dotnet ef database update --project .\LX.TestSpace.Server\LX.TestSpace.Server.csproj
dotnet run --project .\LX.TestSpace.Server\LX.TestSpace.Server.csproj --urls https://localhost:7063;http://localhost:5091