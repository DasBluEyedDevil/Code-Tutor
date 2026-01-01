---
type: "EXPERIMENT"
title: "Best Practices"
---


### 1. Use 2-7 Tabs
✅ **Good**: 2-7 tabs (readable, manageable)
❌ **Bad**: 10+ tabs (use scrollable or different pattern)

### 2. Short Labels
✅ **Good**: "Home", "Search", "Profile"
❌ **Bad**: "Home Dashboard", "Advanced Search", "User Profile Settings"

### 3. Icons + Text (Mobile)
✅ **Good**: Icon with short text
❌ **Bad**: Text only (harder to recognize quickly)

### 4. Preserve State
✅ **Good**: Use AutomaticKeepAliveClientMixin for lists
❌ **Bad**: Rebuild everything each switch

### 5. Dispose Controllers
✅ **Good**: Always dispose TabController in dispose()
❌ **Bad**: Memory leak!

