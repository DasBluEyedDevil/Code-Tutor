import time

# Synchronous version
def fetch_weather(city):
    """Fetches weather data (simulated)"""
    print(f"Fetching weather for {city}...")
    time.sleep(1)  # Simulate API call
    return {"city": city, "temp": 72, "condition": "sunny"}

# TODO: Convert to async version
# async def fetch_weather_async(city):
#     ...

# Test the sync version
result = fetch_weather("New York")
print(f"Result: {result}")

# TODO: Test the async version
# result = asyncio.run(fetch_weather_async("New York"))