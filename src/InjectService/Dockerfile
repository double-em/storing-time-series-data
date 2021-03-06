﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Application/Application.csproj", "Application/"]
COPY ["InjectService/InjectService.csproj", "InjectService/"]

RUN dotnet restore "Application/Application.csproj"
RUN dotnet restore "InjectService/InjectService.csproj"

COPY . .

RUN dotnet build "/src/Application/Application.csproj" -c Release -o /app/build

WORKDIR "/src/InjectService"

RUN dotnet build "InjectService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InjectService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InjectService.dll"]
