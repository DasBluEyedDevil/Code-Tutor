// scripts/normalize-examples.js
// Migrates JavaScript EXAMPLE sections from content-only to code+content pattern

const fs = require('fs');
const path = require('path');

const COURSE_PATH = path.join(__dirname, '../content/courses/javascript/course.json');
const BACKUP_PATH = path.join(__dirname, '../content/courses/javascript/course.backup.json');

function normalizeExamples() {
  // Read course file
  const courseData = JSON.parse(fs.readFileSync(COURSE_PATH, 'utf8'));

  // Create backup
  fs.writeFileSync(BACKUP_PATH, JSON.stringify(courseData, null, 2));
  console.log(`Backup created: ${BACKUP_PATH}`);

  let migratedCount = 0;
  let skippedCount = 0;

  // Process all modules -> lessons -> contentSections
  for (const module of courseData.modules || []) {
    for (const lesson of module.lessons || []) {
      for (const section of lesson.contentSections || []) {
        if (section.type === 'EXAMPLE' && !section.code && section.content) {
          // Check if content looks like code (starts with //, let, const, function, etc.)
          const content = section.content.trim();
          const codePatterns = [
            /^\/\//,           // JavaScript comment
            /^\/\*/,           // Block comment
            /^(let|const|var)\s/,  // Variable declaration
            /^function\s/,     // Function declaration
            /^class\s/,        // Class declaration
            /^import\s/,       // Import statement
            /^export\s/,       // Export statement
            /^async\s/,        // Async function
            /^console\./,      // Console statement
            /^if\s*\(/,        // If statement
            /^for\s*\(/,       // For loop
            /^while\s*\(/,     // While loop
            /^\{/,             // Object literal
            /^\[/,             // Array literal
          ];

          const looksLikeCode = codePatterns.some(pattern => pattern.test(content));

          if (looksLikeCode) {
            // Migrate: move content to code, add brief explanation
            section.code = section.content;
            section.content = `See the code example above demonstrating ${section.title || 'this concept'}.`;
            migratedCount++;
            console.log(`Migrated: ${lesson.title} -> ${section.title}`);
          } else {
            skippedCount++;
            console.log(`Skipped (not code): ${lesson.title} -> ${section.title}`);
          }
        }
      }
    }
  }

  // Write updated course file
  fs.writeFileSync(COURSE_PATH, JSON.stringify(courseData, null, 2));

  console.log(`\nMigration complete:`);
  console.log(`  Migrated: ${migratedCount} sections`);
  console.log(`  Skipped: ${skippedCount} sections`);
  console.log(`  Backup at: ${BACKUP_PATH}`);
}

normalizeExamples();
