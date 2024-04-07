#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Service/Catalog/Catalog.API/Catalog.API.csproj", "Service/Catalog/Catalog.API/"]
COPY ["Service/Catalog/Catalog.Application/Catalog.Application.csproj", "Service/Catalog/Catalog.Application/"]
COPY ["Service/Catalog/Catalog.Core/Catalog.Core.csproj", "Service/Catalog/Catalog.Core/"]
COPY ["Service/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "Service/Catalog/Catalog.Infrastructure/"]
RUN dotnet restore "Service/Catalog/Catalog.API/Catalog.API.csproj"
COPY . .
COPY Service/Catalog/Catalog.Infrastructure/Data/SeedData/products.json /app/Catalog/Catalog.Infrastructure/Data/SeedData/products.json
COPY Service/Catalog/Catalog.Infrastructure/Data/SeedData/brands.json /app/Catalog/Catalog.Infrastructure/Data/SeedData/brands.json
COPY Service/Catalog/Catalog.Infrastructure/Data/SeedData/types.json /app/Catalog/Catalog.Infrastructure/Data/SeedData/types.json

WORKDIR "/src/Service/Catalog/Catalog.API"
RUN dotnet build "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]