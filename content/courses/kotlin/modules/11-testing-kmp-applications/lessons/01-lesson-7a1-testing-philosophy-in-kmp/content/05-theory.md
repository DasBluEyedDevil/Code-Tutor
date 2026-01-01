---
type: "THEORY"
title: "Test Directory Structure"
---

```
shared/
├── src/
│   ├── commonMain/           # Production code
│   ├── commonTest/           # Shared tests (run on JVM)
│   │   └── kotlin/
│   │       ├── data/
│   │       │   └── NoteRepositoryTest.kt
│   │       ├── domain/
│   │       │   └── ValidateNoteUseCaseTest.kt
│   │       ├── presentation/
│   │       │   └── NotesViewModelTest.kt
│   │       └── fakes/
│   │           ├── FakeNoteRepository.kt
│   │           └── FakeSettingsRepository.kt
│   ├── androidTest/          # Android-specific tests
│   │   └── kotlin/
│   │       └── AndroidDatabaseDriverTest.kt
│   ├── iosTest/              # iOS-specific tests
│   │   └── kotlin/
│   │       └── IosDatabaseDriverTest.kt
│   └── jvmTest/              # JVM-specific tests
│       └── kotlin/
│           └── DesktopIntegrationTest.kt
```

### Running Tests

```bash
# Run all shared tests on JVM (fastest)
./gradlew :shared:jvmTest

# Run tests for specific platform
./gradlew :shared:androidUnitTest
./gradlew :shared:iosSimulatorArm64Test

# Run all tests
./gradlew :shared:allTests
```