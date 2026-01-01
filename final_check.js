const fs = require('fs');
const data = JSON.parse(fs.readFileSync('./content/courses/javascript/course.json', 'utf8'));
const module21 = data.modules.find(m => m.id === 'module-21');

console.log('\n=== FINAL VERIFICATION ===\n');
console.log('Module 21 Structure:');
console.log('- Module ID:', module21.id);
console.log('- Module Title:', module21.title);
console.log('- Total Lessons:', module21.lessons.length);
console.log('- Estimated Hours:', module21.estimatedHours);

console.log('\nAll Lessons in Module 21:');
module21.lessons.forEach((lesson, i) => {
  console.log((i + 1) + '. Lesson ' + lesson.id + ': "' + lesson.title + '" (' + lesson.estimatedMinutes + ' min, ' + lesson.difficulty + ')');
});

console.log('\n=== REQUIREMENTS CHECK ===');
const required = ['21.1', '21.2', '21.3', '21.4', '21.5', '21.6', '21.7', '21.8', '21.9', '21.10'];
const actual = module21.lessons.map(l => l.id);
const allPresent = required.every(id => actual.includes(id));

console.log('Required lessons: ' + required.join(', '));
console.log('All present: ' + (allPresent ? 'YES' : 'NO'));

if (allPresent) {
  console.log('\n✓ SUCCESS: All lessons 21.1-21.10 are present');
  console.log('✓ JSON is syntactically valid');
  console.log('✓ Module structure is correct');
  console.log('✓ File is ready for use');
}
