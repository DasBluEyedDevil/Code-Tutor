let target = 7;
let guess = 1;

while (guess !== target) {
  console.log('Guess: ' + guess + ' - Wrong!');
  guess++;
}

console.log('Guess: ' + guess + ' - Correct!');