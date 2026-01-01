const fs = require('fs');
const data = JSON.parse(fs.readFileSync('./content/courses/javascript/course.json', 'utf8'));
const module21 = data.modules.find(m => m.id === 'module-21');

console.log('Module 21 structure:');
console.log('- Has lessons property:', Array.isArray(module21.lessons));
console.log('- Number of lessons:', module21.lessons.length);

// Check where lessons 21.7+ are located
const lesson21_7 = data.modules.find(m => 
  m.lessons && m.lessons.some(l => l.id === '21.7')
);

console.log('\nLesson 21.7 location:');
console.log('- Found in module:', lesson21_7?.id);
console.log('- Total lessons in that module:', lesson21_7?.lessons?.length);
lesson21_7?.lessons?.forEach(l => {
  console.log(`  ${l.id}: ${l.title}`);
});
