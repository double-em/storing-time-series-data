FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Application/Application.csproj", "Application/"]
COPY ["InjectorService/InjectorService.csproj", "InjectorService/"]

RUN dotnet restore "Application/Application.csproj"
RUN dotnet restore "InjectorService/InjectorService.csproj"

COPY . .

RUN dotnet build "/src/Application/Application.csproj" -c Release -o /app/build

WORKDIR "/src/InjectorService"

RUN dotnet build "InjectorService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InjectorService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InjectorService.dll"]
