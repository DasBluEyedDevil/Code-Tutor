import asyncio

# Async version of fetch_weather
async def fetch_weather_async(city):
    """Fetches weather data asynchronously (simulated)"""
    print(f"Fetching weather for {city}...")
    await asyncio.sleep(1)  # Non-blocking sleep
    return {"city": city, "temp": 72, "condition": "sunny"}

# Main async function to demonstrate
async def main():
    print("=== Async Weather Fetch ===")
    
    # Single fetch
    result = await fetch_weather_async("New York")
    print(f"Result: {result}\n")
    
    # Multiple fetches (still sequential for now)
    print("Fetching multiple cities...")
    cities = ["London", "Tokyo", "Paris"]
    for city in cities:
        weather = await fetch_weather_async(city)
        print(f"  {weather}")

# Run the async code
print("Starting async weather app...\n")
asyncio.run(main())
print("\nDone!")