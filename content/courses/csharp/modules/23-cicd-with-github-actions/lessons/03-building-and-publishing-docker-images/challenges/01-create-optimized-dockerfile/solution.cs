# Multi-stage Dockerfile for ShopFlow API

# ========== STAGE 1: BUILD ==========
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files first for layer caching
COPY ["src/ShopFlow.Api/ShopFlow.Api.csproj", "ShopFlow.Api/"]
COPY ["src/ShopFlow.Core/ShopFlow.Core.csproj", "ShopFlow.Core/"]
COPY ["src/ShopFlow.Infrastructure/ShopFlow.Infrastructure.csproj", "ShopFlow.Infrastructure/"]

# Restore dependencies (cached unless .csproj changes)
RUN dotnet restore "ShopFlow.Api/ShopFlow.Api.csproj"

# Copy source code
COPY src/ .

# Build and publish
RUN dotnet publish "ShopFlow.Api/ShopFlow.Api.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ========== STAGE 2: TEST ==========
FROM build AS test
WORKDIR /src

COPY ["tests/ShopFlow.Tests.Unit/ShopFlow.Tests.Unit.csproj", "tests/ShopFlow.Tests.Unit/"]
RUN dotnet restore "tests/ShopFlow.Tests.Unit/ShopFlow.Tests.Unit.csproj"

COPY tests/ tests/
RUN dotnet test "tests/ShopFlow.Tests.Unit/ShopFlow.Tests.Unit.csproj" \
    -c Release --no-restore

# ========== STAGE 3: RUNTIME ==========
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS runtime

# Create non-root user
RUN addgroup -S appgroup && adduser -S appuser -G appgroup

WORKDIR /app
COPY --from=build /app/publish .

# Set ownership and switch to non-root user
RUN chown -R appuser:appgroup /app
USER appuser

# Expose port and configure environment
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Health check
HEALTHCHECK --interval=30s --timeout=3s \
    CMD wget --quiet --tries=1 --spider http://localhost:8080/healthz/live || exit 1

ENTRYPOINT ["dotnet", "ShopFlow.Api.dll"]