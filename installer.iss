; Code Tutor - Inno Setup Installer Script
; Template file - placeholders will be replaced by build script

#define MyAppName "Code Tutor"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Code Tutor"
#define MyAppURL "https://github.com/DasBluEyedDevil/Code-Tutor"
#define MyAppExeName "CodeTutor.exe"

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
LicenseFile=C:\Users\dasbl\Downloads\Code-Tutor\LICENSE
OutputDir=C:\Users\dasbl\Downloads\Code-Tutor\dist
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
Source: "C:\Users\dasbl\Downloads\Code-Tutor\publish\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\dasbl\Downloads\Code-Tutor\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

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
    MissingRuntimes := MissingRuntimes + '  â€¢ Python 3.x (for Python courses)' + #13#10;
  if not NodeInstalled then
    MissingRuntimes := MissingRuntimes + '  â€¢ Node.js 18+ (for JavaScript courses)' + #13#10;
  if not JavaInstalled then
    MissingRuntimes := MissingRuntimes + '  â€¢ Java 17+ (for Java courses)' + #13#10;

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

