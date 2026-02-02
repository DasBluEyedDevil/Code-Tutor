---
type: "WARNING"
title: "Common Contract Testing Mistakes"
---


**1. Testing Only Happy Paths**

Also test error cases like 404 for missing resources and 400 for invalid input.

**2. Coupling Tests to Implementation**

Test observable behavior, not internal JSON structure.

**3. Ignoring Optional Field Changes**

Handle optional fields safely and verify required fields are present.

**4. Not Testing Array Responses**

Validate each element in array responses matches the contract.

**5. Hardcoding Test Data**

Use fixtures that tests create themselves, not production data.

