---
type: "EXAMPLE"
title: "Code Example"
---

The conftest.py file is a special pytest file that shares fixtures across multiple test files in the same directory.

```python
# conftest.py - Fixtures available to all tests in directory
import pytest
from pathlib import Path
import tempfile

@pytest.fixture(scope="session")
def database_connection():
    """Expensive setup - reused across all tests."""
    print("\nConnecting to database...")
    conn = {"host": "localhost", "connected": True}  # Simulated
    yield conn
    print("\nClosing database connection...")
    conn["connected"] = False

@pytest.fixture(scope="function")
def temp_file(tmp_path):
    """Fresh temp file for each test."""
    file_path = tmp_path / "test_data.txt"
    file_path.write_text("initial content")
    return file_path

@pytest.fixture
def user_factory():
    """Factory fixture - returns a function to create users."""
    created_users = []
    
    def _create_user(name: str, role: str = "user"):
        user = {"name": name, "role": role, "id": len(created_users) + 1}
        created_users.append(user)
        return user
    
    yield _create_user
    
    # Cleanup: delete created users
    print(f"\nCleaning up {len(created_users)} test users")

# test_example.py
def test_database_query(database_connection):
    assert database_connection["connected"] is True

def test_file_operations(temp_file):
    assert temp_file.read_text() == "initial content"
    temp_file.write_text("modified")
    assert temp_file.read_text() == "modified"

def test_multiple_users(user_factory):
    admin = user_factory("Alice", role="admin")
    user = user_factory("Bob")
    assert admin["role"] == "admin"
    assert user["id"] == 2

```
