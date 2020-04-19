#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DockerRedis.NetCore/DockerRedis.NetCore.csproj", "DockerRedis.NetCore/"]
RUN dotnet restore "DockerRedis.NetCore/DockerRedis.NetCore.csproj"
COPY . .
WORKDIR "/src/DockerRedis.NetCore"
RUN dotnet build "DockerRedis.NetCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerRedis.NetCore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "DockerRedis.NetCore.dll"]