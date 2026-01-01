// Add JSDoc here
function processItems(items) {
  return items.map(item => item.toUpperCase());
}

// This should show a type error in your IDE
processItems('hello');