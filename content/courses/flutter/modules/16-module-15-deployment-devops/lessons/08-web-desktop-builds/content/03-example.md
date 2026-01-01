---
type: "EXAMPLE"
title: "Windows Builds"
---


Build, package, and distribute Windows applications with MSIX:



```yaml
# Prerequisites:
# - Visual Studio 2022 with "Desktop development with C++" workload
# - Windows 10 SDK

# Build Windows executable
flutter build windows --release

# Output: build/windows/x64/runner/Release/
# Contains: your_app.exe and required DLLs

---

# MSIX Packaging with msix package
# Add to pubspec.yaml:
dev_dependencies:
  msix: ^3.16.0

msix_config:
  display_name: My Flutter App
  publisher_display_name: Your Company
  identity_name: com.yourcompany.myapp
  msix_version: 1.0.0.0
  logo_path: assets/icons/app_icon.png
  capabilities: internetClient, microphone, webcam
  languages: en-us, es-es, fr-fr
  # For Microsoft Store
  store: true
  # For sideloading (unsigned)
  # store: false

# Create MSIX package
flutter pub run msix:create

# Output: build/windows/x64/runner/Release/my_app.msix

---

# Code Signing for Windows
# Option 1: Self-signed certificate (for testing)
New-SelfSignedCertificate -Type Custom `
  -Subject "CN=Your Company, O=Your Company, C=US" `
  -KeyUsage DigitalSignature `
  -FriendlyName "My App Signing Cert" `
  -CertStoreLocation "Cert:\CurrentUser\My" `
  -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.3")

# Export certificate
$pwd = ConvertTo-SecureString -String "YourPassword" -Force -AsPlainText
Export-PfxCertificate `
  -Cert "Cert:\CurrentUser\My\<thumbprint>" `
  -FilePath certificate.pfx `
  -Password $pwd

# Sign MSIX with certificate
msix_config:
  certificate_path: certificate.pfx
  certificate_password: YourPassword

# Option 2: Trusted certificate from CA
# Purchase from DigiCert, Sectigo, or similar
# Required for Windows SmartScreen trust

---

# Inno Setup Installer (Alternative to MSIX)
# Download Inno Setup from jrsoftware.org

# installer_script.iss
[Setup]
AppName=My Flutter App
AppVersion=1.0.0
DefaultDirName={autopf}\My Flutter App
DefaultGroupName=My Flutter App
OutputDir=installer
OutputBaseFilename=MyAppSetup
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Files]
Source: "build\windows\x64\runner\Release\*"; DestDir: "{app}"; Flags: recursesubdirs

[Icons]
Name: "{group}\My Flutter App"; Filename: "{app}\my_app.exe"
Name: "{commondesktop}\My Flutter App"; Filename: "{app}\my_app.exe"

[Run]
Filename: "{app}\my_app.exe"; Description: "Launch My App"; Flags: postinstall nowait

# Compile: iscc installer_script.iss

---

# GitHub Actions for Windows Build
jobs:
  build-windows:
    runs-on: windows-latest
    steps:
      - uses: actions/checkout@v4
      
      - uses: subosito/flutter-action@v2
        with:
          flutter-version: '3.24.0'
          cache: true

      - run: flutter config --enable-windows-desktop
      - run: flutter pub get
      - run: flutter build windows --release
      
      - name: Create MSIX
        run: flutter pub run msix:create

      - uses: actions/upload-artifact@v4
        with:
          name: windows-msix
          path: build/windows/x64/runner/Release/*.msix
```
