from unittest.mock import patch

def get_user_greeting(user_id: int) -> str:
    from db import get_user
    user = get_user(user_id)
    return f"Hello, {user['name']}!"

def test_user_greeting():
    with patch("db.get_user") as mock_get_user:
        mock_get_user.return_value = {"id": 1, "name": "Alice"}
        
        result = get_user_greeting(1)
        
        assert result == "Hello, Alice!"
        mock_get_user.assert_called_once_with(1)
