import asyncio
import time

async def fetch_user(user_id):
    """Simulates fetching a user from API"""
    print(f"  Fetching user {user_id}...")
    await asyncio.sleep(0.5)  # Simulate network delay
    return {"id": user_id, "name": f"User_{user_id}"}

async def fetch_all_users(user_ids):
    """Fetch all users concurrently using asyncio.gather()"""
    # Create coroutines for each user
    coroutines = [fetch_user(uid) for uid in user_ids]
    
    # Run all concurrently and collect results
    results = await asyncio.gather(*coroutines)
    
    return results

async def main():
    user_ids = [1, 2, 3, 4, 5]
    print(f"Fetching {len(user_ids)} users concurrently...")
    
    start = time.time()
    users = await fetch_all_users(user_ids)
    elapsed = time.time() - start
    
    print(f"\nResults (took {elapsed:.2f}s):")
    for user in users:
        print(f"  {user}")
    
    print(f"\nNote: 5 users in {elapsed:.2f}s instead of 2.5s!")

asyncio.run(main())