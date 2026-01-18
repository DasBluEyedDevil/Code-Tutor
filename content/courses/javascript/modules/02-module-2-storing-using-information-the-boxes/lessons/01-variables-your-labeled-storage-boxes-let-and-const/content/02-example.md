---
type: "EXAMPLE"
title: "Variable Declaration and Reassignment"
---

This example shows how to declare variables, update them, and use them to calculate new values.

```javascript
// 1. Declaration and Initialization
const playerName = 'CyberKnight'; // This won't change
let currentLevel = 1;             // This will change
let healthPoints = 100;           // This will change

console.log('Player: ' + playerName);
console.log('Starting Level: ' + currentLevel);

// 2. Updating values (Reassignment)
// We don't use 'let' again when updating!
currentLevel = 2; 
healthPoints = healthPoints - 20; // Taking damage

console.log('Level Up! Now at level: ' + currentLevel);
console.log('Current Health: ' + healthPoints);

// 3. Using variables in calculations
const attackPower = 15;
const criticalHitMultiplier = 2;
let damageDealt = attackPower * criticalHitMultiplier;

console.log(playerName + ' dealt ' + damageDealt + ' damage!');

// 4. Constants cannot be changed
// const gravity = 9.8;
// gravity = 10; // This would throw a "TypeError: Assignment to constant variable"
```

**Expected Output**:

```text
Player: CyberKnight
Starting Level: 1
Level Up! Now at level: 2
Current Health: 80
CyberKnight dealt 30 damage!
```