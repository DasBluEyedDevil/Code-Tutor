import asyncio

async def slow_api_call(delay):
    """Simulates a slow API that takes 'delay' seconds"""
    await asyncio.sleep(delay)
    return {"status": "success", "data": "result"}

async def fetch_with_timeout(delay, timeout):
    """Fetch data with timeout. Return None if timeout occurs."""
    # TODO: Use asyncio.wait_for with timeout
    # TODO: Handle TimeoutError and return None
    pass

async def main():
    # This should succeed (0.5s delay, 1s timeout)
    result1 = await fetch_with_timeout(0.5, 1.0)
    print(f"Result 1: {result1}")
    
    # This should timeout (2s delay, 0.5s timeout)
    result2 = await fetch_with_timeout(2.0, 0.5)
    print(f"Result 2: {result2}")

asyncio.run(main())