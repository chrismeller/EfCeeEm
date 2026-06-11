FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["EfCeeEmSharp.Host/EfCeeEmSharp.Host.csproj", "EfCeeEmSharp.Host/"]
RUN dotnet restore "EfCeeEmSharp.Host/EfCeeEmSharp.Host.csproj"
COPY . .
WORKDIR "/src/EfCeeEmSharp.Host"
RUN dotnet build "EfCeeEmSharp.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EfCeeEmSharp.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EfCeeEmSharp.Host.dll"]
