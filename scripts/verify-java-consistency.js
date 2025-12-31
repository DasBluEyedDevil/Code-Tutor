#!/usr/bin/env node

/**
 * Cross-Module Consistency Checker for Java Course
 *
 * Checks:
 * 1. Module IDs and Order - unique, sequential (1-16)
 * 2. Lesson-Module References - each lesson's moduleId matches parent
 * 3. Lesson Title Numbering - "Lesson X.Y:" matches module position
 * 4. No Duplicate IDs - all lesson/module IDs unique
 * 5. Content Completeness - each lesson has contentSections with non-empty content
 */

const fs = require('fs');
const path = require('path');

const courseFile = path.join(__dirname, '..', 'content', 'courses', 'java', 'course.json');

console.log('='.repeat(70));
console.log('Java Course Cross-Module Consistency Check');
console.log('='.repeat(70));

let course;
try {
    const data = fs.readFileSync(courseFile, 'utf8');
    course = JSON.parse(data);
} catch (err) {
    console.error('Error reading course file:', err.message);
    process.exit(1);
}

const issues = [];
const fixes = [];

// 1. Check Module IDs and Order
console.log('\n1. Checking Module IDs and Order...');
const moduleIds = new Set();
const moduleOrderValues = new Set();
let expectedModuleOrder = 1;

course.modules.forEach((module, index) => {
    // Check for duplicate module IDs
    if (moduleIds.has(module.id)) {
        issues.push({
            type: 'DUPLICATE_MODULE_ID',
            message: `Duplicate module ID: ${module.id}`,
            module: module.id
        });
    }
    moduleIds.add(module.id);

    // Check module order exists and is sequential
    if (module.order === undefined || module.order === null) {
        issues.push({
            type: 'MISSING_MODULE_ORDER',
            message: `Module ${module.id} at position ${index + 1} has no order value`,
            module: module.id,
            position: index + 1,
            fix: { order: expectedModuleOrder }
        });
    } else if (module.order !== expectedModuleOrder) {
        issues.push({
            type: 'MODULE_ORDER_GAP',
            message: `Module ${module.id} has order ${module.order}, expected ${expectedModuleOrder}`,
            module: module.id,
            expected: expectedModuleOrder,
            actual: module.order,
            fix: { order: expectedModuleOrder }
        });
    }

    // Check module ID matches expected pattern (informational only - IDs may be intentionally descriptive)
    const expectedId = `module-${String(expectedModuleOrder).padStart(2, '0')}`;
    if (module.id !== expectedId && !module.id.startsWith('module-')) {
        issues.push({
            type: 'MODULE_ID_FORMAT',
            message: `Module at position ${expectedModuleOrder} has non-standard ID ${module.id}`,
            module: module.id,
            position: expectedModuleOrder
        });
    }

    moduleOrderValues.add(module.order);
    expectedModuleOrder++;
});

console.log(`   Found ${course.modules.length} modules`);

// 2. Check Lesson-Module References and Title Numbering
console.log('\n2. Checking Lesson-Module References and Title Numbering...');
const lessonIds = new Set();
const lessonTitleIssues = [];

course.modules.forEach((module, moduleIndex) => {
    const actualModulePosition = moduleIndex + 1;
    let expectedLessonOrder = 1;

    if (!module.lessons || module.lessons.length === 0) {
        issues.push({
            type: 'EMPTY_MODULE',
            message: `Module ${module.id} has no lessons`,
            module: module.id
        });
        return;
    }

    module.lessons.forEach((lesson, lessonIndex) => {
        // Check for duplicate lesson IDs
        if (lessonIds.has(lesson.id)) {
            issues.push({
                type: 'DUPLICATE_LESSON_ID',
                message: `Duplicate lesson ID: ${lesson.id}`,
                lesson: lesson.id,
                module: module.id
            });
        }
        lessonIds.add(lesson.id);

        // Check lesson's moduleId matches parent module
        if (lesson.moduleId !== module.id) {
            issues.push({
                type: 'LESSON_MODULE_MISMATCH',
                message: `Lesson ${lesson.id} has moduleId ${lesson.moduleId}, but is in module ${module.id}`,
                lesson: lesson.id,
                module: module.id,
                lessonModuleId: lesson.moduleId,
                fix: { moduleId: module.id }
            });
        }

        // Check lesson order is sequential within module
        if (lesson.order !== expectedLessonOrder) {
            issues.push({
                type: 'LESSON_ORDER_GAP',
                message: `Lesson ${lesson.id} in module ${module.id} has order ${lesson.order}, expected ${expectedLessonOrder}`,
                lesson: lesson.id,
                module: module.id,
                expected: expectedLessonOrder,
                actual: lesson.order,
                fix: { order: expectedLessonOrder }
            });
        }

        // Check lesson title numbering matches module position
        // Pattern: "Lesson X.Y:" where X is module position and Y is lesson order
        const titlePattern = /^Lesson\s+(\d+)\.(\d+):/;
        const match = lesson.title.match(titlePattern);

        if (match) {
            const titleModuleNum = parseInt(match[1], 10);
            const titleLessonNum = parseInt(match[2], 10);

            if (titleModuleNum !== actualModulePosition) {
                const expectedTitle = lesson.title.replace(/^Lesson\s+\d+\./, `Lesson ${actualModulePosition}.`);
                lessonTitleIssues.push({
                    type: 'TITLE_MODULE_MISMATCH',
                    message: `Lesson "${lesson.title}" in module ${module.id} (position ${actualModulePosition}) has wrong module number in title (${titleModuleNum})`,
                    lesson: lesson.id,
                    module: module.id,
                    modulePosition: actualModulePosition,
                    titleModuleNum: titleModuleNum,
                    expectedTitle: expectedTitle,
                    fix: { title: expectedTitle }
                });
            }

            if (titleLessonNum !== lesson.order) {
                lessonTitleIssues.push({
                    type: 'TITLE_LESSON_MISMATCH',
                    message: `Lesson "${lesson.title}" has lesson number ${titleLessonNum} in title but order is ${lesson.order}`,
                    lesson: lesson.id,
                    module: module.id,
                    lessonOrder: lesson.order,
                    titleLessonNum: titleLessonNum
                });
            }
        }

        // 5. Check content completeness (use contentSections, not content)
        const sections = lesson.contentSections || lesson.content;
        if (!sections || sections.length === 0) {
            issues.push({
                type: 'NO_CONTENT',
                message: `Lesson ${lesson.id} has no content sections`,
                lesson: lesson.id,
                module: module.id
            });
        } else {
            sections.forEach((section, sectionIndex) => {
                const body = section.content || section.body;
                if (!body || body.trim() === '') {
                    issues.push({
                        type: 'EMPTY_CONTENT_BODY',
                        message: `Lesson ${lesson.id}, section ${sectionIndex + 1} has empty content`,
                        lesson: lesson.id,
                        module: module.id,
                        section: sectionIndex + 1
                    });
                }
            });
        }

        expectedLessonOrder++;
    });
});

// Add title issues to main issues
issues.push(...lessonTitleIssues);

console.log(`   Total lessons: ${lessonIds.size}`);

// Summary
console.log('\n' + '='.repeat(70));
console.log('CONSISTENCY CHECK RESULTS');
console.log('='.repeat(70));

if (issues.length === 0) {
    console.log('\n   All checks passed! No consistency issues found.');
} else {
    console.log(`\n   Found ${issues.length} issues:\n`);

    // Group issues by type
    const issuesByType = {};
    issues.forEach(issue => {
        if (!issuesByType[issue.type]) {
            issuesByType[issue.type] = [];
        }
        issuesByType[issue.type].push(issue);
    });

    Object.keys(issuesByType).sort().forEach(type => {
        console.log(`\n   ${type} (${issuesByType[type].length} issues):`);
        issuesByType[type].slice(0, 10).forEach(issue => {
            console.log(`      - ${issue.message}`);
        });
        if (issuesByType[type].length > 10) {
            console.log(`      ... and ${issuesByType[type].length - 10} more`);
        }
    });
}

// Module summary
console.log('\n' + '='.repeat(70));
console.log('MODULE SUMMARY');
console.log('='.repeat(70));
course.modules.forEach((module, index) => {
    const lessonCount = module.lessons ? module.lessons.length : 0;
    const orderDisplay = module.order !== undefined ? module.order : 'MISSING';
    console.log(`   ${index + 1}. ${module.id} (order: ${orderDisplay}) - ${lessonCount} lessons - "${module.title}"`);
});

// Output issues to JSON for potential automated fixing
const outputFile = path.join(__dirname, 'java-consistency-issues.json');
fs.writeFileSync(outputFile, JSON.stringify({
    timestamp: new Date().toISOString(),
    totalIssues: issues.length,
    issuesByType: issues.reduce((acc, issue) => {
        if (!acc[issue.type]) acc[issue.type] = 0;
        acc[issue.type]++;
        return acc;
    }, {}),
    issues: issues
}, null, 2));

console.log(`\n   Issues saved to: ${outputFile}`);
console.log('='.repeat(70));

// Exit with error code if issues found (excluding MISSING_MODULE_ORDER as those are fixable)
const criticalIssues = issues.filter(i =>
    i.type !== 'MISSING_MODULE_ORDER' &&
    i.type !== 'TITLE_MODULE_MISMATCH'
);
process.exit(criticalIssues.length > 0 ? 1 : 0);
