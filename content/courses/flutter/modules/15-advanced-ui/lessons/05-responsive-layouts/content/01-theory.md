---
type: "THEORY"
title: "Introduction - Why Responsive Design"
---


**Flutter Runs Everywhere - Your Layouts Should Too**

Flutter apps can run on phones, tablets, desktops, and the web. Screen sizes range from 320px wide phones to 4K monitors. A layout that looks great on one device can be unusable on another.

**The Challenge:**

| Device | Typical Width | Considerations |
|--------|--------------|----------------|
| Phone (portrait) | 320-428px | Limited space, touch targets |
| Phone (landscape) | 568-926px | Wider but shorter |
| Tablet (portrait) | 768-834px | More space, could show more |
| Tablet (landscape) | 1024-1194px | Desktop-like layouts possible |
| Desktop/Web | 1200-1920px+ | Full desktop layouts |

**Why Not Just Scale?**

Simply stretching a phone layout to tablet size wastes space. A single-column list that works on a phone should become a grid on a tablet. Navigation that's a bottom bar on mobile should become a side rail on desktop.

**Responsive vs Adaptive:**

- **Responsive:** Layout fluidly adjusts to any size (like CSS media queries)
- **Adaptive:** Distinct layouts for different size classes (phone vs tablet)

Flutter supports both approaches. We'll learn the tools for each.

**What We'll Build:**

A dashboard that automatically adapts:
- Phone: Single column with bottom navigation
- Tablet: Two-column with navigation rail
- Desktop: Three-column with full sidebar

