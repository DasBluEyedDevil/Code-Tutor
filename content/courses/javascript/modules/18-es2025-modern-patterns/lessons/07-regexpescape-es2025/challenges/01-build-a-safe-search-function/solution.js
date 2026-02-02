function safeSearch(text, searchTerm) {
  const escaped = RegExp.escape(searchTerm);
  const regex = new RegExp(escaped, 'gi');
  const positions = [];
  let match;
  while ((match = regex.exec(text)) !== null) {
    positions.push(match.index);
  }
  return positions;
}

const text = 'The price is $100. Is $100 too much? $100!';
const results = safeSearch(text, '$100');
console.log(results);