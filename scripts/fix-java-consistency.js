#!/usr/bin/env node

/**
 * Fix Cross-Module Consistency Issues for Java Course
 *
 * Fixes:
 * 1. Missing module order values
 * 2. Lesson title numbering mismatches
 */

const fs = require('fs');
const path = require('path');

const courseFile = path.join(__dirname, '..', 'content', 'courses', 'java', 'course.json');
const backupFile = path.join(__dirname, '..', 'content', 'courses', 'java', 'course.json.backup');

console.log('='.repeat(70));
console.log('Java Course Consistency Fixer');
console.log('='.repeat(70));

let course;
try {
    const data = fs.readFileSync(courseFile, 'utf8');
    course = JSON.parse(data);
} catch (err) {
    console.error('Error reading course file:', err.message);
    process.exit(1);
}

// Create backup
console.log('\nCreating backup...');
fs.writeFileSync(backupFile, JSON.stringify(course, null, 2));
console.log(`   Backup saved to: ${backupFile}`);

let moduleOrderFixes = 0;
let titleFixes = 0;

// Fix module order values
console.log('\n1. Fixing module order values...');
course.modules.forEach((module, index) => {
    const expectedOrder = index + 1;
    if (module.order !== expectedOrder) {
        console.log(`   ${module.id}: order ${module.order} -> ${expectedOrder}`);
        module.order = expectedOrder;
        moduleOrderFixes++;
    }
});
console.log(`   Fixed ${moduleOrderFixes} module order values`);

// Fix lesson title numbering
console.log('\n2. Fixing lesson title numbering...');
course.modules.forEach((module, moduleIndex) => {
    const actualModulePosition = moduleIndex + 1;

    if (!module.lessons) return;

    module.lessons.forEach((lesson, lessonIndex) => {
        // Pattern: "Lesson X.Y:" where X is module position and Y is lesson order
        const titlePattern = /^Lesson\s+(\d+)\.(\d+):/;
        const match = lesson.title.match(titlePattern);

        if (match) {
            const titleModuleNum = parseInt(match[1], 10);
            const titleLessonNum = parseInt(match[2], 10);

            if (titleModuleNum !== actualModulePosition) {
                const oldTitle = lesson.title;
                const newTitle = lesson.title.replace(
                    /^Lesson\s+\d+\./,
                    `Lesson ${actualModulePosition}.`
                );
                lesson.title = newTitle;
                console.log(`   "${oldTitle}" -> "${newTitle}"`);
                titleFixes++;
            }
        }
    });
});
console.log(`   Fixed ${titleFixes} lesson titles`);

// Save the fixed course
console.log('\n3. Saving fixed course...');
fs.writeFileSync(courseFile, JSON.stringify(course, null, 2));
console.log(`   Course saved to: ${courseFile}`);

// Summary
console.log('\n' + '='.repeat(70));
console.log('FIX SUMMARY');
console.log('='.repeat(70));
console.log(`   Module order fixes: ${moduleOrderFixes}`);
console.log(`   Lesson title fixes: ${titleFixes}`);
console.log(`   Total fixes: ${moduleOrderFixes + titleFixes}`);
console.log('='.repeat(70));
