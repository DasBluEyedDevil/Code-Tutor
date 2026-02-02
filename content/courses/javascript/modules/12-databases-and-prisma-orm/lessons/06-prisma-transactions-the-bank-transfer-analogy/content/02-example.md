---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Prisma transactions.

```javascript
// Prisma Transactions - Complete Guide

// Simulating Prisma transaction concepts
console.log('=== Prisma Transactions ===\n');

// 1. SEQUENTIAL TRANSACTIONS (Array Syntax)
// All operations run in sequence, all succeed or all fail

let sequentialExample = `
// Array syntax: Pass array of operations
const [user, post] = await prisma.$transaction([
  prisma.user.create({
    data: { email: 'alice@example.com', name: 'Alice' }
  }),
  prisma.post.create({
    data: { title: 'Hello World', authorId: 1 }
  })
]);

// Both operations are guaranteed to succeed or fail together
console.log(user, post);
`;

console.log('1. SEQUENTIAL TRANSACTIONS (Array Syntax):');
console.log(sequentialExample);

// 2. INTERACTIVE TRANSACTIONS (Callback Syntax)
// More control: read data, make decisions, then write

let interactiveExample = `
// Callback syntax: Full control over transaction
const result = await prisma.$transaction(async (tx) => {
  // Step 1: Read current balance
  const fromAccount = await tx.account.findUnique({
    where: { id: 1 }
  });
  
  // Step 2: Check if sufficient funds
  if (fromAccount.balance < 100) {
    throw new Error('Insufficient funds');
    // Transaction will automatically rollback!
  }
  
  // Step 3: Subtract from sender
  const sender = await tx.account.update({
    where: { id: 1 },
    data: { balance: { decrement: 100 } }
  });
  
  // Step 4: Add to receiver
  const receiver = await tx.account.update({
    where: { id: 2 },
    data: { balance: { increment: 100 } }
  });
  
  // Return result
  return { sender, receiver };
});

// If any step throws an error, ALL changes are rolled back
`;

console.log('\n2. INTERACTIVE TRANSACTIONS (Callback Syntax):');
console.log(interactiveExample);

// 3. NESTED WRITES (Automatic Transactions)
// Prisma automatically wraps nested creates/updates in a transaction

let nestedWriteExample = `
// Nested writes are automatically transactional!
const user = await prisma.user.create({
  data: {
    email: 'bob@example.com',
    name: 'Bob',
    profile: {
      create: { bio: 'Hello!' }  // Created in same transaction
    },
    posts: {
      create: [                   // All created in same transaction
        { title: 'Post 1' },
        { title: 'Post 2' }
      ]
    }
  },
  include: {
    profile: true,
    posts: true
  }
});

// If ANY nested create fails, the entire operation rolls back
// User, profile, and posts are either ALL created or NONE
`;

console.log('\n3. NESTED WRITES (Automatic Transactions):');
console.log(nestedWriteExample);

// 4. TRANSACTION OPTIONS

let optionsExample = `
// Configure transaction behavior
const result = await prisma.$transaction(
  async (tx) => {
    // Your transaction logic here
    const user = await tx.user.create({ ... });
    return user;
  },
  {
    maxWait: 5000,      // Max time to wait for transaction slot (ms)
    timeout: 10000,     // Max time for transaction to complete (ms)
    isolationLevel: 'Serializable'  // Transaction isolation level
  }
);

// Isolation levels:
// - ReadUncommitted: Lowest isolation, fastest
// - ReadCommitted: See only committed data (default in PostgreSQL)
// - RepeatableRead: Consistent reads within transaction
// - Serializable: Highest isolation, slowest
`;

console.log('\n4. TRANSACTION OPTIONS:');
console.log(optionsExample);

// 5. ERROR HANDLING IN TRANSACTIONS

let errorHandlingExample = `
// Proper error handling
try {
  const result = await prisma.$transaction(async (tx) => {
    // Operations that might fail
    const user = await tx.user.create({ ... });
    const order = await tx.order.create({ ... });
    
    // Simulate a business logic check
    if (order.total > 10000) {
      throw new Error('Order exceeds maximum limit');
    }
    
    return { user, order };
  });
  
  console.log('Transaction succeeded:', result);
} catch (error) {
  // Transaction automatically rolled back
  console.error('Transaction failed:', error.message);
  
  // Handle specific errors
  if (error.code === 'P2002') {
    console.error('Unique constraint violation');
  }
}
`;

console.log('\n5. ERROR HANDLING:');
console.log(errorHandlingExample);

// 6. WHEN TO USE TRANSACTIONS

console.log('\n6. WHEN TO USE TRANSACTIONS:');

let useCases = {
  'Money Transfers': 'Debit one account, credit another',
  'Order Processing': 'Create order + update inventory + charge payment',
  'User Registration': 'Create user + create profile + send welcome email record',
  'Bulk Updates': 'Update multiple related records together',
  'Data Migration': 'Transform and move data safely',
  'Booking Systems': 'Reserve seat + create ticket + charge customer'
};

for (let [useCase, description] of Object.entries(useCases)) {
  console.log(`  - ${useCase}: ${description}`);
}

// SIMULATION: Bank Transfer

console.log('\n=== SIMULATION: Bank Transfer ===\n');

class MockPrisma {
  constructor() {
    this.accounts = [
      { id: 1, name: 'Alice', balance: 1000 },
      { id: 2, name: 'Bob', balance: 500 }
    ];
    this.transactionLog = [];
  }
  
  async $transaction(callback) {
    // Create a snapshot for rollback
    const snapshot = JSON.parse(JSON.stringify(this.accounts));
    
    try {
      // Create transaction context
      const tx = {
        account: {
          findUnique: async ({ where }) => {
            return this.accounts.find(a => a.id === where.id);
          },
          update: async ({ where, data }) => {
            const account = this.accounts.find(a => a.id === where.id);
            if (!account) throw new Error('Account not found');
            
            if (data.balance?.decrement) {
              account.balance -= data.balance.decrement;
            }
            if (data.balance?.increment) {
              account.balance += data.balance.increment;
            }
            
            return account;
          }
        }
      };
      
      // Execute transaction
      const result = await callback(tx);
      
      // Commit: log success
      this.transactionLog.push({ status: 'COMMITTED', timestamp: new Date() });
      console.log('Transaction COMMITTED');
      
      return result;
    } catch (error) {
      // Rollback: restore snapshot
      this.accounts = snapshot;
      this.transactionLog.push({ status: 'ROLLED_BACK', error: error.message, timestamp: new Date() });
      console.log('Transaction ROLLED BACK:', error.message);
      
      throw error;
    }
  }
}

const mockPrisma = new MockPrisma();

async function transferMoney(fromId, toId, amount) {
  return mockPrisma.$transaction(async (tx) => {
    // Get sender account
    const from = await tx.account.findUnique({ where: { id: fromId } });
    console.log(`Sender ${from.name} has $${from.balance}`);
    
    // Check balance
    if (from.balance < amount) {
      throw new Error(`Insufficient funds: ${from.name} only has $${from.balance}`);
    }
    
    // Debit sender
    const sender = await tx.account.update({
      where: { id: fromId },
      data: { balance: { decrement: amount } }
    });
    console.log(`Debited $${amount} from ${sender.name}`);
    
    // Credit receiver
    const receiver = await tx.account.update({
      where: { id: toId },
      data: { balance: { increment: amount } }
    });
    console.log(`Credited $${amount} to ${receiver.name}`);
    
    return { sender, receiver };
  });
}

// Test successful transfer
console.log('Test 1: Transfer $200 from Alice to Bob');
transferMoney(1, 2, 200).then(result => {
  console.log(`Alice: $${result.sender.balance}, Bob: $${result.receiver.balance}`);
});

// Test failed transfer (insufficient funds)
setTimeout(() => {
  console.log('\nTest 2: Transfer $5000 from Alice to Bob (should fail)');
  transferMoney(1, 2, 5000).catch(e => {
    console.log('Final balances:', mockPrisma.accounts);
  });
}, 100);
```
