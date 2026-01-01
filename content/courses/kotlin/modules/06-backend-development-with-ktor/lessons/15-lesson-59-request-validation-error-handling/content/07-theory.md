---
type: "THEORY"
title: "Solution"
---


### Complete Product Validation System






### Test Cases

**Test 1: Valid Product Creation**

Response (201 Created):

**Test 2: Validation Errors**

Response (400 Bad Request):

**Test 3: Duplicate SKU**

Response (409 Conflict):

**Test 4: Updating Inactive Product**

Response (422 Unprocessable Entity):

---



```json
{
  "success": false,
  "message": "Cannot update inactive product. Reactivate it first.",
  "timestamp": "2025-01-15T14:27:45.012"
}
```
