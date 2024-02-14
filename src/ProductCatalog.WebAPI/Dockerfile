#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ProductCatalog.WebAPI/ProductCatalog.WebAPI.csproj", "src/ProductCatalog.WebAPI/"]
COPY ["src/ProductCatalog.Infra.IoC/ProductCatalog.Infra.IoC.csproj", "src/ProductCatalog.Infra.IoC/"]
COPY ["src/ProductCatalog.Application/ProductCatalog.Application.csproj", "src/ProductCatalog.Application/"]
COPY ["src/ProductCatalog.Domain/ProductCatalog.Domain.csproj", "src/ProductCatalog.Domain/"]
COPY ["src/ProductCatalog.Infra.Data/ProductCatalog.Infra.Data.csproj", "src/ProductCatalog.Infra.Data/"]
RUN dotnet restore "./src/ProductCatalog.WebAPI/./ProductCatalog.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/ProductCatalog.WebAPI"
RUN dotnet build "./ProductCatalog.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProductCatalog.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductCatalog.WebAPI.dll"]