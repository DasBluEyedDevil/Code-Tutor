function safeSearch(text, searchTerm) {
  // 1. Escape the search term
  // 2. Create a case-insensitive global regex
  // 3. Find all matches and return their indices
}

// Test with special characters
const text = 'The price is $100. Is $100 too much? $100!';
const results = safeSearch(text, '$100');
console.log(results);  // Should print [13, 26, 41]