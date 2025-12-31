// scripts/enhance-lesson.js
const fs = require('fs');

/**
 * Adds new content sections to an existing lesson in course.json
 * @param {string} coursePath - Path to the course.json file
 * @param {string} moduleId - ID of the module (e.g., 'module-01')
 * @param {string} lessonId - ID of the lesson (e.g., 'lesson-01-01')
 * @param {Array} newSections - Array of section objects to add
 */
function enhanceLesson(coursePath, moduleId, lessonId, newSections) {
  // Validate inputs
  if (!fs.existsSync(coursePath)) {
    throw new Error(`Course file not found: ${coursePath}`);
  }
  if (!Array.isArray(newSections)) {
    throw new Error('newSections must be an array');
  }
  if (newSections.some(s => !s.type)) {
    throw new Error('Each section must have a type property');
  }

  // Read and parse course.json
  const course = JSON.parse(fs.readFileSync(coursePath, 'utf8'));

  // Find the module
  const module = course.modules.find(m => m.id === moduleId);
  if (!module) {
    throw new Error(`Module ${moduleId} not found`);
  }

  // Find the lesson
  const lesson = module.lessons.find(l => l.id === lessonId);
  if (!lesson) {
    throw new Error(`Lesson ${lessonId} not found in module ${moduleId}`);
  }

  // Add new sections (skip if type already exists)
  let addedCount = 0;
  newSections.forEach(section => {
    const exists = lesson.contentSections.find(s => s.type === section.type && s.title === section.title);
    if (!exists) {
      lesson.contentSections.push(section);
      addedCount++;
    }
  });

  // Write back to file
  fs.writeFileSync(coursePath, JSON.stringify(course, null, 2));
  console.log(`Enhanced ${lessonId}: added ${addedCount} section(s)`);

  return addedCount;
}

/**
 * Updates an existing content section in a lesson
 * @param {string} coursePath - Path to the course.json file
 * @param {string} moduleId - Module ID
 * @param {string} lessonId - Lesson ID
 * @param {string} sectionType - Type of section to update (e.g., 'EXAMPLE')
 * @param {object} updates - Object with fields to update
 */
function updateSection(coursePath, moduleId, lessonId, sectionType, updates) {
  // Validate file exists
  if (!fs.existsSync(coursePath)) {
    throw new Error(`Course file not found: ${coursePath}`);
  }

  const course = JSON.parse(fs.readFileSync(coursePath, 'utf8'));

  const module = course.modules.find(m => m.id === moduleId);
  if (!module) throw new Error(`Module ${moduleId} not found`);

  const lesson = module.lessons.find(l => l.id === lessonId);
  if (!lesson) throw new Error(`Lesson ${lessonId} not found`);

  const section = lesson.contentSections.find(s => s.type === sectionType);
  if (!section) throw new Error(`Section ${sectionType} not found in ${lessonId}`);

  Object.assign(section, updates);
  fs.writeFileSync(coursePath, JSON.stringify(course, null, 2));
  console.log(`Updated ${sectionType} section in ${lessonId}`);
}

/**
 * Lists all lessons in a module with their section types
 * @param {string} coursePath - Path to course.json
 * @param {string} moduleId - Module ID to list
 */
function listLessons(coursePath, moduleId) {
  // Validate file exists
  if (!fs.existsSync(coursePath)) {
    throw new Error(`Course file not found: ${coursePath}`);
  }

  const course = JSON.parse(fs.readFileSync(coursePath, 'utf8'));

  const module = course.modules.find(m => m.id === moduleId);
  if (!module) throw new Error(`Module ${moduleId} not found`);

  console.log(`\n${module.title} (${module.id}):`);
  module.lessons.forEach(lesson => {
    console.log(`  ${lesson.id}: ${lesson.title}`);
    lesson.contentSections.forEach(s => {
      console.log(`    - ${s.type}: ${s.title || '(no title)'}`);
    });
  });
}

// Export for use as module
module.exports = { enhanceLesson, updateSection, listLessons };

// CLI usage if run directly
if (require.main === module) {
  try {
    const args = process.argv.slice(2);

    if (args[0] === 'list' && args[1] && args[2]) {
      listLessons(args[1], args[2]);
    } else {
      console.log('Usage:');
      console.log('  node enhance-lesson.js list <course.json> <module-id>');
      console.log('');
      console.log('Or require as module:');
      console.log('  const { enhanceLesson } = require("./enhance-lesson");');
    }
  } catch (error) {
    console.error('Error:', error.message);
    process.exit(1);
  }
}
