/**
 * Phase 2: Content Gap Filler
 *
 * - Audits all courses for empty/missing fields
 * - Fills in starterCode, solution, hints, commonMistakes
 * - Adds challenges to Rust lessons
 */

const fs = require('fs');
const path = require('path');

const COURSES_DIR = path.join(__dirname, 'courses');

// Template generators for different languages
const TEMPLATES = {
    python: {
        starterCode: (title) => `# ${title}\n# Write your code below\n\n`,
        helloWorld: `print("Hello, World!")`,
        comment: '#'
    },
    javascript: {
        starterCode: (title) => `// ${title}\n// Write your code below\n\n`,
        helloWorld: `console.log("Hello, World!");`,
        comment: '//'
    },
    java: {
        starterCode: (title) => `// ${title}\n// Write your code below\n\npublic class Solution {\n    public static void main(String[] args) {\n        \n    }\n}`,
        helloWorld: `System.out.println("Hello, World!");`,
        comment: '//'
    },
    csharp: {
        starterCode: (title) => `// ${title}\n// Write your code below\n\nusing System;\n\nclass Program {\n    static void Main() {\n        \n    }\n}`,
        helloWorld: `Console.WriteLine("Hello, World!");`,
        comment: '//'
    },
    rust: {
        starterCode: (title) => `// ${title}\n// Write your code below\n\nfn main() {\n    \n}`,
        helloWorld: `println!("Hello, World!");`,
        comment: '//'
    },
    kotlin: {
        starterCode: (title) => `// ${title}\n// Write your code below\n\nfun main() {\n    \n}`,
        helloWorld: `println("Hello, World!")`,
        comment: '//'
    },
    dart: {
        starterCode: (title) => `// ${title}\n// Write your code below\n\nvoid main() {\n    \n}`,
        helloWorld: `print("Hello, World!");`,
        comment: '//'
    }
};

// Common mistakes templates by language
const COMMON_MISTAKES = {
    python: [
        { mistake: "Forgetting the colon after if/for/while", consequence: "SyntaxError", correction: "Add : at the end of the line" },
        { mistake: "Using = instead of == for comparison", consequence: "Assignment instead of comparison", correction: "Use == for equality checks" },
        { mistake: "Incorrect indentation", consequence: "IndentationError", correction: "Use consistent 4-space indentation" }
    ],
    javascript: [
        { mistake: "Forgetting semicolons", consequence: "Unexpected behavior", correction: "Add ; at end of statements" },
        { mistake: "Using = instead of === for comparison", consequence: "Type coercion issues", correction: "Use === for strict equality" },
        { mistake: "Forgetting to declare variables with let/const", consequence: "Global variable pollution", correction: "Always declare with let or const" }
    ],
    java: [
        { mistake: "Forgetting semicolons", consequence: "Compilation error", correction: "Add ; at end of statements" },
        { mistake: "Using = instead of == for comparison", consequence: "Assignment instead of comparison", correction: "Use == for primitives, .equals() for objects" },
        { mistake: "Incorrect capitalization (Java is case-sensitive)", consequence: "Cannot find symbol error", correction: "Match exact capitalization" }
    ],
    csharp: [
        { mistake: "Forgetting semicolons", consequence: "Compilation error", correction: "Add ; at end of statements" },
        { mistake: "Using = instead of == for comparison", consequence: "Assignment instead of comparison", correction: "Use == for comparison" },
        { mistake: "Forgetting to use 'new' for objects", consequence: "Null reference exception", correction: "Use new ClassName() to create objects" }
    ],
    rust: [
        { mistake: "Forgetting semicolons", consequence: "Compilation error", correction: "Add ; at end of statements (except return expressions)" },
        { mistake: "Trying to mutate immutable variables", consequence: "Cannot assign twice to immutable variable", correction: "Use 'let mut' for mutable variables" },
        { mistake: "Ownership/borrowing errors", consequence: "Borrow checker errors", correction: "Use references (&) or clone() when needed" }
    ],
    kotlin: [
        { mistake: "Forgetting null safety operators", consequence: "NullPointerException", correction: "Use ?. for safe calls, ?: for elvis operator" },
        { mistake: "Using var when val would suffice", consequence: "Unnecessary mutability", correction: "Prefer val for immutable values" },
        { mistake: "Incorrect string interpolation", consequence: "Syntax error", correction: "Use $variable or ${expression}" }
    ],
    dart: [
        { mistake: "Forgetting semicolons", consequence: "Syntax error", correction: "Add ; at end of statements" },
        { mistake: "Not handling null safety", consequence: "Null check operator errors", correction: "Use ? for nullable types, ! for assertion" },
        { mistake: "Forgetting async/await", consequence: "Future not awaited", correction: "Add async to function, await before Future" }
    ]
};

// Generate hints based on challenge content
function generateHints(challenge, language) {
    const hints = [];
    const desc = (challenge.description || '').toLowerCase();

    if (desc.includes('print') || desc.includes('output') || desc.includes('display')) {
        hints.push({ level: 1, text: `Use the ${language === 'python' ? 'print()' : language === 'javascript' ? 'console.log()' : language === 'rust' ? 'println!()' : 'print/println'} function to display output.` });
    }

    if (desc.includes('variable') || desc.includes('store')) {
        hints.push({ level: 1, text: `Create a variable to store your value. In ${language}, use ${language === 'python' ? 'variable_name = value' : language === 'rust' ? 'let variable_name = value;' : 'appropriate syntax'}.` });
    }

    if (desc.includes('function') || desc.includes('method')) {
        hints.push({ level: 2, text: `Define a function using the ${language} syntax. Don't forget the return statement if needed.` });
    }

    if (desc.includes('loop') || desc.includes('repeat') || desc.includes('iterate')) {
        hints.push({ level: 2, text: `Use a for loop or while loop to repeat the operation.` });
    }

    if (desc.includes('condition') || desc.includes('if') || desc.includes('check')) {
        hints.push({ level: 2, text: `Use an if statement to check the condition.` });
    }

    // Always add a generic hint
    if (hints.length === 0) {
        hints.push({ level: 1, text: `Read the instructions carefully and break down the problem into smaller steps.` });
    }

    hints.push({ level: 3, text: `If stuck, try writing out the solution in plain English first, then convert to ${language} code.` });

    return hints;
}

// Generate Rust challenges for lessons that don't have any
function generateRustChallenges(lesson, lessonIndex) {
    const challenges = [];
    const content = lesson.contentSections.map(s => s.content).join(' ').toLowerCase();

    // Skip installation/setup lessons
    if (content.includes('install') || content.includes('setup') || content.includes('visual studio code')) {
        return challenges;
    }

    // Generate challenge based on lesson content
    const challengeId = `${lesson.id}-challenge-1`;

    if (content.includes('hello') || content.includes('first program') || content.includes('print')) {
        challenges.push({
            type: 'FREE_CODING',
            id: challengeId,
            title: 'Hello World in Rust',
            description: 'Write a Rust program that prints "Hello, World!" to the console.',
            instructions: 'Use the println! macro to display the message. Remember that Rust uses println! with an exclamation mark.',
            starterCode: '// Print "Hello, World!" to the console\n\nfn main() {\n    // Your code here\n}',
            solution: 'fn main() {\n    println!("Hello, World!");\n}',
            language: 'rust',
            testCases: [
                { id: 'test-1', description: 'Prints Hello, World!', expectedOutput: 'Hello, World!', isVisible: true }
            ],
            hints: [
                { level: 1, text: 'Use println!() - note the exclamation mark, it\'s a macro.' },
                { level: 2, text: 'Put your text in double quotes inside the parentheses.' }
            ],
            commonMistakes: COMMON_MISTAKES.rust,
            difficulty: 'beginner'
        });
    } else if (content.includes('variable') || content.includes('let')) {
        challenges.push({
            type: 'FREE_CODING',
            id: challengeId,
            title: 'Variables in Rust',
            description: 'Create a variable called `message` that stores the text "Learning Rust!" and print it.',
            instructions: 'Use let to create a variable, then use println! to display it.',
            starterCode: '// Create a variable and print it\n\nfn main() {\n    // Create a variable called message\n    \n    // Print the variable\n}',
            solution: 'fn main() {\n    let message = "Learning Rust!";\n    println!("{}", message);\n}',
            language: 'rust',
            testCases: [
                { id: 'test-1', description: 'Prints the message', expectedOutput: 'Learning Rust!', isVisible: true }
            ],
            hints: [
                { level: 1, text: 'Use let variable_name = value; to create a variable.' },
                { level: 2, text: 'Use println!("{}", variable); to print a variable.' }
            ],
            commonMistakes: COMMON_MISTAKES.rust,
            difficulty: 'beginner'
        });
    } else if (content.includes('function') || content.includes('fn ')) {
        challenges.push({
            type: 'FREE_CODING',
            id: challengeId,
            title: 'Functions in Rust',
            description: 'Create a function called `greet` that takes a name parameter and prints a greeting.',
            instructions: 'Define a function using fn, accept a &str parameter, and use println! to display the greeting.',
            starterCode: '// Create a greet function\n\nfn greet(name: &str) {\n    // Print a greeting with the name\n}\n\nfn main() {\n    greet("Rustacean");\n}',
            solution: 'fn greet(name: &str) {\n    println!("Hello, {}!", name);\n}\n\nfn main() {\n    greet("Rustacean");\n}',
            language: 'rust',
            testCases: [
                { id: 'test-1', description: 'Greets correctly', expectedOutput: 'Hello, Rustacean!', isVisible: true }
            ],
            hints: [
                { level: 1, text: 'Use {} as a placeholder in println! for variable interpolation.' },
                { level: 2, text: 'The function body should use println!("Hello, {}!", name);' }
            ],
            commonMistakes: COMMON_MISTAKES.rust,
            difficulty: 'beginner'
        });
    } else if (content.includes('ownership') || content.includes('borrow')) {
        challenges.push({
            type: 'FREE_CODING',
            id: challengeId,
            title: 'Understanding Ownership',
            description: 'Fix the code so it compiles by properly handling ownership.',
            instructions: 'The code has an ownership error. Use borrowing (&) to fix it.',
            starterCode: 'fn main() {\n    let s1 = String::from("hello");\n    let s2 = s1;\n    println!("{}", s1); // This line causes an error!\n}',
            solution: 'fn main() {\n    let s1 = String::from("hello");\n    let s2 = s1.clone();\n    println!("{}", s1);\n}',
            language: 'rust',
            testCases: [
                { id: 'test-1', description: 'Compiles and prints', expectedOutput: 'hello', isVisible: true }
            ],
            hints: [
                { level: 1, text: 'When s1 is moved to s2, s1 becomes invalid.' },
                { level: 2, text: 'Use .clone() to create a copy instead of moving.' }
            ],
            commonMistakes: COMMON_MISTAKES.rust,
            difficulty: 'intermediate'
        });
    }

    return challenges;
}

// Process a single course
function processCourse(courseName) {
    const coursePath = path.join(COURSES_DIR, courseName, 'course.json');

    if (!fs.existsSync(coursePath)) {
        console.log(`⚠️  Skipping ${courseName}: course.json not found`);
        return null;
    }

    console.log(`\n📖 Processing ${courseName}...`);
    const courseData = JSON.parse(fs.readFileSync(coursePath, 'utf8'));
    const language = courseData.language || courseName;

    let stats = {
        course: courseName,
        emptyStarterCode: 0,
        emptyHints: 0,
        emptyCommonMistakes: 0,
        challengesAdded: 0,
        fixed: {
            starterCode: 0,
            hints: 0,
            commonMistakes: 0
        }
    };

    // Process each module
    for (const module of courseData.modules) {
        for (const lesson of module.lessons) {
            // For Rust, add challenges if missing
            if (language === 'rust' && (!lesson.challenges || lesson.challenges.length === 0)) {
                const newChallenges = generateRustChallenges(lesson, lesson.order);
                if (newChallenges.length > 0) {
                    lesson.challenges = newChallenges;
                    stats.challengesAdded += newChallenges.length;
                }
            }

            // Process existing challenges
            if (lesson.challenges) {
                for (const challenge of lesson.challenges) {
                    // Fix empty starterCode
                    if (!challenge.starterCode || challenge.starterCode.trim() === '' || challenge.starterCode === '// Your code here\n') {
                        stats.emptyStarterCode++;
                        const template = TEMPLATES[language] || TEMPLATES.javascript;
                        challenge.starterCode = template.starterCode(challenge.title || 'Exercise');
                        stats.fixed.starterCode++;
                    }

                    // Fix empty hints
                    if (!challenge.hints || challenge.hints.length === 0) {
                        stats.emptyHints++;
                        challenge.hints = generateHints(challenge, language);
                        stats.fixed.hints++;
                    }

                    // Fix empty commonMistakes
                    if (!challenge.commonMistakes || challenge.commonMistakes.length === 0) {
                        stats.emptyCommonMistakes++;
                        challenge.commonMistakes = COMMON_MISTAKES[language] || COMMON_MISTAKES.javascript;
                        stats.fixed.commonMistakes++;
                    }

                    // Ensure testCases have IDs
                    if (challenge.testCases) {
                        challenge.testCases = challenge.testCases.map((tc, idx) => ({
                            id: tc.id || `test-${idx + 1}`,
                            description: tc.description || `Test case ${idx + 1}`,
                            expectedOutput: tc.expectedOutput || '',
                            isVisible: tc.isVisible !== undefined ? tc.isVisible : true
                        }));
                    }
                }
            }
        }
    }

    // Write updated course
    fs.writeFileSync(coursePath, JSON.stringify(courseData, null, 2), 'utf8');

    console.log(`  ✅ Fixed: ${stats.fixed.starterCode} starterCode, ${stats.fixed.hints} hints, ${stats.fixed.commonMistakes} commonMistakes`);
    if (stats.challengesAdded > 0) {
        console.log(`  ➕ Added ${stats.challengesAdded} new challenges`);
    }

    return stats;
}

// Main
function main() {
    console.log('🚀 Phase 2: Content Gap Filler');
    console.log('================================');

    const courses = fs.readdirSync(COURSES_DIR).filter(dir => {
        const stat = fs.statSync(path.join(COURSES_DIR, dir));
        return stat.isDirectory();
    });

    const results = [];
    for (const course of courses) {
        try {
            const result = processCourse(course);
            if (result) results.push(result);
        } catch (error) {
            console.error(`❌ Error processing ${course}: ${error.message}`);
        }
    }

    // Summary
    console.log('\n\n📊 PHASE 2 SUMMARY');
    console.log('==================');
    console.log('Course       | StarterCode | Hints | CommonMistakes | New Challenges');
    console.log('-------------|-------------|-------|----------------|---------------');
    for (const r of results) {
        console.log(`${r.course.padEnd(12)} | ${String(r.fixed.starterCode).padStart(11)} | ${String(r.fixed.hints).padStart(5)} | ${String(r.fixed.commonMistakes).padStart(14)} | ${String(r.challengesAdded).padStart(14)}`);
    }

    const totals = results.reduce((acc, r) => ({
        starterCode: acc.starterCode + r.fixed.starterCode,
        hints: acc.hints + r.fixed.hints,
        commonMistakes: acc.commonMistakes + r.fixed.commonMistakes,
        challengesAdded: acc.challengesAdded + r.challengesAdded
    }), { starterCode: 0, hints: 0, commonMistakes: 0, challengesAdded: 0 });

    console.log('-------------|-------------|-------|----------------|---------------');
    console.log(`${'TOTAL'.padEnd(12)} | ${String(totals.starterCode).padStart(11)} | ${String(totals.hints).padStart(5)} | ${String(totals.commonMistakes).padStart(14)} | ${String(totals.challengesAdded).padStart(14)}`);

    console.log('\n✨ Phase 2 complete!');
}

main();
