import asyncpg
import asyncio

async def create_users_table():
    conn = await asyncpg.connect(
        host='localhost',
        port=5432,
        user='finance_user',
        password='secure_password',
        database='finance_tracker'
    )
    
    # TODO: Create the users table
    await conn.execute('''
        CREATE TABLE IF NOT EXISTS users (
            ____ SERIAL PRIMARY KEY,
            email VARCHAR(255) ____ NOT NULL,
            name VARCHAR(100) NOT NULL,
            created_at TIMESTAMP DEFAULT ____
        )
    ''')
    
    # TODO: Insert a test user using $1, $2 parameters
    await conn.execute('''
        INSERT INTO users (email, name) VALUES ($1, ____)
        ON CONFLICT (email) DO NOTHING
    ''', 'test@example.com', 'Test User')
    
    # Query the user
    user = await conn.fetchrow(
        'SELECT * FROM users WHERE email = $1',
        'test@example.com'
    )
    
    if user:
        print(f"User created: {user['name']} ({user['email']})")
    
    await conn.close()

asyncio.run(create_users_table())