---
type: "WARNING"
title: "Common Pitfalls"
---

## Docker Image Pitfalls

**Using SDK Image in Production**: The `mcr.microsoft.com/dotnet/sdk:9.0` image contains compilers, MSBuild, and development tools -- it is over 800 MB. Production containers should use the minimal `mcr.microsoft.com/dotnet/aspnet:9.0-alpine` runtime image (under 100 MB). Smaller images mean faster deployments, less attack surface, and lower storage costs.

**Running as Root in Containers**: The default user in Docker containers is root. If an attacker exploits your application, they have root access to the container and potentially the host. Always create and switch to a non-root user in your Dockerfile with `USER appuser`.

**Secrets Baked Into Images**: Using `COPY appsettings.Production.json` with connection strings or API keys in the Docker image means anyone with access to the image can extract your secrets. Pass secrets as environment variables or mount them as volumes at runtime. Never embed credentials in image layers.

**Missing Health Checks**: Without a `HEALTHCHECK` instruction, container orchestrators cannot detect when your application is unhealthy. An app that has deadlocked or run out of memory continues receiving traffic. Add health check endpoints (`/healthz/live`, `/healthz/ready`) and configure Docker health checks to restart unhealthy containers.

**Ignoring Layer Caching Order**: Docker caches each layer. If you `COPY . .` before `dotnet restore`, every source code change invalidates the restore cache and redownloads all NuGet packages. Copy .csproj files first, restore, then copy source code. This optimization can reduce build times from minutes to seconds.
