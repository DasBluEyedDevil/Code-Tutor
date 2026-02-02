---
type: "THEORY"
title: "Provider Overrides in Tests"
---


The key to testing Riverpod widgets is **provider overrides**. You can replace any provider with a test-specific implementation.

**Why Override?**
- Replace API clients with mocks
- Provide known test data
- Simulate error states
- Control async timing

