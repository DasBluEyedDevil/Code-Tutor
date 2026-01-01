---
type: "THEORY"
title: "Understanding Semantic Versioning"
---


**What is Semantic Versioning?**

Semantic Versioning (SemVer) is a versioning scheme that gives meaning to version numbers. It follows the format MAJOR.MINOR.PATCH, where each component communicates specific information about changes.

**The Three Components**

```
2.4.1
| | |
| | +-- PATCH: Bug fixes, no new features
| +---- MINOR: New features, backward compatible
+------ MAJOR: Breaking changes
```

**When to Increment Each**

| Change Type | Increment | Example |
|-------------|-----------|----------|
| Bug fix | PATCH | 1.0.0 -> 1.0.1 |
| New feature (backward compatible) | MINOR | 1.0.1 -> 1.1.0 |
| Breaking change | MAJOR | 1.1.0 -> 2.0.0 |
| Security patch | PATCH | 2.0.0 -> 2.0.1 |
| API deprecation | MINOR | 2.0.1 -> 2.1.0 |
| API removal | MAJOR | 2.1.0 -> 3.0.0 |

**Version vs Build Number**

Flutter apps have two version identifiers:

1. **Version Name** (e.g., 2.4.1) - Human-readable, shown to users
2. **Build Number** (e.g., 45) - Machine-readable, always incrementing

```yaml
# pubspec.yaml
version: 2.4.1+45
#        |     |
#        |     +-- Build number
#        +-------- Version name
```

**Platform Requirements**

- **iOS**: Build number must increment for each App Store submission
- **Android**: versionCode must increment for each Play Store submission
- **Both**: Version name can stay same, but build number must increase

**Pre-release Versions**

For beta or alpha releases:

```
2.0.0-alpha.1   First alpha
2.0.0-beta.1    First beta
2.0.0-rc.1      Release candidate
2.0.0           Final release
```

**Best Practices**

1. Start at 1.0.0 for first public release
2. Never reuse version numbers
3. Document what changed in each version
4. Automate version bumping in CI/CD
5. Tag releases in git with version number

