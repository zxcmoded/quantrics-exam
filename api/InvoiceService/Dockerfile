FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5298

ENV ASPNETCORE_URLS=http://+:5298

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InvoiceService.csproj", "./"]
RUN dotnet restore "InvoiceService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "InvoiceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvoiceService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvoiceService.dll"]
