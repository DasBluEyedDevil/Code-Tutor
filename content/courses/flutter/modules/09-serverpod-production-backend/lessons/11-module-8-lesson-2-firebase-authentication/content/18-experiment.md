---
type: "EXAMPLE"
title: "Best Practices"
---


### ✅ DO:
1. **Always validate input** (email format, password strength)
2. **Show user-friendly error messages** (not technical Firebase codes)
3. **Verify emails** before allowing sensitive actions
4. **Use StreamBuilder** for auth state changes
5. **Handle loading states** (show spinners)
6. **Test on real devices** (not just emulator)

### ❌ DON'T:
1. **Don't store passwords** in your app (Firebase handles this)
2. **Don't show raw error codes** to users
3. **Don't allow weak passwords** (< 6 characters)
4. **Don't forget to sign out** from social providers too

