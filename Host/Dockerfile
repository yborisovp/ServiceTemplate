FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Production
WORKDIR /src

COPY ["Host/", "Host/"]
COPY ["ServiceTemplate.DataAccess/", "ServiceTemplate.DataAccess/"]
COPY ["ServiceTemplate.DataContracts/", "ServiceTemplate.DataContracts/"]

RUN dotnet restore "Host/Host.csproj"
COPY . .
WORKDIR "/src/Host"
RUN --mount=type=secret,id=secret_user     \
    sed -i "s/{{d_secret_user}}/$(cat /run/secrets/secret_user)/g" appsettings.Production.json
RUN --mount=type=secret,id=secret_password \
    sed -i "s/{{d_secret_password}}/$(cat /run/secrets/secret_password)/g" appsettings.Production.json
RUN --mount=type=secret,id=secret_server   \
    sed -i "s/{{d_secret_server}}/$(cat /run/secrets/secret_server)/g" appsettings.Production.json
RUN --mount=type=secret,id=secret_database   \
    sed -i "s/{{d_secret_database}}/$(cat /run/secrets/secret_database)/g" appsettings.Production.json

RUN dotnet build "Host.csproj" -c $BUILD_CONFIGURATION -o /app/build



FROM build AS publish
ARG BUILD_CONFIGURATION=Production
RUN dotnet publish "Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Host.dll"]