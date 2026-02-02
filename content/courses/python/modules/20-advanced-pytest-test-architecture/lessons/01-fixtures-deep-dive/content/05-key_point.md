---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Scope controls lifetime**: function < class < module < package < session
- **Factory fixtures** return functions to create test data
- **Fixtures can depend on fixtures** for complex setups
- **`conftest.py`** makes fixtures available to all tests in a directory
- **Use `yield`** for setup/teardown in a single fixture