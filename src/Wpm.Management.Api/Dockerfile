# Imagen base para la ejecución de la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia y restauración de dependencias
COPY ["src/Wpm.Management.Api/Wpm.Management.Api.csproj", "Wpm.Management.Api/"]
RUN dotnet restore "Wpm.Management.Api/Wpm.Management.Api.csproj"

# Copia todo el código fuente
COPY ./src ./src
WORKDIR "/src/Wpm.Management.Api"

# Compilación del proyecto
RUN dotnet build "Wpm.Management.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Wpm.Management.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagen final para producción
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wpm.Management.Api.dll"]
