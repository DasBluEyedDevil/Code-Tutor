---
type: "THEORY"
title: "Introduction: Why Route Protection Matters"
---

In the previous lessons, you built authentication flows for registration, login, and OAuth. But authentication alone is not enough. Users can still navigate directly to protected URLs, bookmark authenticated pages, or receive deep links to restricted content. Without route protection, your app's security is incomplete.

**What is Route Protection?**

Route protection ensures that users can only access screens they are authorized to view. Think of it like a nightclub with a bouncer:

```
Without Route Protection:
User types /admin in browser -> Goes directly to admin panel -> Security breach!

With Route Protection:
User types /admin in browser -> Bouncer checks credentials -> Redirects to login
User logs in as admin -> Bouncer checks again -> Welcome to admin panel!
```

**Why This Matters**

1. **Security**: Prevents unauthorized access to sensitive screens
2. **User Experience**: Guides unauthenticated users to login instead of showing errors
3. **Deep Links**: Handles links shared via email, push notifications, or social media
4. **State Consistency**: Ensures navigation state matches authentication state
5. **Role-Based Access**: Restricts premium or admin features to authorized users

**What You Will Build**

By the end of this lesson, you will have:

1. **GoRouter integration** with Riverpod for reactive route protection
2. **Auth redirects** that automatically send unauthenticated users to login
3. **Protected route groups** using ShellRoute for authenticated sections
4. **Role-based access** controlling admin and premium routes
5. **Deep link preservation** that remembers the destination after login

**Prerequisites**

This lesson builds on Lessons 10.4-10.6 (Authentication). You should have:
- Working authentication with auth state management in Riverpod
- Basic familiarity with GoRouter for navigation
- Understanding of Riverpod providers and state management