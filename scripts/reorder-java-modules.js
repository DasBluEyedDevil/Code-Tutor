/**
 * Reorder Java course modules to match new curriculum structure
 */
const fs = require('fs');
const path = require('path');

const coursePath = path.join(__dirname, '..', 'content', 'courses', 'java', 'course.json');

// Read the course file
const course = JSON.parse(fs.readFileSync(coursePath, 'utf8'));

// Current modules indexed by ID
const moduleMap = {};
course.modules.forEach(m => {
  moduleMap[m.id] = m;
});

console.log('Current module order:');
course.modules.forEach((m, i) => {
  console.log(`  ${i + 1}. ${m.id}: ${m.title}`);
});

// Define the new order based on execution plan
// The plan says:
// 1. module-01: Java Fundamentals
// 2. module-02: Data Types, Loops, Methods
// 3. module-git: Git & Development Workflow (NEW position)
// 4. module-03 (OOP) -> becomes module-04
// 5. module-04 (Collections) -> becomes module-05
// 6. module-streams: Streams & Functional Programming (NEW position)
// 7. module-concurrency: Concurrency & Virtual Threads (NEW position - moved up)
// 8. module-05 (Testing) -> becomes module-08
// 9. module-06: Databases & JPA
// 10. module-07: Web Fundamentals
// 11. module-08 (Spring Boot) -> becomes module-11
// 12. module-security: Security - Sessions & JWT (NEW position)
// 13. module-react: React Frontend Integration (NEW position)
// 14. module-devops: DevOps & Deployment (NEW position)
// 15. module-10 (Capstone) -> becomes module-15
// Remove module-09 (Full-Stack Development) as it's being merged elsewhere

const newOrder = [
  'module-01',           // 1. Java Fundamentals
  'module-02',           // 2. Data Types, Loops, Methods
  'module-git',          // 3. Git & Development Workflow
  'module-03',           // 4. OOP (will rename)
  'module-04',           // 5. Collections (will rename)
  'module-streams',      // 6. Streams & Functional Programming
  'module-concurrency',  // 7. Concurrency & Virtual Threads
  'module-05',           // 8. Testing and Build Tools (will rename)
  'module-06',           // 9. Databases and SQL
  'module-07',           // 10. Web Fundamentals
  'module-08',           // 11. Spring Boot (will rename)
  'module-security',     // 12. Security
  'module-react',        // 13. React Frontend
  'module-devops',       // 14. DevOps
  'module-09',           // 15. Full-Stack Development (keep for now, might merge content later)
  'module-10',           // 16. Capstone (will rename)
];

// Check what modules we have vs what we need
const availableModules = new Set(Object.keys(moduleMap));
const neededModules = new Set(newOrder);

console.log('\nMissing modules (in new order but not in file):');
newOrder.forEach(id => {
  if (!availableModules.has(id)) {
    console.log(`  - ${id}`);
  }
});

console.log('\nExtra modules (in file but not in new order):');
availableModules.forEach(id => {
  if (!neededModules.has(id)) {
    console.log(`  - ${id}: ${moduleMap[id].title}`);
  }
});

// Reorder modules based on new order (only include modules that exist)
const reorderedModules = newOrder
  .filter(id => moduleMap[id])
  .map(id => moduleMap[id]);

// Module ID remapping based on execution plan
const moduleIdRemap = {
  'module-03': 'module-04',  // OOP moves to position 4
  'module-04': 'module-05',  // Collections moves to position 5
  'module-05': 'module-08',  // Testing moves to position 8
  'module-08': 'module-11',  // Spring Boot moves to position 11
  'module-09': 'module-full-stack',  // Full-Stack gets descriptive ID
  'module-10': 'module-15',  // Capstone moves to position 15
};

// Lesson title number mapping (lesson numbers should match module numbers)
const lessonNumberRemap = {
  'module-04': '4',   // OOP lessons become 4.x
  'module-05': '5',   // Collections lessons become 5.x
  'module-08': '8',   // Testing lessons become 8.x
  'module-11': '11',  // Spring Boot lessons become 11.x
  'module-15': '15',  // Capstone lessons become 15.x
};

// Update module IDs and lesson references
reorderedModules.forEach(module => {
  const oldId = module.id;
  const newId = moduleIdRemap[oldId];

  if (newId) {
    console.log(`\nRenaming ${oldId} -> ${newId}`);
    module.id = newId;

    // Update all lessons in this module
    if (module.lessons) {
      module.lessons.forEach(lesson => {
        lesson.moduleId = newId;

        // Update lesson title numbering
        const newNum = lessonNumberRemap[newId];
        if (newNum && lesson.title) {
          // Match patterns like "Lesson 2.1:", "Lesson 3.5:", etc.
          const oldTitle = lesson.title;
          lesson.title = lesson.title.replace(
            /^(Lesson )\d+(\.\d+:)/,
            `$1${newNum}$2`
          );
          if (oldTitle !== lesson.title) {
            console.log(`  ${oldTitle} -> ${lesson.title}`);
          }
        }
      });
    }
  }
});

// Update the course with reordered modules
course.modules = reorderedModules;

console.log('\n\nNew module order:');
course.modules.forEach((m, i) => {
  console.log(`  ${i + 1}. ${m.id}: ${m.title}`);
});

// Write the updated course file
fs.writeFileSync(coursePath, JSON.stringify(course, null, 2));
console.log('\n\nCourse file updated successfully!');
