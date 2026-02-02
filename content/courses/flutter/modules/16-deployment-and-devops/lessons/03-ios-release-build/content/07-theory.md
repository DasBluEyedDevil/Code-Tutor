---
type: "THEORY"
title: "Xcode Archive"
---


**Building an iOS Archive**

An archive is a packaged version of your compiled app ready for distribution. The archive contains:
- Compiled binary (ARM64)
- App resources and assets
- Signing information
- Debug symbols (dSYMs) for crash reporting

**Archive Workflow:**

1. **Build the Flutter iOS project:**
   ```bash
   flutter build ios --release
   ```

2. **Open in Xcode:**
   ```bash
   open ios/Runner.xcworkspace
   ```

3. **Select destination:**
   - Choose "Any iOS Device (arm64)" as build destination
   - NOT a simulator - simulators can't create distribution builds

4. **Create archive:**
   - Product -> Archive (or Cmd + Shift + B, then Product -> Archive)
   - Xcode builds and opens Organizer when complete

5. **Validate and distribute:**
   - In Organizer, select your archive
   - Click "Validate App" to check for issues
   - Click "Distribute App" to upload or export

