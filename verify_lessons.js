const fs = require('fs');
const data = JSON.parse(fs.readFileSync('./content/courses/javascript/course.json', 'utf8'));
const module21 = data.modules.find(m => m.id === 'module-21');

console.log('Module 21 found:', !!module21);
if(module21) {
  console.log('Total lessons in Module 21:', module21.lessons.length);
  module21.lessons.slice(-4).forEach(l => {
    console.log(`  Lesson ${l.id}: ${l.title} (${l.estimatedMinutes} min)`);
  });
}
