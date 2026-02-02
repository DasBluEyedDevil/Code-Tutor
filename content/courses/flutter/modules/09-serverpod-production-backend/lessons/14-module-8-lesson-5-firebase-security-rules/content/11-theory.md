---
type: "THEORY"
title: "Production-Ready Rules Checklist"
---


Before launching your app, verify:

- [ ] **No `if true` rules** except for truly public data
- [ ] **All write operations require authentication**
- [ ] **Users can only access their own data**
- [ ] **Data validation on all fields**
- [ ] **File size limits enforced**
- [ ] **File type validation for uploads**
- [ ] **Admin actions require admin role**
- [ ] **Rules tested with emulator**
- [ ] **No sensitive data in public reads**
- [ ] **Subcollections have appropriate rules**

