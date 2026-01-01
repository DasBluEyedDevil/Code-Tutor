let players = [
  { name: 'Alice', score: 850 },
  { name: 'Bob', score: 920 },
  { name: 'Charlie', score: 780 },
  { name: 'Diana', score: 1050 },
  { name: 'Eve', score: 890 }
];

// 1. Get top 3 players (sorted by score, highest first)
let top3 = players.toSorted((a, b) => b.score - a.score).slice(0, 3);
console.log('Top 3:', top3.map(p => p.name));  // Diana, Bob, Eve

// 2. Get the last place player using at()
let sorted = players.toSorted((a, b) => b.score - a.score);
let lastPlace = sorted.at(-1);
console.log('Last place:', lastPlace.name);  // Charlie

// 3. Remove player at index 2 (Charlie) using toSpliced()
let withoutCharlie = players.toSpliced(2, 1);
console.log('Without Charlie:', withoutCharlie.map(p => p.name));

// Verify original is unchanged
console.log('Original:', players.map(p => p.name));