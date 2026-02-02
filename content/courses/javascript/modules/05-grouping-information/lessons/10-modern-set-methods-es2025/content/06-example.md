---
type: "EXAMPLE"
title: "symmetricDifference() - Items Unique to Either Set"
---

The symmetricDifference() method returns items that are in EITHER Set, but not in BOTH. It's the opposite of intersection - everything except the overlap.

```javascript
// symmetricDifference() returns items in EITHER set, but not BOTH
// Like 'people invited to only one party, not both'

let teamA = new Set(['Alice', 'Bob', 'Charlie']);
let teamB = new Set(['Charlie', 'Diana', 'Eve']);

let exclusiveMembers = teamA.symmetricDifference(teamB);
console.log(exclusiveMembers);
// Set { 'Alice', 'Bob', 'Diana', 'Eve' }
// Charlie is in BOTH, so excluded!

// This is the same as union minus intersection
// (A union B) - (A intersection B) = symmetricDifference

// Practical: Find changes between two versions
let version1Features = new Set(['login', 'dashboard', 'profile']);
let version2Features = new Set(['login', 'dashboard', 'settings', 'notifications']);

let changes = version1Features.symmetricDifference(version2Features);
console.log('Changed features:', changes);
// Set { 'profile', 'settings', 'notifications' }
// 'profile' was removed, 'settings' and 'notifications' were added

// Find students who didn't attend both days
let day1Attendance = new Set(['Alice', 'Bob', 'Charlie']);
let day2Attendance = new Set(['Bob', 'Charlie', 'Diana']);

let inconsistent = day1Attendance.symmetricDifference(day2Attendance);
console.log('Missed a day:', inconsistent);  // Alice and Diana
```
