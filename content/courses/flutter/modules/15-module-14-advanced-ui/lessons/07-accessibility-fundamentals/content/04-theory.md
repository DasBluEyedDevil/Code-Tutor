---
type: "THEORY"
title: "Screen Reader Support - VoiceOver and TalkBack"
---


**Platform Screen Readers:**

| Platform | Screen Reader | Activation |
|----------|---------------|------------|
| iOS | VoiceOver | Settings > Accessibility > VoiceOver |
| Android | TalkBack | Settings > Accessibility > TalkBack |
| macOS | VoiceOver | Cmd + F5 |
| Windows | Narrator | Win + Ctrl + Enter |

**VoiceOver Gestures (iOS):**

| Gesture | Action |
|---------|--------|
| Swipe right/left | Move to next/previous element |
| Double-tap | Activate selected element |
| Two-finger swipe up | Read from top |
| Two-finger swipe down | Read from current position |
| Three-finger swipe | Scroll |

**TalkBack Gestures (Android):**

| Gesture | Action |
|---------|--------|
| Swipe right/left | Move to next/previous element |
| Double-tap | Activate selected element |
| Swipe up then right | Next heading |
| Two-finger scroll | Scroll |

**Testing Your App:**

1. **Enable screen reader** on your test device
2. **Close your eyes** and try to use your app
3. **Listen for announcements** - Are they clear and helpful?
4. **Check navigation order** - Does it make logical sense?
5. **Verify all actions** - Can every feature be accessed?

**Common Issues Found During Testing:**

- Missing labels on icon buttons
- Decorative images announced
- Focus order doesn't match visual order
- Custom widgets not accessible
- Dynamic content not announced

