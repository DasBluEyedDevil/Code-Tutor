---
type: "WARNING"
title: "CRUD Security & Best Practices"
---

## Security Considerations

**Input Validation** (always validate!):
- Use DataAnnotations: [Required], [StringLength], [Range]
- Validate on BOTH client (Blazor) AND server (API)
- Never trust client data - always re-validate on API

**Delete Operations**:
- Always confirm before delete
- Consider soft-delete (IsDeleted flag) vs hard-delete
- Cascading deletes can be dangerous!

**Optimistic Concurrency**:
- Add RowVersion/Timestamp to entities
- Detect if someone else modified data
- Prevents overwriting others' changes

**API Security**:
- Add [Authorize] to protect endpoints
- Validate user owns the resource before update/delete
- Return 404 (not 403) to hide resource existence