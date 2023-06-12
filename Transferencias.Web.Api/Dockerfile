FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Transferencias.Web.Api/Transferencias.Web.Api.csproj", "Transferencias.Web.Api/"]
RUN dotnet restore "Transferencias.Web.Api/Transferencias.Web.Api.csproj"
COPY . .
WORKDIR "/src/Transferencias.Web.Api"
RUN dotnet build "Transferencias.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transferencias.Web.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transferencias.Web.Api.dll"]