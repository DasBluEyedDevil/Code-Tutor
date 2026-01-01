function formatPrice(price) {
  return '$' + price.toFixed(2);
}

const result = formatPrice('19.99');
console.log(result);