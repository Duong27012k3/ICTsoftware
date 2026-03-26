# ─── Stage 1: Build ───────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy toàn bộ solution (4 projects)
COPY . .

# Restore
RUN dotnet restore AZT-Backend/AZT-Backend.csproj

# Publish
RUN dotnet publish AZT-Backend/AZT-Backend.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ─── Stage 2: Runtime ─────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Tạo thư mục lưu file upload
RUN mkdir -p wwwroot/uploads/images \
             wwwroot/uploads/catalogues

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "AZT-Backend.dll"]