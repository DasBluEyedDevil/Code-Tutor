---
type: "THEORY"
title: "Introduction - Shared Element Transitions"
---


**What Are Shared Element Transitions?**

When navigating between screens, shared element transitions create visual continuity by animating a common element from its position on one page to its position on another. This gives users a sense of spatial relationship between screens.

**Real-World Examples:**
- **Photo galleries** - Thumbnail expands to full-screen image
- **Product lists** - Product card transforms into detail page
- **Profile screens** - Avatar animates from list to header
- **Music apps** - Album art flows from list to now-playing screen

**Why Shared Element Transitions Matter:**

| Without Transitions | With Transitions |
|--------------------|-----------------|
| Abrupt page changes | Smooth, connected flow |
| Users lose context | Users understand spatial relationships |
| Feels like separate screens | Feels like one cohesive app |
| Jarring experience | Delightful, polished UX |

**In Flutter, the `Hero` widget makes this surprisingly easy.**

The Hero widget "flies" from one route to another, maintaining visual continuity. You just need matching tags on both screens, and Flutter handles the rest.

