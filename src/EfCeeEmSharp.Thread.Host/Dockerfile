FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/EfCeeEmSharp.Thread.Host/EfCeeEmSharp.Thread.Host.csproj", "EfCeeEmSharp.Thread.Host/"]
RUN dotnet restore "src/EfCeeEmSharp.Thread.Host/EfCeeEmSharp.Thread.Host.csproj"
COPY . .
WORKDIR "/src/EfCeeEmSharp.Thread.Host"
RUN dotnet build "EfCeeEmSharp.Thread.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EfCeeEmSharp.Thread.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EfCeeEmSharp.Thread.Host.dll"]
