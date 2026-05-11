FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["TextNuvem.Api/TextNuvem.Api.csproj", "TextNuvem.Api/"]
COPY ["TextNuvem.Infra/TextNuvem.Infra.csproj", "TextNuvem.Infra/"]
COPY ["TextNuvem.Application/TextNuvem.Application.csproj", "TextNuvem.Application/"]
COPY ["TextNuvem.Domain/TextNuvem.Domain.csproj", "TextNuvem.Domain/"]

RUN dotnet restore "TextNuvem.Api/TextNuvem.Api.csproj"

COPY . .

WORKDIR "/src/TextNuvem.Api"

RUN dotnet publish "./TextNuvem.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

FROM base AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TextNuvem.Api.dll"]