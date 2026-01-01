---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`builder.Services.AddApiVersioning()`**: Registers versioning services. Configure default version, readers, and behavior here.

**`new ApiVersion(1, 0)`**: Represents version 1.0. Use major.minor format. Major = breaking changes, Minor = additions.

**`AssumeDefaultVersionWhenUnspecified`**: If client doesn't specify version, use default. Good for backward compatibility.

**`ReportApiVersions = true`**: Adds `api-supported-versions` header to responses. Clients can discover available versions.

**`ApiVersionReader.Combine(...)`**: Accept version from multiple sources. URL segment is clearest, header is cleanest.

**`app.NewVersionedApi()`**: Creates a version set for grouping endpoints. Use with MapGroup for organized versioning.

**`.HasApiVersion(new ApiVersion(1, 0))`**: Marks the group as version 1.0. Only clients requesting v1.0 reach these endpoints.

**`{version:apiVersion}`**: Route constraint that captures version from URL. Works with SubstituteApiVersionInUrl.

**Version-specific models**: Common pattern is ProductV1, ProductV2. Each version can have different properties without breaking clients.