---
type: "THEORY"
title: "Architecture Decision Checklist"
---

### Before Starting a Feature, Ask:

**1. Data Layer**
- [ ] What's the data source? (API, DB, both)
- [ ] Need caching strategy? (Offline-first?)
- [ ] Data transformation needed? (DTO → Entity → Domain)

**2. Domain Layer**
- [ ] What are the domain entities?
- [ ] Are there complex business rules? (Use cases needed?)
- [ ] Repository interfaces defined?

**3. Presentation Layer**
- [ ] What state does the UI need?
- [ ] What actions can the user take?
- [ ] Loading/error/empty states covered?
- [ ] Navigation flows mapped?

**4. Testing**
- [ ] Domain logic testable in isolation?
- [ ] Repository mockable?
- [ ] UI state changes predictable?

### Quick Reference

| Question | If Yes | If No |
|----------|--------|-------|
| Multiple data sources? | Repository pattern | Direct access OK |
| Complex business logic? | Use cases | ViewModel handles |
| Shared across screens? | Domain models | UI models OK |
| Needs offline? | Local-first with sync | API-only OK |
| Large team? | Strict layer boundaries | Flexible structure |