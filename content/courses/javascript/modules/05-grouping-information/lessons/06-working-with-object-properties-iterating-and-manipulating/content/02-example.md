---
type: "EXAMPLE"
title: "Object Manipulation"
---

```javascript
const stats = {
    strength: 15,
    agility: 20,
    intelligence: 10
};

// 1. Getting all Keys
const keys = Object.keys(stats);
console.log('Keys:', keys); // ["strength", "agility", "intelligence"]

// 2. Getting all Values
const values = Object.values(stats);
console.log('Values:', values); // [15, 20, 10]

// 3. Getting Key-Value Pairs (Entries)
const entries = Object.entries(stats);
console.log('Entries:', entries); 
// [["strength", 15], ["agility", 20], ["intelligence", 10]]

// 4. The for...in Loop
// This is a special loop just for objects!
for (const key in stats) {
    console.log(`${key.toUpperCase()}: ${stats[key]}`);
}

// 5. Deleting a property
delete stats.intelligence;
console.log('After delete:', stats); // { strength: 15, agility: 20 }

// 6. Checking for a property
if ('strength' in stats) {
    console.log("This character is strong!");
}
```