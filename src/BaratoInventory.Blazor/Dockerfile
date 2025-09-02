FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/BaratoInventory.Blazor/BaratoInventory.Blazor.csproj src/BaratoInventory.Blazor/
COPY src/BaratoInventory.Core/BaratoInventory.Core.csproj src/BaratoInventory.Core/
COPY src/BaratoInventory.Infrastructure/BaratoInventory.Infrastructure.csproj src/BaratoInventory.Infrastructure/

RUN dotnet restore "src/BaratoInventory.Blazor/BaratoInventory.Blazor.csproj"

COPY . .

WORKDIR /src/src/BaratoInventory.Blazor
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "BaratoInventory.Blazor.dll"]
