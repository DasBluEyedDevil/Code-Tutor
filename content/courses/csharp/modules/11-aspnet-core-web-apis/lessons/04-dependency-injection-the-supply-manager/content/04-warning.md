---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues!

**Captive Dependency (CRITICAL!)**: Injecting Scoped into Singleton = disaster! The Scoped service becomes effectively Singleton, causing stale data and threading bugs. Use IServiceScopeFactory if you need Scoped from Singleton.

**Registering AFTER Build()**: Services must be registered BEFORE 'builder.Build()'! After Build(), it's too late. You'll get runtime errors.

**Injecting implementation instead of interface**: Use '(IRepository repo)' not '(Repository repo)'. Interface enables testing with mocks and swapping implementations!

**Forgetting to register**: If you inject IService but forgot to register it, you get: 'Unable to resolve service for type IService'. Always register before using!