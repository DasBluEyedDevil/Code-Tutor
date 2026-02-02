function rollDice() {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      // YOUR CODE: Generate random number 1-6 and resolve with it
    }, 1000);
  });
}

// Test it
rollDice()
  .then(result => {
    console.log('You rolled:', result);
  });