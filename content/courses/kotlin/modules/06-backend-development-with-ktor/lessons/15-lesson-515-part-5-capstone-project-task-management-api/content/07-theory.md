---
type: "THEORY"
title: "Bonus Challenges (+20 points)"
---


### Challenge 1: Task Comments (+5 points)
Implement the comment system:
- Add comments to tasks
- Get task comments
- Delete comments (author or admin only)

### Challenge 2: Pagination (+5 points)
Add pagination to GET /api/tasks:
- `page` query parameter (default: 1)
- `pageSize` query parameter (default: 20)
- Return metadata: totalPages, totalItems, currentPage

### Challenge 3: Task Tags (+5 points)
Add tagging system:
- Tasks can have multiple tags
- Filter tasks by tags
- Create/delete tags

### Challenge 4: Task Analytics (+5 points)
Add analytics endpoints:
- GET /api/analytics/summary - Task counts by status
- GET /api/analytics/user/:id - User's task statistics
- GET /api/analytics/overdue - Overdue tasks report

---

