<img src="https://r2cdn.perplexity.ai/pplx-full-logo-primary-dark%402x.png" style="height:64px;margin-right:32px"/>

# And now Kotlin

Your Kotlin Multiplatform course is very strong on modern Kotlin language features, collections, OOP, and KMP setup, but there are gaps around Android/iOS app architecture, networking, persistence, testing, and deployment that prevent it from being a full “newbie to production multiplatform developer” path.[^1][^2]

## Potentially outdated or brittle content

- **Tooling and version pinning**
    - You hard‑code Android Studio “Ladybug (2024.2)” and specific KMP plugin flows; these labels and wizards change frequently, so instructions will age quickly.[^2][^1]
    - Recommendation: Frame versions as “Android Studio 2024.2+ (current stable)” and show how to check for newer KMP templates / plugins rather than depending on exact names and screens.
- **KMP ecosystem still evolving**
    - You position “In 2025, learning Kotlin means learning KMP from day one,” which is aspirational but not universally true; many Kotlin jobs are still Android‑only or backend‑JVM, and KMP APIs, Gradle DSLs, and Compose Multiplatform are still moving targets.[^1][^2]
    - Recommendation: Add a short note on KMP being rapidly evolving, and explicitly describe which Kotlin/Gradle/Compose versions the course targets and how to update them when newer templates appear.


## Incomplete Kotlin / KMP content vs course promise

The fundamentals track is excellent: variables, control flow, functions, collections, classes, data classes, enums, value classes, and idiomatic Kotlin patterns are covered with clear examples and practice. What is light or missing:[^1]

- **Coroutines and structured concurrency**
    - There is no dedicated module for `suspend` functions, `CoroutineScope`, `launch`/`async`, dispatchers, cancellation, and error handling, even though coroutines are central for KMP networking, I/O, and UI code.[^2][^1]
    - Recommendation: Add at least one “Coroutines \& Flows” module, including `Flow` for streams, sharing flows across Android/iOS, and using them with Compose.
- **Multiplatform specifics beyond project structure**
    - You explain `commonMain`, `androidMain`, `iosMain` and value classes, but there’s limited coverage of:
        - Expect/actual declarations.
        - Multiplatform libraries (e.g., Ktor, SQLDelight, Koin/other DI libs with KMP support).
        - Interop with Swift/Objective‑C and platform‑specific APIs.[^2][^1]
    - Recommendation: Add a “KMP in practice” module where learners:
        - Declare expect/actual for a few platform services.
        - Use at least one common KMP networking and persistence library.


## Missing “full‑stack” / production aspects

To go from newbie to someone who can ship KMP apps, learners need more than language + KMP project scaffolding.[^2]

- **Networking and backend integration**
    - No structured coverage of HTTP clients (Ktor client or retrofit‑style for Android), JSON serialization (`kotlinx.serialization`), API modeling, error handling, and auth flows.[^1][^2]
    - Recommendation:
        - Add “Networking with Ktor Client” module in `commonMain`, including DTOs, serialization, retries, and mapping errors to domain models.
        - Tie at least one module to a simple backend (REST or GraphQL) so students actually consume real APIs.
- **Persistence and offline work**
    - There is no dedicated module on shared persistence (e.g., SQLDelight) or platform‑specific storage (Android Room, iOS keychain/user defaults), which is critical for real apps.[^1][^2]
    - Recommendation: Add “Data \& Persistence” module:
        - Shared database with SQLDelight in `commonMain`.
        - Simple secure storage pattern per platform.
- **App architecture and DI**
    - Fundamentals of classes and OOP are well covered, but there is no explicit app‑level architecture (e.g., MVVM / MVI for KMP with Compose Multiplatform) and only scattered best‑practice notes.[^3][^1]
    - Recommendation:
        - Introduce a consistent architecture (e.g., “shared ViewModels with flows, platform‑specific UI layers”) for a mid‑sized sample app.
        - Add a DI module (Koin/Hilt‑style for Kotlin) and show how to wire repositories, use cases, and view models.
- **Testing and CI**
    - There is no comprehensive testing story: unit tests (common), instrumentation tests (Android), and iOS tests with shared logic, nor examples of running tests for all platforms in CI.[^2][^1]
    - Recommendation: Add a testing module for:
        - Shared business‑logic tests in `commonTest`.
        - UI tests on Android/Compose.
        - Simple CI pipeline example (GitHub Actions) that runs tests and builds binaries.
- **Deployment and distribution**
    - The course sets up Android Studio and Xcode but does not guide:
        - Signing and releasing Android builds to Play Store.
        - Creating iOS archives, dealing with provisioning profiles, and TestFlight/App Store basics.[^1][^2]
    - Recommendation: Add a “Shipping your KMP app” module: store requirements, versioning, build configurations, and a step‑by‑step release checklist.


## Structural gaps and missing capstones

- **Emphasis on console and basic examples**
    - Early modules use console programs (`readln`, `println`) and rich exercises (bank account, student system, word frequency), which are great for fundamentals but do not transition clearly into UI, KMP shared code, and platform apps.[^1]
    - Recommendation: After basics, pivot quickly into “same concepts, now inside Compose Multiplatform UI” so learners see their knowledge applied to real screens.
- **Lack of end‑to‑end KMP projects**
    - There are many focused exercises (temperature converter, banking system, collections demos), but no large project that combines:
        - Shared domain logic.
        - Networking and persistence.
        - Compose Multiplatform UI for Android and iOS.
        - Testing and deployment.[^3][^1]
    - Recommendation: Define at least one KMP capstone, for example:
        - “Cross‑platform Notes App” or “Tasks app” with sync, offline mode, and basic auth.
        - Build it gradually across modules so students always see the big picture.

If you want, a follow‑up message can outline a concrete add‑on roadmap: where to insert coroutines/Ktor/persistence/architecture/testing/deployment modules into the existing sequence so your Kotlin course truly takes students from language basics to production‑ready multiplatform apps.
<span style="display:none">[^4][^5]</span>

<div align="center">⁂</div>

[^1]: course.json

[^2]: https://roadmap.sh/flutter

[^3]: https://dev.to/bestaoui_aymen/flutter-roadmap-2025-what-you-should-learn-to-stay-ahead-3b18

[^4]: https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/132573446/67cf6a04-3bfa-4df9-909f-9de2f9daa1d8/course.json

[^5]: https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/132573446/b0412405-97aa-4f2f-81a5-e0d096759cb0/course.json

