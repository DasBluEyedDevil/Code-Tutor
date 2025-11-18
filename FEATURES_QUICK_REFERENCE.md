# Code Tutor - Features Quick Reference

## 6 INTERACTIVE CHALLENGE TYPES

### 1. Multiple Choice (MULTIPLE_CHOICE)
- Radio button selection with A, B, C, D... labels
- Markdown support for options
- Correct/incorrect visual feedback
- Explanation display
- Hints available (before submission)

### 2. True/False (TRUE_FALSE)
- Large buttons with 👍/👎 icons
- Boolean validation
- Color-coded feedback
- Explanation display
- Hints available (before submission)

### 3. Code Output (CODE_OUTPUT)
- User predicts code output without writing code
- Read-only code display with syntax highlighting
- Textarea for predicted output entry
- Side-by-side output comparison
- Output normalization (trim, newline handling)
- Hints available

### 4. Free Coding (FREE_CODING)
- Full code editor with starter code
- Multiple test cases with execution
- 4 test comparison modes: exact, contains, regex, pattern
- Visible + hidden test cases
- Run Tests button
- Show/Hide Solution toggle
- Reset Code button
- Hints panel with 5 levels
- Common mistakes panel
- Bonus challenges (after passing all tests)
- Success/failure progress indicator

### 5. Code Completion (CODE_COMPLETION)
- Partial code with TODO markers
- Fill-in-the-blanks style challenge
- Same test case validation as Free Coding
- Auto-highlights TODO markers
- All features of Free Coding
- Emphasizes completion over writing from scratch

### 6. Conceptual (CONCEPTUAL)
- Free-form text answer
- Not auto-graded
- Shows explanation after submission
- Shows sample good answers
- Progressive hint reveal
- Manual grading capable

---

## CHALLENGE SHARED FEATURES

### Hints System
- **Progressive reveal**: One hint at a time
- **5 levels**: Gentle nudge (1-2) → Strong hint (3-4) → Almost solution (5)
- **Content**: Markdown text + optional code snippet
- **Tracking**: Counts hints used
- **Warning**: Score impact warning on first reveal
- **Colors**: Blue → Orange → Red based on level

### Common Mistakes Panel (Free Coding & Code Completion)
- **Table layout**: Mistake | Consequence | Correction
- **Code examples**: Wrong/Right side-by-side
- **Expandable**: Each mistake item expands separately
- **Color coded**: Red for wrong, green for correct

### Test Results Panel (Free Coding & Code Completion)
- Summary of all test results
- Individual test case details
- Expected vs. actual output comparison
- Error messages (if applicable)
- Color-coded by pass/fail status

### Bonus Challenges (Free Coding only)
- Unlocked after all tests pass
- Difficulty rating (1-5)
- Optional hints and solutions
- Shown in right panel

---

## 7 KEYBOARD SHORTCUTS

| Shortcut | Action |
|----------|--------|
| ? | Show keyboard shortcuts help |
| h | Go to home page |
| Ctrl+T (⌘+T Mac) | Toggle theme (light/dark) |
| Ctrl+, (⌘+, Mac) | Open settings |
| Ctrl+K (⌘+K Mac) | Open command palette |
| Ctrl+A (⌘+A Mac) | Open achievements |
| Escape | Close all dialogs |

---

## EDITOR CONFIGURATION

**Available Settings**:
- Font size: 10-24px (default 14)
- Tab size: 2, 4, or 8 spaces (default 2)
- Word wrap: on/off (default on)
- Minimap: on/off (default off)
- Line numbers: on/off/relative (default on)
- Format on paste: on/off (default on)
- Format on type: on/off (default on)
- Font family: Fira Code, Consolas, Monaco, Courier New

**Syntax Highlighting**:
- JavaScript/TypeScript
- Java, Python, Kotlin, Rust, C#, Dart/Flutter
- HTML, CSS, JSON, SQL, and 100+ more languages

**Themes**:
- Light mode: `code-tutor-light`
- Dark mode: `code-tutor-dark`

---

## AUTO-SAVE SYSTEM

- **Delay**: Configurable 1-10 seconds (default 2s)
- **Storage**: localStorage with key `code-{language}-{moduleId}-{lessonId}`
- **Toggle**: Can be disabled in settings
- **Behavior**: Debounced, only saves on changes
- **Cleanup**: Clears timeout on unmount

---

## ACHIEVEMENTS (10 Total)

| Achievement | Icon | Requirement |
|-------------|------|-------------|
| First Steps | 🎯 | Complete 1 lesson |
| Getting Started | 📚 | Complete 10 lessons |
| Dedicated Learner | 🌟 | Complete 50 lessons |
| Century | 💯 | Complete 100 lessons |
| Course Graduate | 🎓 | Complete 1 course |
| Multi-linguist | 🗣️ | Complete 3 courses |
| Week Warrior | 🔥 | Maintain 7-day streak |
| Consistency Champion | ⚡ | Maintain 30-day streak |
| Test Master | ✅ | Pass 100 test cases |
| Code Runner | ▶️ | Run code 100 times |

**Tracking**:
- Lessons completed
- Courses completed
- Daily streak (consecutive days)
- Tests passed
- Code executions
- Progress bars for in-progress achievements

---

## STATE MANAGEMENT STORES

### Progress Store (`progress-storage`)
- Lesson completion status
- Last accessed timestamp
- Code submissions count
- Time spent (minutes)
- Course/module/lesson hierarchy

### Achievements Store (`achievements-storage`)
- Achievement unlock status and dates
- Bookmarks for lessons
- User statistics (lessons, courses, streak, tests, runs)

### Theme Store (`theme-storage`)
- Light/Dark theme preference
- Motion preference (auto/always/reduced)

### Preferences Store (`user-preferences`)
- Editor settings (font, tabs, formatting)
- Auto-save settings
- Sound effects toggle
- Notifications toggle

### Toast Store (ephemeral)
- Success, error, warning, info notifications
- Auto-dismiss with configurable duration

### Keyboard Store (runtime)
- Registered global shortcuts
- Keyboard shortcuts help visibility

### Auth Store (`auth-storage`)
- User info (id, email, name)
- Authentication token
- Login status

---

## CONTENT STRUCTURE

### Lesson Content
- Markdown-rendered lesson body with GFM extensions
- Code examples with syntax highlighting
- Exercise instructions
- Expandable hints panel
- Code editor with auto-save

### Markdown Features
- Headers, lists, tables, blockquotes, code blocks
- Inline code, links, images
- Bold, italic, strikethrough
- GitHub Flavored Markdown support

### Code Examples in Content
- Language-specific syntax highlighting
- Read-only Monaco editor display
- Optional line highlighting
- Explanation labels
- Compact display in lessons

---

## ACCESSIBILITY FEATURES

- WCAG 2.1 AA Compliance
- Semantic HTML structure
- ARIA labels and roles
- Focus management (focus trap in modals, restoration after close)
- Keyboard navigation support
- Color contrast ratios
- Skip to content link
- Screen reader optimization
- Reduced motion support (`prefers-reduced-motion`)

---

## DATA PERSISTENCE

### Browser Storage (localStorage)
- Progress
- Achievements & bookmarks
- Theme preference
- Editor preferences
- User authentication
- Code (per lesson)

### Desktop App Storage
- Progress file: `{userData}/progress.json`
- Content directory: `/content/courses/`
- Nested structure: userId > courseId > moduleId > lessonId

---

## SUPPORTED LANGUAGES (7 Total)

1. **Python** - 73 lessons
2. **Java** - 20 lessons
3. **Kotlin** - 29 lessons
4. **Rust** - 60 lessons
5. **C#** - 26 lessons
6. **Flutter/Dart** - 95 lessons
7. **JavaScript/TypeScript** - Coming soon

---

## CHALLENGE VALIDATION LOGIC

### Test Comparison Types
1. **exact** - Exact match (whitespace trimmed, optional case-insensitive)
2. **contains** - Output must contain expected string
3. **regex** - Regex pattern matching
4. **pattern** - Custom pattern matching

### Visible vs Hidden Tests
- **Visible**: Shown to user with results
- **Hidden**: Run server-side, prevent hardcoding

### Multiple Choice Validation
- Number or letter format for correct answer
- Single submission per attempt

### Code Output Validation
- Whitespace normalization
- Newline standardization
- Case-sensitive by default

### Free Coding / Code Completion
- Execute code in respective runtime
- Compare against all test cases
- Completion = all tests pass
- Tracks hints used

### Conceptual
- No auto-validation
- Completion on submission
- Manual grading capable

---

## ROUTES

- `/` - Landing page with course selection
- `/course/:language` - Course overview with modules and lessons
- `/course/:language/module/:moduleId/lesson/:lessonId` - Interactive lesson page
- `*` - Not found page

---

## FILE MIGRATION CHECKLIST

Essential files to migrate to native desktop app:

### Types & Interfaces
- [ ] `types/interactive.ts` - All challenge types
- [ ] `types/content.ts` - Course/lesson structure

### Challenge Components (All 6)
- [ ] `components/challenges/MultipleChoiceChallenge.tsx`
- [ ] `components/challenges/TrueFalseChallenge.tsx`
- [ ] `components/challenges/CodeOutputChallenge.tsx`
- [ ] `components/challenges/FreeCodingChallenge.tsx`
- [ ] `components/challenges/CodeCompletionChallenge.tsx`
- [ ] `components/challenges/ConceptualChallenge.tsx`
- [ ] `components/challenges/ChallengeContainer.tsx`

### Supporting Components
- [ ] `components/challenges/HintsPanel.tsx`
- [ ] `components/challenges/CommonMistakesPanel.tsx`
- [ ] `components/challenges/TestResultsPanel.tsx`

### UI Components
- [ ] `components/Button.tsx`
- [ ] `components/Card.tsx`
- [ ] `components/Badge.tsx`
- [ ] `components/ProgressBar.tsx`
- [ ] `components/Settings.tsx`
- [ ] `components/Achievements.tsx`
- [ ] `components/CommandPalette.tsx`
- [ ] `components/KeyboardShortcutsHelp.tsx`

### State Management (All 7 Stores)
- [ ] `stores/progressStore.ts`
- [ ] `stores/achievementsStore.ts`
- [ ] `stores/themeStore.ts`
- [ ] `stores/preferencesStore.ts`
- [ ] `stores/keyboardStore.ts`
- [ ] `stores/authStore.ts`
- [ ] `stores/toastStore.ts`

### Hooks
- [ ] `hooks/useAutoSave.ts`
- [ ] `hooks/useKeyboardShortcuts.ts`
- [ ] `hooks/useMonacoSetup.ts`
- [ ] `hooks/useFocusTrap.ts`
- [ ] `hooks/useFocusReturn.ts`
- [ ] `hooks/useReducedMotion.ts`

### Utilities
- [ ] `utils/monacoConfig.ts`
- [ ] `utils/errorLogging.ts`

### Backend (Desktop Specific)
- [ ] `desktop/src/challenge-validator.ts`
- [ ] `desktop/src/executors.ts`
- [ ] `desktop/src/runtime-installer.ts`
- [ ] `desktop/src/main.ts`

---

