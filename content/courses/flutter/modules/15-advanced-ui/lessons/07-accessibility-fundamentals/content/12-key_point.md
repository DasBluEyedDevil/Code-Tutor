---
type: "KEY_POINT"
title: "Accessibility Checklist"
---


**Before Release - Accessibility Audit:**

**Semantics:**
- [ ] All interactive elements have labels
- [ ] Images have descriptions (or excluded if decorative)
- [ ] Form fields have labels and error messages
- [ ] Headings use `header: true` in Semantics
- [ ] Live regions announce dynamic updates

**Touch Targets:**
- [ ] All touch targets are minimum 48x48 dp
- [ ] Minimum 8dp spacing between targets
- [ ] Buttons have adequate padding

**Color & Contrast:**
- [ ] Text has 4.5:1 contrast ratio (AA)
- [ ] Large text has 3:1 contrast ratio
- [ ] Status indicators don't rely on color alone
- [ ] Works for colorblind users

**Focus Management:**
- [ ] Logical focus order
- [ ] Visible focus indicators
- [ ] Dialogs trap focus
- [ ] Keyboard navigation works

**Screen Reader Testing:**
- [ ] Tested with VoiceOver (iOS)
- [ ] Tested with TalkBack (Android)
- [ ] All features accessible without vision

**Testing Tools:**

```dart
// Enable semantics debugger
SemanticsDebugger(child: MyApp())

// Check contrast programmatically
AccessibleColors.meetsAA(foreground, background)

// Announce to screen reader
SemanticsService.announce('Message', TextDirection.ltr)
```

