# Imagen base para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia y restaura dependencias
COPY Wpm.Management.Api/Wpm.Management.Api.csproj Wpm.Management.Api/
RUN dotnet restore Wpm.Management.Api/Wpm.Management.Api.csproj

# Copia el código fuente y compila
COPY . .
WORKDIR /src/Wpm.Management.Api
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publicación
FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final para ejecución
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wpm.Management.Api.dll"]
