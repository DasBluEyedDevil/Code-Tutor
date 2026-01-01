# TODO: Create a multi-stage Dockerfile for ShopFlow API

# STAGE 1: Build
# - Use mcr.microsoft.com/dotnet/sdk:9.0
# - Copy project files and restore dependencies
# - Copy source code and build
# - Publish to /app/publish

# STAGE 2: Test
# - Extend from build stage
# - Copy test project and run tests

# STAGE 3: Runtime
# - Use mcr.microsoft.com/dotnet/aspnet:9.0-alpine
# - Create and switch to non-root user
# - Copy from build stage
# - Expose port and set entry point