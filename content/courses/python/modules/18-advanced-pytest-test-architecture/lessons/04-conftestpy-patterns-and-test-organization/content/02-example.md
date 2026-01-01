---
type: "EXAMPLE"
title: "Code Example"
---

**A well-organized conftest.py:**

```python
# tests/conftest.py
import pytest
from pathlib import Path
import json

# === Configuration ===
def pytest_configure(config):
    """Called at pytest startup."""
    config.addinivalue_line(
        "markers", "slow: marks tests as slow (deselect with '-m \"not slow\"')"
    )

# === Shared Fixtures ===
@pytest.fixture(scope="session")
def test_data_dir():
    """Path to test data directory."""
    return Path(__file__).parent / "data"

@pytest.fixture(scope="session")
def sample_config(test_data_dir):
    """Load sample configuration."""
    config_file = test_data_dir / "config.json"
    return json.loads(config_file.read_text())

# === Factory Fixtures ===
@pytest.fixture
def make_user():
    """Factory for creating test users."""
    _created = []
    
    def _make_user(name: str, role: str = "user"):
        user = {"name": name, "role": role}
        _created.append(user)
        return user
    
    yield _make_user
    # Cleanup after test
    _created.clear()

# === Autouse Fixtures ===
@pytest.fixture(autouse=True)
def reset_environment(monkeypatch):
    """Reset environment for each test."""
    monkeypatch.setenv("TESTING", "true")
    monkeypatch.setenv("DEBUG", "false")

# === Parameterized Fixtures ===
@pytest.fixture(params=["sqlite", "postgres"])
def database_type(request):
    """Run tests with different databases."""
    return request.param

# === Skip Markers ===
def pytest_collection_modifyitems(config, items):
    """Add skip markers based on conditions."""
    import sys
    if sys.platform == "win32":
        skip_unix = pytest.mark.skip(reason="Unix only")
        for item in items:
            if "unix_only" in item.keywords:
                item.add_marker(skip_unix)

```
