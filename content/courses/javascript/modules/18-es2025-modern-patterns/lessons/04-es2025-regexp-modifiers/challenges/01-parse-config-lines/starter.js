// Create regex with inline modifiers
const configRegex = // your regex here

const match = configRegex.exec('port=MyValue');
console.log(match[1]);  // Should print 'MyValue' (exact case)