# Technology Stack

**Analysis Date:** 2026-02-02

## Languages

**Primary:**
- C# 13.0 - WPF desktop application, interactive code execution, model handling
- Dart (target language) - Backend support via Dart Frog, interactively executed in students' code

**Secondary:**
- XAML - UI markup for WPF application
- JSON - Configuration, course content, user progress serialization
- Markdown - Lesson content, documentation

## Runtime

**Environment:**
- .NET 8.0 (net8.0-windows for WPF; net8.0 for test projects)
- Windows 10 (1809) or later as primary target platform

**Package Manager:**
- NuGet
- Lockfile: Yes (implicit in .NET project files)

## Frameworks

**Core:**
- WPF (Windows Presentation Foundation) - Desktop UI framework for `native-app-wpf/CodeTutor.Wpf.csproj`
- Microsoft.Extensions.DependencyInjection 8.0.0 - Dependency injection container

**Code Execution:**
- Microsoft.CodeAnalysis.CSharp.Scripting 4.8.0 - In-process C# code compilation and execution via Roslyn
- Microsoft.ML.OnnxRuntimeGenAI.DirectML 0.5.2 - ONNX Runtime for local Phi-4 model inference (DirectML GPU acceleration)

**Code Editor:**
- AvalonEdit 6.3.0.90 - WPF-based code editor control with syntax highlighting

**AI/ML:**
- ONNX Runtime GenAI - Runs locally hosted Phi-4 mini model for intelligent tutoring

**Testing:**
- xunit 2.6.3 - Test framework
- FluentAssertions 6.12.0 - Assertion library
- Moq 4.20.70 - Mocking framework
- Microsoft.NET.Test.Sdk 17.8.0 - Test runner infrastructure
- coverlet.collector 6.0.0 - Code coverage collection

**Build/Dev:**
- Inno Setup 6 (external) - Windows installer creation
- PowerShell - Build automation scripts

## Key Dependencies

**Critical:**
- AvalonEdit 6.3.0.90 - Provides rich code editing experience essential to user learning interface
- Microsoft.ML.OnnxRuntimeGenAI.DirectML 0.5.2 - Enables local AI tutor without external API calls; DirectML provides GPU acceleration on Windows

**Infrastructure:**
- System.Text.Json 8.0.5 - JSON serialization for course content, user progress, API responses
- Microsoft.Extensions.DependencyInjection 8.0.0 - Service container for loose coupling between components

## Configuration

**Environment:**
- No .env file detected; configuration via hardcoded paths and defaults in code
- Application data stored in `%LOCALAPPDATA%\CodeTutor` directory on Windows
- Model search path: `{AppDomain.CurrentDomain.BaseDirectory}/models/phi4/gpu/gpu-int4-rtn-block-32`
- Course content path: `{AppDomain.CurrentDomain.BaseDirectory}/Content/courses`

**Build:**
- CodeTutor.Wpf.csproj - Primary WPF application
- native-app.Tests.csproj - Test project
- build-installer.ps1 - PowerShell build and packaging script
- build-installer.bat - Batch wrapper for PowerShell build script
- installer.iss - Inno Setup configuration for Windows installer creation

**Build Profiles:**
- Release (self-contained, x64 Windows)
- Linux (x64 self-contained)
- macOS (x64 and arm64 self-contained)

## Platform Requirements

**Development:**
- .NET 8.0 SDK - Required for compilation and testing
- PowerShell 5.0+ - For build automation
- Visual Studio 2022 (recommended) or VS Code with C# extensions
- Inno Setup 6 (optional) - For Windows installer creation

**Production:**
- Windows 10 (1809) or later - Primary deployment target
- Self-contained deployment - .NET runtime bundled (no system .NET required)
- Language runtimes detected at runtime (optional, for code execution):
  - Python 3.10+ (via `python` command)
  - Node.js 18.15.0+ (via `node` command)
  - Java 15.0.2+ (via `javac` and `java`)
  - Kotlin 1.8.20+ (via `kotlinc`)
  - Rust 1.68.2+ (via `rustc`)
  - Dart 2.19.6+ (via `dart`)

---

*Stack analysis: 2026-02-02*
