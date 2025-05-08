# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Salin file csproj dan restore dependencies
COPY ["ProductManagement.API/ProductManagement.API.csproj", "ProductManagement.API/"]
COPY ["ProductManagement.Common/ProductManagement.Common.csproj", "ProductManagement.Common/"]
COPY ["ProductManagement.Domain/ProductManagement.Domain.csproj", "ProductManagement.Domain/"]
COPY ["ProductManagement.Features/ProductManagement.Features.csproj", "ProductManagement.Features/"]
COPY ["ProductManagement.Persistence/ProductManagement.Persistence.csproj", "ProductManagement.Persistence/"]
RUN dotnet restore "ProductManagement.API/ProductManagement.API.csproj"

# Salin seluruh isi solution
COPY . .

COPY ProductManagement.API/appsettings.json ./ProductManagement.API/appsettings.json

# Build project
WORKDIR /app/ProductManagement.API
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Jalankan aplikasi
ENTRYPOINT ["dotnet", "ProductManagement.API.dll"]
