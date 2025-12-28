/**
 * Course Content Normalization Script
 *
 * Standardizes all course.json files to a consistent schema:
 * - Removes courseMetadata wrapper (flattens to root)
 * - Converts content.body markdown to contentSections format
 * - Renames exercises to challenges
 * - Standardizes module/lesson ID formats
 */

const fs = require('fs');
const path = require('path');

const COURSES_DIR = path.join(__dirname, 'courses');

// Target schema field mappings
const STANDARD_FIELDS = {
    id: true,
    language: true,
    title: true,
    description: true,
    difficulty: true,
    estimatedHours: true,
    prerequisites: true,
    modules: true
};

/**
 * Convert markdown body to contentSections array
 */
function markdownToContentSections(markdownBody) {
    if (!markdownBody || typeof markdownBody !== 'string') {
        return [];
    }

    const sections = [];
    const lines = markdownBody.split('\n');
    let currentSection = null;
    let currentContent = [];

    for (const line of lines) {
        // Check for headers that indicate new sections
        if (line.startsWith('# ')) {
            // Main title - create THEORY section
            if (currentSection) {
                currentSection.content = currentContent.join('\n').trim();
                if (currentSection.content) sections.push(currentSection);
            }
            currentSection = { type: 'THEORY', title: line.substring(2).trim(), content: '' };
            currentContent = [];
        } else if (line.startsWith('## ')) {
            // H2 - new THEORY section
            if (currentSection) {
                currentSection.content = currentContent.join('\n').trim();
                if (currentSection.content) sections.push(currentSection);
            }
            const title = line.substring(3).trim();
            // Check for special section types
            if (title.includes('💡') || title.toLowerCase().includes('key insight')) {
                currentSection = { type: 'KEY_POINT', title: title.replace(/[💡🎯]/g, '').trim(), content: '' };
            } else if (title.includes('🎯')) {
                currentSection = { type: 'KEY_POINT', title: title.replace(/[💡🎯]/g, '').trim(), content: '' };
            } else {
                currentSection = { type: 'THEORY', title: title, content: '' };
            }
            currentContent = [];
        } else if (line.startsWith('```')) {
            // Code block - check if we should create an EXAMPLE section
            currentContent.push(line);
        } else {
            currentContent.push(line);
        }
    }

    // Don't forget the last section
    if (currentSection) {
        currentSection.content = currentContent.join('\n').trim();
        if (currentSection.content) sections.push(currentSection);
    }

    // If no sections were created, create one THEORY section with all content
    if (sections.length === 0 && markdownBody.trim()) {
        sections.push({
            type: 'THEORY',
            title: 'Content',
            content: markdownBody.trim()
        });
    }

    return sections;
}

/**
 * Normalize a lesson object
 */
function normalizeLesson(lesson, moduleId, lessonIndex) {
    const normalized = {
        id: lesson.id || `${moduleId}-lesson-${String(lessonIndex + 1).padStart(2, '0')}`,
        title: lesson.title || `Lesson ${lessonIndex + 1}`,
        moduleId: moduleId,
        order: lesson.order || lessonIndex + 1,
        estimatedMinutes: lesson.estimatedMinutes || 15,
        difficulty: lesson.difficulty || 'beginner',
        contentSections: [],
        challenges: []
    };

    // Convert content.body to contentSections if needed
    if (lesson.content && lesson.content.body) {
        normalized.contentSections = markdownToContentSections(lesson.content.body);
    } else if (lesson.contentSections) {
        normalized.contentSections = lesson.contentSections;
    }

    // Rename exercises to challenges
    if (lesson.exercises && lesson.exercises.length > 0) {
        normalized.challenges = lesson.exercises.map((ex, idx) => normalizeChallenge(ex, normalized.id, idx));
    } else if (lesson.challenges && lesson.challenges.length > 0) {
        normalized.challenges = lesson.challenges.map((ch, idx) => normalizeChallenge(ch, normalized.id, idx));
    }

    return normalized;
}

/**
 * Normalize a challenge/exercise object
 */
function normalizeChallenge(challenge, lessonId, challengeIndex) {
    const normalized = {
        type: challenge.type === 'coding' ? 'FREE_CODING' : (challenge.type || 'FREE_CODING'),
        id: challenge.id || `${lessonId}-challenge-${challengeIndex + 1}`,
        title: challenge.title || `Challenge ${challengeIndex + 1}`,
        description: challenge.description || challenge.instructions || '',
        instructions: challenge.instructions || challenge.description || '',
        starterCode: challenge.starterCode || '// Your code here\n',
        solution: challenge.solution || '',
        language: challenge.language || 'java',
        testCases: normalizeTestCases(challenge.testCases || []),
        hints: normalizeHints(challenge.hints || []),
        commonMistakes: challenge.commonMistakes || [],
        difficulty: challenge.difficulty || 'beginner'
    };

    return normalized;
}

/**
 * Normalize test cases
 */
function normalizeTestCases(testCases) {
    return testCases.map((tc, idx) => ({
        id: tc.id || `test-${idx + 1}`,
        description: tc.description || `Test case ${idx + 1}`,
        expectedOutput: tc.expectedOutput || '',
        isVisible: tc.isVisible !== undefined ? tc.isVisible : true
    }));
}

/**
 * Normalize hints
 */
function normalizeHints(hints) {
    if (!hints || hints.length === 0) {
        return [];
    }
    return hints.map((hint, idx) => ({
        level: hint.level || idx + 1,
        text: hint.text || hint || ''
    }));
}

/**
 * Normalize a module object
 */
function normalizeModule(module, moduleIndex) {
    // Standardize module ID format
    let moduleId = module.id;
    if (moduleId && moduleId.startsWith('epoch-')) {
        // Convert epoch-X to module-XX format
        const epochNum = moduleId.replace('epoch-', '');
        moduleId = `module-${String(parseInt(epochNum) + 1).padStart(2, '0')}`;
    } else if (!moduleId || !moduleId.startsWith('module-')) {
        moduleId = `module-${String(moduleIndex + 1).padStart(2, '0')}`;
    }

    const normalized = {
        id: moduleId,
        title: module.title || `Module ${moduleIndex + 1}`,
        description: module.description || '',
        difficulty: module.difficulty || 'beginner',
        estimatedHours: module.estimatedHours || 2,
        lessons: []
    };

    // Normalize lessons
    if (module.lessons && module.lessons.length > 0) {
        normalized.lessons = module.lessons.map((lesson, idx) =>
            normalizeLesson(lesson, moduleId, idx)
        );
    }

    return normalized;
}

/**
 * Normalize an entire course
 */
function normalizeCourse(courseData, courseName) {
    let source = courseData;

    // Handle courseMetadata wrapper
    if (courseData.courseMetadata) {
        source = {
            ...courseData.courseMetadata,
            modules: courseData.modules
        };
    }

    const normalized = {
        id: source.id || courseName,
        language: source.language || courseName,
        title: source.title || source.displayName || `${courseName} Programming`,
        description: source.description || '',
        difficulty: normalizeDifficulty(source.difficulty),
        estimatedHours: Math.round(source.estimatedHours || 20),
        prerequisites: source.prerequisites || [],
        modules: []
    };

    // Normalize modules
    if (source.modules && source.modules.length > 0) {
        normalized.modules = source.modules.map((module, idx) =>
            normalizeModule(module, idx)
        );
    }

    return normalized;
}

/**
 * Normalize difficulty string
 */
function normalizeDifficulty(difficulty) {
    if (!difficulty) return 'beginner';
    const lower = difficulty.toLowerCase();
    if (lower.includes('advanced')) return 'advanced';
    if (lower.includes('intermediate')) return 'intermediate';
    return 'beginner';
}

/**
 * Process a single course file
 */
function processCourse(courseName) {
    const coursePath = path.join(COURSES_DIR, courseName, 'course.json');

    if (!fs.existsSync(coursePath)) {
        console.log(`⚠️  Skipping ${courseName}: course.json not found`);
        return null;
    }

    console.log(`📖 Reading ${courseName}...`);
    const rawData = fs.readFileSync(coursePath, 'utf8');
    const courseData = JSON.parse(rawData);

    console.log(`🔧 Normalizing ${courseName}...`);
    const normalized = normalizeCourse(courseData, courseName);

    // Create backup
    const backupPath = path.join(COURSES_DIR, courseName, 'course.backup.json');
    fs.writeFileSync(backupPath, rawData, 'utf8');
    console.log(`💾 Backup saved to ${backupPath}`);

    // Write normalized data
    fs.writeFileSync(coursePath, JSON.stringify(normalized, null, 2), 'utf8');
    console.log(`✅ ${courseName} normalized successfully!`);

    return {
        course: courseName,
        modules: normalized.modules.length,
        lessons: normalized.modules.reduce((sum, m) => sum + m.lessons.length, 0),
        challenges: normalized.modules.reduce((sum, m) =>
            sum + m.lessons.reduce((lsum, l) => lsum + l.challenges.length, 0), 0)
    };
}

/**
 * Main entry point
 */
function main() {
    console.log('🚀 Course Content Normalization Script');
    console.log('=====================================\n');

    // Get list of courses
    const courses = fs.readdirSync(COURSES_DIR).filter(dir => {
        const stat = fs.statSync(path.join(COURSES_DIR, dir));
        return stat.isDirectory();
    });

    console.log(`Found ${courses.length} courses: ${courses.join(', ')}\n`);

    const results = [];
    for (const course of courses) {
        try {
            const result = processCourse(course);
            if (result) results.push(result);
        } catch (error) {
            console.error(`❌ Error processing ${course}: ${error.message}`);
        }
        console.log('');
    }

    // Summary
    console.log('\n📊 NORMALIZATION SUMMARY');
    console.log('========================');
    console.log('Course       | Modules | Lessons | Challenges');
    console.log('-------------|---------|---------|------------');
    for (const r of results) {
        console.log(`${r.course.padEnd(12)} | ${String(r.modules).padStart(7)} | ${String(r.lessons).padStart(7)} | ${String(r.challenges).padStart(10)}`);
    }
    console.log('\n✨ All courses normalized!');
}

main();
