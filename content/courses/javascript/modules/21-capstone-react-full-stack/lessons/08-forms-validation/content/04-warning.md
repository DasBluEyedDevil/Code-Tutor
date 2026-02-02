---
type: "WARNING"
title: "Form Validation Rules"
---

**Client validation** = UX, not security
**Server validation** = Mandatory, security

Both must validate:
```typescript
// Frontend: Quick feedback
// Backend: Enforced security
// Never trust frontend alone!
```

Show server errors with setError:
```typescript
onError: (error) => {
  if (error.details) {
    Object.entries(error.details).forEach(([field, msg]) => {
      setError(field, { type: 'server', message: msg });
    });
  }
}
```