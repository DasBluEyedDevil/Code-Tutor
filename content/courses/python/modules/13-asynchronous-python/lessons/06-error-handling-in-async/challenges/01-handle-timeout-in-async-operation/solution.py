import asyncio

async def slow_api_call(delay):
    """Simulates a slow API that takes 'delay' seconds"""
    print(f"  API call starting (will take {delay}s)...")
    await asyncio.sleep(delay)
    return {"status": "success", "data": "result"}

async def fetch_with_timeout(delay, timeout):
    """Fetch data with timeout. Return None if timeout occurs."""
    try:
        result = await asyncio.wait_for(
            slow_api_call(delay),
            timeout=timeout
        )
        return result
    except asyncio.TimeoutError:
        print(f"  Timeout after {timeout}s!")
        return None

async def main():
    print("=== Test 1: Should succeed ===")
    result1 = await fetch_with_timeout(0.5, 1.0)
    print(f"Result 1: {result1}\n")
    
    print("=== Test 2: Should timeout ===")
    result2 = await fetch_with_timeout(2.0, 0.5)
    print(f"Result 2: {result2}\n")
    
    print("=== Test 3: Multiple with gather ===")
    results = await asyncio.gather(
        fetch_with_timeout(0.2, 1.0),
        fetch_with_timeout(0.3, 1.0),
        fetch_with_timeout(5.0, 0.5),  # This will timeout
    )
    
    for i, r in enumerate(results):
        status = "SUCCESS" if r else "TIMEOUT"
        print(f"  Task {i+1}: {status}")

asyncio.run(main())