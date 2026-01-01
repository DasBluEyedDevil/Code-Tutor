---
type: "WARNING"
title: "Common Pitfalls"
---

## AOT Configuration Gotchas

**TrimMode Dangers**: Using `TrimMode=full` can remove code that appears unused but is accessed via reflection. If your app crashes in AOT but works in JIT, trimming is often the culprit. Use `[DynamicallyAccessedMembers]` attributes to preserve reflection targets.

**InvariantGlobalization Side Effects**: When enabled, `CultureInfo.CurrentCulture` throws for non-invariant cultures. Date/number formatting becomes culture-agnostic. This breaks apps requiring localization - only use for single-culture deployments.

**Analyzer Warnings Are Critical**: Never ignore AOT or trim analyzer warnings! They indicate code that WILL fail in production. Treat `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>` as mandatory for AOT projects.

**Build Time Increases**: AOT compilation is significantly slower than JIT builds. Expect 2-10x longer build times. Use JIT for development (`dotnet run`) and only publish with AOT for releases.

**Third-Party Library Compatibility**: Many NuGet packages use reflection internally. Check package documentation for AOT compatibility or look for [RequiresUnreferencedCode] warnings during build.