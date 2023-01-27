#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used for VS debugging on Docker
#FROM mcr.microsoft.com/dotnet/aspnet:5.0.15-alpine3.15 AS base
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
#RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
#RUN apt-get install -y libsqlite3-mod-spatialite \
#    && apt-get install -y libspatialite-dev \
#    && apt-get install -y sqlite3 \
#    && apt-get install -y nodejs  
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

#FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine3.15 AS build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
#RUN apt-get install -y nodejs  

WORKDIR /src
COPY ["src/presentation/MockBank.WebApi/appsettings*.json", "presentation/MockBank.WebApi/"]
COPY ["src/presentation/MockBank.WebApi/MockBank.WebApi.csproj", "presentation/MockBank.WebApi/"]
COPY ["src/core/MockBank.Application/MockBank.Application.csproj", "core/MockBank.Application/"]
COPY ["src/core/MockBank.Domain/MockBank.Domain.csproj", "core/MockBank.Domain/"]
COPY ["src/infrastructure/MockBank.Data/MockBank.Data.csproj", "infrastructure/MockBank.Data/"]
RUN dotnet restore "presentation/MockBank.WebApi/MockBank.WebApi.csproj"

 
WORKDIR "/src/presentation/MockBank.WebApi"
COPY . .

RUN ASPNETCORE_ENVIRONMENT=Development dotnet build "MockBank.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN ASPNETCORE_ENVIRONMENT=Development dotnet publish "MockBank.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MockBank.WebApi.dll"]