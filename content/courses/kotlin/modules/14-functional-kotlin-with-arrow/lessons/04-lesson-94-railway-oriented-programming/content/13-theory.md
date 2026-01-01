---
type: "THEORY"
title: "Exercise: Build an Order Pipeline"
---


**Goal**: Create a complete order processing railway.

**Steps**:
1. Validate order (check items, quantities)
2. Check customer credit
3. Reserve inventory
4. Process payment
5. Generate invoice
6. Send confirmation

**Requirements**:
- Each step has its own error type
- Short-circuit on any failure
- Handle all error types at the end

---

