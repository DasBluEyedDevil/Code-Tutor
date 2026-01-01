// Given these dependencies, organize them into proper modules:
// - NotesRepository (interface in commonMain)
// - NotesRepositoryImpl (implementation in commonMain, needs database)
// - AppDatabase (platform-specific creation)
// - AndroidSqliteDriver (Android only)
// - NativeSqliteDriver (iOS only)
// - NotesViewModel (shared, needs repository)
// - NetworkClient (shared Ktor, no platform deps)
// - PlatformLogger interface (common) with AndroidLogger/IOSLogger impls

// TODO: Create these modules:

// commonMain/kotlin/di/CommonModule.kt
val commonModule = module {
    // Add shared dependencies here
}

// androidMain/kotlin/di/PlatformModule.android.kt  
val androidPlatformModule = module {
    // Add Android-specific dependencies here
}

// iosMain/kotlin/di/PlatformModule.ios.kt
val iosPlatformModule = module {
    // Add iOS-specific dependencies here
}