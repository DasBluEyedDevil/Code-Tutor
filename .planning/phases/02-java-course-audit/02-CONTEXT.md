# Phase 2: Java Course Audit - Context

**Gathered:** 2026-02-02
**Status:** Ready for planning

<domain>
## Phase Boundary

Audit the Java course for accuracy, progression, and completeness. Every lesson is verified against Java 25 LTS, every challenge compiles and runs, and students follow a coherent path from absolute beginner to deploying a full-stack Spring Boot web application. This phase does NOT add new courses, change the app architecture, or modify other courses.

</domain>

<decisions>
## Implementation Decisions

### Java Version Target
- **Java 25 LTS** (released September 2025) is the target version, replacing the previous Java 21 pin
- Version manifest (`content/version-manifest.json`) must be updated from Java 21 to Java 25
- Full Java 25 verification of ALL lesson content -- not a delta audit, every API and pattern checked against Java 25

### IO.println vs System.out.println
- Use `IO.println` (from `java.lang.IO`, finalized in Java 25) as the standard output method throughout the course
- Mention `System.out.println` exactly once in an early lesson: "You'll see System.out.println in older code and tutorials -- it still works, but IO.println is the modern standard"
- All 75+ files currently using `System.out.println` must be migrated to `IO.println`

### Compact Source Files
- Early lessons (fundamentals, variables, control flow) use compact main method syntax (`void main()`) -- no class declaration boilerplate
- Full class structure (`public class Main { public static void main(String[] args) }`) introduced when teaching OOP
- Transition is explicit: a lesson explains why classes exist and when the full syntax matters

### Java 25 Modern Features
- Teach modern patterns first, acknowledge older approaches briefly (students will encounter legacy code in the wild)
- Enhanced switch, text blocks, records, sealed classes, pattern matching -- use Java 25 finalized versions
- Flexible constructor bodies (finalized in Java 25) taught when relevant

### Broken Content Handling
- Wrong APIs or phantom methods: **rewrite the entire section**, not just fix-in-place (if the API is wrong, the explanation around it is likely wrong too)
- Broken challenges: **rewrite from scratch** to properly test the lesson's concepts against Java 25
- Outdated patterns: teach modern Java 25 way, briefly acknowledge the older approach exists
- Thin but correct content: **flag only, don't fix during this audit** -- keep the audit focused on accuracy and correctness

### Voice and Tone
- **Friendly mentor** voice throughout: warm, encouraging, uses analogies. Conversational but not dumbed down
- **Consistent explanation depth** from Module 1 through final module -- every concept gets the same thoroughness regardless of where it appears in the course
- **Every lesson gets an ANALOGY section** -- even simpler concepts benefit from a relatable comparison
- **Bridge lessons** at major transitions (fundamentals to OOP, OOP to collections, collections to Spring Boot, etc.) -- dedicated lessons that connect "here's what you know" to "here's where we're going"

### Capstone Project
- **Full-stack web app**: Spring Boot backend + simple frontend (Thymeleaf or similar)
- **Deployed to cloud**: Students end the course with a live URL, not just local instructions
- **Deployment target**: Railway ($5 trial credit, simplest GitHub-to-deploy experience)
- **Dedicated final module**: Capstone is its own module at the end, built from scratch, not incrementally across the course

</decisions>

<specifics>
## Specific Ideas

- IO.println usage follows Java 25 finalized API in `java.lang.IO` package (not `java.io.IO` from preview versions)
- The "legacy mention" lesson for System.out.println should use LEGACY_COMPARISON content type
- Compact source file syntax should be the FIRST thing students see -- eliminate the "public static void main" barrier for beginners
- Bridge lessons should feel like a friendly "checkpoint" -- celebrate what's been learned, preview what's next

</specifics>

<deferred>
## Deferred Ideas

None -- discussion stayed within phase scope

</deferred>

---

*Phase: 02-java-course-audit*
*Context gathered: 2026-02-02*
