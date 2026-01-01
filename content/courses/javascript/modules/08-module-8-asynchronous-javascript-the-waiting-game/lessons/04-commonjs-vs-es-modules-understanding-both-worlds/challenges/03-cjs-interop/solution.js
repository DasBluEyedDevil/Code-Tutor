// Import the CJS package 'legacy-utils' in ESM
// CJS module.exports becomes the default export
import legacyUtils from 'legacy-utils';

// Extract formatDate and VERSION from the default export
const { formatDate, VERSION } = legacyUtils;

// Use the imported functions
const today = new Date();
const formatted = formatDate(today);
console.log('Formatted date:', formatted);
console.log('Version:', VERSION);

// Alternative: single line with default import + destructure
// import pkg from 'legacy-utils';
// const { formatDate, VERSION } = pkg;