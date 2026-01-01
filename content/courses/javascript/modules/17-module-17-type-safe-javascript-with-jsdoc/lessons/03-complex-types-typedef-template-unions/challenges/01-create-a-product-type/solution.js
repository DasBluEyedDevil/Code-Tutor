/**
 * @typedef {Object} Product
 * @property {number} id
 * @property {string} name
 * @property {number} price
 * @property {boolean} inStock
 */

/**
 * @param {number} id
 * @returns {Product}
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