---
type: "THEORY"
title: "Pagination with Page and Pageable"
---

Pagination is essential for APIs that return potentially large collections. Spring Data provides excellent pagination support out of the box.

Client Request:
GET /api/tasks?page=0&size=10&sort=dueDate,asc

Parsed by Spring into Pageable:
- page: 0 (first page, zero-indexed)
- size: 10 (items per page)
- sort: dueDate ascending

Repository method returns Page<Task>:
```java
Page<Task> findByOwnerId(Long ownerId, Pageable pageable);
```

Page object contains:
- content: List of items for this page
- totalElements: Total count across all pages
- totalPages: Calculated from totalElements and size
- number: Current page number
- size: Requested page size
- numberOfElements: Actual items on this page

JSON Response Structure:
```json
{
  "content": [
    { "id": 1, "title": "Task 1", ... },
    { "id": 2, "title": "Task 2", ... }
  ],
  "pageable": {
    "pageNumber": 0,
    "pageSize": 10,
    "sort": { "sorted": true, "direction": "ASC" }
  },
  "totalElements": 47,
  "totalPages": 5,
  "last": false,
  "first": true,
  "empty": false
}
```

Frontend can use this metadata to render pagination controls ("Page 1 of 5", Previous/Next buttons).