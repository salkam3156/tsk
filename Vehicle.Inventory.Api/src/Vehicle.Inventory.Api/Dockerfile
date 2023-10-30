FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Vehicle.Inventory.Api.csproj", "Vehicle.Inventory.Api/"]
RUN dotnet restore "Vehicle.Inventory.Api/Vehicle.Inventory.Api.csproj"
WORKDIR "/src/Vehicle.Inventory.Api"
COPY . .
RUN dotnet build "Vehicle.Inventory.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vehicle.Inventory.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vehicle.Inventory.Api.dll"]