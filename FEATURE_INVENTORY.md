# Code Tutor - Comprehensive Feature Inventory

## OVERVIEW
Code Tutor is a dual-platform interactive programming education application with:
- **Web App**: React/Vite frontend with Monaco code editor
- **Desktop App**: Electron wrapper with native code execution and IPC communication
- **7 Programming Languages**: Java, Python, Kotlin, Rust, C#, Flutter, JavaScript/TypeScript (coming soon)
- **340+ Lessons** across multiple courses

---

## 1. UI COMPONENTS AND VIEWS

### 1.1 PAGES

#### Landing Page (`/pages/LandingPage.tsx`)
- **Purpose**: Main entry point with course selection
- **Features**:
  - Hero section with gradient animations
  - Stats display: 340+ lessons, 7 languages, 100% free
  - Feature cards: Concept-First Learning, Instant Execution, Track Progress
  - Language cards grid with:
    - Course descriptions
    - Lesson count
    - Difficulty badges
    - "Coming Soon" indicators
  - Theme toggle (Light/Dark mode)
  - Animated background with floating shapes

**Key Stats Displayed**:
- 340+ Lessons
- 7 Languages
- 100% Free

**Route**: `/`

#### Course Page (`/pages/CoursePage.tsx`)
- **Purpose**: Browse modules and lessons for a specific language
- **Features**:
  - Course header with:
    - Course title and description
    - Overall progress percentage
    - Difficulty level badge
  - Info cards:
    - Total modules count
    - Estimated completion time
    - Course difficulty
  - Module list with:
    - Module title and description
    - Module progress bar
    - Completed/Total lessons counter
    - Estimated hours
  - Expandable lesson list with:
    - Completion status indicator
    - Lesson title
    - Lesson type badge (reading/interactive/project)
    - Difficulty badge
    - Estimated minutes
    - Breadcrumb navigation

**Route**: `/course/:language`

#### Lesson Page (`/pages/LessonPage.tsx`)
- **Purpose**: Interactive learning environment for individual lessons
- **Features**:
  - Responsive split-view (desktop) / tab-based (mobile)
  - Left panel: Lesson content
    - Markdown-rendered lesson body
    - Syntax-highlighted code examples
    - Exercise instructions
    - Hints panel (expandable)
  - Right panel: Code editor
    - Monaco editor with syntax highlighting
    - "Run Code" button with loading state
    - Output display with color-coded success/error
    - Test results visualization
  - Navigation buttons: Previous, Mark Complete
  - Mobile toggle: Switch between Content and Editor views
  - Tab bar (mobile): Content | Editor

**Key Interactive Elements**:
- Code execution with test results
- Auto-save functionality
- Lesson completion tracking
- Navigation between lessons

**Route**: `/course/:language/module/:moduleId/lesson/:lessonId`

#### Not Found Page (`/pages/NotFoundPage.tsx`)
- 404 error handling
- Link back to home

---

### 1.2 CORE UI COMPONENTS

#### Command Palette (`/components/CommandPalette.tsx`)
- **Purpose**: Quick action/navigation menu
- **Features**:
  - Search input with real-time filtering
  - Grouped actions by category:
    - Navigation
    - Settings
    - Help
    - Lessons
  - Keyboard navigation (↑↓ to navigate, Enter to execute)
  - Mouse interaction support
  - Focus management with focus trap
  - ESC to close
  - Category icons and labels
  - Action descriptions
- **Default Actions**:
  - Go to Home
  - Open Settings
  - Show Keyboard Shortcuts

**Activation**: Ctrl+K

#### Settings Modal (`/components/Settings.tsx`)
- **Sections**:
  1. **Appearance**:
     - Theme (Light/Dark)
     - Animation preference (Auto/Always/Reduced motion)
  2. **Code Editor**:
     - Font size (10-24px)
     - Tab size (2/4/8 spaces)
     - Word wrap toggle
     - Minimap toggle
     - Format on paste
     - Format on type
  3. **Learning**:
     - Auto-save toggle
     - Auto-save delay (1-10 seconds)
     - Sound effects toggle
     - Notifications toggle
  4. **Reset to Defaults** button
- **Accessibility**: Focus trap, modal dialog with proper ARIA

#### Achievements Modal (`/components/Achievements.tsx`)
- **Displays**:
  - Stats overview:
    - Total lessons completed
    - Total courses completed
    - Current day streak with flame icon
    - Tests passed count
  - Bookmarked lessons (up to 5 shown)
  - Achievement categories:
    - Unlocked achievements (earned)
    - In Progress achievements (with progress bars)
    - Locked achievements (greyed out)
  - Each achievement shows:
    - Icon (emoji)
    - Title
    - Description
    - Progress percentage (if not earned)
    - Earn date (if earned)
    - Badge status

**Activation**: Ctrl+A

#### Keyboard Shortcuts Help (`/components/KeyboardShortcutsHelp.tsx`)
- Lists all registered keyboard shortcuts
- Includes descriptions and visual keyboard representations
- Platform-aware (Mac: ⌘, Windows: Ctrl)

**Activation**: ? key

#### Toast Notifications (`/components/Toast.tsx`)
- **Types**: success, error, warning, info
- **Features**:
  - Auto-dismiss after duration
  - Manual dismiss button
  - Color-coded by type
  - Stacked display

#### Other Components
- `Badge.tsx`: Difficulty/status badges
- `Button.tsx`: Customizable buttons with variants (primary, outline, success, danger, ghost)
- `Card.tsx`: Reusable card containers with hover effects
- `ProgressBar.tsx`: Visual progress indicators with optional labels
- `Loading.tsx`: Spinner and skeleton card loaders
- `EmptyState.tsx`: Placeholder for empty content
- `ErrorBoundary.tsx`: Error handling wrapper
- `SkipToContent.tsx`: Accessibility link for keyboard users

---

## 2. CHALLENGE TYPES

### 2.1 CHALLENGE ARCHITECTURE

All challenges are routed through `ChallengeContainer.tsx` which:
- Determines challenge type
- Manages submission state
- Handles API communication
- Provides error handling and loading states

### 2.2 CHALLENGE TYPES DETAILED

#### Type 1: MULTIPLE_CHOICE
**File**: `/components/challenges/MultipleChoiceChallenge.tsx`

**Structure**:
```typescript
{
  type: 'MULTIPLE_CHOICE',
  id: string,
  title: string,
  description?: string,
  options: string[], // Rendered as A, B, C, D...
  correctAnswer: number | string, // 0-based index or letter
  explanation: string,
  hints?: Hint[],
  estimatedMinutes?: number,
  difficulty?: 'beginner' | 'intermediate' | 'advanced'
}
```

**UI Features**:
- Radio button options with letter labels (A, B, C, D...)
- Options rendered with Markdown support
- Visual feedback after submission:
  - Correct answer: Green highlight + checkmark
  - Wrong selection: Red highlight + X mark
  - Other options: Faded out
- Explanation shown after submission
- "Try Another Question" reset button
- Hints panel (expandable before submission)

**Validation Logic**:
- Exact match between selected index and correctAnswer
- Tracks hints used
- Single submission per attempt

---

#### Type 2: TRUE_FALSE
**File**: `/components/challenges/TrueFalseChallenge.tsx`

**Structure**:
```typescript
{
  type: 'TRUE_FALSE',
  id: string,
  title: string,
  description?: string,
  question: string,
  correctAnswer: boolean,
  explanation: string,
  hints?: Hint[],
  estimatedMinutes?: number,
  difficulty?: 'beginner' | 'intermediate' | 'advanced'
}
```

**UI Features**:
- Large TRUE/FALSE buttons with icons (👍 / 👎)
- Question displayed in card above buttons
- Color-coded feedback:
  - TRUE button: Green when correct
  - FALSE button: Red when correct
  - Selected wrong: Faded out
- Explanation shown after submission
- "Try Another Question" reset button
- Hints panel (before submission)

**Validation Logic**:
- Boolean comparison
- Tracks hints used
- Single submission

---

#### Type 3: CODE_OUTPUT
**File**: `/components/challenges/CodeOutputChallenge.tsx`

**Structure**:
```typescript
{
  type: 'CODE_OUTPUT',
  id: string,
  title: string,
  description?: string,
  code: string,
  language: string,
  correctOutput: string,
  explanation: string,
  hints?: Hint[],
  estimatedMinutes?: number,
  difficulty?: 'beginner' | 'intermediate' | 'advanced'
}
```

**UI Features**:
- Code displayed in read-only Monaco editor with syntax highlighting
- Textarea for user to enter predicted output
- Output comparison display:
  - Expected output (green)
  - User's output (green if correct, red if wrong)
  - Side-by-side layout
- Explanation after submission
- "Try Another Question" reset button
- Hints panel (before submission)

**Validation Logic**:
- Output normalization (trim whitespace, normalize newlines)
- Case-sensitive comparison
- Tracks hints used

---

#### Type 4: FREE_CODING
**File**: `/components/challenges/FreeCodingChallenge.tsx`

**Structure**:
```typescript
{
  type: 'FREE_CODING',
  id: string,
  title: string,
  description?: string,
  instructions: string,
  starterCode: string,
  solution: string,
  language: string,
  testCases: TestCase[],
  commonMistakes?: CommonMistake[],
  bonusChallenges?: BonusChallenge[],
  hints?: Hint[],
  estimatedMinutes?: number,
  difficulty?: 'beginner' | 'intermediate' | 'advanced' | 1 | 2 | 3 | 4 | 5
}

interface TestCase {
  id?: string,
  description: string,
  inputs?: any[],
  expectedOutput: any,
  isVisible: boolean,
  testType?: 'exact' | 'contains' | 'regex' | 'pattern',
  customMessage?: string
}
```

**UI Features**:
- Instructions panel (Markdown rendered)
- 3-column layout:
  - Left column (2/3): Monaco editor + test runner
  - Right column (1/3): Hints, common mistakes, bonus challenges
- Action buttons:
  - **Run Tests**: Validates code against test cases
  - **Reset Code**: Reverts to starter code
  - **Show/Hide Solution**: Reveals correct solution
- Test results visualization:
  - Summary: "X of Y tests passed"
  - Color-coded: Green (all passed), Yellow (some passed), Red (none passed)
  - Expandable test details:
    - Test description
    - Passed/Failed status
    - Expected vs. Actual output
- Features when all tests pass:
  - Bonus challenges revealed (if available)
  - Success message and celebration emoji
- Right-side panels (collapsible):
  - **Hints Panel**: Progressive hint reveal (Levels 1-5)
  - **Common Mistakes Panel**: Expandable mistakes with wrong/right code examples
  - **Bonus Challenges**: Shown only after all tests pass

**Validation Logic**:
- Test case execution and comparison
- Supports 4 test comparison types: exact, contains, regex, pattern
- Only visible tests shown to user, hidden tests validated server-side
- Tracks hints used
- Completion triggered when all tests pass
- Solution shown read-only when revealed

---

#### Type 5: CODE_COMPLETION
**File**: `/components/challenges/CodeCompletionChallenge.tsx`

**Structure**:
```typescript
{
  type: 'CODE_COMPLETION',
  id: string,
  title: string,
  description?: string,
  starterCode: string, // Contains TODO markers
  solution: string,
  language: string,
  testCases: TestCase[],
  commonMistakes?: CommonMistake[],
  hints?: Hint[],
  estimatedMinutes?: number,
  difficulty?: 'beginner' | 'intermediate' | 'advanced'
}
```

**UI Features**:
- Similar to FREE_CODING but emphasizes code completion
- Instructions: "✏️ Complete the Code"
- Editor detects TODO markers and highlights them
- Auto-selects first TODO on editor mount
- Identical button layout and results display to FREE_CODING
- Success message: "🎯 Code Completed!"
- Same right-side panels: Hints, Common Mistakes

**Validation Logic**:
- Identical to FREE_CODING
- Test cases must pass for completion
- TODO detection for visual guidance

---

#### Type 6: CONCEPTUAL
**File**: `/components/challenges/ConceptualChallenge.tsx`

**Structure**:
```typescript
{
  type: 'CONCEPTUAL',
  id: string,
  title: string,
  description?: string,
  question: string,
  sampleAnswers?: string[],
  explanation: string,
  hints?: Hint[],
  estimatedMinutes?: number,
  difficulty?: 'beginner' | 'intermediate' | 'advanced'
}
```

**UI Features**:
- Large textarea for free-form answer entry
- Not auto-graded (marked complete on submission)
- Features:
  - Hint reveal button (click to reveal one hint at a time)
  - Submitted answer display
  - Explanation section
  - Sample good answers (toggleable, expandable)
  - Additional context section (if provided)
- Helpful text: "This will not be auto-graded - answer thoughtfully"

**Validation Logic**:
- No auto-validation
- Completion occurs when user submits an answer
- Tracks hints used
- Server-side manual grading support

---

### 2.3 SHARED CHALLENGE FEATURES

#### Hints System (All Challenge Types)
**File**: `/components/challenges/HintsPanel.tsx`

**Features**:
- Progressive hint reveal (one at a time)
- 5 hint levels:
  - Levels 1-2: "Gentle Nudge" (blue)
  - Levels 3-4: "Strong Hint" (orange)
  - Level 5: "Almost Solution" (red)
- Each hint can include:
  - Markdown text
  - Code snippet
- Warning message on first reveal
- Tracks total hints used
- "Reveal Next Hint" button with counter
- All hints revealed message
- Impact on scoring

**Tracking**:
- Hints used count passed to submission
- Used for achievement unlocking

---

#### Common Mistakes Panel (FREE_CODING, CODE_COMPLETION)
**File**: `/components/challenges/CommonMistakesPanel.tsx`

**Structure**:
```typescript
interface CommonMistake {
  mistake: string,
  consequence: string,
  correction: string,
  example?: {
    wrong: string,
    right: string
  }
}
```

**UI Features**:
- Expandable panel with all common mistakes
- Table-like layout showing:
  - Mistake description
  - Consequence of the mistake
  - Correction approach
- Code examples (if provided):
  - Side-by-side comparison
  - Red box: Wrong code
  - Green box: Correct code
  - Color-coded markers
- Expandable mistake items for viewing code examples
- Default collapsed but expandable

---

#### Test Results Panel (FREE_CODING, CODE_COMPLETION)
**File**: `/components/challenges/TestResultsPanel.tsx`

**Displays**:
- Summary of all test results
- For each test case:
  - Test description
  - Pass/Fail status with icon
  - Expected output
  - Actual output (if failed)
  - Error message (if applicable)
- Color coding by test status

---

---

## 3. STATE MANAGEMENT

All state management uses **Zustand** with **persist middleware** for localStorage persistence.

### 3.1 STORES

#### Progress Store (`/stores/progressStore.ts`)

**Persisted to**: `progress-storage`

**State**:
```typescript
interface LessonProgress {
  lessonId: string,
  completed: boolean,
  lastAccessed: string,  // ISO timestamp
  codeSubmissions: number,
  timeSpent: number  // minutes
}

interface CourseProgress {
  [courseId: string]: {
    [moduleId: string]: {
      [lessonId: string]: LessonProgress
    }
  }
}
```

**Actions**:
- `markLessonComplete(courseId, moduleId, lessonId)`: Mark lesson as done
- `updateLessonProgress(courseId, moduleId, lessonId, data)`: Update specific lesson data
- `getLessonProgress(courseId, moduleId, lessonId)`: Retrieve lesson progress
- `getCourseProgress(courseId)`: Calculate overall course progress percentage

**Data Calculated**:
- Overall course completion percentage
- Module progress percentage
- Total completed lessons count

---

#### Achievements Store (`/stores/achievementsStore.ts`)

**Persisted to**: `achievements-storage`

**State**:
```typescript
interface Achievement {
  id: string,
  title: string,
  description: string,
  icon: string,  // emoji
  requirement: {
    type: 'lessons_completed' | 'course_completed' | 'streak_days' | 'tests_passed' | 'code_runs',
    target: number,
    courseId?: string
  },
  earned: boolean,
  earnedAt?: string,  // ISO timestamp
  progress: number  // 0-100
}

interface Bookmark {
  id: string,
  courseId: string,
  moduleId: string,
  lessonId: string,
  title: string,
  createdAt: string,
  note?: string
}

interface Stats {
  totalLessonsCompleted: number,
  totalCoursesCompleted: number,
  currentStreak: number,
  longestStreak: number,
  totalTestsPassed: number,
  totalCodeRuns: number,
  lastActiveDate: string  // YYYY-MM-DD
}
```

**Default Achievements** (10 total):
1. **First Steps** (🎯): Complete 1 lesson
2. **Getting Started** (📚): Complete 10 lessons
3. **Dedicated Learner** (🌟): Complete 50 lessons
4. **Century** (💯): Complete 100 lessons
5. **Course Graduate** (🎓): Complete 1 course
6. **Multi-linguist** (🗣️): Complete 3 courses
7. **Week Warrior** (🔥): Maintain 7-day streak
8. **Consistency Champion** (⚡): Maintain 30-day streak
9. **Test Master** (✅): Pass 100 test cases
10. **Code Runner** (▶️): Run code 100 times

**Actions**:
- `initializeAchievements()`: Create default achievements
- `checkAndUnlockAchievements()`: Check conditions and unlock if met
- `getEarnedAchievements()`: Get all unlocked achievements
- `getUnlockedCount()`: Get count of unlocked achievements
- `addBookmark(bookmark)`: Add lesson bookmark
- `removeBookmark(id)`: Remove bookmark
- `getBookmarks()`: Get all bookmarks sorted by date
- `incrementLessonsCompleted()`: Increment lesson counter and check achievements
- `incrementCoursesCompleted()`: Increment course counter
- `incrementTestsPassed(count)`: Add to test count
- `incrementCodeRuns()`: Increment code execution count
- `updateStreak()`: Calculate daily streak

**Streak Calculation**:
- Increments on consecutive days
- Resets if user skips a day
- Tracks longest streak separately

---

#### Theme Store (`/stores/themeStore.ts`)

**Persisted to**: `theme-storage`

**State**:
```typescript
type Theme = 'light' | 'dark'
type MotionPreference = 'auto' | 'always' | 'reduced'

interface ThemeStore {
  theme: Theme,
  motionPreference: MotionPreference
}
```

**Actions**:
- `toggleTheme()`: Switch between light and dark
- `setTheme(theme)`: Set specific theme
- `setMotionPreference(preference)`: Set motion preference
- `getEffectiveMotionPreference(systemPrefersReduced)`: Get final preference considering system settings

**Applied to**:
- Document root element: `dark` class added/removed
- Document root element: `reduce-motion` class added/removed
- Monaco editor: Custom themes `code-tutor-dark` and `code-tutor-light`

---

#### Preferences Store (`/stores/preferencesStore.ts`)

**Persisted to**: `user-preferences`

**State**:
```typescript
interface EditorPreferences {
  fontSize: number,  // 10-24px, default 14
  fontFamily: string,  // Monospace fonts
  tabSize: number,  // 2, 4, or 8, default 2
  wordWrap: 'on' | 'off',  // default 'on'
  minimap: boolean,  // default false
  lineNumbers: 'on' | 'off' | 'relative',  // default 'on'
  formatOnPaste: boolean,  // default true
  formatOnType: boolean  // default true
}

interface UserPreferences {
  editor: EditorPreferences,
  autoSave: boolean,  // default true
  autoSaveDelay: number,  // milliseconds, default 2000
  soundEffects: boolean,  // default true
  notifications: boolean  // default true
}
```

**Actions**:
- `setEditorPreference(key, value)`: Update individual editor setting
- `setAutoSave(enabled)`: Toggle auto-save
- `setAutoSaveDelay(delay)`: Set auto-save delay in ms
- `setSoundEffects(enabled)`: Toggle sound effects
- `setNotifications(enabled)`: Toggle notifications
- `resetToDefaults()`: Restore default preferences

---

#### Toast Store (`/stores/toastStore.ts`)

**Not persisted** (ephemeral)

**State**:
```typescript
interface Toast {
  id: string,
  message: string,
  type: 'success' | 'error' | 'warning' | 'info',
  duration?: number  // milliseconds
}
```

**Actions**:
- `addToast(toast)`: Add notification
- `removeToast(id)`: Remove notification

**Usage**:
- Success: Code execution, lesson completion
- Error: Submission failures, validation errors
- Warning: Hint usage warnings
- Info: General notifications

---

#### Keyboard Store (`/stores/keyboardStore.ts`)

**Not persisted** (runtime only)

**State**:
```typescript
interface KeyboardShortcut {
  key: string,
  ctrl?: boolean,
  meta?: boolean,
  shift?: boolean,
  alt?: boolean,
  description: string,
  action: () => void
}
```

**Actions**:
- `registerShortcuts(shortcuts)`: Register global shortcuts
- `toggleHelp()`: Toggle shortcuts help display
- `setHelpOpen(open)`: Set help visibility

**Default Shortcuts**:
| Key | Ctrl | Description |
|-----|------|-------------|
| ? | - | Show keyboard shortcuts |
| h | - | Go to home page |
| t | Yes | Toggle theme |
| , | Yes | Open settings |
| k | Yes | Open command palette |
| a | Yes | Open achievements |
| Escape | - | Close dialogs |

---

#### Auth Store (`/stores/authStore.ts`)

**Persisted to**: `auth-storage`

**State**:
```typescript
interface User {
  id: string,
  email: string,
  name: string
}

interface AuthStore {
  user?: User,
  token?: string,
  isAuthenticated: boolean
}
```

**Actions**:
- `login(email, password)`: Authenticate user
- `register(email, password, name)`: Create account
- `logout()`: Clear session
- `verifyToken()`: Validate stored token

---

---

## 4. INTERACTIVE FEATURES

### 4.1 CODE EDITOR

**Technology**: Monaco Editor (`@monaco-editor/react`)

**Languages Supported**:
- JavaScript/TypeScript
- Java
- Python
- Kotlin
- Rust
- C#
- Dart/Flutter

**Features**:
- **Syntax highlighting** with custom Code Tutor themes
- **Auto-formatting** on paste/type (configurable)
- **Line numbers** (configurable)
- **Minimap** (optional)
- **Word wrap** (configurable)
- **Custom font family**: Fira Code, Consolas, Monaco, Courier New
- **Configurable font size**: 10-24px
- **Tab size**: 2, 4, or 8 spaces
- **Monospace font family**

**Themes**:
- Light theme: `code-tutor-light`
- Dark theme: `code-tutor-dark`
- Read-only mode for displaying code examples
- Highlight specific lines (for code examples)

**Editor Configuration** (`/utils/monacoConfig.ts`):
```typescript
interface EditorOptions {
  language: string,
  readOnly?: boolean,
  compact?: boolean,  // Reduce height for examples
  userPreferences?: EditorPreferences
}
```

---

### 4.2 AUTO-SAVE

**Hook**: `/hooks/useAutoSave.ts`

**Features**:
- Debounced save with configurable delay (default 2000ms)
- Saves code to localStorage with key format: `code-{language}-{moduleId}-{lessonId}`
- Skips save if data hasn't changed
- Skips save if auto-save disabled
- Cleans up timeout on unmount or data change

**Integration**:
- Auto-saves code edits in code editors
- Auto-saves lesson progress
- Works with user preferences (can be toggled off)

**localStorage Keys**:
- Code: `code-{language}-{moduleId}-{lessonId}`
- Lesson progress: Handled by progress store
- Preferences: Stored in zustand persist middleware

---

### 4.3 KEYBOARD SHORTCUTS SYSTEM

**Hook**: `/hooks/useKeyboardShortcuts.ts`

**Features**:
- Global keyboard event listener
- Modifier key support: Ctrl, Meta (⌘), Shift, Alt
- Platform-aware:
  - macOS: Uses Cmd (Meta key)
  - Windows: Uses Ctrl
- Prevents default browser shortcuts when registered
- Smart input detection:
  - Disables shortcuts when typing in INPUT, TEXTAREA, contentEditable
- Custom shortcut display formatting

**Global Shortcuts** (registered in App.tsx):
| Key | Modifier | Action |
|-----|----------|--------|
| ? | - | Toggle keyboard shortcuts help |
| h | - | Navigate to home |
| t | Ctrl/Cmd | Toggle theme (light/dark) |
| , | Ctrl/Cmd | Open settings |
| k | Ctrl/Cmd | Open command palette |
| a | Ctrl/Cmd | Open achievements |
| Escape | - | Close all dialogs |

---

### 4.4 HINTS SYSTEM

**Components**:
- `HintsPanel.tsx`: Main hints display
- `HintType`: Stored in types with level and text

**Features**:
- **Progressive reveal**: Show one hint at a time
- **5 hint levels**:
  - 1-2: Gentle nudge (blue background)
  - 3-4: Strong hint (orange background)
  - 5: Almost solution (red background)
- **Hint content**: Markdown text + optional code snippet
- **Usage tracking**: Counts hints used per challenge
- **Warning**: First hint reveal shows score impact warning
- **Visual indicators**: Level badges and colors
- **Expandable/collapsible**: Panel can be minimized
- **Completion message**: "All hints revealed. You've got this! 💪"

**Integration**:
- Available on MULTIPLE_CHOICE, TRUE_FALSE, CODE_OUTPUT, FREE_CODING, CODE_COMPLETION, CONCEPTUAL challenges
- Hidden on CONCEPTUAL after submission shows sample answers
- Tracks hints for scoring

---

### 4.5 COMMON MISTAKES DISPLAY

**Component**: `CommonMistakesPanel.tsx`

**Features**:
- **Table layout**: Mistake | Consequence | Correction columns
- **Expandable code examples**: Side-by-side wrong/right comparison
- **Color coding**:
  - Red background: Wrong example
  - Green background: Correct example
- **Markdown support**: For mistake descriptions
- **Responsive**: Stacks on mobile
- **Default collapsed**: Expandable by user
- **Per-mistake expansion**: Each mistake item expands independently

**Data Structure**:
```typescript
interface CommonMistake {
  mistake: string,
  consequence: string,
  correction: string,
  example?: {
    wrong: string,
    right: string
  }
}
```

**Integration**:
- Shows on FREE_CODING and CODE_COMPLETION challenges
- Helps prevent common errors

---

### 4.6 BONUS CHALLENGES

**Feature**: Additional challenges unlocked after main challenge completion

**Available on**: FREE_CODING challenges only

**Structure**:
```typescript
interface BonusChallenge {
  id: string,
  title: string,
  description: string,
  difficulty: 1 | 2 | 3 | 4 | 5,
  hints?: string[],
  solution?: string
}
```

**Display**:
- Shows in right panel only after all tests pass
- Gradient background (purple to pink)
- List with:
  - Title
  - Description
  - Difficulty rating (1-5)
  - "🌟 Bonus Challenges" header

---

---

## 5. CONTENT FEATURES

### 5.1 LESSON CONTENT RENDERING

**Technology**: 
- `react-markdown` with plugins
- `rehype-highlight` for syntax highlighting
- `remark-gfm` for GitHub Flavored Markdown

**Features**:
- Full Markdown support with GFM extensions
- Code block syntax highlighting (via highlight.js)
- Styled code blocks with language labels
- Tables, lists, blockquotes, etc.
- Dark mode prose styling

**Lesson Content Structure**:
```typescript
interface LessonContent {
  format: 'markdown',
  body: string,  // Main lesson content
  bodyFile?: string,  // Alternative file reference
  codeExamples: CodeExample[]
}

interface CodeExample {
  id: string,
  language: string,
  code: string,
  explanation: string,
  runnable: boolean,
  highlightLines?: number[]
}
```

**Code Examples Display**:
- Syntax-highlighted read-only Monaco editor
- Explanation label above
- Language indicator
- Optional line highlighting
- Compact height (200px default)

---

### 5.2 MARKDOWN SUPPORT

**Supported Elements**:
- Headers (H1-H6)
- Paragraphs
- Lists (ordered/unordered)
- Tables (GitHub Flavored)
- Blockquotes
- Code blocks with language specification
- Inline code
- Links
- Images (where appropriate)
- Bold, italic, strikethrough
- Emphasis

**Custom Styling**:
- Dark mode prose styling
- Typography classes for responsive text
- Proper contrast for accessibility

---

### 5.3 SYNTAX HIGHLIGHTING

**Technology**: 
- highlight.js library
- CSS theme: `github-dark.css`
- Monaco editor custom themes

**Languages Supported**:
- JavaScript/TypeScript
- Java
- Python
- Kotlin
- Rust
- C#
- Dart/Flutter
- HTML
- CSS
- JSON
- SQL
- And 100+ more

**Features**:
- Color-coded tokens
- Line numbers in Monaco editor
- Read-only display in code examples
- Scrollable for long code blocks

---

### 5.4 CODE EXAMPLES

**Display Options**:
1. **In lesson content**: Syntax-highlighted examples
2. **In hints**: Code snippets within hint text
3. **In common mistakes**: Wrong vs. right examples
4. **In bonus challenges**: Solution code (if provided)

**Features**:
- Language label
- Syntax highlighting
- Read-only editing (prevents modification)
- Optional line highlighting
- Scrollable content
- Monospace font (Fira Code, Consolas, etc.)

---

### 5.5 CONTENT SECTIONS

**Type**: `ContentSectionType`

**Available Types**:
1. **THEORY**: Main concept explanation
2. **ANALOGY**: Real-world analogies
3. **EXAMPLE**: Code examples with explanations
4. **KEY_POINT**: Important takeaways
5. **WARNING**: Common pitfalls
6. **EXPERIMENT**: Guided experimentation

**Section Structure**:
```typescript
interface ContentSection {
  type: ContentSectionType,
  title: string,
  content: string,  // Markdown or HTML
  code?: string,
  language?: string  // Programming language for code
}
```

---

---

## 6. DATA PERSISTENCE

### 6.1 BROWSER STORAGE

**Storage Method**: localStorage via Zustand persist middleware

**Stored Data**:

| Store | Key | Data | Persistence |
|-------|-----|------|-------------|
| Progress | `progress-storage` | Lesson completion, code submissions, time spent | Permanent |
| Achievements | `achievements-storage` | Achievement status, stats, bookmarks | Permanent |
| Theme | `theme-storage` | Light/dark, motion preference | Permanent |
| Preferences | `user-preferences` | Editor settings, auto-save, sound, notifications | Permanent |
| Auth | `auth-storage` | User info, token, authentication state | Permanent |

**Code Storage**:
- Key format: `code-{language}-{moduleId}-{lessonId}`
- Saved per lesson
- Allows resuming work across sessions

---

### 6.2 DESKTOP APP PERSISTENCE

**File Storage** (`/desktop/src/main.ts`):

**Progress File**:
- Location: `{app.getPath('userData')}/progress.json`
- Stores: User progress for all lessons across all courses
- Format: JSON with nested structure by userId > courseId > moduleId > lessonId

**Structure**:
```json
{
  "default": {
    "java": {
      "module-1": {
        "lesson-1": {
          "completed": true,
          "lastUpdated": "2024-01-01T10:00:00Z",
          "codeSubmissions": 5,
          "timeSpent": 30
        }
      }
    }
  }
}
```

**Content Directory**:
- Location (Development): `{project}/content/courses/`
- Location (Packaged): `{resourcesPath}/content/`
- Contains: course.json, module definitions, lesson content

---

---

## 7. ELECTRON DESKTOP APP (Backend)

### 7.1 MAIN PROCESS (`/src/main.ts`)

**Window Configuration**:
- Dimensions: 1400x900 (min 800x600)
- Context isolation enabled
- Node integration disabled
- Preload script: `preload.ts`

**Runtime Management**:
- Checks all programming language runtimes on startup
- Shows warning dialog if runtimes missing
- Displays installed vs. missing runtimes
- Provides download URLs for missing runtimes
- Option to exit and install or continue

---

### 7.2 IPC HANDLERS

**Courses**:
- `get-courses`: List all available courses
- `get-course`: Get specific course with all modules/lessons

**Code Execution**:
- `execute-code`: Run code and return output/errors
- Supports all 7 languages
- Timeout protection
- Memory limits

**Validation**:
- `validate-challenge`: Validate any challenge type
- `validate-visible-tests`: Run only visible test cases

**Progress**:
- `get-progress`: Retrieve user progress
- `save-progress`: Save lesson progress and code submission

**Authentication** (Simplified):
- `auth-login`: Single-user authentication
- `auth-register`: User account creation
- `auth-verify`: Token validation
- Desktop: Always authenticates as "Desktop User"

**System**:
- `check-runtimes`: List installed programming languages

---

### 7.3 CODE EXECUTION (`/src/executors.ts`)

**Supported Languages**:
1. **JavaScript/Node.js**
2. **Python 3**
3. **Java**
4. **Kotlin**
5. **Rust**
6. **C# (.NET)**
7. **Dart/Flutter**

**Execution Process**:
1. Create temporary file with user code
2. Execute with language-specific runtime
3. Capture stdout, stderr, execution time
4. Clean up temporary files
5. Return results with error handling

**Constraints**:
- Execution timeout
- Memory limits
- Output character limit
- Process isolation

---

### 7.4 CHALLENGE VALIDATION (`/src/challenge-validator.ts`)

**Validation Logic**:

For each challenge type:

**FREE_CODING & CODE_COMPLETION**:
1. Execute user code
2. Run against all test cases
3. Compare outputs based on testType
4. Return pass/fail for each test
5. Complete when all tests pass

**MULTIPLE_CHOICE & TRUE_FALSE**:
1. Compare user selection to correctAnswer
2. Return pass/fail immediately
3. Return explanation

**CODE_OUTPUT**:
1. User predicts output without writing code
2. Compare prediction to expected output
3. Return pass/fail

**CONCEPTUAL**:
1. Accept user answer as free text
2. Manual grading required (server-side)
3. Mark complete on submission

---

### 7.5 TEST CASE TYPES

**4 Comparison Modes**:

1. **exact**: Output must match exactly (with optional case-insensitivity)
2. **contains**: Output must contain expected string
3. **regex**: Output must match regular expression
4. **pattern**: Pattern-based matching for complex outputs

**Structure**:
```typescript
interface TestCase {
  id?: string,
  description: string,
  inputs?: any[],  // Function parameters
  expectedOutput: any,
  isVisible: boolean,  // Shown to user?
  testType?: 'exact' | 'contains' | 'regex' | 'pattern',
  customMessage?: string
}
```

**Visible vs Hidden**:
- **Visible**: User sees results
- **Hidden**: Run on server, not shown to user
- Prevents hardcoding outputs

---

---

## 8. ROUTING STRUCTURE

```
/                                           → Landing Page
/course/:language                           → Course Overview
/course/:language/module/:moduleId/lesson/:lessonId → Lesson Page (Interactive)
*                                           → Not Found Page
```

---

## 9. ACCESSIBILITY FEATURES

**WCAG 2.1 Compliance**:
- Semantic HTML structure
- ARIA labels and roles
- Focus management:
  - Focus trap in modals
  - Focus restoration after modal close
- Keyboard navigation (Tab, Arrow keys, Enter, Escape)
- Color contrast ratios
- Skip to content link
- Screen reader support
- Reduced motion support (`prefers-reduced-motion`)

**Accessibility Components**:
- `SkipToContent.tsx`: Jump to main content
- `useFocusTrap.ts`: Trap focus in modals
- `useFocusReturn.ts`: Restore focus after modal close

---

## 10. PERFORMANCE OPTIMIZATIONS

1. **Code Splitting**: Lazy loading pages with Suspense
2. **Memoization**: React.memo for components
3. **Debouncing**: Auto-save with debounced delays
4. **LocalStorage**: Persist state without network requests
5. **Image Optimization**: Gradient backgrounds instead of images
6. **Bundle Size**: Tree-shaking unused code

---

## 11. ERROR HANDLING

**Error Boundaries**: `ErrorBoundary.tsx` for React errors

**Toast Notifications**: For user-facing errors

**Try-Catch Blocks**: In IPC handlers and async functions

**Logging**: Console logging for debugging

**User Feedback**:
- Toast notifications
- Error messages in modals
- Fallback UI for errors

---

## 12. TESTING

**Testing Frameworks**:
- Vitest
- React Testing Library

**Test Files**:
- `/stores/__tests__/themeStore.test.ts`
- `/hooks/__tests__/useAutoSave.test.ts`
- `/components/__tests__/Badge.test.tsx`
- `/components/__tests__/Button.test.tsx`

---

## 13. FEATURES SUMMARY TABLE

| Feature | Web | Desktop | Status |
|---------|-----|---------|--------|
| 6 Challenge Types | ✅ | ✅ | Complete |
| Code Editor (Monaco) | ✅ | ✅ | Complete |
| Auto-Save | ✅ | ✅ | Complete |
| Keyboard Shortcuts | ✅ | ✅ | Complete |
| Hints System | ✅ | ✅ | Complete |
| Common Mistakes | ✅ | ✅ | Complete |
| Bonus Challenges | ✅ | ✅ | Complete |
| Achievements | ✅ | ✅ | Complete |
| Progress Tracking | ✅ | ✅ | Complete |
| Bookmarks | ✅ | ✅ | Complete |
| Settings | ✅ | ✅ | Complete |
| Dark/Light Theme | ✅ | ✅ | Complete |
| Reduced Motion | ✅ | ✅ | Complete |
| Command Palette | ✅ | ✅ | Complete |
| 7 Languages Support | ✅ | ✅ (needs runtimes) | Complete |
| Offline Mode | (localStorage) | ✅ | Complete |
| Accessibility | ✅ | ✅ | Complete |

---

## 14. FILES TO MIGRATE TO NATIVE APP

### Core Types & Interfaces
- `types/interactive.ts` - Challenge types
- `types/content.ts` - Lesson/course structure

### Challenge Components (All 6)
- `components/challenges/MultipleChoiceChallenge.tsx`
- `components/challenges/TrueFalseChallenge.tsx`
- `components/challenges/CodeOutputChallenge.tsx`
- `components/challenges/FreeCodingChallenge.tsx`
- `components/challenges/CodeCompletionChallenge.tsx`
- `components/challenges/ConceptualChallenge.tsx`
- `components/challenges/ChallengeContainer.tsx`

### Supporting Challenge Components
- `components/challenges/HintsPanel.tsx`
- `components/challenges/CommonMistakesPanel.tsx`
- `components/challenges/TestResultsPanel.tsx`

### UI Components (Core)
- `components/Button.tsx`
- `components/Card.tsx`
- `components/Badge.tsx`
- `components/ProgressBar.tsx`
- `components/Settings.tsx`
- `components/Achievements.tsx`
- `components/CommandPalette.tsx`
- `components/KeyboardShortcutsHelp.tsx`

### State Management (All Stores)
- `stores/progressStore.ts`
- `stores/achievementsStore.ts`
- `stores/themeStore.ts`
- `stores/preferencesStore.ts`
- `stores/keyboardStore.ts`
- `stores/authStore.ts`
- `stores/toastStore.ts`

### Hooks (Custom)
- `hooks/useAutoSave.ts`
- `hooks/useKeyboardShortcuts.ts`
- `hooks/useMonacoSetup.ts`
- `hooks/useFocusTrap.ts`
- `hooks/useFocusReturn.ts`
- `hooks/useReducedMotion.ts`

### Utilities
- `utils/monacoConfig.ts` - Editor configuration
- `utils/errorLogging.ts` - Error handling

### Styles & Assets
- Global CSS/Tailwind configuration
- Fonts and icons (lucide-react icons)
- Theme colors and gradients

### Backend (Desktop App)
- `desktop/src/challenge-validator.ts` - All validation logic
- `desktop/src/executors.ts` - Code execution engine
- `desktop/src/runtime-installer.ts` - Runtime detection
- `desktop/src/main.ts` - Electron main process

---

