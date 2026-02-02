---
type: "EXAMPLE"
title: "Security Rules Best Practices"
---


### ✅ DO:

1. **Start restrictive, gradually allow** (deny by default)
2. **Validate all data types and sizes**
3. **Prevent users from changing critical fields** (userId, createdAt)
4. **Use helper functions** for reusable logic
5. **Test rules thoroughly** before deploying
6. **Log and monitor** rule violations
7. **Review rules regularly** as your app evolves

### ❌ DON'T:

1. **Don't use test mode** in production
2. **Don't trust client-side validation**
3. **Don't allow unlimited file sizes**
4. **Don't forget subcollection rules**
5. **Don't expose sensitive data** in public reads
6. **Don't allow users to read all users** (privacy issue)

