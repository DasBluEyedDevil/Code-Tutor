---
type: "WARNING"
title: "Common Installation Mistakes to Avoid"
---


**Before You Install:**

1. **Don't install Flutter in paths with spaces**
   - ❌ BAD: `C:\Program Files\flutter`
   - ✅ GOOD: `C:\src\flutter` or `C:\flutter`

2. **Don't use PowerShell `setx` to set PATH**
   - PowerShell's setx command can truncate your PATH variable (max 1024 characters)
   - Use the Windows Environment Variables GUI instead (search 'environment variables' in Start menu)

3. **On Mac, restart Terminal after PATH changes**
   - Changes to `.zshrc` or `.zprofile` only take effect in new terminal windows

4. **Don't panic about red X marks in `flutter doctor`**
   - ❌ Chrome, Android Studio, Xcode are OPTIONAL for now
   - ✅ You only need Flutter and Dart checkmarks to continue

5. **Apple Silicon Macs (M1/M2/M3/M4) may need Rosetta**
   - Run: `sudo softwareupdate --install-rosetta`

