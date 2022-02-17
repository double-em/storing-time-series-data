FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Application/Application.csproj", "Application/"]
COPY ["DataInjector/DataInjector.csproj", "DataInjector/"]

RUN dotnet restore "Application/Application.csproj"
RUN dotnet restore "DataInjector/DataInjector.csproj"

COPY . .

RUN dotnet build "/src/Application/Application.csproj" -c Release -o /app/build

WORKDIR "/src/DataInjector"

RUN dotnet build "DataInjector.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataInjector.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataInjector.dll"]
