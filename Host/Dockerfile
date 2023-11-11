FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ServiceTemplate/ServiceTemplate.csproj", "ServiceTemplate/"]
RUN dotnet restore "ServiceTemplate/ServiceTemplate.csproj"
COPY . .
WORKDIR "/src/ServiceTemplate"
RUN dotnet build "ServiceTemplate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceTemplate.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceTemplate.dll"]
