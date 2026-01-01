---
type: "WARNING"
title: "Common Pitfalls"
---

## Watch Out For These Issues!

**Not assigning ID in POST**: Server assigns IDs, not client! Use 'item.Id = nextId++' before adding. Letting clients set IDs is a security risk!

**Modifying wrong object in PUT**: Don't modify the 'updatedItem' parameter! Find the existing item in the list, then update ITS properties. Otherwise changes aren't persisted!

**Forgetting to actually remove in DELETE**: Calling Results.NoContent() doesn't delete anything! Must call 'list.Remove(item)' first, THEN return NoContent.

**PUT vs PATCH confusion**: PUT replaces the ENTIRE resource (all fields). PATCH updates only specified fields. Most APIs use PUT for simplicity.