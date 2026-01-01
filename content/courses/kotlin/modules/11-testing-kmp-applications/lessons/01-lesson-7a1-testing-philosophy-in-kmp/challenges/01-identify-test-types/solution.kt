// Feature A: Validate that email format is correct
// Answer: Unit test in commonTest
// Reason: Pure logic with no dependencies

// Feature B: Verify notes are saved to SQLite and can be retrieved
// Answer: Integration test in commonTest (using in-memory SQLite)
// Reason: Tests repository + database interaction, can run on JVM

// Feature C: Test that Android notification permissions are requested
// Answer: Integration test in androidTest
// Reason: Android-specific API (NotificationManager)

// Feature D: Verify ViewModel updates state when repository returns data
// Answer: Unit test in commonTest
// Reason: Uses fake repository, tests shared ViewModel logic

// Feature E: Test iOS Keychain secure storage
// Answer: Integration test in iosTest  
// Reason: iOS-specific API (Security framework)

// Feature F: Verify full user flow from login to viewing notes
// Answer: E2E test in androidTest/iosTest (platform-specific)
// Reason: Tests entire app flow, requires UI automation