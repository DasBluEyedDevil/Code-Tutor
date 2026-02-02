function calculateTotal() {
  let price = 100;
  let tax = 0.08;
  
  if (price > 50) {
    let discount = 10;
  }
  
  let total = price - discount + (price * tax);
  return total;
}

console.log(calculateTotal());