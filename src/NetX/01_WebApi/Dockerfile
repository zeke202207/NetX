#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Directory.Build.props", "."]
COPY ["src/NetX/01_WebApi/NetX.WebApi.csproj", "NetX/01_WebApi/"]
COPY ["src/01_NetXFramework/01_App/NetX.App.csproj", "01_NetXFramework/01_App/"]
COPY ["src/01_NetXFramework/00_Utils/Common/NetX.Common.csproj", "01_NetXFramework/00_Utils/Common/"]
COPY ["src/01_NetXFramework/00_Utils/SharedFramework/ChainPipeline/NetX.SharedFramework.ChainPipeline.csproj", "01_NetXFramework/00_Utils/SharedFramework/ChainPipeline/"]
COPY ["src/01_NetXFramework/02_Module/NetX.Module.csproj", "01_NetXFramework/02_Module/"]
COPY ["src/01_NetXFramework/03_EventBus/NetX.EventBus.csproj", "01_NetXFramework/03_EventBus/"]
COPY ["src/01_NetXFramework/04_DatabaseSetup/NetX.DatabaseSetup.csproj", "01_NetXFramework/04_DatabaseSetup/"]
COPY ["src/01_NetXFramework/05_Tenant/NetX.Tenants.csproj", "01_NetXFramework/05_Tenant/"]
COPY ["src/01_NetXFramework/06_Authentication/Authentication.Jwt/NetX.Authentication.Jwt.csproj", "01_NetXFramework/06_Authentication/Authentication.Jwt/"]
COPY ["src/01_NetXFramework/06_Authentication/Authentication.Core/NetX.Authentication.Core.csproj", "01_NetXFramework/06_Authentication/Authentication.Core/"]
COPY ["src/01_NetXFramework/09_Cache/Cache.Core/NetX.Cache.Core.csproj", "01_NetXFramework/09_Cache/Cache.Core/"]
COPY ["src/01_NetXFramework/09_Cache/MemoryCache/NetX.InMemoryCache.csproj", "01_NetXFramework/09_Cache/MemoryCache/"]
COPY ["src/01_NetXFramework/06_Authentication/Authentication.OAuth/Authentication.OAuth.csproj", "01_NetXFramework/06_Authentication/Authentication.OAuth/"]
COPY ["src/01_NetXFramework/07_Logging/DiagnosticLog/NetX.DiagnosticLog.csproj", "01_NetXFramework/07_Logging/DiagnosticLog/"]
COPY ["src/01_NetXFramework/07_Logging/NetX.AuditLog/NetX.AuditLog.csproj", "01_NetXFramework/07_Logging/NetX.AuditLog/"]
COPY ["src/01_NetXFramework/12_MessageQueue/MemoryQueue/NetX.MemoryQueue.csproj", "01_NetXFramework/12_MessageQueue/MemoryQueue/"]
COPY ["src/01_NetXFramework/08_ApiDoc/Swagger/NetX.Swagger.csproj", "01_NetXFramework/08_ApiDoc/Swagger/"]
COPY ["src/01_NetXFramework/10_Scheduling/QuartzScheduling/Netx.QuartzScheduling.csproj", "01_NetXFramework/10_Scheduling/QuartzScheduling/"]
COPY ["src/01_NetXFramework/11_DomainDrivenDesign/Ddd.Domain/Netx.Ddd.Domain.csproj", "01_NetXFramework/11_DomainDrivenDesign/Ddd.Domain/"]
COPY ["src/01_NetXFramework/11_DomainDrivenDesign/Ddd.Core/Netx.Ddd.Core.csproj", "01_NetXFramework/11_DomainDrivenDesign/Ddd.Core/"]
COPY ["src/01_NetXFramework/11_DomainDrivenDesign/Ddd.Infrastructure/Netx.Ddd.Infrastructure.csproj", "01_NetXFramework/11_DomainDrivenDesign/Ddd.Infrastructure/"]
COPY ["src/01_NetXFramework/13_FileSystem/SimpleFileSystem/NetX.SimpleFileSystem.csproj", "01_NetXFramework/13_FileSystem/SimpleFileSystem/"]
RUN dotnet restore "NetX/01_WebApi/NetX.WebApi.csproj"
COPY . .
WORKDIR "/src/NetX/01_WebApi"
RUN dotnet build "NetX.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetX.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetX.WebApi.dll"]