# Phase 1: Foundation and Content Normalization - Context

**Gathered:** 2026-02-02
**Status:** Ready for planning

<domain>
## Phase Boundary

Clean infrastructure and standardize all content schemas across all 6 courses (812 lessons) before any course-specific editing begins. Deliver: a single validated schema, standardized content section types, sequential numbering with no gaps, Python Module 14 restructured, and pinned version targets. Every subsequent audit phase (2-7) operates on this normalized data.

</domain>

<decisions>
## Implementation Decisions

### Schema design
- Define a shared JSON Schema with required core fields (id, title, content sections, metadata structure) but allow course-specific extra properties
- JSON Schema file for automated validation + markdown summary for human reference (both maintained)
- When a lesson.json fails validation: auto-fix in place (missing fields get defaults, wrong types get coerced), log all changes made
- ID format: Claude derives the most consistent pattern from existing IDs across all 6 courses and standardizes on that

### Content type migration
- Standard types: THEORY, EXAMPLE, KEY_POINT, ANALOGY, WARNING, LEGACY_COMPARISON (Claude may add more if analysis warrants it)
- Non-standard types (CODE, CONCEPT, etc.): Claude decides per-file whether a simple rename suffices or content needs adjustment to match the new type's purpose
- Content section files numbered sequentially by teaching order (01-theory.md, 02-example.md, 03-key_point.md, etc.), not grouped by type
- Lessons missing KEY_POINT sections get placeholder stubs added in Phase 1 so the schema is fully satisfied everywhere; audit phases fill them with real content

### Numbering & restructuring
- Rename directories to match new numbers (e.g., 01-module-0-flutter-development becomes a clean sequential name)
- Claude decides on directory naming convention (number + slug vs number only) based on existing patterns
- Python Module 14 split: Claude determines the number of resulting modules based on content analysis and natural boundaries
- Duplicate lessons (same concept taught twice): merge the best parts of both into a single lesson

### Version manifest
- Pin to major + minor version (e.g., Java 21, Python 3.12, Flutter 3.27) -- specific enough for API verification, not chasing patches
- Include both language/runtime versions AND key framework versions per course (Spring Boot, React, FastAPI, ASP.NET Core, etc.)
- Each entry includes a lastVerified date so auditors know when each version was last confirmed
- Manifest file location: Claude's discretion based on project structure

### Claude's Discretion
- ID format derivation (analyze existing, pick best)
- Whether additional content types beyond the 6 standard ones are needed
- Per-file decision on rename vs content adjustment during type migration
- Directory naming convention (number + slug vs number only)
- Python Module 14 split count and boundaries
- Version manifest file location

</decisions>

<specifics>
## Specific Ideas

No specific requirements -- open to standard approaches. User's core priority is that everything is consistent and validated so audit phases don't waste time on schema issues.

</specifics>

<deferred>
## Deferred Ideas

None -- discussion stayed within phase scope.

</deferred>

---

*Phase: 01-foundation-and-content-normalization*
*Context gathered: 2026-02-02*
