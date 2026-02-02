---
type: "KEY_POINT"
title: "Accessibility Implementation Checklist"
---


**Focus Indicators:**
- [ ] All focusable elements have visible focus rings
- [ ] Focus indicator has at least 3:1 contrast ratio
- [ ] Focus indicator is at least 2px thick
- [ ] Custom widgets handle keyboard Enter/Space
- [ ] Focus order matches visual order

**Dynamic Text Scaling:**
- [ ] Tested with text scale factor at 2.0x
- [ ] No text overflow or clipping
- [ ] Layouts adapt gracefully to larger text
- [ ] Icons scale appropriately with text
- [ ] Touch targets remain accessible at all scales

**Reduced Motion:**
- [ ] Check `MediaQuery.disableAnimationsOf(context)`
- [ ] Provide static alternatives to animations
- [ ] Page transitions respect motion preference
- [ ] Loading indicators have static alternatives
- [ ] No auto-playing animations

**Accessible Forms:**
- [ ] Every input has a visible label (not just placeholder)
- [ ] Required fields are marked and announced
- [ ] Errors use icon + color + text
- [ ] Errors are announced to screen readers
- [ ] Focus moves to first error on validation
- [ ] Clear error messages with suggestions

**Testing:**
- [ ] Run SemanticsDebugger to visualize tree
- [ ] Write tests for semantics labels
- [ ] Verify touch target sizes (48x48 minimum)
- [ ] Test with VoiceOver (iOS) and TalkBack (Android)
- [ ] Run Android Accessibility Scanner

