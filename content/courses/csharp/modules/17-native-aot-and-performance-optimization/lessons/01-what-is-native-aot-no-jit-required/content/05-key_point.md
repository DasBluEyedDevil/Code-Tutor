---
type: "KEY_POINT"
title: "Native AOT Fundamentals"
---

## Key Takeaways

- **`<PublishAot>true</PublishAot>` compiles to native code** -- the output is a single platform-specific executable with no JIT compilation at runtime. Startup is measured in milliseconds.

- **AOT is for publish, not development** -- `dotnet run` still uses JIT for fast iteration. AOT only applies when you `dotnet publish`. This preserves your development workflow.

- **Choose AOT when startup time matters** -- CLI tools, serverless functions, and microservices benefit most. Choose regular JIT when you need reflection, dynamic loading, or maximum library compatibility.
