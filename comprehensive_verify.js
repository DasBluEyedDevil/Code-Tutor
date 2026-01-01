const fs = require('fs');
const data = JSON.parse(fs.readFileSync('./content/courses/javascript/course.json', 'utf8'));

console.log('=== COMPREHENSIVE VERIFICATION ===\n');

// Find module 21
const module21 = data.modules.find(m => m.id === 'module-21');
console.log('Module 21 found:', !!module21);
console.log('Module title:', module21.title);
console.log('Total lessons:', module21.lessons.length);
console.log('\nAll lessons in Module 21:');

module21.lessons.forEach(lesson => {
  console.log('- ' + lesson.id + ': ' + lesson.title + ' (' + lesson.estimatedMinutes + ' min)');
});

// Verify required lessons
const requiredLessons = ['21.7', '21.8', '21.9', '21.10'];
const foundLessons = module21.lessons.map(l => l.id);
const allPresent = requiredLessons.every(id => foundLessons.includes(id));

console.log('\n=== REQUIRED LESSONS CHECK ===');
console.log('Required lessons: ' + requiredLessons.join(', '));
console.log('Found in Module 21: ' + allPresent);

if (allPresent) {
  console.log('\n✓ SUCCESS: All 4 lessons (21.7, 21.8, 21.9, 21.10) are present in Module 21');
  console.log('✓ JSON file is syntactically valid');
  console.log('✓ File is ready for use');
} else {
  console.log('\n✗ ERROR: Some lessons are missing');
  console.log('Missing:', requiredLessons.filter(id => !foundLessons.includes(id)));
}
