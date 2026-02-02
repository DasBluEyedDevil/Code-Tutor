let players = [
  { name: 'Alice', score: 850 },
  { name: 'Bob', score: 920 },
  { name: 'Charlie', score: 780 },
  { name: 'Diana', score: 1050 },
  { name: 'Eve', score: 890 }
];

// 1. Get top 3 players (sorted by score, highest first)
let top3 = // YOUR CODE HERE
console.log('Top 3:', top3.map(p => p.name));

// 2. Get the last place player using at()
let lastPlace = // YOUR CODE HERE
console.log('Last place:', lastPlace.name);

// 3. Remove player at index 2 (Charlie) using toSpliced()
let withoutCharlie = // YOUR CODE HERE
console.log('Without Charlie:', withoutCharlie.map(p => p.name));

// Verify original is unchanged
console.log('Original:', players.map(p => p.name));