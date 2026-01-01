---
type: "THEORY"
title: "Running on iOS"
---


### Building for iOS

1. **Open in Xcode**: Open the `iosApp` folder in Xcode
2. **Select Simulator**: Choose an iOS Simulator (e.g., iPhone 15)
3. **Run**: Click the Run button

Or from command line:
```bash
./gradlew :composeApp:iosSimulatorArm64Run
```

### iOS-Specific Testing

Test these features on iOS Simulator:
1. Swipe-back navigation works
2. Safe areas are respected (notch, home indicator)
3. Keyboard handling is smooth
4. Data persists across app restarts
5. Animations run at 60fps

### Cross-Platform Verification

| Feature | Android Test | iOS Test |
|---------|-------------|----------|
| Create task | Run on emulator | Run on simulator |
| Edit task | Verify save | Verify save |
| Delete (swipe) | Test gesture | Test gesture |
| Categories | Filter works | Filter works |
| Search | Results match | Results match |
| Persistence | Restart app | Restart app |

---

