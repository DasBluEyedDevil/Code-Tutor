---
type: "EXPERIMENT"
title: "Best Practices"
---


### 1. Use for Secondary Navigation
✅ **Good**: Settings, help, account options
❌ **Bad**: Primary app destinations (use bottom nav instead)

### 2. Always Close Before Navigating
✅ **Good**: `Navigator.pop(context)` then navigate
❌ **Bad**: Navigate without closing (drawer stays open!)

### 3. Max 12 Items
✅ **Good**: 5-12 well-organized items
❌ **Bad**: 20+ items (too overwhelming!)

### 4. Use Sections
✅ **Good**: Group related items with dividers/headers
❌ **Bad**: Flat list of everything

### 5. Show Current Selection
✅ **Good**: Highlight current page in drawer
❌ **Bad**: No indication where you are

