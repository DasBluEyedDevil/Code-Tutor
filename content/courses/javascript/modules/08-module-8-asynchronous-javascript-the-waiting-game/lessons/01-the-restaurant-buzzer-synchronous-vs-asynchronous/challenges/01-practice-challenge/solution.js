let count = 3;

let intervalId = setInterval(() => {
  console.log(count);
  count--;
  
  if (count < 0) {
    console.log('Liftoff!');
    clearInterval(intervalId);
  }
}, 1000);