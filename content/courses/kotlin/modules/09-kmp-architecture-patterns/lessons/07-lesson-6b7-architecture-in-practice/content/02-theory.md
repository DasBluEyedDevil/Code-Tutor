---
type: "THEORY"
title: "Complete Project Structure"
---

### Recommended KMP Project Structure

```
shared/
├── src/
│   ├── commonMain/
│   │   ├── kotlin/com/example/notes/
│   │   │   ├── App.kt                 # App entry point
│   │   │   ├── di/
│   │   │   │   └── Modules.kt         # Koin modules
│   │   │   ├── domain/
│   │   │   │   ├── model/
│   │   │   │   │   └── Note.kt
│   │   │   │   └── repository/
│   │   │   │       └── NoteRepository.kt
│   │   │   ├── data/
│   │   │   │   ├── local/
│   │   │   │   │   └── NoteDao.kt
│   │   │   │   ├── remote/
│   │   │   │   │   ├── NoteApi.kt
│   │   │   │   │   └── NoteDto.kt
│   │   │   │   ├── repository/
│   │   │   │   │   └── NoteRepositoryImpl.kt
│   │   │   │   └── mapper/
│   │   │   │       └── NoteMappers.kt
│   │   │   ├── presentation/
│   │   │   │   ├── viewmodel/
│   │   │   │   │   ├── BaseViewModel.kt
│   │   │   │   │   ├── NotesViewModel.kt
│   │   │   │   │   └── NoteDetailViewModel.kt
│   │   │   │   └── model/
│   │   │   │       └── NotesUiState.kt
│   │   │   └── ui/
│   │   │       ├── navigation/
│   │   │       │   └── AppNavigation.kt
│   │   │       ├── screens/
│   │   │       │   ├── NotesScreen.kt
│   │   │       │   └── NoteDetailScreen.kt
│   │   │       ├── components/
│   │   │       │   ├── NoteCard.kt
│   │   │       │   └── LoadingIndicator.kt
│   │   │       └── theme/
│   │   │           └── Theme.kt
│   │   └── sqldelight/
│   │       └── com/example/notes/
│   │           └── Note.sq
│   ├── androidMain/
│   │   └── kotlin/
│   │       ├── Platform.android.kt
│   │       └── DatabaseDriver.android.kt
│   └── iosMain/
│       └── kotlin/
│           ├── Platform.ios.kt
│           └── DatabaseDriver.ios.kt
```