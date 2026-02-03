# Phase 4: C# Course Audit - Research

**Researched:** 2026-02-02
**Domain:** C# 12/.NET 8 course content (with .NET 9/C# 13 contamination), ASP.NET Core, Blazor, .NET Aspire, Roslyn code execution
**Confidence:** HIGH (course structure verified via filesystem analysis; version conflicts verified via project files and content search)

## Summary

The C# course contains 24 modules with 132 lessons, 532 content files, and 132 challenges (128 with solution/starter pairs). It is a comprehensive path from absolute basics through ASP.NET Core, Blazor, .NET Aspire, clean architecture, authentication, and a full capstone project (ShopFlow e-commerce app). The course is well-structured with consistent content types and good analogy coverage (130/132 lessons have analogies).

The most critical finding is a **version mismatch between the version manifest and the actual course content**. The version manifest targets C# 12/.NET 8, but the course was actually written for C# 13/.NET 9. Evidence: (a) lesson titles reference C# 13 features (`params-collections-c-13`, `lock-type-c-13`, `implicit-index-access-c-13`), (b) lesson 09-07 explicitly teaches .NET 9-only APIs (`CountBy`, `AggregateBy`), (c) all capstone `.csproj` files target `net9.0` with .NET 9 package references, (d) Module 12 teaches `HybridCache` (a .NET 9 API), (e) Module 19 is titled "OpenAPI in .NET 9: Built-in Support", (f) 44 content files reference ".NET 9" vs only 15 referencing ".NET 8". This version target question must be resolved before planning begins.

The second critical finding is the **Roslyn executor version mismatch**. The WPF app uses `Microsoft.CodeAnalysis.CSharp.Scripting` version 4.8.0, which supports C# 12 (not C# 13). Challenges using C# 13 features (`params` collections, the new `Lock` type) may not compile. Additionally, Roslyn scripting runs code as scripts (top-level statements), not as compiled projects, which means ASP.NET Core, Blazor, EF Core, and similar framework-dependent challenges cannot execute in the app. Only pure C# logic challenges (Modules 01-10) can run in Roslyn.

The third finding is the **KEY_POINT enrichment scope**: only 1 KEY_POINT exists across 132 lessons (in lesson 14-03). CSRP-05 requires adding KEY_POINTs to all 132 lessons -- this is the single largest content addition task.

**Primary recommendation:** Resolve the .NET 8 vs .NET 9 version target decision first. The course content is overwhelmingly .NET 9 already, so updating the version manifest to .NET 9/C# 13 is the path of least resistance. Then execute the audit in 4-5 plans: structural review + version alignment, accuracy passes grouped by theme, KEY_POINT enrichment, and challenge validation.

## Standard Stack

The established technologies for this course domain:

### Core Runtime (Version Decision Required)

| Technology | Manifest Version | Actual Course Version | Resolution Needed |
|------------|-----------------|----------------------|-------------------|
| .NET | 8.0 (LTS) | 9.0 (STS) | **YES -- content written for .NET 9** |
| C# | 12 | 13 | **YES -- lesson titles reference C# 13** |

**Support timelines (critical context):**
- .NET 8 LTS: End of support November 10, 2026
- .NET 9 STS: End of support November 10, 2026 (same date, policy changed Sep 2025)
- .NET 10 LTS: Released November 11, 2025. End of support November 10, 2028

Both .NET 8 and .NET 9 reach end-of-support on the SAME day. .NET 10 is already released and is the next LTS.

### Framework Stack

| Technology | Manifest Version | Course Content Uses | Notes |
|------------|-----------------|---------------------|-------|
| ASP.NET Core | 8.0 | 9.0 APIs | Built-in OpenAPI (Module 19) is .NET 9 feature |
| Entity Framework Core | 8.0 | 9.0 patterns | HybridCache, compiled models referenced |
| Blazor | 8.0 | 8.0/9.0 mixed | Rendering modes lesson titled "Blazor Rendering Modes .NET 8" |
| xUnit | 2.x | 2.x | Consistent |
| .NET Aspire | 9.x | 9.x | Now at version 13.0 (aligned with .NET 10) |

### Version Decision: .NET 8 vs .NET 9 vs .NET 10

| Option | Pros | Cons |
|--------|------|------|
| Keep .NET 8 (manifest) | LTS, stable, longer runway if upgraded to .NET 10 later | Requires rewriting ~50+ files that reference .NET 9 features; must remove CountBy/AggregateBy lesson, HybridCache, OpenAPI built-in; capstone must be downgraded |
| Update to .NET 9 (match content) | Minimal content changes; content already written for .NET 9; Roslyn 4.12+ supports C# 13 | STS -- ends Nov 2026 same as .NET 8; not future-proof |
| Update to .NET 10 (latest LTS) | Future-proof (support until Nov 2028); C# 14 features; file-based apps | Requires verifying ALL content against .NET 10; C# 14 features may not yet be in Roslyn scripting; .NET Aspire jumped to 13.0 |

**Recommendation:** Update version manifest to **.NET 9 / C# 13** to match the existing course content. This requires the fewest changes. In a future cycle, the course can be updated to .NET 10. The current content was clearly written targeting .NET 9, and forcing it back to .NET 8 would require removing entire lessons and features.

## Architecture Patterns

### Course Module Progression
```
Modules 01-04: Language fundamentals (basics, variables, control flow, loops)     -- 21 lessons
Module 05:     Collections (arrays, List, Dictionary, C# 12/13 features)          -- 6 lessons
Module 06:     Methods/OOP Intro (constructors, properties, methods, access)       -- 8 lessons
Module 07:     OOP (inheritance, polymorphism, abstract, interfaces, records)      -- 7 lessons
Module 08:     Advanced OOP (exceptions, namespaces, NuGet)                        -- 5 lessons
Module 09:     LINQ (Where, Select, OrderBy, GroupBy, .NET 9 CountBy/AggregateBy) -- 7 lessons
Module 10:     Async (async/await, Task<T>, C# 13 Lock type)                      -- 5 lessons
Module 11:     ASP.NET Core Web APIs (minimal APIs, DI, routing, auth)             -- 6 lessons
Module 12:     Databases (EF Core, migrations, HybridCache)                        -- 8 lessons
Module 13:     Blazor (components, parameters, events, binding, QuickGrid)         -- 7 lessons
Module 14:     Blazor + Aspire Deployment (CRUD, Aspire, Git, Azure)               -- 6 lessons
Module 15:     Unit Testing (xUnit, mocking, integration tests, TDD)               -- 4 lessons
Module 16:     .NET Aspire Advanced (service discovery, OpenTelemetry, Polly)       -- 5 lessons
Module 17:     Native AOT & Performance (AOT, source generators, benchmarking)      -- 5 lessons
Module 18:     Clean Architecture (4 layers, domain, application, infrastructure)   -- 4 lessons
Module 19:     Modern API Dev (OpenAPI/Scalar, versioning, Kiota)                   -- 5 lessons
Module 20:     Authentication (Identity, registration/login, JWT, refresh tokens)   -- 5 lessons
Module 21:     External Auth (OAuth, Google/Microsoft/GitHub sign-in)                -- 4 lessons
Module 22:     Authorization (roles, claims, policies, resource-based)               -- 4 lessons
Module 23:     CI/CD (GitHub Actions, Docker, environments, secrets)                 -- 5 lessons
Module 24:     Capstone (ShopFlow: catalog, cart, orders, deployment)                -- 5 lessons
```

### Content File Structure (Per Lesson)
```
lesson-XX/
  lesson.json           # Metadata (id, title, description, order)
  content/
    01-analogy.md       # ANALOGY section (130/132 lessons have this)
    02-example.md       # EXAMPLE section (all 132 lessons)
    03-theory.md        # THEORY section (most lessons, some have 2+)
    04-warning.md       # WARNING section (105/132 lessons)
    05-architecture.md  # NON-STANDARD filename (type: THEORY) -- 11 files
    05-real_world.md    # NON-STANDARD filename (type: ANALOGY) -- 8 files
    05-deep_dive.md     # NON-STANDARD filename (type: THEORY) -- 5 files
    NN-key_point.md     # KEY_POINT section (ONLY 1 lesson has this!)
  challenges/
    01-challenge-name/
      challenge.json    # Test cases, hints, commonMistakes, difficulty
      solution.cs       # Reference solution (top-level statements for Roslyn)
      starter.cs        # Starting code template
```

### Content Type Distribution (532 files)
| Type (Frontmatter) | Filename Pattern | Count | Standard Filename? |
|---------------------|-----------------|-------|-----------|
| THEORY | `*-theory.md` | 129 | YES |
| EXAMPLE | `*-example.md` | 143 | YES |
| ANALOGY | `*-analogy.md` | 130 | YES |
| WARNING | `*-warning.md` | 105 | YES |
| THEORY | `*-architecture.md` | 11 | **FILENAME MISMATCH** (should be theory.md) |
| ANALOGY | `*-real_world.md` | 8 | **FILENAME MISMATCH** (should be analogy.md) |
| THEORY | `*-deep_dive.md` | 5 | **FILENAME MISMATCH** (should be theory.md) |
| KEY_POINT | `*-key_point.md` | 1 | YES (but only 1 exists!) |

**Non-standard filename count: 24 files** need renaming (11 architecture + 8 real_world + 5 deep_dive). Phase 1 migrated the frontmatter types but did NOT rename filenames -- same pattern as the JavaScript course.

### Code Execution Architecture (Roslyn)
The WPF app executes C# challenges via `RoslynCSharpExecutor`:
1. Uses `CSharpScript.EvaluateAsync()` from `Microsoft.CodeAnalysis.CSharp.Scripting` 4.8.0
2. Default imports: `System`, `System.Collections.Generic`, `System.IO`, `System.Linq`, `System.Text`, `System.Text.RegularExpressions`, `System.Threading.Tasks`
3. Default references: `typeof(object).Assembly`, `typeof(Console).Assembly`, `typeof(Enumerable).Assembly`
4. 30-second timeout
5. Captures `Console.Out` for output comparison

**Critical limitations:**
- Roslyn 4.8.0 supports **C# 12 max** -- C# 13 features will not compile
- Runs code as **scripts** (top-level statements) -- no project context
- Only references core BCL assemblies -- no ASP.NET Core, EF Core, Blazor
- Challenges in Modules 11-24 that reference framework-specific APIs cannot execute
- Only Modules 01-10 (pure C# fundamentals) can run in the Roslyn executor

### Capstone Project (ShopFlow)
```
capstone/
  ShopFlow.sln                          # Solution file
  .github/workflows/ci.yml             # CI pipeline (targets .NET 9)
  .config/                              # Tool configuration
  src/
    ShopFlow.Api/                       # ASP.NET Core Web API (net9.0)
    ShopFlow.Application/               # Application layer (net9.0)
    ShopFlow.Core/                      # Core/shared (net9.0)
    ShopFlow.Domain/                    # Domain layer (net9.0)
    ShopFlow.Infrastructure/            # Infrastructure layer (net9.0, EF Core 9.0)
    ShopFlow.Web/                       # Blazor Server (net9.0)
    ShopFlow.Web.Client/                # Blazor WebAssembly client (net9.0)
  tests/
    ShopFlow.Tests.Unit/                # xUnit tests (net9.0)
    ShopFlow.Tests.Integration/         # Integration tests with PostgreSQL (net9.0)
```

The capstone has 87 C# source files, uses clean architecture (4 layers), PostgreSQL database with EF Core migrations, JWT authentication, and Blazor for the UI. All project files target `net9.0` with .NET 9 package references (e.g., `Microsoft.AspNetCore.Authentication.JwtBearer 9.0.0`).

### Anti-Patterns to Avoid
- **Targeting .NET 8 in the manifest while content uses .NET 9:** Creates constant confusion during accuracy passes
- **Assuming Roslyn can execute ASP.NET/Blazor challenges:** It cannot -- only pure C# logic challenges work
- **Adding KEY_POINTs in the accuracy pass:** KEY_POINT enrichment is a separate, massive task (131 new files) and should be its own plan
- **Treating C# 13 features as C# 12:** `params` collections, the new `Lock` type, and implicit index access are C# 13 only

## Don't Hand-Roll

Problems that have existing solutions -- do not create custom approaches:

| Problem | Don't Build | Use Instead | Why |
|---------|-------------|-------------|-----|
| Non-standard filename rename | Manual file-by-file | Scripted batch rename (24 files) | Same approach used for JS course (143 files) |
| Version reference sweep | Reading each file manually | Grep/search across all 532 content files | Automated sweep catches all ".NET 8", ".NET 9", "C# 12", "C# 13" references |
| KEY_POINT generation | Manual per-lesson | Batch template with per-lesson customization | 131 new KEY_POINT files needed -- bulk approach |
| Challenge validation | Manual review | Execute solutions via Roslyn for M01-10, review-only for M11-24 | Roslyn can only execute pure C# challenges |
| Content type verification | Manual frontmatter check | E2E validation tests (already exist from Phase 1) | `CourseContentValidationTests.cs` validates all 6 standard types |

**Key insight:** The C# course has fewer non-standard files than Java (24 vs 0) or JavaScript (143 filename mismatches). The biggest work item is KEY_POINT enrichment (131 new files) and version alignment (deciding .NET 8 vs .NET 9).

## Common Pitfalls

### Pitfall 1: Version Manifest vs Content Mismatch
**What goes wrong:** The manifest says .NET 8/C# 12 but the content teaches .NET 9/C# 13 features. Accuracy reviewers may flag correct .NET 9 content as "wrong" because the manifest says .NET 8.
**Why it happens:** The course was written for .NET 9 but the version manifest was set to .NET 8 during Phase 1 (perhaps reflecting the LTS target at that time).
**How to avoid:** Resolve the version target FIRST, before any accuracy work begins. Update the manifest to match the actual content.
**Evidence:**
- 44 files reference ".NET 9" vs 15 referencing ".NET 8"
- Lesson titles include "C# 13" in 3 lesson directory names
- All capstone .csproj files target `net9.0`
- `CountBy`/`AggregateBy` lesson explicitly says ".NET 9 required!"
- `HybridCache` is a .NET 9 API
- OpenAPI built-in support is a .NET 9 feature

### Pitfall 2: Roslyn Executor Cannot Run C# 13 Code
**What goes wrong:** Challenges using C# 13 features fail to compile in the WPF app's Roslyn executor.
**Why it happens:** The WPF app uses `Microsoft.CodeAnalysis.CSharp.Scripting` 4.8.0 which supports C# 12 max. C# 13 requires Roslyn 4.12+.
**How to avoid:** Either (a) upgrade the Roslyn NuGet package to 4.12+ (app change, possibly out of scope), or (b) ensure challenges with C# 13 features have fallback patterns that work in C# 12.
**Affected challenges:** Lessons in Modules 05 (`implicit-index-access-c-13`), 06 (`params-collections-c-13`), and 10 (`lock-type-c-13`) have challenges using C# 13 features.

### Pitfall 3: Roslyn Cannot Execute Framework-Dependent Challenges
**What goes wrong:** Challenges in Modules 11-24 reference ASP.NET Core, EF Core, Blazor, or other framework APIs. These cannot execute in the Roslyn scripting host because it only references core BCL assemblies.
**Why it happens:** Roslyn scripting runs code in an isolated context without project-level dependencies.
**How to avoid:** Accept that Modules 11-24 challenges are "conceptual" or "review-only" in the app -- they teach the patterns but cannot be executed. Challenge validation for these modules must be manual code review, not execution.
**Scope:** ~75 challenges in Modules 11-24 cannot be executed via Roslyn.

### Pitfall 4: Module 12 Missing All WARNING Sections
**What goes wrong:** All 8 lessons in Module 12 (File I/O, Databases, Caching) have zero WARNING sections. This is the only module in the entire course with zero warnings.
**Why it happens:** Content generation inconsistency.
**How to avoid:** The accuracy pass for Module 12 should add WARNING sections. Common database pitfalls (N+1 queries, connection pool exhaustion, migration conflicts) are natural WARNING topics.
**Distribution of missing warnings:**
  - Module 12: 8/8 lessons (all)
  - Module 14: 2/6 lessons
  - Module 18: 3/4 lessons
  - Module 22: 3/4 lessons
  - Module 23: 3/5 lessons
  - Module 24: 5/5 lessons (all)
  - Total: 27 lessons missing WARNING sections

### Pitfall 5: Non-Standard Filenames (24 files)
**What goes wrong:** Files named `architecture.md`, `real_world.md`, `deep_dive.md` have correct frontmatter types (THEORY/ANALOGY) but inconsistent filenames.
**Why it happens:** Phase 1 migrated frontmatter types but intentionally did not rename files.
**How to avoid:** Batch rename in the first plan (same approach as JS audit plan 03-01).
**Distribution:**
  - `architecture.md` (type: THEORY): 11 files across modules 02, 03, 05, 07, 18, 20, 21, 22, 23, 24
  - `real_world.md` (type: ANALOGY): 8 files across modules 01, 02, 03, 06, 18, 24
  - `deep_dive.md` (type: THEORY): 5 files across modules 01, 04, 18, 20, 24

### Pitfall 6: .NET Aspire Version Drift
**What goes wrong:** The course teaches .NET Aspire 9.x patterns, but Aspire has since released version 13.0 (aligned with .NET 10). APIs and project structure have changed significantly.
**Why it happens:** .NET Aspire releases independently and moves faster than .NET itself.
**How to avoid:** Since the course targets .NET 9, keep Aspire 9.x content as-is. Note in the version manifest that Aspire has moved to 13.0 for .NET 10.
**Impact:** Module 14 and Module 16 teach Aspire patterns that are still valid for .NET 9 but are outdated for .NET 10.

### Pitfall 7: Estimated Hours Are Unrealistic
**What goes wrong:** The course.json says 29 estimated hours for 24 modules and 132 lessons. Module-level estimates are also low (most say 2 hours).
**Why it happens:** AI-generated metadata with no calibration against reality.
**How to avoid:** Recalibrate estimated hours during the structural review. A course of this scope likely takes 80-120 hours.

## Code Examples

### Top-Level Statement Pattern (Used in All Challenges)
```csharp
// Source: All challenge solution.cs files in the C# course
// Roslyn scripting executes code as scripts -- no class wrapping needed
Console.WriteLine("Hello, World!");

// Variables, logic, collections all work directly
var items = new List<string> { "apple", "banana", "cherry" };
foreach (var item in items)
    Console.WriteLine(item);
```

### C# 13 Feature: params Collections (Lesson 06-08)
```csharp
// Source: Module 06, Lesson 08 content
// C# 13 feature -- requires Roslyn 4.12+ to compile
void PrintAll(params IEnumerable<string> items)
{
    foreach (var item in items)
        Console.WriteLine(item);
}

// Can now pass any IEnumerable, not just arrays
PrintAll(new List<string> { "a", "b" });
PrintAll(new HashSet<string> { "c", "d" });
```

### .NET 9 LINQ: CountBy / AggregateBy (Lesson 09-07)
```csharp
// Source: Module 09, Lesson 07 challenge solution
// .NET 9 only -- not available in .NET 8
var sales = new[]
{
    new { Region = "North", Amount = 500m },
    new { Region = "South", Amount = 300m },
    new { Region = "North", Amount = 200m }
};

var countPerRegion = sales.CountBy(s => s.Region);
var totalPerRegion = sales.AggregateBy(
    s => s.Region,
    0m,
    (sum, sale) => sum + sale.Amount);
```

### Minimal API Pattern (ASP.NET Core, Module 11+)
```csharp
// Source: Module 11 and Module 19 content
// Cannot execute in Roslyn -- requires ASP.NET Core project context
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi(); // .NET 9 built-in OpenAPI

var app = builder.Build();
app.MapOpenApi(); // Expose OpenAPI document

app.MapGet("/products", () => new[] {
    new Product(1, "Laptop", 999.99m)
})
.WithName("GetProducts")
.WithTags("Products");

app.Run();
```

## Quantitative Audit Summary

### Scope Numbers
| Metric | Count | vs. Java (Phase 2) | vs. JS (Phase 3) |
|--------|-------|---------------------|-------------------|
| Total modules | 24 | 16 (+50%) | 21 (+14%) |
| Total lessons | 132 | 96 (+38%) | 132 (same) |
| Total content files (.md) | 532 | 678 (-22%) | 707 (-25%) |
| Total challenges | 132 | 182 (-27%) | 151 (-13%) |
| Files with non-standard filenames | 24 | 0 | 143 |
| Lessons missing KEY_POINT | 131 | ~29 | 115 |
| Lessons missing ANALOGY | 2 | 95 | 32 |
| Lessons missing WARNING | 27 | ~35 | ~4 |

### Module-by-Module Assessment

| Module | Lessons | Challenges | Key Issues |
|--------|---------|------------|------------|
| 01 Getting Started | 5 | 5 | References ".NET 9" in 3 files; `.NET 8` in 1 file |
| 02 Variables/Data Types | 6 | 6 | 2 non-standard filenames (architecture, real_world) |
| 03 Control Flow | 5 | 5 | 2 non-standard filenames |
| 04 Loops | 5 | 5 | 1 non-standard filename (deep_dive) |
| 05 Collections | 6 | 6 | C# 12 collection expressions + C# 13 implicit index access (version feature split) |
| 06 Methods/Functions | 8 | 8 | C# 13 params collections lesson; 1 non-standard filename |
| 07 OOP Basics | 7 | 7 | C# 12 primary constructors; 1 non-standard filename |
| 08 Advanced OOP | 5 | 5 | Clean; mentions "net8.0" in debug paths |
| 09 LINQ | 7 | 7 | **.NET 9 CountBy/AggregateBy lesson** -- requires .NET 9 |
| 10 Async | 5 | 5 | **C# 13 Lock type** -- requires .NET 9 runtime |
| 11 ASP.NET Core APIs | 6 | 6 | References both ".NET 8" and ".NET 9"; framework-dependent |
| 12 Databases/Caching | 8 | 8 | **All 8 missing WARNING**; HybridCache is .NET 9; framework-dependent |
| 13 Blazor | 7 | 7 | Lesson titles reference ".NET 8" features; framework-dependent |
| 14 Blazor + Aspire | 6 | 5 | **Only lesson with KEY_POINT** (14-03); Aspire 9.x patterns |
| 15 Testing | 4 | 4 | xUnit patterns; mocking; framework-dependent |
| 16 Aspire Advanced | 5 | 5 | Aspire 9.x; may need freshness check; framework-dependent |
| 17 Native AOT | 5 | 5 | References ".NET 9"; framework-dependent |
| 18 Clean Architecture | 4 | 4 | 3 non-standard filenames; 3/4 missing WARNING; all .csproj examples say net9.0 |
| 19 OpenAPI/Scalar | 5 | 5 | **Lesson title says ".NET 9"**; built-in OpenAPI is .NET 9 feature |
| 20 Authentication | 5 | 5 | 2 non-standard filenames; Identity framework patterns |
| 21 External Auth | 4 | 4 | 1 non-standard filename; OAuth patterns |
| 22 Authorization | 4 | 4 | 1 non-standard filename; 3/4 missing WARNING |
| 23 CI/CD | 5 | 5 | 1 non-standard filename; CI config references .NET 9 |
| 24 Capstone | 5 | 5 | 5 non-standard filenames; **5/5 missing WARNING**; 2/5 missing ANALOGY |

### Non-Standard Filename Distribution (24 files)
| Filename Pattern | Frontmatter Type | Count | Modules |
|------------------|-----------------|-------|---------|
| `*-architecture.md` | THEORY | 11 | 02, 03, 05, 07, 18, 20, 21, 22, 23, 24 |
| `*-real_world.md` | ANALOGY | 8 | 01, 02, 03, 06, 18, 24 |
| `*-deep_dive.md` | THEORY | 5 | 01, 04, 18, 20, 24 |

## State of the Art

| Old/Current in Course | Latest State | When Changed | Impact on Course |
|----------------------|-------------|-------------|------------------|
| .NET 8 in manifest | .NET 10 released Nov 2025 (LTS) | Nov 2025 | Manifest says .NET 8 but content is .NET 9; .NET 10 is now latest LTS |
| .NET 9 in content | .NET 9 STS, EOL Nov 2026 | Nov 2024 | Content is accurately .NET 9; decide whether to stay or upgrade |
| .NET Aspire 9.x | .NET Aspire 13.0 released | Nov 2025 | Aspire aligned with .NET 10; course patterns still valid for .NET 9 |
| Roslyn 4.8.0 (C# 12) | Roslyn 4.14.0 (C# 14) | Nov 2025 | App Roslyn version limits challenge C# features |
| xUnit 2.x | xUnit 2.x (stable) | Ongoing | No significant changes needed |
| EF Core 9.0 | EF Core 10.0 | Nov 2025 | Course EF Core patterns still valid for .NET 9 target |

**Deprecated/outdated items found in current content:**
- Version manifest targeting .NET 8 while content targets .NET 9 (mismatch)
- `course.json` says `"minimumRuntimeVersion": ".NET 8.0"` while capstone targets .NET 9
- `course.json` says `"estimatedHours": 29` which is unrealistically low for 132 lessons
- Some files reference ".NET 8" paths like `obj/Debug/net8.0/` while most reference .NET 9
- `refactor_course.py` artifact in course root directory

## Suggested Plan Structure (5 plans)

Based on patterns from Java audit (8 plans for 96 lessons) and JS audit (7 plans for 132 lessons):

| Plan | Focus | Scope | Rationale |
|------|-------|-------|-----------|
| 04-01 | Version alignment + filename migration + structural review | All 24 modules (scripted + analysis) | Resolve .NET 8 vs .NET 9 in manifest, rename 24 non-standard files, assess progression, update course.json. Must complete first. |
| 04-02 | Accuracy pass: Fundamentals (M01-10) | 59 lessons, ~282 content files | Core C# -- verify against C# 12/13 and .NET 8/9 target, pure language features |
| 04-03 | Accuracy pass: Web/Blazor/Data/Testing (M11-15) | 31 lessons, ~135 content files | ASP.NET Core, EF Core, Blazor, xUnit -- framework-specific accuracy |
| 04-04 | Accuracy pass: Advanced/Auth/DevOps/Capstone (M16-24) | 42 lessons, ~175 content files | Aspire, AOT, Clean Architecture, Auth, CI/CD, capstone completeness |
| 04-05 | KEY_POINT enrichment + challenge validation | All 132 lessons + all 132 challenges | Add 131 KEY_POINT sections; validate M01-10 challenges via Roslyn; review M11-24 challenges manually |

**Why 5 plans instead of 4:** The roadmap suggests 4 plans, but the KEY_POINT enrichment is massive (131 new files) and should not be bundled with accuracy work. Separating it ensures accuracy passes focus on correctness while KEY_POINT enrichment focuses on pedagogical value.

**Why not more plans:** The C# course has fewer structural issues than Java (no frontend framework decision, capstone exists and is complete, analogies already present in 130/132 lessons). The main work is version alignment and KEY_POINT enrichment.

## Open Questions

Things that could not be fully resolved:

1. **Version Target: .NET 8 vs .NET 9**
   - What we know: The version manifest says .NET 8/C# 12. The actual content (44 files, lesson titles, capstone project files, framework APIs used) targets .NET 9/C# 13.
   - What's unclear: Was the .NET 8 target intentional (conservative choice) or an oversight (manifest not updated when content was written)?
   - Recommendation: Update manifest to .NET 9/C# 13 to match existing content. Rewriting to .NET 8 would require removing entire lessons and significant content changes.
   - **Impact if .NET 9:** Update version-manifest.json and course.json only (minimal changes)
   - **Impact if .NET 8:** Remove/rewrite lesson 09-07 (CountBy/AggregateBy), lesson 10-05 (Lock type), lesson 05-06 (implicit index access), lesson 06-08 (params collections), lesson 19-01 (OpenAPI built-in), lesson 12-08 (HybridCache); downgrade all capstone .csproj files; massive scope increase

2. **Roslyn Executor Upgrade**
   - What we know: The WPF app uses Roslyn 4.8.0 (C# 12 max). The app targets .NET 8.0 Windows.
   - What's unclear: Whether upgrading Roslyn to 4.12+ (for C# 13 support) is in scope for this content audit phase.
   - Recommendation: Flag as a separate app upgrade task, not part of the content audit. Content should be written correctly for the target version; the app can be upgraded independently.

3. **Missing WARNING Sections (27 lessons)**
   - What we know: 27 lessons across 6 modules have no WARNING section, with Module 12 and Module 24 having zero warnings across all their lessons.
   - What's unclear: Whether adding warnings to all 27 lessons is in scope or just flagging them.
   - Recommendation: Add warnings during the accuracy passes where natural pitfalls exist (database modules, deployment modules). Not every lesson needs a WARNING.

4. **Capstone Buildability**
   - What we know: ShopFlow has 87 C# files, clean architecture, PostgreSQL with EF Core, JWT auth, Blazor UI, and a CI pipeline.
   - What's unclear: Whether the capstone actually builds and tests pass. The CI workflow exists but may not have been executed.
   - Recommendation: Attempt to build the capstone during challenge validation (Plan 04-05) to verify CSRP-04.

5. **refactor_course.py Artifact**
   - What we know: A Python script exists at the course root, used for initial course generation.
   - What's unclear: Whether it should be deleted or preserved.
   - Recommendation: Delete during Plan 04-01 as cleanup.

## Sources

### Primary (HIGH confidence)
- Filesystem analysis of `C:/Users/dasbl/Downloads/Code-Tutor/content/courses/csharp/` -- direct file counting, content inspection, and structure analysis
- `content/version-manifest.json` -- pinned version targets showing .NET 8/C# 12
- `native-app-wpf/CodeTutor.Wpf.csproj` -- Roslyn 4.8.0 package reference, net8.0-windows target
- `native-app-wpf/Services/Executors/RoslynCSharpExecutor.cs` -- CSharpScript.EvaluateAsync execution model
- `content/courses/csharp/capstone/src/ShopFlow.Api/ShopFlow.Api.csproj` -- net9.0 target, .NET 9 package references
- Phase 1 Summary (`01-03-SUMMARY.md`) -- confirmed 24 type migrations for C# (ARCHITECTURE->THEORY, REAL_WORLD->ANALOGY, DEEP_DIVE->THEORY)
- Phase 2 Research (`02-RESEARCH.md`) -- Java audit patterns and plan structure
- Phase 3 Research (`03-RESEARCH.md`) -- JavaScript audit patterns and plan structure

### Secondary (MEDIUM confidence)
- [.NET Support Policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core) -- .NET 8 and .NET 9 both EOL November 2026
- [.NET STS Extended to 24 Months](https://devblogs.microsoft.com/dotnet/dotnet-sts-releases-supported-for-24-months/) -- Microsoft policy change September 2025
- [.NET 10 Announcement](https://devblogs.microsoft.com/dotnet/announcing-dotnet-10/) -- .NET 10 LTS released November 2025
- [C# 13 Features - Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-13) -- params collections, Lock type, implicit indexer
- [.NET 9 What's New](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9/overview) -- CountBy, AggregateBy, HybridCache, built-in OpenAPI
- [Roslyn NuGet Packages](https://github.com/dotnet/roslyn/blob/main/docs/wiki/NuGet-packages.md) -- Version-to-language mapping
- [NuGet: Microsoft.CodeAnalysis.CSharp 4.8.0](https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp/4.8.0) -- Ships with .NET 8, C# 12 support
- [.NET Aspire 9.5 Release](https://learn.microsoft.com/en-us/dotnet/aspire/whats-new/dotnet-aspire-9.5) -- Latest Aspire in 9.x line
- [.NET Aspire Roadmap](https://aspireify.net/a/250727/aspire-roadmap-(2025-%E2%86%92-2026)) -- Aspire 13.0 aligned with .NET 10

### Tertiary (LOW confidence)
- [InfoQ: .NET 10 Release](https://www.infoq.com/news/2025/11/dotnet-10-release/) -- .NET 10 features overview
- Various Medium articles on C# 13 features (cross-verified with Microsoft Learn)

## Metadata

**Confidence breakdown:**
- Course structure and file counts: **HIGH** -- directly measured via filesystem
- Content type distribution: **HIGH** -- directly measured via frontmatter grep
- Version mismatch (.NET 8 manifest vs .NET 9 content): **HIGH** -- verified via .csproj files, lesson titles, content references
- Roslyn version limitation: **HIGH** -- verified via NuGet package version in .csproj and Roslyn documentation
- .NET support timeline: **HIGH** -- verified via Microsoft official support policy
- .NET Aspire version state: **MEDIUM** -- web search verified, but Aspire releases frequently
- KEY_POINT count: **HIGH** -- only 1 file found via filesystem search
- Challenge executability assessment: **MEDIUM** -- based on Roslyn executor code analysis, not tested

**Research date:** 2026-02-02
**Valid until:** 2026-03-04 (30 days -- .NET ecosystem is stable; Aspire and tooling may update)
