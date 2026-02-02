# Codebase Concerns

**Analysis Date:** 2026-02-02

## Tech Debt

**PowerShell Unicode Parser Issues:**
- Issue: `build-installer.ps1` had Unicode characters (✓⚠📦🎉) that corrupted PowerShell 5.1 parser
- Files: `build-installer.ps1`
- Impact: Build script completely non-functional when executed, multiple spurious parse errors
- Fix approach: Replace Unicode symbols with ASCII equivalents ([OK], [WARN], [INFO], [SUCCESS]); Extract Inno Setup script to separate template file (`installer-template.iss`) for cleaner separation of concerns
- Status: Resolved (documented in `BUILD-INSTALLER-FIX.md`)

**Course Content JSON Structure Inconsistencies:**
- Issue: Multiple `*.json.bak` backup files scattered throughout course content indicating past migration/repair operations
- Files: `content/courses/{python,java,kotlin,javascript,csharp,flutter}/course.json.bak`
- Impact: Unclear which version is authoritative; accumulates technical debt; confuses version control
- Fix approach: Remove all `.bak` files from version control and use proper git history; implement pre-commit hooks to prevent backup files
- Priority: Medium

**Large Compiled Binaries in Content Directory:**
- Issue: Compiled .NET binaries in `content/courses/csharp/capstone/src/ShopFlow.Web/bin/` and `/obj/` directories checked into git
- Files: `content/courses/csharp/capstone/src/ShopFlow.Web/**/{bin,obj}/**`
- Impact: Repository bloat; makes cloning slow; binaries can diverge from source; violates .gitignore intent
- Fix approach: Remove bin/obj directories from git history using `git filter-branch` or `bfg-repo-cleaner`; ensure .gitignore rules are enforced
- Priority: High

**Incomplete Placeholder Code in Starter Files:**
- Issue: 487+ starter and example files contain TODO comments representing incomplete learning exercises
- Files: `content/courses/**/**/challenges/**/{starter,solution}.*`, `content/courses/**/**/content/*.md`
- Impact: Intended behavior (these are learning challenges), but creates noise in technical analysis; TODO comments scatter across curriculum
- Fix approach: This is by design for educational content, but ensure a separate "CURRICULUM_STATUS.md" document tracks which modules are complete vs. draft
- Priority: Low (by design)

**Untracked Test Coverage Gaps:**
- Issue: Native app test coverage not tracked; E2E tests for challenges exist but scope is limited
- Files: `native-app.Tests/E2E/ContentValidation/ChallengeValidationTests.cs`
- Impact: Unknown test coverage percentage; difficult to verify refactoring safety
- Fix approach: Add code coverage instrumentation (OpenCover or Coverlet); publish coverage reports to CI/CD pipeline; target 70%+ for core services
- Priority: Medium

---

## Known Bugs

**Challenge Validator TODO Markers:**
- Symptoms: ChallengeValidationTests.cs explicitly checks for TODO in starter code (line 325)
- Files: `native-app.Tests/E2E/ContentValidation/ChallengeValidationTests.cs`
- Trigger: Any starter.* file containing "TODO" string
- Workaround: This appears intentional (tests allow TODO comments in educational starters); document as expected behavior in test comments
- Analysis: Not a bug; appears to be intentional validation that TODO comments are present in incomplete challenges

**Deleted File in Git Status:**
- Symptoms: `D "C:\357\200\272UsersdasblDownloadsCode-Tutortemp_flutter_course.json"` appears as deleted in git status
- Files: temp_flutter_course.json (deleted)
- Trigger: File was likely created during curriculum import/export operations and deleted
- Workaround: Run `git status` to see pending deletion; commit or restore as needed
- Analysis: Temporary file; should be added to .gitignore pattern or cleaned up

---

## Security Considerations

**Environment Variable Handling:**
- Risk: Build scripts reference environment variables for paths; potential for path injection if build run from untrusted sources
- Files: `build-installer.ps1` (lines 51: `${env:ProgramFiles(x86)}`)
- Current mitigation: Script assumes local Windows execution; validates paths before use
- Recommendations: Add explicit path validation; use `[IO.Path]::Combine()` instead of string concatenation; document that script should only be run in trusted development environments

**Secrets in Configuration:**
- Risk: No explicit secrets management shown; .env files are gitignored but approach not documented
- Files: `.gitignore` lines 18-22 properly ignore `.env` files
- Current mitigation: Standard .NET environment variable masking; no hardcoded secrets detected in code samples
- Recommendations: Add explicit documentation for CI/CD secret injection; use Azure Key Vault or similar for production builds

**Course Content Validation:**
- Risk: Course JSON files accept arbitrary code snippets in starter/solution fields; no sanitization visible
- Files: `native-app.Tests/E2E/ContentValidation/ChallengeValidationTests.cs` validates structure but not content
- Current mitigation: Content is displayed in code editor only; not executed directly as course code (execution happens in sandboxed process)
- Recommendations: Validate starter code matches expected language syntax; scan for malicious patterns (e.g., `rm -rf /`, `DROP TABLE`)

---

## Performance Bottlenecks

**Course Content Loading:**
- Problem: All courses loaded from JSON files on startup; no lazy loading detected
- Files: Content system in `GEMINI.md` indicates course loading from `content/courses/{language}/course.json`
- Cause: Course JSON files for mature courses (Java, Python) likely 1-2MB+; multiple courses loaded per session
- Improvement path: Implement lazy loading per course; cache parsed JSON in SQLite after first load; load only active course + 1 ahead

**Large JSON File Sizes:**
- Problem: Course metadata files contain full lesson content, not references to separate files
- Files: `content/courses/java/course.json` (estimated 5-10MB based on module count)
- Cause: Single JSON file contains all lessons, challenges, and content; no pagination
- Improvement path: Split course.json into modular structure (`modules/{module-id}/module.json`, `modules/{module-id}/lessons/{lesson-id}/lesson.json`); enables parallel loading

**WPF Rendering on Large Course Lists:**
- Problem: Course browser in WPF may render all 100+ courses in memory
- Files: Views likely in `native-app-wpf/Views/`; architecture not detailed in exploration
- Cause: No virtualizing panel mention in ARCHITECTURE notes
- Improvement path: Use `VirtualizingStackPanel` for course lists; implement search/filter before rendering

---

## Fragile Areas

**Build Script Dependency Chain:**
- Files: `build-installer.ps1`, `installer-template.iss`, `BuildInstaller.bat`
- Why fragile: PowerShell script depends on Inno Setup installation path; fails silently if Inno not found; external tool coupling
- Safe modification: Wrap Inno Setup invocation in try-catch; provide detailed error messages; separate installer generation from portable build
- Test coverage: No automated tests for build script; manual verification only
- Risk: Silent build failures that appear successful (no .exe generated but script completes)

**Content Structure Coupling:**
- Files: `native-app.Tests/E2E/ContentValidation/ChallengeValidationTests.cs` hard-codes valid challenge types
- Why fragile: Adding new challenge type requires updating validator; no single source of truth for types
- Safe modification: Create `ChallengeTypes.cs` enum; use reflection to validate types; centralize type definitions
- Test coverage: ChallengeValidationTests exists but coverage of new challenge types unclear

**Database Context Initialization:**
- Files: `native-app-wpf/Data/CodeTutorDbContext.cs` (referenced in GEMINI.md but not explored)
- Why fragile: EF Core migrations must be applied in correct order; SQLite schema changes can be problematic
- Safe modification: Use EF Core migrations; never modify schema manually; test migrations on clean database before deployment
- Test coverage: No migration tests visible in exploration

**PowerShell 5.1 Compatibility:**
- Files: `build-installer.ps1` (still targets PowerShell 5.1)
- Why fragile: PowerShell 5.1 is Windows-only and has Unicode/encoding quirks; PowerShell Core (.NET 5+) handles these better
- Safe modification: Consider migrating to C# executable for build; or provide `build-installer-ps7.ps1` for PowerShell Core users
- Test coverage: Unicode issue previously broke script; no regression tests exist

---

## Scaling Limits

**Course Content Memory Usage:**
- Current capacity: Single course JSON ~5-10MB; all courses loaded ~50-100MB total
- Limit: WPF can handle this; but adding 1000+ lessons per course would cause issues
- Scaling path: Implement lazy loading; use indexed SQLite database; stream large course sections; implement paging in UI

**Challenge Validator Performance:**
- Current capacity: 487+ files scanned in E2E tests; completes in minutes
- Limit: If courses grow to 2000+ challenges, validation becomes slow
- Scaling path: Parallel challenge validation; cache validation results; use incremental validation (only changed files)

**Code Execution Resource Limits:**
- Current capacity: Local process spawning with resource limits (mentioned in GEMINI.md)
- Limit: Cannot handle 100+ concurrent execution requests
- Scaling path: Implement execution queue; add request throttling; consider containerized execution (Docker) for remote scaling

---

## Dependencies at Risk

**PowerShell 5.1 (Windows Dependency):**
- Risk: Deprecated; Windows 11 default is PowerShell 5.1, but Core is recommended; Unicode issues as documented
- Impact: Build script may fail on newer Windows Server versions if Core is required
- Migration plan: Provide C# replacement for build script; or migrate to PowerShell 7+ only; document that Core is recommended

**Inno Setup (Optional External Tool):**
- Risk: Windows-only; 32-bit application; requires external installation
- Impact: Users must install Inno Setup separately for full build; portable .exe always available as fallback
- Migration plan: Use WiX Toolset (more modern); or use NSIS (more portable); make installer generation fully optional with clear documentation

**Entity Framework Core 8.0:**
- Risk: .NET 8.0 LTS support ends November 2026; migrations to 9.0 will eventually be required
- Impact: Security vulnerabilities may emerge; support gradually phase out
- Migration plan: Monitor for 9.0 LTS (Feb 2025); plan upgrade path early; test against RC versions

**TextMateSharp (Syntax Highlighting):**
- Risk: Maintenance status unknown; syntax definitions require manual updates for new language features
- Impact: New language versions may not syntax-highlight correctly until definitions updated
- Migration plan: Evaluate Roslyn-based highlighting (for C# specific); or maintain TextMate grammars in-repo

---

## Missing Critical Features

**Offline Course Storage:**
- Problem: Courses stored in JSON files; no optimization for offline-first usage
- Blocks: Mobile companion app; airplane mode support; slow network scenarios
- Recommendation: Implement indexed SQLite database with course metadata; enable users to "download for offline"

**User Progress Persistence:**
- Problem: No mention of user progress tracking in architecture
- Blocks: Students cannot resume from last lesson; no achievement system
- Recommendation: Add user_progress table; track completed challenges; implement resume functionality

**Multi-language Runtime Support:**
- Problem: Relies on system-installed language runtimes (Python, Java, Node.js, Rust)
- Blocks: Users without runtimes cannot execute code
- Recommendation: Bundle lightweight runtime versions; or provide cloud execution option; detect missing runtimes and guide installation

**Code Sharing & Collaboration:**
- Problem: No mechanism to share solutions or collaborate on challenges
- Blocks: Classroom usage; peer review; team-based learning
- Recommendation: Add challenge sharing via URLs; implement discussion threads; consider GitHub Gist integration

---

## Test Coverage Gaps

**Service Layer Unit Tests:**
- What's not tested: CodeExecutor sandboxing behavior; CourseService content loading; authentication/session handling
- Files: All services in `native-app-wpf/Services/` likely need coverage
- Risk: Refactoring CodeExecutor could break execution limits silently; no regression tests
- Priority: High

**ViewModel Logic Tests:**
- What's not tested: State transitions; async operation cancellation; error handling in ViewModels
- Files: All ViewModels in `native-app-wpf/ViewModels/` (100+ likely)
- Risk: UI state corruption on edge cases (rapid navigation, network failures)
- Priority: Medium

**Database Migration Tests:**
- What's not tested: Schema migration correctness; upgrade paths between versions; rollback scenarios
- Files: EF Core migrations (referenced but not explored)
- Risk: Silent data loss during upgrade; version incompatibilities
- Priority: High

**Build Script Integration Tests:**
- What's not tested: Full build pipeline; installer creation; artifact validation
- Files: `build-installer.ps1`, `installer-template.iss`
- Risk: Build artifacts may be corrupted; installer may fail to run on target systems
- Priority: High

**Content Validation Edge Cases:**
- What's not tested: Malformed JSON recovery; circular references in course structure; validation of very large courses (10000+ challenges)
- Files: `native-app.Tests/E2E/ContentValidation/ChallengeValidationTests.cs`
- Risk: Application crash on corrupted course file; no graceful degradation
- Priority: Medium

---

## Documentation Gaps

**Architecture Documentation:**
- Missing: Data flow diagrams; deployment architecture; disaster recovery procedures; scaling strategies
- Impact: New developers struggle to understand system; deployment requires manual tribal knowledge
- Files: `GEMINI.md`, `native-app-wpf/ARCHITECTURE.md` exist but may need detail

**API Documentation:**
- Missing: REST API specs (if any backend API exists); service interface documentation; event system documentation
- Impact: Frontend-backend integration unclear; no contract testing

**Database Schema Documentation:**
- Missing: Entity-relationship diagrams; schema versioning strategy; query performance notes
- Impact: Developers unfamiliar with schema; no optimization guidance

---

*Concerns audit: 2026-02-02*
