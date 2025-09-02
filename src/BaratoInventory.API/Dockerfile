FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/BaratoInventory.API/BaratoInventory.API.csproj ./BaratoInventory.API/
COPY src/BaratoInventory.Core/BaratoInventory.Core.csproj ./BaratoInventory.Core/
COPY src/BaratoInventory.Infrastructure/BaratoInventory.Infrastructure.csproj ./BaratoInventory.Infrastructure/

WORKDIR /src/BaratoInventory.API
RUN dotnet restore "BaratoInventory.API.csproj"

WORKDIR /src
COPY src/ ./ 

WORKDIR /src/BaratoInventory.API
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "BaratoInventory.API.dll"]
