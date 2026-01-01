---
type: "THEORY"
title: "Image Optimization Techniques"
---

## Multi-Stage Builds

Multi-stage builds dramatically reduce final image size by separating build-time and runtime dependencies. A typical .NET SDK image weighs over 800MB, while the runtime-only image is under 200MB. By using the SDK in the build stage and copying only the published output to a runtime stage, you create smaller, faster, more secure images.

**Before (single-stage):** 850MB image with SDK, compiler, build tools
**After (multi-stage):** 180MB image with only runtime and application

The build stage can include development tools, test frameworks, and build-time dependencies. The final stage contains only what runs in production. Each stage is a separate image that can be cached independently.

## Base Image Selection

Choosing the right base image balances size, security, and compatibility:

| Base Image | Size | Security | Compatibility |
|------------|------|----------|---------------|
| `aspnet:9.0` | ~220MB | Good | Full glibc, broad compatibility |
| `aspnet:9.0-alpine` | ~100MB | Better | musl libc, most apps work |
| `aspnet:9.0-chiseled` | ~50MB | Best | Minimal attack surface |

Alpine-based images use musl libc instead of glibc, which occasionally causes compatibility issues with certain NuGet packages. Chiseled images are the smallest and most secure but contain no shell or package manager, making debugging difficult.

## Layer Caching

Docker builds images in layers, caching each layer for reuse. Optimize for caching by:

1. **Order commands by change frequency**: Put rarely-changing steps first
2. **Copy dependency files before source code**: Project files change less often than source
3. **Use .dockerignore**: Exclude files that would invalidate cache unnecessarily

```dockerfile
# GOOD: Copy .csproj first, then restore
COPY *.csproj ./
RUN dotnet restore

# Source code changes won't invalidate restore cache
COPY . ./
RUN dotnet build
```

```dockerfile
# BAD: Copy everything at once
COPY . ./
RUN dotnet restore  # Invalidated by any source change
RUN dotnet build
```

## The .dockerignore File

Like .gitignore, .dockerignore prevents files from being sent to the build context. This speeds up builds and prevents sensitive files from entering images:

```
# .dockerignore
**/.git
**/.vs
**/bin
**/obj
**/node_modules
**/*.md
**/Dockerfile*
**/.dockerignore
**/docker-compose*
**/.env*
**/secrets*
```

## Security Best Practices

**Run as non-root:** Never run containers as root in production. Create a dedicated user:
```dockerfile
RUN adduser -D appuser
USER appuser
```

**Scan for vulnerabilities:** Use Trivy, Snyk, or similar tools in your CI pipeline to detect vulnerable packages before deployment.

**Generate SBOM:** Software Bill of Materials lists all components in your image, enabling vulnerability tracking when new CVEs are announced.

**Use distroless or chiseled images:** These minimal images contain no shell or package manager, dramatically reducing attack surface.

## Image Tagging Strategy

Consistent tagging enables reliable deployments and rollbacks:

- `latest` - Current main branch build (use cautiously in production)
- `v1.2.3` - Semantic version from git tags
- `sha-abc123` - Specific commit for exact reproducibility
- `main` / `develop` - Branch-based for environments

Production should always deploy specific versions (v1.2.3 or sha-abc123), never `latest`. This ensures you know exactly what is running and can rollback to a specific version.