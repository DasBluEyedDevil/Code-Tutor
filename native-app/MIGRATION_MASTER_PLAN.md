# Code Tutor: Electron → Native C#/Avalonia Migration Master Plan

## Executive Summary

**Goal:** Migrate complete Code Tutor functionality from Electron/React to native C#/Avalonia desktop application.

**Status:** Foundation complete, full feature migration in progress.

**Estimated Effort:** 120-160 hours

---

## Migration Phases

### ✅ Phase 0: Foundation (COMPLETE)
- [x] Native project structure created
- [x] Basic window and UI framework
- [x] Course data models
- [x] Course loading service
- [x] Code execution engine
- [x] Initial documentation

**Files Created:**
- `CodeTutor.Native.csproj`
- `Program.cs`, `App.axaml`
- `Models/Course.cs`, `Models/ExecutionResult.cs`
- `Services/CourseService.cs`, `Services/CodeExecutor.cs`
- `ViewModels/MainWindowViewModel.cs`
- `Views/MainWindow.axaml`

---

### 🔄 Phase 1: Core UI & Navigation (Week 1)
**Priority: CRITICAL**

#### 1.1 Page System
- [ ] Implement navigation framework (routing)
- [ ] Create LandingPage (course selection)
- [ ] Create CoursePage (module/lesson browser)
- [ ] Create LessonPage (main learning interface)
- [ ] Create NotFoundPage (404 handler)

#### 1.2 Layout Components
- [ ] Main navigation bar
- [ ] Breadcrumb navigation
- [ ] Progress indicators
- [ ] Loading states and spinners
- [ ] Error boundaries

#### 1.3 Theme System
- [ ] Dark theme (primary)
- [ ] Light theme (optional)
- [ ] Theme persistence
- [ ] Dynamic theme switching
- [ ] Color schemes for syntax highlighting

**Dependencies:** None
**Estimated Time:** 25-30 hours
**Documentation Required:** `UI_ARCHITECTURE.md`, `NAVIGATION_SPEC.md`

---

### 🔄 Phase 2: Challenge System (Week 2)
**Priority: CRITICAL**

#### 2.1 Challenge Infrastructure
- [ ] Base challenge interface/contract
- [ ] Challenge factory pattern
- [ ] Validation engine
- [ ] Test result display component
- [ ] Scoring system

#### 2.2 Six Challenge Types

##### 2.2.1 Multiple Choice Challenge
- [ ] Radio button UI with A/B/C/D labels
- [ ] Answer validation
- [ ] Explanation display
- [ ] Hints panel integration

##### 2.2.2 True/False Challenge
- [ ] Large True/False buttons
- [ ] Answer validation
- [ ] Explanation display

##### 2.2.3 Code Output Challenge
- [ ] Code display (read-only)
- [ ] Text input for expected output
- [ ] Output comparison
- [ ] "Run to see output" option

##### 2.2.4 Free Coding Challenge
- [ ] Full code editor integration (AvaloniaEdit)
- [ ] Starter code loading
- [ ] Test case execution
- [ ] Solution reveal
- [ ] Bonus challenges

##### 2.2.5 Code Completion Challenge
- [ ] TODO marker highlighting
- [ ] Partial code validation
- [ ] Inline hints

##### 2.2.6 Conceptual Challenge
- [ ] Multi-line text input
- [ ] Sample answer display
- [ ] Manual grading support

#### 2.3 Validation System
- [ ] Exact match validator
- [ ] Contains validator
- [ ] Regex pattern validator
- [ ] Whitespace normalization
- [ ] Case-insensitive comparison

**Dependencies:** Phase 1 (UI framework)
**Estimated Time:** 35-40 hours
**Documentation Required:** `CHALLENGE_SYSTEM.md`, `VALIDATION_SPEC.md`

---

### 🔄 Phase 3: Code Editor Integration (Week 3)
**Priority: HIGH**

#### 3.1 AvaloniaEdit Setup
- [ ] AvaloniaEdit NuGet package
- [ ] TextMate grammar integration
- [ ] Syntax highlighting for 7 languages:
  - Python
  - JavaScript
  - Java
  - Rust
  - C#
  - Kotlin
  - Dart/Flutter

#### 3.2 Editor Features
- [ ] Line numbers
- [ ] Code folding
- [ ] Auto-indentation
- [ ] Bracket matching
- [ ] Multi-cursor support
- [ ] Find/Replace
- [ ] Undo/Redo stack

#### 3.3 Editor Configuration
- [ ] Font family/size settings
- [ ] Tab size (2/4 spaces)
- [ ] Word wrap toggle
- [ ] Minimap (optional)
- [ ] Ligatures support

#### 3.4 Code Execution Integration
- [ ] Run button in editor toolbar
- [ ] Keyboard shortcut (Ctrl+Enter)
- [ ] Output panel
- [ ] Error highlighting
- [ ] Execution status indicator

**Dependencies:** Phase 1 (UI framework)
**Estimated Time:** 20-25 hours
**Documentation Required:** `CODE_EDITOR_SPEC.md`

---

### 🔄 Phase 4: State Management & Persistence (Week 4)
**Priority: HIGH**

#### 4.1 Data Stores (Replace Zustand)
Replace 7 Zustand stores with C# services:

##### 4.1.1 ProgressStore
- [ ] Lesson completion tracking
- [ ] Challenge scores
- [ ] Current lesson position
- [ ] Per-course progress
- [ ] SQLite database schema

##### 4.1.2 AchievementsStore
- [ ] 10 achievement types
- [ ] Unlock conditions
- [ ] Progress tracking
- [ ] Notification system

##### 4.1.3 ThemeStore
- [ ] Current theme (dark/light)
- [ ] Custom color overrides
- [ ] Persistence to settings file

##### 4.1.4 PreferencesStore
- [ ] Editor settings
- [ ] Auto-save enabled/delay
- [ ] Keyboard shortcuts
- [ ] UI preferences

##### 4.1.5 AuthStore (Desktop Single User)
- [ ] User profile
- [ ] Settings persistence
- [ ] Optional cloud sync preparation

##### 4.1.6 KeyboardStore
- [ ] Shortcut registration
- [ ] Customizable bindings
- [ ] Conflict detection

##### 4.1.7 NotificationStore
- [ ] Toast notifications
- [ ] Achievement unlocks
- [ ] Error messages
- [ ] Success confirmations

#### 4.2 Persistence Layer
- [ ] SQLite database setup (`CodeTutor.db`)
- [ ] Settings file (`settings.json`)
- [ ] Migration from localStorage data
- [ ] Backup/restore functionality

**Dependencies:** None (parallel to other phases)
**Estimated Time:** 20-25 hours
**Documentation Required:** `STATE_MANAGEMENT.md`, `DATABASE_SCHEMA.md`

---

### 🔄 Phase 5: Interactive Features (Week 5)
**Priority: MEDIUM**

#### 5.1 Hints System
- [ ] Progressive hint reveal (5 levels)
- [ ] Markdown rendering in hints
- [ ] Hint usage tracking
- [ ] Cost/penalty system
- [ ] Collapsible UI panel

#### 5.2 Auto-Save System
- [ ] Debounced save (configurable delay)
- [ ] Visual save indicator
- [ ] Draft recovery
- [ ] Per-lesson state

#### 5.3 Command Palette
- [ ] Fuzzy search UI (Ctrl+K)
- [ ] Course navigation
- [ ] Quick actions
- [ ] Recent items

#### 5.4 Settings Panel
- [ ] Tabbed interface
- [ ] Editor settings
- [ ] Appearance settings
- [ ] Keyboard shortcuts editor
- [ ] Data management (backup/restore)

#### 5.5 Keyboard Shortcuts
Implement 7 global shortcuts:
- [ ] `Ctrl+K` - Command palette
- [ ] `Ctrl+Enter` - Run code
- [ ] `Ctrl+S` - Save progress
- [ ] `Ctrl+/` - Toggle hints
- [ ] `Ctrl+R` - Reset challenge
- [ ] `Ctrl+.` - Settings
- [ ] `F1` - Help

**Dependencies:** Phase 1, 2
**Estimated Time:** 15-20 hours
**Documentation Required:** `INTERACTIVE_FEATURES.md`

---

### 🔄 Phase 6: Content Rendering (Week 6)
**Priority: MEDIUM**

#### 6.1 Markdown Rendering
- [ ] Markdown.Avalonia integration
- [ ] GitHub Flavored Markdown support
- [ ] Code block syntax highlighting
- [ ] Tables support
- [ ] Links (internal navigation + external)
- [ ] Images (embedded in markdown)

#### 6.2 Lesson Content Display
- [ ] Header with lesson title
- [ ] Body content (markdown)
- [ ] Code examples panel
- [ ] Key concepts highlights
- [ ] Learning objectives

#### 6.3 Code Examples
- [ ] Read-only code display
- [ ] Copy to clipboard button
- [ ] "Try it" button (load into editor)
- [ ] Explanation tooltips

#### 6.4 Common Mistakes Panel
- [ ] Collapsible panel
- [ ] Mistake patterns
- [ ] How to fix guidance
- [ ] Related hints

**Dependencies:** Phase 3 (editor)
**Estimated Time:** 12-15 hours
**Documentation Required:** `CONTENT_RENDERING.md`

---

### 🔄 Phase 7: Achievements & Gamification (Week 7)
**Priority: LOW**

#### 7.1 Achievement System
Implement 10 achievements:
- [ ] **First Steps** - Complete first lesson
- [ ] **Quick Learner** - Complete lesson in under 30 minutes
- [ ] **Perfectionist** - 100% score on all challenges in lesson
- [ ] **Polyglot** - Complete lessons in 3+ languages
- [ ] **Marathon Runner** - 7-day learning streak
- [ ] **Speed Demon** - Complete 5 challenges without hints
- [ ] **Debugger** - Fix 10 failing test cases
- [ ] **Course Complete** - Finish entire course
- [ ] **Test Master** - Pass 100 test cases
- [ ] **Night Owl** - Complete lesson after 10 PM

#### 7.2 Achievement UI
- [ ] Achievement unlock animations
- [ ] Progress bars
- [ ] Achievement gallery
- [ ] Share functionality (export image)

#### 7.3 Gamification Elements
- [ ] Streak counter
- [ ] Points/score system
- [ ] Leaderboard (local)
- [ ] Progress visualization

**Dependencies:** Phase 4 (state management)
**Estimated Time:** 10-12 hours
**Documentation Required:** `ACHIEVEMENTS_SPEC.md`

---

### 🔄 Phase 8: Polish & UX (Week 8)
**Priority: MEDIUM**

#### 8.1 Animations & Transitions
- [ ] Page transitions
- [ ] Challenge completion celebrations
- [ ] Loading animations
- [ ] Smooth scrolling
- [ ] Button hover effects

#### 8.2 Accessibility
- [ ] Keyboard navigation (tab order)
- [ ] Screen reader support
- [ ] High contrast mode
- [ ] Font scaling
- [ ] Focus indicators

#### 8.3 Error Handling
- [ ] Graceful error displays
- [ ] Retry mechanisms
- [ ] Offline mode detection
- [ ] Missing runtime warnings
- [ ] Corrupt data recovery

#### 8.4 Performance Optimization
- [ ] Lazy loading for lessons
- [ ] Virtual scrolling for lists
- [ ] Code editor performance tuning
- [ ] Startup time optimization

**Dependencies:** All previous phases
**Estimated Time:** 15-18 hours
**Documentation Required:** `UX_GUIDELINES.md`, `ACCESSIBILITY.md`

---

### 🔄 Phase 9: Testing & Quality Assurance (Week 9)
**Priority: HIGH**

#### 9.1 Unit Tests
- [ ] Service layer tests
- [ ] Validation logic tests
- [ ] Data model tests
- [ ] Utility function tests

#### 9.2 Integration Tests
- [ ] Course loading end-to-end
- [ ] Code execution end-to-end
- [ ] Challenge submission flow
- [ ] Progress persistence

#### 9.3 UI Tests
- [ ] View model tests
- [ ] Navigation tests
- [ ] Command tests
- [ ] Data binding validation

#### 9.4 Manual Testing
- [ ] Complete 1 full course
- [ ] Test all 6 challenge types
- [ ] Verify all achievements
- [ ] Test all keyboard shortcuts
- [ ] Cross-platform testing (Windows/macOS/Linux)

**Dependencies:** All implementation phases
**Estimated Time:** 20-25 hours
**Documentation Required:** `TESTING_PLAN.md`, `QA_CHECKLIST.md`

---

### 🔄 Phase 10: Packaging & Distribution (Week 10)
**Priority: CRITICAL**

#### 10.1 Build Configuration
- [ ] Release build optimization
- [ ] AOT compilation settings
- [ ] Trimming configuration
- [ ] Asset bundling

#### 10.2 Platform-Specific Packaging
- [ ] Windows installer (NSIS/WiX)
- [ ] macOS DMG/PKG
- [ ] Linux AppImage/DEB/RPM
- [ ] Code signing certificates

#### 10.3 Distribution
- [ ] GitHub Releases setup
- [ ] Auto-update mechanism
- [ ] Version numbering scheme
- [ ] Release notes automation

#### 10.4 Documentation
- [ ] User guide
- [ ] Installation instructions
- [ ] Troubleshooting guide
- [ ] FAQ

**Dependencies:** Phase 9 (testing complete)
**Estimated Time:** 15-20 hours
**Documentation Required:** `PACKAGING_GUIDE.md`, `DISTRIBUTION.md`

---

## Feature Migration Checklist

### Pages (3 total)
- [ ] LandingPage - Course selection grid
- [ ] CoursePage - Module and lesson browser
- [ ] LessonPage - Main learning interface with split panels

### UI Components (15+ total)
- [ ] Card - Reusable container
- [ ] Badge - Labels and tags
- [ ] Button - Multiple variants
- [ ] ProgressBar - Linear progress indicator
- [ ] LoadingSpinner - Async operation indicator
- [ ] EmptyState - No content placeholder
- [ ] ErrorBoundary - Error handling wrapper
- [ ] Modal/Dialog - Overlays
- [ ] Tooltip - Contextual help
- [ ] Toast/Notification - Temporary messages
- [ ] Tabs - Tabbed interfaces
- [ ] Accordion - Collapsible sections
- [ ] CommandPalette - Quick actions (Ctrl+K)
- [ ] Settings panel - Configuration UI
- [ ] Achievements panel - Gamification UI

### Challenge Types (6 total)
- [ ] MultipleChoiceChallenge
- [ ] TrueFalseChallenge
- [ ] CodeOutputChallenge
- [ ] FreeCodingChallenge
- [ ] CodeCompletionChallenge
- [ ] ConceptualChallenge

### Services (7+ total)
- [x] CourseService - Load courses
- [x] CodeExecutor - Execute code
- [ ] ChallengeValidator - Validate submissions
- [ ] ProgressService - Track user progress
- [ ] AchievementService - Manage achievements
- [ ] ThemeService - Theme management
- [ ] SettingsService - Preferences persistence

### Interactive Features
- [ ] Auto-save with debounce
- [ ] Hints system (5-level progressive)
- [ ] Keyboard shortcuts (7 global)
- [ ] Command palette
- [ ] Settings panel
- [ ] Code editor (AvaloniaEdit)
- [ ] Syntax highlighting (7 languages)
- [ ] Achievement notifications

### Content Features
- [ ] Markdown rendering
- [ ] Code syntax highlighting
- [ ] Code examples with "Try it" button
- [ ] Common mistakes panel
- [ ] Hints panel
- [ ] Test results display
- [ ] Solution reveal

### State Management
- [ ] Progress tracking (SQLite)
- [ ] Achievements (SQLite)
- [ ] Theme preferences
- [ ] Editor settings
- [ ] Keyboard shortcuts
- [ ] Notifications queue
- [ ] Auth/user profile

---

## Critical Path

**Must Complete in Order:**

1. **Phase 1** (UI/Navigation) → Enables all other UI work
2. **Phase 2** (Challenges) → Core learning functionality
3. **Phase 4** (State Management) → Required for progress tracking
4. **Phase 9** (Testing) → Quality gate
5. **Phase 10** (Packaging) → Delivery

**Can Parallelize:**

- Phase 3 (Editor) + Phase 6 (Content Rendering)
- Phase 5 (Interactive Features) + Phase 7 (Achievements)
- Phase 8 (Polish) can happen throughout

---

## Risk Assessment

### High Risk
- **AvaloniaEdit complexity** - Monaco replacement may require custom work
- **Markdown rendering** - Need to ensure feature parity with React Markdown
- **Performance** - Large courses may require optimization

### Medium Risk
- **Cross-platform testing** - Need access to Windows/macOS/Linux
- **Code signing** - Certificate costs and setup
- **Data migration** - Converting localStorage to SQLite

### Low Risk
- **Core architecture** - Foundation is solid
- **Data models** - Already defined
- **Service layer** - Straightforward C# implementation

---

## Success Metrics

### Functional Completeness
- [ ] All 6 challenge types working
- [ ] All 7 programming languages supported
- [ ] All 10 achievements unlockable
- [ ] All 3 pages navigable
- [ ] All keyboard shortcuts functional

### Performance Targets
- [ ] Startup time < 1 second
- [ ] Code execution < 5 seconds
- [ ] Page navigation < 100ms
- [ ] Memory usage < 100MB idle

### Quality Gates
- [ ] 80%+ unit test coverage
- [ ] All integration tests passing
- [ ] Zero critical bugs
- [ ] Cross-platform verified
- [ ] Accessibility audit passed

---

## Migration Execution Timeline

### Weeks 1-2: Foundation
- Phase 1: Core UI & Navigation
- Phase 2: Challenge System

### Weeks 3-4: Editor & Data
- Phase 3: Code Editor Integration
- Phase 4: State Management

### Weeks 5-6: Features & Content
- Phase 5: Interactive Features
- Phase 6: Content Rendering

### Weeks 7-8: Polish & Extras
- Phase 7: Achievements
- Phase 8: Polish & UX

### Weeks 9-10: Quality & Release
- Phase 9: Testing & QA
- Phase 10: Packaging & Distribution

**Total Estimated Time:** 10 weeks (120-160 hours)

---

## Documentation Dependencies

This master plan requires the following supporting documents:

### Architecture & Design
- [ ] `NATIVE_ARCHITECTURE.md` - Overall system architecture
- [ ] `UI_ARCHITECTURE.md` - UI framework and patterns
- [ ] `NAVIGATION_SPEC.md` - Routing and navigation
- [ ] `DATABASE_SCHEMA.md` - SQLite schema design

### Component Specifications
- [ ] `CHALLENGE_SYSTEM.md` - Detailed challenge implementation
- [ ] `VALIDATION_SPEC.md` - Validation engine specification
- [ ] `CODE_EDITOR_SPEC.md` - Editor integration guide
- [ ] `CONTENT_RENDERING.md` - Markdown and content display

### Feature Specifications
- [ ] `STATE_MANAGEMENT.md` - Store architecture and persistence
- [ ] `INTERACTIVE_FEATURES.md` - Auto-save, hints, shortcuts
- [ ] `ACHIEVEMENTS_SPEC.md` - Gamification system
- [ ] `UX_GUIDELINES.md` - Design patterns and conventions

### Quality & Delivery
- [ ] `TESTING_PLAN.md` - Test strategy and coverage
- [ ] `QA_CHECKLIST.md` - Manual testing checklist
- [ ] `PACKAGING_GUIDE.md` - Build and packaging instructions
- [ ] `ACCESSIBILITY.md` - Accessibility requirements

### Migration Guides
- [ ] `DATA_MIGRATION.md` - Converting localStorage to SQLite
- [ ] `COMPONENT_MAPPING.md` - React → Avalonia component mapping
- [ ] `API_MIGRATION.md` - HTTP/IPC → Direct calls mapping

---

## Next Steps

1. **Review this master plan** - Approve scope and timeline
2. **Create supporting documentation** - Detailed specs for each phase
3. **Set up project board** - Track progress in GitHub Issues
4. **Begin Phase 1 implementation** - Start with navigation framework
5. **Establish CI/CD pipeline** - Automated builds and tests

---

## Appendix: Technology Stack

### Development
- **Framework:** .NET 8
- **UI:** Avalonia 11.1.0
- **MVVM:** ReactiveUI
- **Code Editor:** AvaloniaEdit 11.1.0
- **Markdown:** Markdown.Avalonia 11.0.3
- **Database:** Microsoft.Data.Sqlite 8.0.0
- **Testing:** xUnit + FluentAssertions

### Build & Distribution
- **Build:** dotnet CLI
- **Packaging:** electron-builder → dotnet publish
- **Installers:** NSIS (Windows), DMG (macOS), AppImage (Linux)
- **CI/CD:** GitHub Actions

### Migration Tools
- **Data Conversion:** Custom C# scripts
- **Asset Migration:** Automated file copying
- **Testing:** Parallel Electron vs Native comparison

---

**Document Version:** 1.0
**Last Updated:** 2025-11-18
**Status:** Active Migration In Progress
