// Create @typedef for Product here

/**
 * @param {number} id
 * @returns {???} - Use your Product type here
 */
function getProduct(id) {
  return {
    id: id,
    name: 'Widget',
    price: 9.99,
    inStock: true
  };
}

const product = getProduct(1);
console.log(`${product.name}: $${product.price}`);