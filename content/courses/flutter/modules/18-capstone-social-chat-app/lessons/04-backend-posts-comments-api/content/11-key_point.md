---
type: "KEY_POINT"
title: "Posts & Comments API Checklist"
---


**Production Checklist for Social Feed API**

**Post Operations**
- [ ] Content validation (length, format)
- [ ] Content moderation (banned words, spam)
- [ ] Media processing (resize, thumbnails)
- [ ] Visibility enforcement (public/followers/private)
- [ ] Soft delete with audit trail
- [ ] Edit history tracking

**Pagination**
- [ ] Cursor-based pagination (not offset)
- [ ] Consistent ordering with ties handled
- [ ] Reasonable page size limits
- [ ] Encoded/opaque cursors
- [ ] Handle invalid/expired cursors gracefully

**Comments**
- [ ] Thread depth limiting
- [ ] Reply count denormalization
- [ ] Edit time window
- [ ] Soft delete preserving thread structure
- [ ] Lazy loading for deep threads

**Likes & Reactions**
- [ ] Unique constraint per user per item
- [ ] Atomic count updates
- [ ] Toggle behavior (like/unlike)
- [ ] Multiple reaction types
- [ ] Batch status checking

**Performance**
- [ ] Denormalized counts
- [ ] Proper database indexes
- [ ] Batch loading for related data
- [ ] Caching for hot content
- [ ] Async notifications

**Security**
- [ ] Authentication required for mutations
- [ ] Authorization checks (ownership)
- [ ] Rate limiting
- [ ] Input sanitization
- [ ] SQL injection prevention

