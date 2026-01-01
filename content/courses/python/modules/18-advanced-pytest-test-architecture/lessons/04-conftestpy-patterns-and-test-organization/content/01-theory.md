---
type: "THEORY"
title: "The Power of conftest.py"
---

`conftest.py` is pytest's configuration file that lives alongside your tests.

**What can go in conftest.py:**
- Fixtures (shared across all tests in directory)
- Hooks (customize pytest behavior)
- Plugins (local pytest extensions)
- pytest_configure (setup/teardown)

**Hierarchy:** conftest.py files cascade - fixtures in a parent directory are available to all subdirectories.

```
tests/
  conftest.py           # Global fixtures
  unit/
    conftest.py         # Unit-specific fixtures
    test_models.py
  integration/
    conftest.py         # Integration-specific fixtures  
    test_api.py
```