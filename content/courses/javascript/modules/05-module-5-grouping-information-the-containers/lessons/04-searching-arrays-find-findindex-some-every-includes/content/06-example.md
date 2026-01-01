---
type: "EXAMPLE"
title: "findLast() and findLastIndex() (ES2023)"
---

These methods search from the END of the array, perfect when you need the most recent or last matching element. They work exactly like find() and findIndex() but in reverse order.

```javascript
// === findLast() - Find from the end ===
let transactions = [
  { id: 1, type: 'deposit', amount: 100, date: '2024-01-01' },
  { id: 2, type: 'withdrawal', amount: 50, date: '2024-01-05' },
  { id: 3, type: 'deposit', amount: 200, date: '2024-01-10' },
  { id: 4, type: 'withdrawal', amount: 75, date: '2024-01-15' },
  { id: 5, type: 'deposit', amount: 150, date: '2024-01-20' }
];

// Find MOST RECENT deposit
let lastDeposit = transactions.findLast(t => t.type === 'deposit');
console.log(lastDeposit);
// { id: 5, type: 'deposit', amount: 150, date: '2024-01-20' }

// Compare with find() - finds FIRST deposit
let firstDeposit = transactions.find(t => t.type === 'deposit');
console.log(firstDeposit);
// { id: 1, type: 'deposit', amount: 100, date: '2024-01-01' }

// === findLastIndex() - Position of last match ===
let numbers = [5, 12, 8, 130, 44, 3, 15];

// Find position of LAST number > 10
let lastBigIndex = numbers.findLastIndex(n => n > 10);
console.log(lastBigIndex);  // 6 (the value 15 at position 6)

// Compare with findIndex() - finds FIRST
let firstBigIndex = numbers.findIndex(n => n > 10);
console.log(firstBigIndex);  // 1 (the value 12 at position 1)

// === Real-world: Error Logging ===
let logs = [
  { level: 'info', message: 'App started', time: '09:00' },
  { level: 'error', message: 'DB connection failed', time: '09:05' },
  { level: 'info', message: 'Retrying...', time: '09:06' },
  { level: 'error', message: 'Still failing', time: '09:07' },
  { level: 'info', message: 'Connected!', time: '09:10' }
];

// Get the most recent error
let lastError = logs.findLast(log => log.level === 'error');
console.log('Last error:', lastError.message);  // 'Still failing'

// === Real-world: Version History ===
let versions = [
  { version: '1.0', stable: true },
  { version: '1.1', stable: true },
  { version: '2.0-beta', stable: false },
  { version: '2.0', stable: true },
  { version: '2.1-alpha', stable: false }
];

// Find latest stable version
let latestStable = versions.findLast(v => v.stable);
console.log('Latest stable:', latestStable.version);  // '2.0'

// === When nothing is found ===
let emptyResult = transactions.findLast(t => t.type === 'transfer');
console.log(emptyResult);  // undefined

let emptyIndex = numbers.findLastIndex(n => n > 1000);
console.log(emptyIndex);  // -1

// === Alternative before ES2023 ===
// If you can't use findLast(), you can reverse and find:
let altLastDeposit = [...transactions].reverse().find(t => t.type === 'deposit');
console.log(altLastDeposit.id);  // 5

// But findLast() is cleaner and doesn't create a copy!
```
