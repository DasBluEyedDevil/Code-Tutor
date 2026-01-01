import asyncio

async def fetch_user(user_id):
    """Simulates fetching a user from API"""
    await asyncio.sleep(0.5)  # Simulate network delay
    return {"id": user_id, "name": f"User_{user_id}"}

async def fetch_all_users(user_ids):
    """Fetch all users concurrently"""
    # TODO: Use asyncio.gather() to fetch all users at once
    # Return a list of user data dictionaries
    pass

async def main():
    user_ids = [1, 2, 3, 4, 5]
    print(f"Fetching {len(user_ids)} users...")
    
    users = await fetch_all_users(user_ids)
    
    print("\nResults:")
    for user in users:
        print(f"  {user}")

asyncio.run(main())