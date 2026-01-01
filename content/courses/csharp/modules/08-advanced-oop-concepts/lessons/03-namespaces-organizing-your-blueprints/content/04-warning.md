---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Mistakes!

**Circular dependencies**: If namespace A uses B and B uses A, you'll get confusing errors! Organize so dependencies flow one way: Models -> Services -> UI.

**Missing using in other namespaces**: If MyStore.Services needs Product from MyStore.Models, you still need `using MyStore.Models;` inside that file!

**Implicit usings confusion**: When `<ImplicitUsings>` is enabled, you might use types without realizing they're auto-imported. Check obj/Debug/net8.0/GlobalUsings.g.cs to see what's included.

**Namespace vs folder mismatch**: By convention, namespace should match folder structure. File at 'MyApp/Models/Customer.cs' should have 'namespace MyApp.Models'. Not required, but strongly recommended.

**Mixing file-scoped and block namespaces**: A file can have EITHER 'namespace Foo;' (file-scoped) OR 'namespace Foo { }' (block), not both. Pick one style per file.