FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5266

ENV ASPNETCORE_URLS=http://+:5266

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AssetService.csproj", "./"]
RUN dotnet restore "AssetService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AssetService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AssetService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AssetService.dll"]
