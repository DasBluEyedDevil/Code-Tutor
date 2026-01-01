---
type: "EXAMPLE"
title: "Multi-Stage Dockerfile for ShopFlow"
---

This production Dockerfile uses multi-stage builds to create an optimized, secure container image. Each stage serves a specific purpose, and only the minimal runtime dependencies end up in the final image.

```dockerfile
# ===== Multi-Stage Dockerfile for ShopFlow API =====
# Stage 1: Build environment
# Stage 2: Test environment  
# Stage 3: Production runtime

# ========== STAGE 1: BUILD ==========
# Use the SDK image for building (includes compiler, MSBuild, etc.)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set working directory inside container
WORKDIR /src

# Copy project files first (for layer caching)
# Changes to source code won't invalidate this cached layer
COPY ["src/ShopFlow.Api/ShopFlow.Api.csproj", "ShopFlow.Api/"]
COPY ["src/ShopFlow.Core/ShopFlow.Core.csproj", "ShopFlow.Core/"]
COPY ["src/ShopFlow.Infrastructure/ShopFlow.Infrastructure.csproj", "ShopFlow.Infrastructure/"]
COPY ["Directory.Build.props", "."]
COPY ["Directory.Packages.props", "."]

# Restore NuGet packages (cached unless .csproj changes)
RUN dotnet restore "ShopFlow.Api/ShopFlow.Api.csproj"

# Copy remaining source code
COPY src/ .

# Build the application in Release mode
RUN dotnet build "ShopFlow.Api/ShopFlow.Api.csproj" \
    -c Release \
    -o /app/build \
    --no-restore

# Publish the application (creates deployment-ready output)
RUN dotnet publish "ShopFlow.Api/ShopFlow.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-build \
    /p:UseAppHost=false

# ========== STAGE 2: TEST ==========
# Run tests in a separate stage to validate before publishing
FROM build AS test

WORKDIR /src

# Copy test projects
COPY ["tests/ShopFlow.Tests.Unit/ShopFlow.Tests.Unit.csproj", "tests/ShopFlow.Tests.Unit/"]
COPY ["tests/ShopFlow.Tests.Integration/ShopFlow.Tests.Integration.csproj", "tests/ShopFlow.Tests.Integration/"]

# Restore test dependencies
RUN dotnet restore "tests/ShopFlow.Tests.Unit/ShopFlow.Tests.Unit.csproj"

# Copy test source code
COPY tests/ tests/

# Run unit tests (integration tests need external services)
RUN dotnet test "tests/ShopFlow.Tests.Unit/ShopFlow.Tests.Unit.csproj" \
    -c Release \
    --no-restore \
    --logger:"console;verbosity=normal"

# ========== STAGE 3: PRODUCTION RUNTIME ==========
# Use the minimal ASP.NET runtime image (no SDK, no compiler)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS runtime

# Security: Run as non-root user
RUN addgroup -S shopflow && adduser -S shopflow -G shopflow

# Install curl for health checks (minimal addition)
RUN apk add --no-cache curl

WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Set ownership to non-root user
RUN chown -R shopflow:shopflow /app
USER shopflow

# Expose the port the app listens on
EXPOSE 8080

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Health check command
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/healthz/live || exit 1

# Entry point - run the application
ENTRYPOINT ["dotnet", "ShopFlow.Api.dll"]
```
