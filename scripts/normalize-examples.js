// scripts/normalize-examples.js
// Migrates JavaScript EXAMPLE sections from content-only to code+content pattern

const fs = require('fs');
const path = require('path');

const COURSE_PATH = path.join(__dirname, '../content/courses/javascript/course.json');
const BACKUP_PATH = path.join(__dirname, '../content/courses/javascript/course.backup.json');

function normalizeExamples() {
  let courseData;

  try {
    courseData = JSON.parse(fs.readFileSync(COURSE_PATH, 'utf8'));
  } catch (error) {
    console.error(`Error reading course file: ${error.message}`);
    process.exit(1);
  }

  try {
    fs.writeFileSync(BACKUP_PATH, JSON.stringify(courseData, null, 2));
    console.log(`Backup created: ${BACKUP_PATH}`);
  } catch (error) {
    console.error(`Error creating backup: ${error.message}`);
    process.exit(1);
  }

  let migratedCount = 0;
  let skippedCount = 0;

  // Process all modules -> lessons -> contentSections
  for (const module of courseData.modules || []) {
    for (const lesson of module.lessons || []) {
      for (const section of lesson.contentSections || []) {
        if (section.type === 'EXAMPLE' && !section.code && section.content) {
          // Check if content looks like code - require unambiguous indicators
          const content = section.content.trim();

          // Strong code indicators (unambiguous syntax)
          const strongPatterns = [
            /^\/\//,                    // JavaScript comment
            /^\/\*/,                    // Block comment
            /^(let|const|var)\s+\w+\s*=/, // Variable declaration with assignment
            /^function\s+\w+\s*\(/,     // Named function declaration
            /^class\s+\w+/,             // Class declaration
            /^import\s+.*from\s/,       // Import with from
            /^export\s+(default\s+)?/,  // Export statement
            /^async\s+function/,        // Async function
            /^console\.(log|error|warn)\(/, // Console with method call
          ];

          // Check for strong pattern match
          const hasStrongPattern = strongPatterns.some(pattern => pattern.test(content));

          // Secondary check: contains semicolons or multiple code-like lines
          const hasSemicolons = content.includes(';');
          const hasMultipleLines = content.split('\n').length > 1;
          const hasCodeStructure = hasSemicolons && hasMultipleLines;

          const looksLikeCode = hasStrongPattern || hasCodeStructure;

          if (looksLikeCode) {
            // Migrate: move content to code, add brief explanation
            section.code = section.content;
            section.content = `See the code example above demonstrating ${section.title || 'this concept'}.`;

            // Validate migration
            if (!section.code || section.code.trim().length === 0) {
              console.error(`Validation failed: empty code for ${lesson.title} -> ${section.title}`);
              continue;
            }

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

  try {
    fs.writeFileSync(COURSE_PATH, JSON.stringify(courseData, null, 2));
  } catch (error) {
    console.error(`Error writing course file: ${error.message}`);
    process.exit(1);
  }

  console.log(`\nMigration complete:`);
  console.log(`  Migrated: ${migratedCount} sections`);
  console.log(`  Skipped: ${skippedCount} sections`);
  console.log(`  Backup at: ${BACKUP_PATH}`);
}

normalizeExamples();
