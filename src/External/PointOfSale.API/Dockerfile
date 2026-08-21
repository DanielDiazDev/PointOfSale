FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/External/PointOfSale.API/PointOfSale.API.csproj", "src/External/PointOfSale.API/"]
COPY ["src/External/PointOfSale.Infrastructure/PointOfSale.Infrastructure.csproj", "src/External/PointOfSale.Infrastructure/"]
COPY ["src/Core/PointOfSale.Application/PointOfSale.Application.csproj", "src/Core/PointOfSale.Application/"]
COPY ["src/Core/PointOfSale.Model/PointOfSale.Model.csproj", "src/Core/PointOfSale.Model/"]
COPY ["src/Shared/PointOfSale.Shared/PointOfSale.Shared.csproj", "src/Shared/PointOfSale.Shared/"]
RUN dotnet restore "src/External/PointOfSale.API/PointOfSale.API.csproj"
COPY . .
WORKDIR "/src/src/External/PointOfSale.API"
RUN dotnet build "./PointOfSale.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PointOfSale.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PointOfSale.API.dll"]
