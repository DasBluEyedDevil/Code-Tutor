---
type: "EXAMPLE"
title: "ReturnType<T> and Parameters<T>"
---

ReturnType<T> extracts the return type from a function type. Parameters<T> extracts the parameter types as a tuple. These are invaluable when you don't control the function definition - like when working with third-party libraries or dynamically generating wrapper functions.

```typescript
// RETURNTYPE<T> - Extract the return type of a function

function createUser(name: string, email: string) {
  return {
    id: Math.random(),
    name,
    email,
    createdAt: new Date()
  };
}

// Extract the return type without manually defining it
type User = ReturnType<typeof createUser>;
// User is: { id: number; name: string; email: string; createdAt: Date }

// Now you can use this type elsewhere
function displayUser(user: User): void {
  console.log(`${user.name} (${user.email}) - ID: ${user.id}`);
}

const newUser = createUser('Alice', 'alice@test.com');
displayUser(newUser);
// Alice (alice@test.com) - ID: 0.123456789

// Why is this useful? Working with third-party libraries!
// Imagine this function comes from a library you don't control:
function fetchFromApi<T>(endpoint: string): Promise<{ data: T; status: number }> {
  // Simulated API response
  return Promise.resolve({ data: {} as T, status: 200 });
}

// Extract the response type
type ApiResponse<T> = ReturnType<typeof fetchFromApi<T>>;
// ApiResponse<T> is Promise<{ data: T; status: number }>

// Extracting type from async functions
async function loadUserData(id: string) {
  // Simulated async operation
  return {
    profile: { name: 'Alice', avatar: 'alice.png' },
    settings: { theme: 'dark', notifications: true }
  };
}

type UserData = Awaited<ReturnType<typeof loadUserData>>;
// UserData is: { profile: {...}; settings: {...} }

// PARAMETERS<T> - Extract function parameters as a tuple

function sendEmail(to: string, subject: string, body: string, priority?: number): boolean {
  console.log(`Sending to ${to}: ${subject}`);
  return true;
}

type EmailParams = Parameters<typeof sendEmail>;
// EmailParams is: [to: string, subject: string, body: string, priority?: number]

// Use cases: Creating wrapper functions
function loggedSendEmail(...args: EmailParams): boolean {
  console.log('Email params:', args);
  return sendEmail(...args);
}

loggedSendEmail('test@test.com', 'Hello', 'Message body');
// Email params: ['test@test.com', 'Hello', 'Message body']
// Sending to test@test.com: Hello

// Extract individual parameter types
type FirstParam = Parameters<typeof sendEmail>[0];  // string (to)
type SecondParam = Parameters<typeof sendEmail>[1]; // string (subject)

// Combining ReturnType and Parameters for decorators
function withRetry<T extends (...args: any[]) => any>(
  fn: T,
  maxRetries: number = 3
): (...args: Parameters<T>) => ReturnType<T> {
  return (...args: Parameters<T>): ReturnType<T> => {
    let lastError: Error | undefined;
    for (let i = 0; i < maxRetries; i++) {
      try {
        return fn(...args);
      } catch (e) {
        lastError = e as Error;
        console.log(`Retry ${i + 1}/${maxRetries}`);
      }
    }
    throw lastError;
  };
}

function riskyOperation(value: number): string {
  if (Math.random() > 0.7) {
    return `Success: ${value}`;
  }
  throw new Error('Random failure');
}

const safeOperation = withRetry(riskyOperation, 5);
// safeOperation has same type signature as riskyOperation
console.log(safeOperation(42));
// May output: Retry 1/5, Retry 2/5, then Success: 42

// Extract constructor parameters with ConstructorParameters
class DatabaseConnection {
  constructor(
    public host: string,
    public port: number,
    public database: string
  ) {}
}

type DBParams = ConstructorParameters<typeof DatabaseConnection>;
// DBParams is: [host: string, port: number, database: string]

function createConnection(...args: DBParams): DatabaseConnection {
  return new DatabaseConnection(...args);
}

const db = createConnection('localhost', 5432, 'myapp');
console.log(db);  // DatabaseConnection { host: 'localhost', port: 5432, database: 'myapp' }
```
