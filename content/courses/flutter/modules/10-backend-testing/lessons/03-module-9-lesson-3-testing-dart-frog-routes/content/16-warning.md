---
type: "WARNING"
title: "Integration Test Considerations"
---

Keep these points in mind for integration tests:

1. **Database State**: Use a test database and clean it between tests

2. **Test Isolation**: Each test should be independent - do not rely on state from previous tests

3. **Port Conflicts**: Use port 0 to let the OS assign an available port

4. **Timeouts**: Network operations can be slow - set appropriate timeouts

5. **CI/CD Environment**: Ensure your CI has Docker or databases available

6. **Test Order**: Do not assume tests run in a specific order