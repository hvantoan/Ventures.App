FROM mcr.microsoft.com/dotnet/aspnet:8.0.6-bookworm-slim AS base
WORKDIR /app
EXPOSE 8000
RUN echo "deb http://deb.debian.org/debian bookworm contrib non-free" > /etc/apt/sources.list.d/contrib.list \
    && apt-get update; apt-get install -y ttf-mscorefonts-installer fontconfig fonts-roboto

ENV ASPNETCORE_URLS=http://+:9090

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore "src/CB.Api/CB.Api.csproj"
WORKDIR "/src/src/CB.Api"
RUN dotnet build "CB.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CB.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CB.Api.dll"]