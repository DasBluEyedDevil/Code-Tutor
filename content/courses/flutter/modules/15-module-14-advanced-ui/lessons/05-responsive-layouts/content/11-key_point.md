---
type: "KEY_POINT"
title: "Responsive Design Best Practices"
---


**Design Principles:**

1. **Mobile-first:** Start with mobile layout, then expand for larger screens
2. **Content priority:** Most important content visible at all sizes
3. **Touch targets:** Minimum 48x48dp for touch targets
4. **Readable text:** Don't make text too small on large screens (max-width for text containers)

**Technical Tips:**

1. **Use LayoutBuilder for reusable components** - They adapt to their container
2. **Use MediaQuery for page-level decisions** - Navigation type, app structure
3. **Create a breakpoint utility** - Consistent breakpoints app-wide
4. **Test on real devices** - Emulators don't capture real usage
5. **Consider orientation changes** - Test both portrait and landscape

**Common Patterns:**

- **List to Grid:** Single column on mobile, multi-column on tablet/desktop
- **Stack to Row:** Vertical on mobile, horizontal on larger screens
- **Hide/Show:** Show sidebar on desktop, hide on mobile
- **Master-Detail:** Separate screens on mobile, split view on tablet

