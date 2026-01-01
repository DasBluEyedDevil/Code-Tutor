/**
 * @param {string[]} items - Array of strings to process
 * @returns {string[]} Uppercased strings
 */
function processItems(items) {
  return items.map(item => item.toUpperCase());
}

// This should show a type error in your IDE
processItems('hello');