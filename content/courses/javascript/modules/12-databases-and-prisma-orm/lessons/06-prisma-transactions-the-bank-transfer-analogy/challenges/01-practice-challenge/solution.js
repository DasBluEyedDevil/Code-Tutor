// Complete Transaction System Simulator

class MockDatabase {
  constructor() {
    this.accounts = [
      { id: 1, name: 'Alice', balance: 1000 },
      { id: 2, name: 'Bob', balance: 500 },
      { id: 3, name: 'Charlie', balance: 750 }
    ];
    this.transactionHistory = [];
    this.transactionCount = 0;
  }
  
  snapshot() {
    return JSON.parse(JSON.stringify(this.accounts));
  }
  
  restore(snapshot) {
    this.accounts = snapshot;
  }
  
  async $transaction(callback, options = {}) {
    const transactionId = ++this.transactionCount;
    const startTime = Date.now();
    const snap = this.snapshot();
    const timeout = options.timeout || 5000;
    
    console.log(`[TX-${transactionId}] Starting transaction...`);
    
    try {
      const timeoutPromise = new Promise((_, reject) => {
        setTimeout(() => reject(new Error('Transaction timeout')), timeout);
      });
      
      const result = await Promise.race([
        callback(this),
        timeoutPromise
      ]);
      
      const duration = Date.now() - startTime;
      this.transactionHistory.push({
        id: transactionId,
        status: 'COMMITTED',
        duration: `${duration}ms`,
        time: new Date().toISOString()
      });
      
      console.log(`[TX-${transactionId}] COMMITTED (${duration}ms)`);
      return result;
    } catch (error) {
      this.restore(snap);
      const duration = Date.now() - startTime;
      
      this.transactionHistory.push({
        id: transactionId,
        status: 'ROLLED_BACK',
        error: error.message,
        duration: `${duration}ms`,
        time: new Date().toISOString()
      });
      
      console.log(`[TX-${transactionId}] ROLLED BACK: ${error.message} (${duration}ms)`);
      throw error;
    }
  }
  
  findAccount(id) {
    return this.accounts.find(a => a.id === id);
  }
  
  updateBalance(id, amount) {
    const account = this.findAccount(id);
    if (!account) throw new Error(`Account ${id} not found`);
    account.balance += amount;
    return { ...account };
  }
  
  getBalance(id) {
    const account = this.findAccount(id);
    return account ? account.balance : null;
  }
  
  printBalances() {
    console.log('Balances:');
    this.accounts.forEach(a => {
      console.log(`  ${a.name}: $${a.balance}`);
    });
  }
  
  printHistory() {
    console.log('\nTransaction History:');
    this.transactionHistory.forEach(tx => {
      const status = tx.status === 'COMMITTED' ? '✓' : '✗';
      const error = tx.error ? ` (${tx.error})` : '';
      console.log(`  ${status} TX-${tx.id}: ${tx.status}${error} - ${tx.duration}`);
    });
  }
}

const db = new MockDatabase();

async function transfer(fromId, toId, amount) {
  return db.$transaction(async (database) => {
    const from = database.findAccount(fromId);
    const to = database.findAccount(toId);
    
    if (!from) throw new Error(`Sender account ${fromId} not found`);
    if (!to) throw new Error(`Receiver account ${toId} not found`);
    
    console.log(`  Transferring $${amount} from ${from.name} to ${to.name}`);
    console.log(`  ${from.name}'s balance: $${from.balance}`);
    
    if (from.balance < amount) {
      throw new Error(`Insufficient funds: ${from.name} has $${from.balance}, needs $${amount}`);
    }
    
    const sender = database.updateBalance(fromId, -amount);
    console.log(`  Debited $${amount} from ${from.name}`);
    
    const receiver = database.updateBalance(toId, amount);
    console.log(`  Credited $${amount} to ${to.name}`);
    
    return {
      success: true,
      from: sender,
      to: receiver,
      amount
    };
  });
}

async function batchTransfer(transfers) {
  return db.$transaction(async (database) => {
    const results = [];
    
    for (const { fromId, toId, amount } of transfers) {
      const from = database.findAccount(fromId);
      if (from.balance < amount) {
        throw new Error(`Batch failed: ${from.name} has insufficient funds`);
      }
      database.updateBalance(fromId, -amount);
      database.updateBalance(toId, amount);
      results.push({ fromId, toId, amount, status: 'OK' });
    }
    
    return results;
  });
}

console.log('=== Transaction Simulator ===\n');
db.printBalances();

console.log('\n--- Test 1: Successful Transfer ($200) ---');
transfer(1, 2, 200)
  .then(() => {
    db.printBalances();
    
    console.log('\n--- Test 2: Failed Transfer ($2000 - insufficient) ---');
    return transfer(1, 2, 2000);
  })
  .catch(() => {
    console.log('Balances unchanged after rollback:');
    db.printBalances();
    
    console.log('\n--- Test 3: Batch Transfer (all or nothing) ---');
    return batchTransfer([
      { fromId: 1, toId: 3, amount: 100 },
      { fromId: 2, toId: 3, amount: 5000 }  // This will fail
    ]);
  })
  .catch(() => {
    console.log('Batch rolled back - balances unchanged:');
    db.printBalances();
    db.printHistory();
  });