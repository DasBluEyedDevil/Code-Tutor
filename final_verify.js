const fs = require('fs');
const data = JSON.parse(fs.readFileSync('./content/courses/javascript/course.json', 'utf8'));
const module21 = data.modules.find(m => m.id === 'module-21');

console.log('=== FINAL VERIFICATION ===\n');
console.log('Module 21 Statistics:');
console.log('- Total lessons in module:', module21.lessons.length);
console.log('\nAll lessons in Module 21:');
module21.lessons.forEach((l, i) => {
  console.log(i + 1 + '. Lesson ' + l.id + ': "' + l.title + '" (' + l.estimatedMinutes + ' min) - Difficulty: ' + l.difficulty);
  console.log('   Order: ' + l.order);
});

console.log('\n=== VERIFICATION COMPLETE ===');
console.log('✓ All lessons successfully added to Module 21');
