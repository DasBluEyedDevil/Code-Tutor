let inventory = {
  laptop: 1200,
  mouse: 30,
  keyboard: 80,
  monitor: 350
};

let productCount = Object.keys(inventory).length;
console.log('Total products: ' + productCount);

let totalValue = 0;
for (let price of Object.values(inventory)) {
  totalValue += price;
}
console.log('Total inventory value: $' + totalValue);

console.log('Product list:');
for (let [product, price] of Object.entries(inventory)) {
  console.log(product + ': $' + price);
}