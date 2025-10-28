# ================================================
# 🏗️ Etapa 1: Build
# ================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos de solución y proyectos para aprovechar la caché
COPY ["nsf-backend-util.sln", "./"]

COPY ["Scharff.API.Utils/Scharff.API.Utils.csproj", "Scharff.API.Utils/"]
COPY ["Scharff.Application.Utils/Scharff.Application.csproj", "Scharff.Application.Utils/"]
COPY ["Scharff.Domain.Utils/Scharff.Domain.csproj", "Scharff.Domain.Utils/"]
COPY ["Scharff.Infrastructure.AzureBlobStorage/Scharff.Infrastructure.AzureBlobStorage.csproj", "Scharff.Infrastructure.AzureBlobStorage/"]
COPY ["Scharff.Infrastructure.Http/Scharff.Infrastructure.Http.csproj", "Scharff.Infrastructure.Http/"]
COPY ["Scharff.Infrastructure.Utils/Scharff.Infrastructure.PostgreSQL.csproj", "Scharff.Infrastructure.Utils/"]

# Restaurar dependencias
RUN dotnet restore "Scharff.API.Utils/Scharff.API.Utils.csproj"

# Copiar el código fuente completo
COPY . .

# Compilar y publicar en modo Release
WORKDIR "/src/Scharff.API.Utils"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ================================================
# 🚀 Etapa 2: Runtime
# ================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Configurar el puerto de la aplicación
ENV ASPNETCORE_URLS=http://+:5002
EXPOSE 5002 
# 👈 Importante para que Azure detecte el puerto expuesto

# Copiar solo los archivos publicados
COPY --from=build /app/publish .

# Punto de entrada
ENTRYPOINT ["dotnet", "Scharff.API.Utils.dll"]
