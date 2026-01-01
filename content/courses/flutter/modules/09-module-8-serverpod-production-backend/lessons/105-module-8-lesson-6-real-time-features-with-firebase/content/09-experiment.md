---
type: "EXPERIMENT"
title: "Best Practices for Real-Time Features"
---


### ✅ DO:
1. **Use StreamBuilder** for automatic UI updates
2. **Dispose streams** properly to prevent memory leaks
3. **Limit real-time listeners** (don't listen to huge collections)
4. **Debounce rapid updates** (typing indicators)
5. **Show loading states** while connecting
6. **Handle offline mode** gracefully
7. **Set up presence** on app start, clear on exit

### ❌ DON'T:
1. **Don't listen to entire collections** (use queries with limits)
2. **Don't forget to cancel listeners** (memory leaks!)
3. **Don't update on every keystroke** (use debounce)
4. **Don't rely solely on real-time** (handle offline)
5. **Don't leave presence "online" forever** (set onDisconnect)

