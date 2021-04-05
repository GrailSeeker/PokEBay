#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PokEBay.Orders.API/PokEBay.Orders.API.csproj", "PokEBay.Orders.API/"]
RUN dotnet restore "PokEBay.Orders.API/PokEBay.Orders.API.csproj"
COPY . .
WORKDIR "/src/PokEBay.Orders.API"
RUN dotnet build "PokEBay.Orders.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokEBay.Orders.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokEBay.Orders.API.dll"]
