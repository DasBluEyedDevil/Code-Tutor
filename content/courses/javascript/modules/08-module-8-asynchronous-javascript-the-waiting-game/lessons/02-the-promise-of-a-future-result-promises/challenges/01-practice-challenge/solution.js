function rollDice() {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      let roll = Math.floor(Math.random() * 6) + 1;
      resolve(roll);
    }, 1000);
  });
}

rollDice()
  .then(result => {
    console.log('You rolled:', result);
  });