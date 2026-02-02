# Technology Stack

**Project:** Code Tutor - Interactive Code Education Platform with Local LLM Tutoring
**Researched:** 2026-02-02
**Mode:** Ecosystem (subsequent milestone -- enhancement, not rewrite)

---

## Executive Summary

Code Tutor's existing stack (.NET 8.0, C# 13, WPF, AvalonEdit, ONNX Runtime GenAI 0.5.2) is functional but has significant version lag. The ONNX Runtime GenAI package is 12+ major versions behind current (0.5.2 vs 0.11.4), and the Phi-4 model ecosystem has expanded dramatically since the project was last updated. The six taught languages have all had major releases in 2025. The upgrade path is clear and non-breaking for the app shell; the content freshness gap is the more critical issue.

**Key recommendation:** Upgrade ONNX Runtime GenAI and evaluate Phi-4-mini-reasoning alongside Phi-4-mini-instruct. Upgrade Roslyn and AvalonEdit. Do NOT upgrade to .NET 10 yet -- stay on .NET 8.0 LTS through this milestone to avoid unnecessary churn. Focus effort on content accuracy and LLM prompt engineering over framework upgrades.

---

## Current Stack (What Exists)

| Component | Current Version | Latest Stable | Gap |
|-----------|----------------|---------------|-----|
| .NET Runtime | 8.0 (LTS) | 10.0 (LTS) | 2 majors behind, but 8.0 supported through Nov 2026 |
| C# Language | 13.0 | 14.0 | 1 minor, C# 14 features not critical for this app |
| AvalonEdit | 6.3.0.90 | 6.3.1.120 | Patch update available |
| ONNX Runtime GenAI (DirectML) | 0.5.2 | 0.11.4 | **12 releases behind -- critical upgrade** |
| Roslyn Scripting | 4.8.0 | 5.0.0 | 1 major behind |
| System.Text.Json | 8.0.5 | Current for .NET 8 | OK |
| Microsoft.Extensions.DI | 8.0.0 | Current for .NET 8 | OK |
| xUnit | 2.6.3 | Needs check | Likely behind |

---

## Recommended Stack (Prescriptive)

### A. Application Shell -- Keep and Upgrade Selectively

**Confidence: HIGH** (verified against NuGet, official docs)

| Technology | Current | Target | Why | Priority |
|------------|---------|--------|-----|----------|
| .NET | 8.0 | **8.0** (stay) | LTS through Nov 2026. Upgrading to .NET 10 would require retesting the entire deployment pipeline and Inno Setup installer. Not worth the risk for this milestone. Upgrade to .NET 10 in a future milestone. | Do not change |
| C# | 13.0 | **13.0** (stay) | C# 14's extension members and null-conditional assignment are nice-to-have, not need-to-have. Staying on 13 avoids requiring .NET 10 SDK. | Do not change |
| WPF | (built-in) | (built-in) | No change needed. WPF is the correct choice for a Windows desktop education app. Cross-platform is explicitly out of scope. | Do not change |
| AvalonEdit | 6.3.0.90 | **6.3.1.120** | Patch update with bug fixes. Low risk. | Low |
| ONNX Runtime GenAI DirectML | 0.5.2 | **0.11.4** | Critical upgrade. 0.11.x adds Phi-4 family support, continuous batching, CUDA graph optimization, C# binding for `GetNextTokens`, and fixes Phi-4 multimodal preprocessing bugs. The 0.5.x version predates official Phi-4 support. | **HIGH** |
| Roslyn Scripting | 4.8.0 | **5.0.0** | Enables C# 14 features in student code execution. Even if app stays on C# 13, students benefit from latest language features when running code. | Medium |
| System.Text.Json | 8.0.5 | **8.0.5** (stay) | Current for .NET 8. No action needed. | Do not change |
| Microsoft.Extensions.DI | 8.0.0 | **8.0.0** (stay) | Current for .NET 8. No action needed. | Do not change |

**Resulting csproj changes:**
```xml
<PackageReference Include="AvalonEdit" Version="6.3.1.120" />
<PackageReference Include="Microsoft.CodeAnalysis.CSharp.Scripting" Version="5.0.0" />
<PackageReference Include="Microsoft.ML.OnnxRuntimeGenAI.DirectML" Version="0.11.4" />
<!-- Keep unchanged: -->
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.0" />
<PackageReference Include="System.Text.Json" Version="8.0.5" />
```

### B. Local LLM Tutoring -- Upgrade Model and Prompt Strategy

**Confidence: HIGH** (verified against HuggingFace model cards, Microsoft Azure blog, ONNX Runtime releases)

#### Model Selection

| Model | Parameters | Purpose | Recommendation |
|-------|-----------|---------|----------------|
| Phi-4-mini-instruct | 3.8B | General-purpose instruction following | **Keep as primary tutor model.** Best for explaining code concepts across 6 languages, debugging help, progressive hints. General-purpose instruction following is exactly what a code tutor needs. |
| Phi-4-mini-reasoning | 3.8B | Mathematical/logical reasoning | **Add as optional secondary model.** Superior for step-by-step problem decomposition. Use when student is stuck on algorithm/logic challenges. Do NOT use for general concept explanation -- it was trained exclusively on math and underperforms on general instruction. |
| Phi-4 (14B) | 14B | Full-size Phi-4 | **Do not use.** 14B requires ~9 GB VRAM (4-bit quantized) and 28 GB RAM. Too heavy for student machines. The 3.8B mini models run on consumer hardware. |
| Phi-4-multimodal-instruct | 14B | Text + image + audio | **Do not use.** Overkill for code tutoring. No image/audio input needed. |

**Current model path:** `models/phi4/gpu/gpu-int4-rtn-block-32`
**Recommendation:** Update model download to use `microsoft/Phi-4-mini-instruct-onnx` from HuggingFace with the `cpu_and_mobile/cpu-int4-rtn-block-32-acc-level-4` variant (for broadest hardware compatibility) OR the `gpu/gpu-int4-rtn-block-32` variant (for DirectML acceleration). Both are available in ONNX format.

#### Prompt Engineering Strategy

**Confidence: MEDIUM** (based on 2025 research papers on LLM tutoring, not yet validated in Code Tutor specifically)

Research on LLM-powered tutoring (LeafTutor, MWPTutor, CodeHelp, Physics-STAR) converges on these patterns:

1. **System prompt with pedagogical guardrails:** Tell the model its role, what it must NOT do (give complete solutions), and what it SHOULD do (ask guiding questions, give progressive hints).

2. **Structured context injection:** Include in the prompt:
   - Current lesson title and learning objectives
   - The specific concept being taught
   - Student's current code (if debugging)
   - Error messages (if any)
   - Previous hints given in this session (to avoid repetition)

3. **Progressive hint escalation:**
   - Hint level 1: Conceptual question ("What does a for loop do with this condition?")
   - Hint level 2: Directional nudge ("Look at line 5 -- what value does `i` have after the first iteration?")
   - Hint level 3: Pseudo-code scaffold ("Try: for each item in list, check if...")
   - Hint level 4: Reveal approach (only after 3 failed attempts)

4. **RAG for course context:** Small models benefit enormously from retrieval-augmented generation. Injecting the relevant lesson content into the prompt context enables Phi-4-mini-instruct to answer questions about specific course material accurately, even though it was not trained on that content. Research shows RAG-enhanced small models can match GPT-4 on domain-specific Q&A.

**What NOT to do:**
- Do NOT let the model see the solution code for challenges. This prevents solution leakage.
- Do NOT use Phi-4-mini-reasoning for general concept explanation. It was trained exclusively on math and will produce poor results for "What is a class in Java?" type questions.
- Do NOT try to fine-tune the model. The ONNX Runtime GenAI pipeline does not support fine-tuning. Use prompt engineering and RAG instead.

### C. UI Enhancement Libraries -- Add Selectively

**Confidence: HIGH** (verified against NuGet, GitHub repos)

| Library | Version | Purpose | Recommendation |
|---------|---------|---------|----------------|
| MaterialDesignThemes | 5.3.0 | Material Design 3 styling for WPF | **Recommended.** 16K+ GitHub stars, MIT license, active maintenance. Provides modern cards, buttons, dialogs, progress indicators, and snackbars. Material Design 3 look feels native to education apps. Adds polish without rewriting views. |
| CommunityToolkit.Mvvm | 8.4.0 | MVVM source generators, ObservableProperty, RelayCommand | **Recommended.** Microsoft-maintained, MIT license. Eliminates boilerplate in ViewModels with `[ObservableProperty]` and `[RelayCommand]` attributes. Worth adding incrementally -- no need to rewrite existing ViewModels, just use for new ones. |
| Markdig | 0.44.0 | Markdown-to-HTML parsing | **Recommended if not already used.** The app renders markdown content. Markdig is the gold standard .NET markdown parser: MIT license, CommonMark compliant, extensible, 100x faster than MarkdownSharp. |
| XAML-Math | (latest) | LaTeX math formula rendering | **Optional.** Only needed if courses include math notation. Low priority for code education. |
| LiveCharts | (latest) | Interactive charts | **Optional.** Could visualize student progress, but not critical for this milestone. |

**What NOT to use:**
- **MahApps.Metro:** Overlaps with MaterialDesignThemes. Pick one design system, not two. MaterialDesignThemes is more actively maintained and has Material Design 3 support.
- **Telerik / Infragistics / DevExpress commercial suites:** Expensive, overkill for an education app. Open-source libraries cover all needed controls.
- **WPF UI (lepo.co):** Fluent design, not Material. Less mature than MaterialDesignThemes. Stick with one design language.

### D. Content Validation and Authoring Tools

**Confidence: MEDIUM** (these are recommendations for tooling to build, not off-the-shelf products)

| Tool/Library | Version | Purpose | Recommendation |
|-------------|---------|---------|----------------|
| JsonSchema.Net | 8.0.5 | JSON schema validation for course content | **Recommended.** Define schemas for course.json, module.json, lesson.json, challenge.json. Validate all content files against schemas in CI and in the E2E test suite. Catches structural errors (missing fields, wrong types) before they reach students. MIT license. |
| xUnit | 2.9+ | Test framework (already in use) | **Upgrade.** Current 2.6.3 is old. Latest xUnit 2.x is stable and has better assertion messages. |
| Coverlet | 6.0+ | Code coverage (already in use) | **Keep.** Add coverage gating to CI when CI is implemented. |

**Content validation approach:**
1. Define JSON Schema files for each content type (course, module, lesson, challenge)
2. Add schema validation to existing `ChallengeValidationTests.cs` E2E tests
3. Validate markdown content sections exist and are non-empty
4. Validate code files (starter, solution) parse without syntax errors using language-specific validators
5. Validate that referenced files exist (no broken links between lesson.json and content files)

---

## Taught Language Versions -- Current Stable Standards

These are the versions that course content should reference and teach. Content using older versions should be flagged during audit.

**Confidence: HIGH** (all verified via official sources and web search, February 2026)

### Python

| Aspect | Current Standard | Source |
|--------|-----------------|--------|
| Language version | **Python 3.14.2** (stable, Dec 2025) | [python.org/downloads](https://www.python.org/downloads/) |
| Minimum to teach | Python 3.12+ (match expressions, f-string improvements) | |
| Key framework | None required for beginners; Flask 3.x or FastAPI 0.115+ for web | |
| Package manager | pip + venv (standard), uv emerging as faster alternative | |
| What to avoid | Python 2 syntax, Python 3.9 or earlier patterns, `setup.py` (use `pyproject.toml`) | |
| Runtime detection | Current app checks `python --version`, minimum 3.10+ | Keep |

**Content audit flags:**
- Any `print` without parentheses (Python 2)
- Use of `setup.py` instead of `pyproject.toml`
- Missing type hints (modern Python teaches type hints early)
- `asyncio` patterns from pre-3.11 (TaskGroups are the modern way)

### Java

| Aspect | Current Standard | Source |
|--------|-----------------|--------|
| Language version | **Java 25** (LTS, Sep 2025) | [oracle.com/java/technologies/java-se-support-roadmap.html](https://www.oracle.com/java/technologies/java-se-support-roadmap.html) |
| Minimum to teach | Java 21+ (LTS, still supported through Sep 2028) | |
| Key new features | Records, sealed classes, pattern matching for switch, virtual threads, unnamed classes and instance main methods | |
| Build tool | Gradle 9.x or Maven 3.9+ | |
| Key framework | Spring Boot 4.0.2 (for advanced modules) | [spring.io/blog/2025/11/20/spring-boot-4-0-0-available-now](https://spring.io/blog/2025/11/20/spring-boot-4-0-0-available-now/) |
| Runtime detection | Current app checks `java --version`, minimum 15.0.2+ | Should raise to 21+ |
| What to avoid | Java 8 patterns (anonymous inner classes where lambdas work), `var` avoidance, old date/time APIs | |

**Content audit flags:**
- Pre-Java 17 patterns (no use of records, sealed classes, pattern matching)
- Old `main(String[] args)` without mentioning simplified `void main()` (Java 21+ preview, Java 25 stable)
- Spring Boot 2.x or 3.x references (current is 4.0)
- Use of `java.util.Date` instead of `java.time`

### C#

| Aspect | Current Standard | Source |
|--------|-----------------|--------|
| Language version | **C# 14** (.NET 10 LTS, Nov 2025) | [dotnet.microsoft.com/en-us/download/dotnet](https://dotnet.microsoft.com/en-us/download/dotnet) |
| Runtime | **.NET 10.0** (LTS through Nov 2028) | |
| Minimum to teach | C# 12 / .NET 8.0 (current app's own version is acceptable baseline) | |
| Key new features | Primary constructors (C# 12), collection expressions (C# 12), extension members (C# 14), `dotnet run` for single files (C# 14) | |
| Key framework | ASP.NET Core 10 (for web modules), Entity Framework Core 10 | |
| What to avoid | .NET Framework references, `Console.ReadKey()` patterns in async code, pre-nullable-reference-types code | |
| Roslyn execution | App uses Roslyn 4.8.0 for in-process C# execution. Upgrade to 5.0.0 enables C# 14 features in student code. | |

**Content audit flags:**
- References to .NET Framework (vs .NET Core / .NET 5+)
- Missing `nullable enable` in taught code
- Old `Startup.cs` pattern instead of minimal APIs (if teaching ASP.NET)
- Missing primary constructors where they simplify code

### JavaScript

| Aspect | Current Standard | Source |
|--------|-----------------|--------|
| Language version | **ECMAScript 2025** (ES16, approved Jun 2025) | [tc39.es/ecma262/2025](https://tc39.es/ecma262/2025/) |
| Runtime | **Node.js 24.x LTS** "Krypton" (Active LTS) | [nodejs.org/en/about/previous-releases](https://nodejs.org/en/about/previous-releases) |
| Minimum to teach | ES2022+ (top-level await, private fields, `.at()`) | |
| Key new features | Iterator helpers, Set methods (union, intersection), import attributes, `Promise.try()` | |
| Package manager | npm (bundled with Node), pnpm as modern alternative | |
| Key frameworks | React 19+, Next.js 15+, Express 5.x or Hono for backend | |
| Runtime detection | Current app checks `node --version`, minimum 18.15+ | Should raise to 22+ |
| What to avoid | CommonJS `require()` in new code (teach ESM `import`), `var` keyword, callback-heavy patterns | |

**Content audit flags:**
- Use of `var` instead of `let`/`const`
- CommonJS `require()` instead of ESM `import`
- Callback patterns instead of async/await
- Old Node.js APIs (e.g., `url.parse()` instead of `new URL()`)
- jQuery or other deprecated DOM libraries

### Kotlin

| Aspect | Current Standard | Source |
|--------|-----------------|--------|
| Language version | **Kotlin 2.3.0** (Dec 2025) | [blog.jetbrains.com/kotlin/2025/12/kotlin-2-3-0-released](https://blog.jetbrains.com/kotlin/2025/12/kotlin-2-3-0-released/) |
| Minimum to teach | Kotlin 2.0+ (K2 compiler is now default) | |
| Key new features | Explicit backing fields, unused return value checker, Java 25 support, Swift export improvements | |
| Android Compose | **Jetpack Compose 1.10** + **Material 3 v1.4** (Dec 2025) | [android-developers.googleblog.com](https://android-developers.googleblog.com/2025/12/whats-new-in-jetpack-compose-december.html) |
| Build tool | Gradle 9.x with Kotlin DSL | |
| Runtime detection | Current app checks `kotlinc -version`, minimum 1.8.20+ | Should raise to 2.0+ |
| What to avoid | Pre-coroutines async patterns, Java-style code in Kotlin, old Android View system in new projects | |

**Content audit flags:**
- Kotlin 1.x patterns (pre-K2 compiler)
- Old Android View/XML instead of Jetpack Compose
- Missing coroutine/Flow usage for async
- Gradle Groovy DSL instead of Kotlin DSL
- Old Compose APIs (pre-Material 3)

### Flutter / Dart

| Aspect | Current Standard | Source |
|--------|-----------------|--------|
| Flutter version | **Flutter 3.38.6** (Jan 2026 hotfix) | [docs.flutter.dev/install/archive](https://docs.flutter.dev/install/archive) |
| Dart version | **Dart 3.10.3** (Jan 2026 hotfix) | [dart.dev/resources/whats-new](https://dart.dev/resources/whats-new) |
| Minimum to teach | Flutter 3.29+ / Dart 3.7+ (null safety fully enforced) | |
| Key new features | Dot shorthands syntax, Impeller as default renderer on Android, WebAssembly compilation, 16KB page compliance | |
| State management | Riverpod 2.x (recommended), Provider (legacy but still common) | |
| Navigation | GoRouter (recommended by Flutter team) | |
| Backend | Dart Frog (already in course content) | |
| Runtime detection | Current app checks `dart --version`, minimum 2.19.6+ | Should raise to 3.0+ |
| What to avoid | Old Flutter 2.x patterns, pre-null-safety code, setState-only state management in complex apps | |

**Content audit flags:**
- Pre-null-safety Dart code (no `?` on nullable types)
- Flutter 2.x widget APIs (pre-Material 3)
- Old navigation patterns (Navigator 1.0 push/pop without GoRouter)
- Missing Impeller references for performance
- Dart Frog patterns that may have changed since content was written

---

## Runtime Detection Version Updates

The current `RuntimeDetectionService` checks for minimum versions that are significantly outdated. Recommended updates:

| Language | Current Minimum | Recommended Minimum | Rationale |
|----------|----------------|---------------------|-----------|
| Python | 3.10+ | **3.12+** | 3.10 and 3.11 are in maintenance; 3.12 is the oldest actively maintained version with good match/case support |
| Node.js | 18.15+ | **22.0+** | Node 18 and 20 are in maintenance; 22 is active LTS |
| Java | 15.0.2+ | **21+** | Java 15 is long EOL; 21 is the previous LTS, 25 is current LTS |
| Kotlin | 1.8.20+ | **2.0+** | K2 compiler is the baseline for modern Kotlin |
| Dart | 2.19.6+ | **3.0+** | Dart 3.0 enforced null safety; pre-3.0 code is structurally different |
| Rust | 1.68.2+ | **Remove** | Rust course is deprecated per PROJECT.md |

---

## Alternatives Considered and Rejected

| Category | Recommended | Alternative | Why Rejected |
|----------|-------------|-------------|-------------|
| .NET version | .NET 8.0 (stay) | .NET 10.0 | Unnecessary risk for this milestone. .NET 8 is LTS through Nov 2026. Upgrade in future milestone. |
| UI framework | WPF + MaterialDesignThemes | Avalonia / .NET MAUI | Cross-platform explicitly out of scope. WPF is already built and working. Rewriting UI would consume entire milestone budget. |
| LLM runtime | ONNX Runtime GenAI | Ollama / llama.cpp | ONNX Runtime GenAI is already integrated, has C# NuGet packages, DirectML GPU support, and first-class Phi-4 support. Switching to Ollama would require running a separate server process, adding complexity. |
| LLM model | Phi-4-mini-instruct | Llama 3.1 8B / Qwen 2.5 7B | Phi-4-mini is 3.8B parameters (smaller = faster on student hardware), MIT licensed, purpose-built for instruction following, and has official ONNX Runtime support. Larger models perform better but require more hardware. |
| Code editor | AvalonEdit | Monaco (WebView2) | AvalonEdit is native WPF, already integrated, and performs well. Monaco would require embedding a WebView2 control, adding ~100MB to the installer, and introducing JavaScript interop complexity. |
| Markdown parser | Markdig | Westwind.AspNetCore.Markdown | Markdig is the underlying parser in most .NET markdown libraries. Use it directly. |
| Design system | Material Design (MaterialDesignThemes) | Fluent (WPF UI) / Metro (MahApps) | Material Design is the most recognized design system in education tech. MaterialDesignThemes has the largest community and best documentation. |
| MVVM framework | CommunityToolkit.Mvvm | Prism / ReactiveUI | CommunityToolkit.Mvvm is Microsoft-maintained, lightweight, uses source generators (no runtime reflection), and can be adopted incrementally without rewriting existing code. Prism is heavier and more opinionated. ReactiveUI has a steep learning curve. |

---

## What NOT to Do

These are tempting but counterproductive for this milestone:

1. **Do NOT upgrade to .NET 10.** The .NET 8 to 10 jump requires updating the csproj TargetFramework, retesting WPF rendering, retesting the Inno Setup installer, and potentially breaking ONNX Runtime GenAI compatibility. All risk, no student-facing benefit for this milestone.

2. **Do NOT replace WPF with a web-based UI.** The app works. Electron, Blazor Hybrid, and similar frameworks would require a complete rewrite. The milestone is about content quality and LLM tutoring, not UI framework migration.

3. **Do NOT try to fine-tune Phi-4.** ONNX Runtime GenAI does not support fine-tuning. Use prompt engineering and RAG. The research literature shows RAG-enhanced small models match GPT-4 on domain-specific tasks.

4. **Do NOT add cloud API fallback for the LLM.** The project explicitly requires offline operation. Adding cloud API options (OpenAI, Claude, etc.) would create dependency management headaches and violate the privacy-first design.

5. **Do NOT add Docker as a requirement for code execution.** Students are beginners. Requiring Docker installation is a barrier. The current approach (local runtimes + Piston fallback) is correct.

6. **Do NOT teach framework versions that are in preview/beta.** All course content should target GA (Generally Available) stable releases only. No "Java 26 preview features" or "Flutter 3.41 beta."

---

## Installation / Package Reference Summary

```xml
<!-- UPGRADED packages -->
<PackageReference Include="AvalonEdit" Version="6.3.1.120" />
<PackageReference Include="Microsoft.CodeAnalysis.CSharp.Scripting" Version="5.0.0" />
<PackageReference Include="Microsoft.ML.OnnxRuntimeGenAI.DirectML" Version="0.11.4" />

<!-- UNCHANGED packages -->
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.0" />
<PackageReference Include="System.Text.Json" Version="8.0.5" />

<!-- NEW packages (recommended additions) -->
<PackageReference Include="MaterialDesignThemes" Version="5.3.0" />
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.4.0" />
<PackageReference Include="Markdig" Version="0.44.0" />

<!-- NEW packages (for content validation in test project) -->
<PackageReference Include="JsonSchema.Net" Version="8.0.5" />
```

---

## Sources

### Language Versions
- [Python Downloads](https://www.python.org/downloads/) - Python 3.14.2 stable
- [Oracle Java SE Support Roadmap](https://www.oracle.com/java/technologies/java-se-support-roadmap.html) - Java 25 LTS
- [.NET Downloads](https://dotnet.microsoft.com/en-us/download/dotnet) - .NET 10 / C# 14
- [Node.js Releases](https://nodejs.org/en/about/previous-releases) - Node.js 24.x LTS
- [Kotlin 2.3.0 Release Blog](https://blog.jetbrains.com/kotlin/2025/12/kotlin-2-3-0-released/) - Kotlin 2.3.0
- [Flutter SDK Archive](https://docs.flutter.dev/install/archive) - Flutter 3.38.6
- [Dart What's New](https://dart.dev/resources/whats-new) - Dart 3.10.3
- [ECMAScript 2025 Specification](https://tc39.es/ecma262/2025/) - ES2025

### Framework Versions
- [Spring Boot 4.0.0 Release](https://spring.io/blog/2025/11/20/spring-boot-4-0-0-available-now/) - Spring Boot 4.0.2
- [Jetpack Compose December '25](https://android-developers.googleblog.com/2025/12/whats-new-in-jetpack-compose-december.html) - Compose 1.10 / Material 3 v1.4

### ONNX / LLM
- [ONNX Runtime GenAI Releases](https://github.com/microsoft/onnxruntime-genai/releases) - v0.11.4
- [Microsoft.ML.OnnxRuntimeGenAI on NuGet](https://www.nuget.org/packages/Microsoft.ML.OnnxRuntimeGenAI) - v0.11.4
- [Phi-4-mini-instruct on HuggingFace](https://huggingface.co/microsoft/Phi-4-mini-instruct) - 3.8B general instruction
- [Phi-4-mini-reasoning on HuggingFace](https://huggingface.co/microsoft/Phi-4-mini-reasoning) - 3.8B math reasoning
- [Phi-4-mini-instruct-onnx on HuggingFace](https://huggingface.co/microsoft/Phi-4-mini-instruct-onnx) - ONNX format
- [One Year of Phi - Microsoft Azure Blog](https://azure.microsoft.com/en-us/blog/one-year-of-phi-small-language-models-making-big-leaps-in-ai/) - Phi family overview
- [Best Open Source LLM for Education 2026](https://www.siliconflow.com/articles/en/best-open-source-LLM-for-education-tutoring) - LLM tutoring comparison
- [Small Models for Education (RAG + CAG)](https://arxiv.org/html/2506.05925v1) - RAG with small models matches GPT-4

### WPF / UI
- [MaterialDesignThemes on NuGet](https://www.nuget.org/packages/MaterialDesignThemes/) - v5.3.0
- [MaterialDesignInXamlToolkit on GitHub](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit) - 16K+ stars
- [CommunityToolkit.Mvvm on NuGet](https://www.nuget.org/packages/CommunityToolkit.Mvvm) - v8.4.0
- [MVVM Toolkit Introduction](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/) - Microsoft Learn
- [AvalonEdit on NuGet](https://www.nuget.org/packages/AvalonEdit) - v6.3.1.120
- [AvalonEdit on GitHub](https://github.com/icsharpcode/AvalonEdit) - WPF code editor

### Content Tooling
- [Markdig on NuGet](https://www.nuget.org/packages/Markdig/) - v0.44.0
- [JsonSchema.Net on NuGet](https://www.nuget.org/packages/JsonSchema.Net) - v8.0.5
- [Roslyn CSharp Scripting on NuGet](https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp.Scripting/) - v5.0.0

### Education Platform Research
- [Scaffolding Metacognition in Programming Education](https://arxiv.org/html/2511.04144v1) - AI scaffolding patterns
- [How to Create Interactive Coding Lessons](https://futureclassroom.com.ph/how-to-create-interactive-coding-lessons/) - Exercise design best practices
- [LLM-Powered Tutoring Solutions](https://www.emergentmind.com/topics/llm-powered-tutoring-solutions) - Prompt engineering for tutoring
- [Codecademy Platform Features](https://www.codecademy.com/resources/blog/new-learning-environment-platform-features) - Progressive hints pattern

---

## Confidence Assessment

| Area | Confidence | Reason |
|------|------------|--------|
| Language versions (6 taught languages) | **HIGH** | All verified against official sources (python.org, oracle.com, dotnet.microsoft.com, nodejs.org, kotlinlang.org, docs.flutter.dev) via web search in Feb 2026 |
| ONNX Runtime GenAI upgrade path | **HIGH** | Verified against NuGet gallery and GitHub releases |
| Phi-4 model selection | **HIGH** | Verified against HuggingFace model cards and Microsoft Azure blog |
| LLM prompt engineering strategy | **MEDIUM** | Based on 2025 research papers and platform patterns, not yet validated in Code Tutor specifically |
| UI library recommendations | **HIGH** | MaterialDesignThemes and CommunityToolkit.Mvvm verified on NuGet with version numbers and compatibility |
| Content validation tooling | **MEDIUM** | JsonSchema.Net recommended based on NuGet research, but specific schema design for Code Tutor content is not yet defined |
| "Stay on .NET 8" recommendation | **HIGH** | .NET 8 LTS officially supported through Nov 2026 per Microsoft, confirmed via web search |
| Framework versions for courses (Spring Boot, Compose, etc.) | **HIGH** | Verified against official release announcements |

---

*Stack research: 2026-02-02*
