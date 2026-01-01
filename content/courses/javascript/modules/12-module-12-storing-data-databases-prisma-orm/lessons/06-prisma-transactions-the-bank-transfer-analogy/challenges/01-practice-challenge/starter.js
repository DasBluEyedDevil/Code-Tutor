// Transaction System Simulator

class MockDatabase {
  constructor() {
    this.accounts = [
      { id: 1, name: 'Alice', balance: 1000 },
      { id: 2, name: 'Bob', balance: 500 }
    ];
    this.transactionHistory = [];
  }
  
  // Snapshot current state
  snapshot() {
    return JSON.parse(JSON.stringify(this.accounts));
  }
  
  // Restore from snapshot
  restore(snapshot) {
    this.accounts = snapshot;
  }
  
  // Transaction method
  async $transaction(callback) {
    const snap = this.snapshot();
    
    try {
      const result = await callback(this);
      this.transactionHistory.push({ status: 'COMMITTED', time: new Date() });
      console.log('COMMITTED');
      return result;
    } catch (error) {
      this.restore(snap);
      this.transactionHistory.push({ status: 'ROLLED_BACK', error: error.message, time: new Date() });
      console.log('ROLLED BACK:', error.message);
      throw error;
    }
  }
  
  // Find account
  findAccount(id) {
    return this.accounts.find(a => a.id === id);
  }
  
  // Update balance
  updateBalance(id, amount) {
    const account = this.findAccount(id);
    if (!account) throw new Error('Account not found');
    account.balance += amount;
    return account;
  }
}

const db = new MockDatabase();

// Transfer function using transaction
async function transfer(fromId, toId, amount) {
  return db.$transaction(async (database) => {
    const from = database.findAccount(fromId);
    
    if (from.balance < amount) {
      throw new Error('Insufficient funds');
    }
    
    database.updateBalance(fromId, -amount);
    database.updateBalance(toId, amount);
    
    return { from: database.findAccount(fromId), to: database.findAccount(toId) };
  });
}

// Test
console.log('Initial balances:', db.accounts);

console.log('\nTest 1: Transfer $200 from Alice to Bob');
transfer(1, 2, 200).then(result => {
  console.log('After transfer:', db.accounts);
  
  console.log('\nTest 2: Transfer $2000 (should fail)');
  return transfer(1, 2, 2000);
}).catch(e => {
  console.log('Balances after failed transfer:', db.accounts);
});