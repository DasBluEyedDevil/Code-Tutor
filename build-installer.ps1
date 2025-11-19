# Code Tutor - Windows Installer Build Script

# This script creates a self-contained Windows installer for Code Tutor
# The resulting installer bundles ALL dependencies (no .NET runtime needed)

param(
    [string]$Configuration = "Release",
    [string]$Version = "1.0.0",
    [switch]$SkipBuild = $false
)

$ErrorActionPreference = "Stop"

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Code Tutor - Windows Installer Build" -ForegroundColor Cyan
Write-Host "  Version: $Version" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Paths
$RootDir = $PSScriptRoot
$ProjectDir = Join-Path $RootDir "native-app"
$ProjectFile = Join-Path $ProjectDir "CodeTutor.Native.csproj"
$ContentDir = Join-Path $RootDir "content"
$DocsDir = Join-Path $RootDir "docs"
$PublishDir = Join-Path $RootDir "publish"
$InstallerDir = Join-Path $RootDir "installer"
$OutputDir = Join-Path $RootDir "dist"

# Check prerequisites
Write-Host "[1/6] Checking prerequisites..." -ForegroundColor Yellow

if (-not (Test-Path $ProjectFile)) {
    Write-Error "Project file not found: $ProjectFile"
    exit 1
}

if (-not (Test-Path $ContentDir)) {
    Write-Error "Content directory not found: $ContentDir"
    exit 1
}

# Check for dotnet
try {
    $dotnetVersion = dotnet --version
    Write-Host "  ✓ .NET SDK found: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Error ".NET SDK not found. Please install .NET 8.0 SDK from https://dotnet.microsoft.com/download"
    exit 1
}

# Check for Inno Setup (optional - will create batch file if missing)
$InnoSetupPath = "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe"
$HasInnoSetup = Test-Path $InnoSetupPath

if (-not $HasInnoSetup) {
    Write-Host "  ⚠ Inno Setup not found (installer creation will be skipped)" -ForegroundColor Yellow
    Write-Host "    Download from: https://jrsoftware.org/isdl.php" -ForegroundColor Gray
} else {
    Write-Host "  ✓ Inno Setup found" -ForegroundColor Green
}

Write-Host ""

# Clean publish directory
Write-Host "[2/6] Cleaning publish directory..." -ForegroundColor Yellow
if (Test-Path $PublishDir) {
    Remove-Item $PublishDir -Recurse -Force
}
New-Item -ItemType Directory -Path $PublishDir | Out-Null
Write-Host "  ✓ Cleaned: $PublishDir" -ForegroundColor Green
Write-Host ""

# Build and publish self-contained app
if (-not $SkipBuild) {
    Write-Host "[3/6] Building self-contained Windows executable..." -ForegroundColor Yellow
    Write-Host "  This may take a few minutes..." -ForegroundColor Gray

    $publishArgs = @(
        "publish",
        $ProjectFile,
        "-c", $Configuration,
        "-r", "win-x64",
        "--self-contained", "true",
        "-p:PublishSingleFile=true",
        "-p:PublishTrimmed=false",  # Don't trim - keeps compatibility
        "-p:IncludeNativeLibrariesForSelfExtract=true",
        "-o", $PublishDir
    )

    & dotnet $publishArgs

    if ($LASTEXITCODE -ne 0) {
        Write-Error "Build failed with exit code $LASTEXITCODE"
        exit 1
    }

    Write-Host "  ✓ Build complete" -ForegroundColor Green
} else {
    Write-Host "[3/6] Skipping build (using existing publish directory)..." -ForegroundColor Yellow
}
Write-Host ""

# Copy content directory
Write-Host "[4/6] Copying content files..." -ForegroundColor Yellow
$PublishContentDir = Join-Path $PublishDir "Content"
Copy-Item -Path $ContentDir -Destination $PublishContentDir -Recurse -Force
Write-Host "  ✓ Copied content to publish directory" -ForegroundColor Green
Write-Host ""

# Copy documentation
Write-Host "[5/6] Copying documentation..." -ForegroundColor Yellow
Copy-Item -Path (Join-Path $RootDir "README.md") -Destination $PublishDir -Force
if (Test-Path $DocsDir) {
    Copy-Item -Path $DocsDir -Destination (Join-Path $PublishDir "docs") -Recurse -Force
}
Write-Host "  ✓ Copied documentation" -ForegroundColor Green
Write-Host ""

# Get published exe size
$ExePath = Join-Path $PublishDir "CodeTutor.Native.exe"
if (Test-Path $ExePath) {
    $ExeSize = (Get-Item $ExePath).Length / 1MB
    Write-Host "  📦 Executable size: $([math]::Round($ExeSize, 2)) MB" -ForegroundColor Cyan
}

# Create installer with Inno Setup
Write-Host "[6/6] Creating installer..." -ForegroundColor Yellow

if (-not $HasInnoSetup) {
    Write-Host "  ⚠ Skipping installer creation (Inno Setup not installed)" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "To create an installer:" -ForegroundColor Cyan
    Write-Host "  1. Install Inno Setup: https://jrsoftware.org/isdl.php" -ForegroundColor Gray
    Write-Host "  2. Run this script again" -ForegroundColor Gray
    Write-Host ""
    Write-Host "Published files are in: $PublishDir" -ForegroundColor Cyan
} else {
    # Create output directory
    if (-not (Test-Path $OutputDir)) {
        New-Item -ItemType Directory -Path $OutputDir | Out-Null
    }

    # Create Inno Setup script using verbatim here-string
    $InnoScriptTemplate = @'
; Code Tutor - Inno Setup Installer Script
; Auto-generated by build-installer.ps1

#define MyAppName "Code Tutor"
#define MyAppVersion "___VERSION___"
#define MyAppPublisher "Code Tutor"
#define MyAppURL "https://github.com/DasBluEyedDevil/Code-Tutor"
#define MyAppExeName "CodeTutor.Native.exe"

[Setup]
AppId={{A5B6C7D8-1234-5678-90AB-CDEF12345678}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=___ROOTDIR___\LICENSE
OutputDir=___OUTPUTDIR___
OutputBaseFilename=CodeTutor-Setup-{#MyAppVersion}
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
UninstallDisplayIcon={app}\{#MyAppExeName}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "___PUBLISHDIR___\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "___PUBLISHDIR___\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
function InitializeSetup(): Boolean;
var
  ResultCode: Integer;
  PythonInstalled, NodeInstalled, JavaInstalled: Boolean;
  MissingRuntimes: String;
begin
  Result := True;
  MissingRuntimes := '';

  // Check for Python
  PythonInstalled := (Exec('python', '--version', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) and (ResultCode = 0)) or
                     (Exec('python3', '--version', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) and (ResultCode = 0));

  // Check for Node.js
  NodeInstalled := Exec('node', '--version', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) and (ResultCode = 0);

  // Check for Java
  JavaInstalled := Exec('java', '-version', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) and (ResultCode = 0);

  // Build warning message
  if not PythonInstalled then
    MissingRuntimes := MissingRuntimes + '  • Python 3.x (for Python courses)' + #13#10;
  if not NodeInstalled then
    MissingRuntimes := MissingRuntimes + '  • Node.js 18+ (for JavaScript courses)' + #13#10;
  if not JavaInstalled then
    MissingRuntimes := MissingRuntimes + '  • Java 17+ (for Java courses)' + #13#10;

  // Show warning if any runtimes are missing
  if MissingRuntimes <> '' then
  begin
    if MsgBox('Code Tutor is ready to install!' + #13#10#13#10 +
              'However, the following language runtimes were not detected:' + #13#10#13#10 +
              MissingRuntimes + #13#10 +
              'You can still install Code Tutor, but you won''t be able to execute code ' +
              'in these languages until you install their runtimes.' + #13#10#13#10 +
              'Do you want to continue with the installation?',
              mbInformation, MB_YESNO) = IDNO then
    begin
      Result := False;
    end;
  end;
end;
'@

    # Replace placeholders with actual paths
    $InnoScript = $InnoScriptTemplate `
        -replace '___VERSION___', $Version `
        -replace '___ROOTDIR___', $RootDir `
        -replace '___OUTPUTDIR___', $OutputDir `
        -replace '___PUBLISHDIR___', $PublishDir

    # Save Inno Setup script
    $InnoScriptPath = Join-Path $RootDir "installer.iss"
    $InnoScript | Out-File -FilePath $InnoScriptPath -Encoding UTF8 -Force

    # Run Inno Setup
    Write-Host "  Compiling installer..." -ForegroundColor Gray
    & $InnoSetupPath $InnoScriptPath

    if ($LASTEXITCODE -eq 0) {
        Write-Host "  ✓ Installer created successfully!" -ForegroundColor Green

        # Find the created installer
        $InstallerFile = Get-ChildItem $OutputDir -Filter "CodeTutor-Setup-*.exe" | Select-Object -First 1
        if ($InstallerFile) {
            $InstallerSize = $InstallerFile.Length / 1MB
            Write-Host ""
            Write-Host "========================================" -ForegroundColor Green
            Write-Host "  🎉 BUILD COMPLETE!" -ForegroundColor Green
            Write-Host "========================================" -ForegroundColor Green
            Write-Host ""
            Write-Host "Installer: $($InstallerFile.FullName)" -ForegroundColor Cyan
            Write-Host "Size: $([math]::Round($InstallerSize, 2)) MB" -ForegroundColor Cyan
            Write-Host ""
            Write-Host "To install:" -ForegroundColor Yellow
            Write-Host "  1. Run the installer" -ForegroundColor Gray
            Write-Host "  2. Follow the installation wizard" -ForegroundColor Gray
            Write-Host "  3. Launch Code Tutor from the desktop or start menu" -ForegroundColor Gray
        }
    } else {
        Write-Error "Installer compilation failed with exit code $LASTEXITCODE"
        exit 1
    }
}

Write-Host ""
Write-Host "Done!" -ForegroundColor Green
