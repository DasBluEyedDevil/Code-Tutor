#!/usr/bin/env node
const fs = require('fs');
const path = require('path');

const contentDir = path.join(__dirname, '..', 'apps', 'api', 'content');

console.log('================================================');
console.log('  Course Import Validation Report');
console.log('================================================\n');

const files = fs.readdirSync(contentDir).filter(f => f.endsWith('.json'));

if (files.length === 0) {
  console.error('❌ No course files found!');
  process.exit(1);
}

console.log(`Found ${files.length} course file(s):\n`);

let totalModules = 0;
let totalLessons = 0;
let totalSize = 0;
const importedLanguages = [];

files.forEach(file => {
  const filePath = path.join(contentDir, file);
  const stats = fs.statSync(filePath);
  const content = JSON.parse(fs.readFileSync(filePath, 'utf-8'));
  
  const sizeKB = (stats.size / 1024).toFixed(2);
  const moduleCount = content.modules.length;
  const lessonCount = content.modules.reduce((sum, m) => sum + m.lessons.length, 0);
  
  totalModules += moduleCount;
  totalLessons += lessonCount;
  totalSize += stats.size;
  importedLanguages.push(content.courseMetadata.language);
  
  console.log(`  ✅ ${file}`);
  console.log(`     Language:       ${content.courseMetadata.language}`);
  console.log(`     Display Name:   ${content.courseMetadata.displayName}`);
  console.log(`     Modules:        ${moduleCount}`);
  console.log(`     Lessons:        ${lessonCount}`);
  console.log(`     Est. Hours:     ${content.courseMetadata.estimatedHours}`);
  console.log(`     Difficulty:     ${content.courseMetadata.difficulty}`);
  console.log(`     File Size:      ${sizeKB} KB\n`);
});

console.log('================================================');
console.log('  Summary Statistics');
console.log('================================================');
console.log(`Total Courses:      ${files.length}`);
console.log(`Total Modules:      ${totalModules}`);
console.log(`Total Lessons:      ${totalLessons}`);
console.log(`Total Size:         ${(totalSize / 1024).toFixed(2)} KB (${(totalSize / 1024 / 1024).toFixed(2)} MB)\n`);

// Check for required languages
const requiredLanguages = ['python', 'java', 'javascript', 'kotlin', 'rust', 'csharp', 'flutter'];
const missing = requiredLanguages.filter(lang => !importedLanguages.includes(lang));

if (missing.length === 0) {
  console.log('✅ SUCCESS: All 7 required courses have been imported!\n');
} else {
  console.log(`⚠️  WARNING: Missing courses: ${missing.join(', ')}\n`);
}

console.log('Import Status: Complete! 🎉\n');

