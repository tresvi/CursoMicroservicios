#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Auditoria.Clientes.Consumer/Auditoria.Clientes.Consumer.csproj", "Auditoria.Clientes.Consumer/"]
RUN dotnet restore "Auditoria.Clientes.Consumer/Auditoria.Clientes.Consumer.csproj"
COPY . .
WORKDIR "/src/Auditoria.Clientes.Consumer"
RUN dotnet build "Auditoria.Clientes.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auditoria.Clientes.Consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auditoria.Clientes.Consumer.dll"]