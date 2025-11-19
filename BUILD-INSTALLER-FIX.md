# Build Installer Fix - Documentation

## Problem Summary

The `build-installer.ps1` script was failing with multiple parse errors when executed:

```
The Try statement is missing its Catch or Finally block.
Unexpected token '}' in expression or statement.
Missing function body in function declaration.
The 'var' keyword is not supported in this version of the language.
```

## Root Cause

The script contained **Unicode characters** (✓, ⚠, 📦, 🎉) that PowerShell 5.1 cannot parse correctly when:
- The file is saved without a UTF-8 BOM (Byte Order Mark)
- The script is executed using `powershell.exe -File`

PowerShell 5.1's parser becomes confused by these Unicode characters and reports incorrect syntax errors in completely unrelated parts of the code.

### Technical Details

The Unicode characters were encoded as UTF-8 multi-byte sequences:
- `✓` (U+2713) = 0xE2 0x9C 0x93
- `⚠` (U+26A0) = 0xE2 0x9A 0xA0
- `📦` (U+1F4E6) = 0xF0 0x9F 0x93 0xA6
- `🎉` (U+1F389) = 0xF0 0x9F 0x8E 0x89

Without a UTF-8 BOM, PowerShell 5.1 attempts to parse these as individual bytes rather than multi-byte UTF-8 sequences, causing parser corruption.

## Solution Applied

### 1. Replaced Unicode Characters with ASCII

All Unicode characters were replaced with ASCII equivalents:

| Original | Replacement | Usage |
|----------|-------------|-------|
| ✓ | `[OK]` | Success indicators |
| ⚠ | `[WARN]` | Warning messages |
| 📦 | `[INFO]` | Informational messages |
| 🎉 | `[SUCCESS]` | Build completion |

### 2. Created Inno Setup Template File

Created `installer-template.iss` to separate the Inno Setup Pascal script from PowerShell code:

**Benefits:**
- Eliminates the need for here-strings in PowerShell
- Makes the Inno Setup script easier to edit and maintain
- Avoids potential parsing issues with embedded code
- Uses simple placeholder replacement (`{{VERSION}}`, `{{ROOTDIR}}`, etc.)

### 3. Refactored Build Script

Modified `build-installer.ps1` to:
- Load the template file
- Replace placeholders with actual values
- Save the generated installer script

## Files Modified

### build-installer.ps1
- **Line 43, 56, 59, 70, 97, 108, 117, 124**: Unicode → ASCII replacements
- **Line 131, 167, 175**: Unicode → ASCII replacements
- **Lines 144-160**: Refactored to use template file instead of here-string

### installer-template.iss (NEW)
- Complete Inno Setup script template
- Uses `{{PLACEHOLDER}}` syntax for variable substitution
- Contains all installer configuration and Pascal code

## Verification Steps

### 1. Verify the Script Parses

```powershell
# This should complete without errors
powershell -ExecutionPolicy Bypass -File build-installer.ps1 -SkipBuild
```

**Expected output:**
```
========================================
  Code Tutor - Windows Installer Build
  Version: 1.0.0
========================================

[1/6] Checking prerequisites...
  [OK] .NET SDK found: 9.0.307
  [WARN] Inno Setup not found (installer creation will be skipped)
    Download from: https://jrsoftware.org/isdl.php

[2/6] Cleaning publish directory...
  [OK] Cleaned: C:\Users\...\Code-Tutor\publish

[3/6] Skipping build (using existing publish directory)...

[4/6] Copying content files...
  [OK] Copied content to publish directory

[5/6] Copying documentation...
  [OK] Copied documentation

[6/6] Creating installer...
  [WARN] Skipping installer creation (Inno Setup not installed)
  ...
```

### 2. Check for Parse Errors

```powershell
# Run PowerShell's built-in parser check
$errors = @()
$null = [System.Management.Automation.Language.Parser]::ParseFile(
    (Resolve-Path "build-installer.ps1").Path,
    [ref]$null,
    [ref]$errors
)

if ($errors.Count -eq 0) {
    Write-Host "✓ No parse errors!" -ForegroundColor Green
} else {
    Write-Host "✗ Found $($errors.Count) errors:" -ForegroundColor Red
    $errors | ForEach-Object { Write-Host "  Line $($_.Extent.StartLineNumber): $($_.Message)" }
}
```

**Expected:** "✓ No parse errors!"

### 3. Full Build Test (if prerequisites are installed)

```powershell
# Full build with installer creation
.\build-installer.ps1
```

This will:
1. Build the .NET application
2. Publish as self-contained executable
3. Copy content and documentation
4. Create Windows installer (if Inno Setup is installed)

## Next Steps

### 1. Review Changes

```bash
git diff build-installer.ps1
git status
```

### 2. Test the Build

Run a full build to ensure everything works:

```powershell
.\build-installer.ps1
```

### 3. Commit the Fix

```bash
git add build-installer.ps1 installer-template.iss
git commit -m "Fix PowerShell parse errors caused by Unicode characters

- Replace Unicode symbols (✓⚠📦🎉) with ASCII equivalents ([OK][WARN][INFO][SUCCESS])
- Extract Inno Setup script to installer-template.iss template file
- Refactor build script to use template with placeholder replacement

PowerShell 5.1 cannot parse Unicode characters without UTF-8 BOM when using
-File parameter. This caused spurious parse errors throughout the script.

Fixes #[issue-number]"
```

### 4. Push Changes

```bash
git push origin main
```

## Prevention

To prevent this issue in the future:

### Option 1: Use ASCII Only (Recommended)
- Stick to ASCII characters in all PowerShell scripts
- Use `[OK]`, `[WARN]`, `[ERROR]` instead of Unicode symbols

### Option 2: Use UTF-8 BOM
- Save PowerShell files with UTF-8 BOM encoding
- Most editors have this option in "Save As" dialog
- **Note:** This can cause issues with some tools and git

### Option 3: Use PowerShell Core (pwsh)
- PowerShell Core (v6+) handles Unicode correctly
- Not always available on all systems
- Requires users to install separately

## Troubleshooting

### Issue: "Installer template not found"

**Cause:** The `installer-template.iss` file is missing

**Solution:**
```bash
git restore installer-template.iss
```

### Issue: Still getting parse errors

**Cause:** File may have been re-saved with Unicode

**Solution:**
```bash
# Restore from git
git restore build-installer.ps1

# Or check for Unicode characters
python -c "
with open('build-installer.ps1', 'rb') as f:
    content = f.read()
non_ascii = [(i, b) for i, b in enumerate(content) if b > 127]
if non_ascii:
    print(f'Found {len(non_ascii)} non-ASCII bytes')
else:
    print('File is ASCII-only')
"
```

### Issue: Build fails with "Source file does not exist"

**Cause:** This is a runtime error, not a parse error

**Solution:**
- Don't use `-SkipBuild` flag
- OR ensure publish directory exists with built files

## Additional Resources

- [PowerShell Encoding Issues](https://docs.microsoft.com/en-us/powershell/scripting/dev-cross-plat/vscode/understanding-file-encoding)
- [Inno Setup Documentation](https://jrsoftware.org/ishelp/)
- [PowerShell 5.1 vs Core Differences](https://docs.microsoft.com/en-us/powershell/scripting/whats-new/differences-from-windows-powershell)

---

**Last Updated:** 2025-11-18
**Fixed By:** Claude (Systematic Debugging Process)
**Severity:** High (Script completely non-functional)
**Status:** ✅ Resolved
