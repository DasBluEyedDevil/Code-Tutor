---
type: "EXAMPLE"
title: "Code Example"
---

This example shows how to mock external API calls so tests don't depend on network or live services.

```python
# weather.py
import httpx

def get_weather(city: str) -> dict:
    response = httpx.get(f"https://api.weather.com/{city}")
    response.raise_for_status()
    return response.json()

def weather_message(city: str) -> str:
    data = get_weather(city)
    return f"It's {data['temp']}F in {city}"

# test_weather.py
import pytest
from unittest.mock import patch, MagicMock

def test_weather_message_with_patch():
    # Mock the get_weather function
    with patch("weather.get_weather") as mock_get_weather:
        mock_get_weather.return_value = {"temp": 72, "conditions": "sunny"}
        
        result = weather_message("Seattle")
        
        assert result == "It's 72F in Seattle"
        mock_get_weather.assert_called_once_with("Seattle")

def test_weather_api_error():
    with patch("weather.get_weather") as mock_get_weather:
        mock_get_weather.side_effect = httpx.HTTPError("API down")
        
        with pytest.raises(httpx.HTTPError):
            weather_message("Seattle")

# Using pytest-mock (cleaner)
def test_weather_with_mocker(mocker):
    mock_weather = mocker.patch("weather.get_weather")
    mock_weather.return_value = {"temp": 65}
    
    result = weather_message("Portland")
    
    assert "65" in result

```
