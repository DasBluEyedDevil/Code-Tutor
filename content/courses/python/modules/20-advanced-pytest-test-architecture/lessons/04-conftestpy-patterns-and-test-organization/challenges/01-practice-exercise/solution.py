# conftest.py
import pytest

@pytest.fixture(params=["dev", "prod"])
def environment(request):
    """Run tests with different environments."""
    env = request.param
    return {"name": env, "debug": env == "dev"}

# test_env.py
def test_environment_config(environment):
    if environment["name"] == "dev":
        assert environment["debug"] is True
    else:
        assert environment["debug"] is False
