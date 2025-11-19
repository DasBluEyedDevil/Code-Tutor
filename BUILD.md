# Code Tutor - Build & Distribution Guide

This guide explains how to build Code Tutor installers for distribution.

---

## 🪟 Windows Installer (.exe)

### Prerequisites

1. **.NET 8.0 SDK** - https://dotnet.microsoft.com/download/dotnet/8.0
2. **Inno Setup 6** (optional, for creating installer) - https://jrsoftware.org/isdl.php
3. **PowerShell 5.0+** (included with Windows 10/11)

### Quick Build

**Option 1: Using Batch File (Easiest)**
```batch
# Double-click or run from command line:
build-installer.bat
```

**Option 2: Using PowerShell**
```powershell
# From PowerShell:
.\build-installer.ps1
```

**Option 3: With Custom Version**
```powershell
.\build-installer.ps1 -Version "1.0.1"
```

### What Gets Built

The build script creates:

1. **Self-Contained Executable**
   - Location: `publish/CodeTutor.Native.exe`
   - Size: ~80-120 MB (includes .NET runtime)
   - No installation required (portable)

2. **Windows Installer** (if Inno Setup is installed)
   - Location: `dist/CodeTutor-Setup-1.0.0.exe`
   - Size: ~80-120 MB (compressed)
   - Creates Start Menu shortcuts
   - Creates Desktop shortcut (optional)
   - Detects missing language runtimes

### Build Process Details

**Step 1: Clean Build**
- Removes old publish directory
- Creates fresh build environment

**Step 2: Publish Self-Contained App**
```bash
dotnet publish native-app/CodeTutor.Native.csproj \
  -c Release \
  -r win-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -o publish
```

**Step 3: Copy Content & Documentation**
- Copies `content/` directory (course files)
- Copies `docs/` directory (documentation)
- Copies `README.md`

**Step 4: Create Installer (Inno Setup)**
- Generates `installer.iss` script
- Compiles installer with Inno Setup
- Outputs to `dist/` directory

---

## 🐧 Linux Build (Self-Contained)

### Prerequisites

1. **.NET 8.0 SDK** - https://dotnet.microsoft.com/download/dotnet/8.0

### Build Commands

```bash
# Build self-contained Linux executable
dotnet publish native-app/CodeTutor.Native.csproj \
  -c Release \
  -r linux-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -o publish-linux

# Copy content
cp -r content publish-linux/Content
cp -r docs publish-linux/docs
cp README.md publish-linux/

# Create tarball for distribution
cd publish-linux
tar -czf ../dist/CodeTutor-Linux-x64-1.0.0.tar.gz .
cd ..
```

### Create .deb Package (Debian/Ubuntu)

```bash
# Install dpkg-deb if needed
sudo apt install dpkg-dev

# Create package structure
mkdir -p code-tutor_1.0.0_amd64/DEBIAN
mkdir -p code-tutor_1.0.0_amd64/usr/local/bin
mkdir -p code-tutor_1.0.0_amd64/usr/share/applications
mkdir -p code-tutor_1.0.0_amd64/usr/share/code-tutor

# Copy files
cp publish-linux/CodeTutor.Native code-tutor_1.0.0_amd64/usr/local/bin/code-tutor
cp -r publish-linux/Content code-tutor_1.0.0_amd64/usr/share/code-tutor/
cp -r publish-linux/docs code-tutor_1.0.0_amd64/usr/share/code-tutor/

# Create control file
cat > code-tutor_1.0.0_amd64/DEBIAN/control << EOF
Package: code-tutor
Version: 1.0.0
Section: education
Priority: optional
Architecture: amd64
Depends: libc6
Maintainer: Code Tutor <support@example.com>
Description: Interactive coding education platform
 Learn Python, JavaScript, Java, C#, and Rust through
 interactive lessons with real-time code execution.
EOF

# Build package
dpkg-deb --build code-tutor_1.0.0_amd64
```

---

## 🍎 macOS Build (Self-Contained)

### Prerequisites

1. **.NET 8.0 SDK** - https://dotnet.microsoft.com/download/dotnet/8.0
2. **Xcode Command Line Tools** (for code signing)

### Build Commands

```bash
# Build self-contained macOS executable
dotnet publish native-app/CodeTutor.Native.csproj \
  -c Release \
  -r osx-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -o publish-macos

# Copy content
cp -r content publish-macos/Content
cp -r docs publish-macos/docs
cp README.md publish-macos/

# Create .app bundle
mkdir -p CodeTutor.app/Contents/MacOS
mkdir -p CodeTutor.app/Contents/Resources

cp publish-macos/CodeTutor.Native CodeTutor.app/Contents/MacOS/
cp -r publish-macos/Content CodeTutor.app/Contents/Resources/
cp -r publish-macos/docs CodeTutor.app/Contents/Resources/

# Create Info.plist
cat > CodeTutor.app/Contents/Info.plist << EOF
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>CFBundleExecutable</key>
    <string>CodeTutor.Native</string>
    <key>CFBundleIdentifier</key>
    <string>com.codetutor.app</string>
    <key>CFBundleName</key>
    <string>Code Tutor</string>
    <key>CFBundleVersion</key>
    <string>1.0.0</string>
    <key>LSMinimumSystemVersion</key>
    <string>10.15</string>
</dict>
</plist>
EOF

# Create DMG (requires hdiutil)
hdiutil create -volname "Code Tutor" -srcfolder CodeTutor.app -ov -format UDZO dist/CodeTutor-macOS-1.0.0.dmg
```

---

## 📦 Distribution Checklist

Before distributing your build:

### Testing
- [ ] Run the installer on a clean Windows VM
- [ ] Verify all shortcuts work
- [ ] Test with Python installed
- [ ] Test with Python NOT installed (should show warning)
- [ ] Test with Node.js installed
- [ ] Test code execution in all available languages
- [ ] Verify content loads correctly
- [ ] Check progress saving/loading
- [ ] Test uninstall process

### Documentation
- [ ] Update version number in `build-installer.ps1`
- [ ] Update version number in `native-app/CodeTutor.Native.csproj`
- [ ] Update CHANGELOG.md with release notes
- [ ] Update README.md if needed

### Distribution
- [ ] Upload installer to GitHub Releases
- [ ] Create release notes
- [ ] Tag the release in git: `git tag v1.0.0`
- [ ] Push tag: `git push origin v1.0.0`

---

## 🔧 Troubleshooting

### "dotnet not found"
**Solution**: Install .NET 8.0 SDK from https://dotnet.microsoft.com/download

### "Inno Setup not found"
**Solution**: The build will still create the portable executable in `publish/`. To create an installer:
1. Install Inno Setup from https://jrsoftware.org/isdl.php
2. Run the build script again

### Build fails with "Access Denied"
**Solution**: Close any running instances of Code Tutor and try again

### Installer too large
**Solution**: The self-contained build includes the .NET runtime (~60-80 MB). This is normal and ensures the app works without requiring .NET installation.

To reduce size, use framework-dependent deployment (requires .NET 8.0 installed):
```powershell
.\build-installer.ps1 -SelfContained $false
```
(Not recommended for end-user distribution)

### Code execution fails after installation
**Checklist**:
- [ ] Check if language runtime is installed (python, node, java, etc.)
- [ ] Verify language is in PATH
- [ ] Check CodeExecutor logs in `%APPDATA%\CodeTutor\logs\`

---

## 📝 Advanced Options

### Custom Build Configuration

```powershell
# Debug build (with symbols)
.\build-installer.ps1 -Configuration Debug

# Skip build (use existing publish directory)
.\build-installer.ps1 -SkipBuild

# Custom version
.\build-installer.ps1 -Version "2.0.0-beta"
```

### Manual Build (Without Script)

```bash
# 1. Build
dotnet publish native-app/CodeTutor.Native.csproj -c Release -r win-x64 --self-contained -o publish

# 2. Copy content
xcopy /E /I /Y content publish\Content
xcopy /E /I /Y docs publish\docs
copy README.md publish\

# 3. Create ZIP for distribution
powershell Compress-Archive -Path publish\* -DestinationPath dist\CodeTutor-Portable-1.0.0.zip
```

---

## 🌐 Platform-Specific Notes

### Windows
- **Target**: Windows 10 (1809) or later
- **Architecture**: x64 (64-bit)
- **Runtime**: Self-contained (no .NET installation required)
- **Installer**: Inno Setup creates Start Menu + Desktop shortcuts

### Linux
- **Target**: Ubuntu 20.04+ / Debian 11+ / Fedora 36+
- **Architecture**: x64
- **Distribution**: .tar.gz or .deb package
- **Note**: GTK3 required for Avalonia (usually pre-installed)

### macOS
- **Target**: macOS 10.15 (Catalina) or later
- **Architecture**: x64 (Intel) or arm64 (Apple Silicon)
- **Distribution**: .dmg or .app bundle
- **Note**: Code signing required for Gatekeeper bypass

---

## 📞 Support

**Build Issues**: Check TROUBLESHOOTING.md or create an issue on GitHub

**Distribution Questions**: See CONTRIBUTING.md for release guidelines
