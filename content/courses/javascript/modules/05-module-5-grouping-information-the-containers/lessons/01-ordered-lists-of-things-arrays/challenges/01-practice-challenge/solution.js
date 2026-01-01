let scores = [85, 92, 78, 95, 88];

console.log('First score: ' + scores[0]);
console.log('Last score: ' + scores[scores.length - 1]);

let total = 0;
for (let i = 0; i < scores.length; i++) {
  total += scores[i];
}

let average = total / scores.length;
console.log('Average: ' + average);