/**
 * @typedef {Object} Product
 * @property {string} name
 * @property {number} price
 */

/**
 * @param {Product[]} products
 * @returns {number}
 */
function totalPrice(products) {
  return products.reduce((sum, p) => sum + p.price, 0);
}