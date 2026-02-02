function calculateTotal() {
  let price = 100;
  let tax = 0.08;
  let discount = 0;  // Declare in function scope
  
  if (price > 50) {
    discount = 10;  // Assign (no let)
  }
  
  let total = price - discount + (price * tax);
  return total;
}

console.log(calculateTotal());  // 98