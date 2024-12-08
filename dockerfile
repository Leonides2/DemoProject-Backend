# Etapa base para el entorno de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Define el puerto de producción como variable de entorno
EXPOSE 80
EXPOSE 443

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos los archivos del monorepositorio
COPY . .

COPY ./mydatabase.db /app/mydatabase.db 
# Restaura y construye el proyecto principal API y sus dependencias
WORKDIR /src/API
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Etapa final: para el contenedor que ejecutará la aplicación
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY --from=build /app/mydatabase.db  /app/mydatabase.db 

# Punto de entrada al contenedor
ENTRYPOINT ["dotnet", "API.dll"]
