# ================================================
# 🏗️ Etapa 1: Build
# ================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos los proyectos individualmente para aprovechar la caché
COPY ["nsf-backend-util.sln", "./"]

COPY ["Scharff.API.Utils/Scharff.API.Utils.csproj", "Scharff.API.Utils/"]
COPY ["Scharff.Application.Utils/Scharff.Application.csproj", "Scharff.Application.Utils/"]
COPY ["Scharff.Domain.Utils/Scharff.Domain.csproj", "Scharff.Domain.Utils/"]
COPY ["Scharff.Infrastructure.AzureBlobStorage/Scharff.Infrastructure.AzureBlobStorage.csproj", "Scharff.Infrastructure.AzureBlobStorage/"]
COPY ["Scharff.Infrastructure.Http/Scharff.Infrastructure.Http.csproj", "Scharff.Infrastructure.Http/"]
COPY ["Scharff.Infrastructure.Utils/Scharff.Infrastructure.PostgreSQL.csproj", "Scharff.Infrastructure.Utils/"]

# Restauramos dependencias
RUN dotnet restore "Scharff.API.Utils/Scharff.API.Utils.csproj"

# Copiamos todo el código fuente
COPY . .

# Compilamos y publicamos en modo Release
WORKDIR "/src/Scharff.API.Utils"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ================================================
# 🚀 Etapa 2: Runtime
# ================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Configuramos el puerto
ENV ASPNETCORE_URLS=http://+:5002

# Copiamos solo el resultado del publish
COPY --from=build /app/publish .

# Punto de entrada
ENTRYPOINT ["dotnet", "Scharff.API.Utils.dll"]
