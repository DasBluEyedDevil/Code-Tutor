# Phase 7: Python Course Audit - Context

**Gathered:** 2026-02-04
**Status:** Ready for planning

<domain>
## Phase Boundary

The Python course teaches a complete path from absolute basics through web frameworks to a deployable application. This phase:
1. Restructures the former Module 14 (26 lessons covering FastAPI, Django, PostgreSQL, Auth) into 3-4 focused modules
2. Adds a Git/developer tools module
3. Verifies all 171 lessons for Python 3.12+ accuracy
4. Validates all challenges execute correctly
5. Ensures consistent voice and progression throughout

Content normalization (Phase 1) must be complete before this phase begins.

</domain>

<decisions>
## Implementation Decisions

### Module 14 Restructuring
- **Grouping criteria:** By learning progression — APIs first (FastAPI), then Database (PostgreSQL), then Full-stack (Django), then Auth/Deployment
- **Module count:** 3-4 modules based on lesson cohesion (OpenCode to determine exact split)
- **Framework comparison lessons:** Split into separate focused lessons (one per framework) rather than combined comparison lessons
- **Authentication content:** Split between modules — concepts taught in FastAPI module, Django-specific auth patterns in Django module

### Git/Tools Module Scope
- **Coverage:** Full developer workflow including Git, Python tooling (venv, pip), editor setup, and potentially CI/CD basics — OpenCode to determine exact scope based on course needs
- **Placement:** Spread throughout the course at optimal points — OpenCode to determine positioning (not all in one place)
- **Git depth:** Progressive introduction — basics early, advanced topics introduced when contextually relevant
- **Lesson format:** OpenCode decides per lesson whether challenges or quizzes are more appropriate

### Bridge Lesson Design
- **Transitions requiring bridges:** All major framework transitions — Basics→Web frameworks, Scripting→Database, Single-file→Project structure, Development→Deployment
- **Bridge purpose:** Combination approach — review prerequisites, preview upcoming content, connect concepts, or practical setup based on the specific transition
- **Lesson length:** Variable based on gap size — OpenCode determines appropriate length for each bridge
- **Challenges:** Optional — OpenCode decides per bridge lesson whether challenges add value

### Challenge Fix Strategy
- **Broken challenges:** Fix inline immediately — pause validation, fix, verify, continue (no batching)
- **Solution verification:** Run every solution to confirm it passes (100% verification, not spot-checking)
- **Fundamentally broken challenges:** Replace with a new challenge teaching the same concept (don't remove or convert to quiz)
- **Lesson synchronization:** Always keep challenges and lessons in sync — challenge fixes trigger lesson content updates when needed

### OpenCode's Discretion
- Exact module boundaries for the 3-4 split of former Module 14
- Specific Git topics and their placement throughout the course
- Bridge lesson length and format per transition
- Whether bridge lessons include challenges
- Challenge replacement design for fundamentally broken challenges

</decisions>

<specifics>
## Specific Ideas

- Learning progression ordering for Module 14: APIs → Database → Full-stack → Auth/Deployment
- Auth content should be framework-agnostic concepts first, then framework-specific implementations
- Bridge lessons needed at all major phase transitions in the course
- Every challenge solution must be executed and verified (no exceptions)
- Follow patterns established in Phases 2-6 (Java, JavaScript, C#, Flutter, Kotlin audits)

</specifics>

<deferred>
## Deferred Ideas

None — discussion stayed within phase scope. All ideas (Module 14 restructuring, Git module, bridge lessons, challenge validation) are core to this phase.

</deferred>

---

*Phase: 07-python-course-audit*
*Context gathered: 2026-02-04*
