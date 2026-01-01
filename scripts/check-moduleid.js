const fs = require('fs');
const c = JSON.parse(fs.readFileSync('content/courses/kotlin/course.json', 'utf8'));

let noModuleId = [];
c.modules.forEach(m => {
  m.lessons.forEach(l => {
    if (!l.moduleId || l.moduleId !== m.id) {
      noModuleId.push(`${l.id} in ${m.id} has moduleId=${l.moduleId}`);
    }
  });
});

console.log('Mismatched moduleIds:', noModuleId.length ? noModuleId.slice(0,20).join('\n') : 'None');
if (noModuleId.length > 20) console.log('...and', noModuleId.length - 20, 'more');
