---
type: "THEORY"
title: "Challenge: Extend the TaskList"
---

## Challenge: Add Filtering and Search

Extend the TaskList component with:
1. **Search functionality**: Filter tasks by title or description (case-insensitive)
2. **Status filter**: Show only 'All', 'Active', or 'Completed' tasks
3. **Sort options**: Sort by creation date (newest/oldest) or completion status

**Requirements:**
- Use useState hooks to manage filter state
- Create separate functions for filtering logic
- Update the UI to show current filter selections
- Maintain TypeScript typing throughout
- Keep API calls unchanged (filter on the client side)

**Bonus Challenge:**
- Add a "Clear completed" button that deletes all completed tasks
- Implement pagination to show 10 tasks per page
- Add an animation when tasks are marked complete