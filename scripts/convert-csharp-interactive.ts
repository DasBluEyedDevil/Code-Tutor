#!/usr/bin/env tsx

/**
 * Enhanced C# Lesson Converter - Extracts ALL Interactive Content
 * Converts C# Training Course to new interactive platform format
 *
 * Source: /tmp/CSharp-Training-Course/CSharpLearningPlatform/Content/Lessons/
 * Output: /home/user/Code-Tutor/content/courses/csharp/course.json
 *
 * Features:
 * - ContentSections for structured learning (ANALOGY, EXAMPLE, THEORY)
 * - FreeCodingChallenge with all interactive fields
 * - TestCases from expectedOutputPatterns
 * - CommonMistakes from commonStickingPoints
 * - Complete hint system
 */

import * as fs from 'fs/promises';
import * as path from 'path';

// ============================================================================
// Legacy Format (Source)
// ============================================================================

interface LegacyLesson {
  moduleId: number;
  lessonNumber: number;
  lessonId: string;
  title: string;
  simplifierConcept?: string;
  coderExample?: string;
  syntaxBreakdown?: Array<{
    codeSnippet: string;
    explanation: string;
  }>;
  challenge?: {
    instructions: string;
    starterCode: string;
    solutionCode: string;
    hint?: string;
    expectedOutputPatterns?: string[];
    validationRules?: any[];
    commonStickingPoints?: string[];
  };
}

// ============================================================================
// Interactive Platform Format (Target)
// ============================================================================

type ContentSectionType =
  | 'THEORY'      // Main explanation of concepts
  | 'ANALOGY'     // Real-world analogies
  | 'EXAMPLE'     // Code examples with explanations
  | 'KEY_POINT'   // Important takeaways
  | 'WARNING'     // Common pitfalls
  | 'EXPERIMENT'; // Guided experimentation

interface ContentSection {
  type: ContentSectionType;
  title: string;
  content: string;
  code?: string;
  language?: string;
}

interface Hint {
  level: number;
  text: string;
  code?: string;
}

interface CommonMistake {
  mistake: string;
  consequence: string;
  correction: string;
  example?: {
    wrong: string;
    right: string;
  };
}

interface TestCase {
  id?: string;
  description: string;
  inputs?: any[];
  expectedOutput: any;
  isVisible: boolean;
  testType?: 'exact' | 'contains' | 'regex' | 'pattern';
  customMessage?: string;
}

interface FreeCodingChallenge {
  type: 'FREE_CODING';
  id: string;
  title: string;
  description: string;
  instructions: string;
  starterCode: string;
  solution: string;
  language: string;
  testCases: TestCase[];
  hints?: Hint[];
  commonMistakes?: CommonMistake[];
  difficulty?: 'beginner' | 'intermediate' | 'advanced' | 1 | 2 | 3 | 4 | 5;
  estimatedMinutes?: number;
}

interface InteractiveLesson {
  id: string;
  title: string;
  moduleId: string;
  order: number;
  estimatedMinutes: number;
  difficulty: 'beginner' | 'intermediate' | 'advanced';
  contentSections?: ContentSection[];
  challenges?: FreeCodingChallenge[];
  learningObjectives?: string[];
}

interface Module {
  id: string;
  title: string;
  description: string;
  order: number;
  estimatedHours: number;
  prerequisites: string[];
  lessons: InteractiveLesson[];
}

interface CourseMetadata {
  id: string;
  language: string;
  version: string;
  displayName: string;
  description: string;
  totalModules: number;
  estimatedHours: number;
  difficulty: 'beginner' | 'intermediate' | 'advanced' | 'beginner-to-advanced';
  prerequisites: string[];
  learningOutcomes: string[];
  icon: string;
  color: string;
}

interface LanguageConfig {
  executionEngine: string;
  compilerOptions: {
    version: string;
    flags: string[];
  };
  editorSettings: {
    defaultTemplate: string;
    fileExtension: string;
    monacoLanguageId: string;
    tabSize: number;
    insertSpaces: boolean;
  };
  sandboxConstraints: {
    maxExecutionTimeMs: number;
    maxMemoryMB: number;
    maxOutputChars: number;
    allowedPackages: string[];
    blockedPackages: string[];
  };
}

interface Course {
  courseMetadata: CourseMetadata;
  modules: Module[];
  languageConfig: LanguageConfig;
}

// ============================================================================
// Module Descriptions
// ============================================================================

const MODULE_DESCRIPTIONS: Record<number, { title: string; description: string }> = {
  1: {
    title: 'Getting Started with C#',
    description: 'Introduction to programming, C#, and .NET. Learn basic syntax, output, comments, and your first programs.'
  },
  2: {
    title: 'Variables and Data Types',
    description: 'Master variables, data types (int, string, bool, double), type conversion, and working with numbers and text.'
  },
  3: {
    title: 'Control Flow',
    description: 'Learn decision-making with if/else statements, switch cases, logical operators, and conditional logic.'
  },
  4: {
    title: 'Loops and Iteration',
    description: 'Master for loops, while loops, do-while loops, nested loops, and the foreach statement.'
  },
  5: {
    title: 'Collections',
    description: 'Work with arrays, Lists, Dictionaries, and other collection types for managing groups of data.'
  },
  6: {
    title: 'Methods and Functions',
    description: 'Create reusable code with methods, parameters, return values, method overloading, and scope.'
  },
  7: {
    title: 'Object-Oriented Programming Basics',
    description: 'Introduction to classes, objects, properties, constructors, and the fundamentals of OOP.'
  },
  8: {
    title: 'Advanced OOP Concepts',
    description: 'Explore inheritance, polymorphism, abstract classes, interfaces, and advanced object-oriented patterns.'
  },
  9: {
    title: 'Exception Handling',
    description: 'Learn to handle errors gracefully with try-catch blocks, custom exceptions, and error handling best practices.'
  },
  10: {
    title: 'Asynchronous Programming',
    description: 'Master async/await, Tasks, asynchronous operations, and building responsive applications.'
  },
  11: {
    title: 'LINQ and Query Expressions',
    description: 'Learn Language Integrated Query (LINQ) for querying collections, filtering, sorting, and data transformation.'
  },
  12: {
    title: 'File I/O and Serialization',
    description: 'Work with files, streams, reading/writing data, and JSON serialization/deserialization.'
  },
  13: {
    title: 'Generics and Advanced Types',
    description: 'Understand generic types, constraints, delegates, events, and advanced C# type system features.'
  },
  14: {
    title: 'Modern C# Features',
    description: 'Explore modern C# features including nullable reference types, pattern matching, records, and C# 10+ enhancements.'
  }
};

// ============================================================================
// Conversion Functions
// ============================================================================

/**
 * Extract code snippet from coderExample if it exists
 */
function extractCodeFromExample(coderExample?: string): { code: string; explanation: string } | null {
  if (!coderExample) return null;

  // The coderExample typically contains code with inline comments
  // We'll use it as-is for the code section
  return {
    code: coderExample.trim(),
    explanation: 'This example demonstrates the concepts in action.'
  };
}

/**
 * Parse common sticking points into CommonMistake format
 */
function parseCommonMistakes(stickingPoints: string[]): CommonMistake[] {
  return stickingPoints.map((point, index) => {
    // Try to extract wrong/right examples if they exist
    const wrongMatch = point.match(/(.+?)(?:\s*❌|\s*✗)\s*(.+?)(?:\s*✓|\s*✅)\s*(.+)/);
    const hasExamples = point.includes('❌') || point.includes('✗') || point.includes('✓') || point.includes('✅');

    if (wrongMatch && hasExamples) {
      const [, mistake, wrong, right] = wrongMatch;
      return {
        mistake: mistake.trim(),
        consequence: 'This will cause an error or unexpected behavior.',
        correction: 'Use the correct syntax as shown.',
        example: {
          wrong: wrong.trim(),
          right: right.trim()
        }
      };
    }

    // Try to split by colon for mistake: explanation format
    const colonIndex = point.indexOf(':');
    if (colonIndex > 0 && colonIndex < point.length - 1) {
      return {
        mistake: point.substring(0, colonIndex).trim(),
        consequence: point.substring(colonIndex + 1).trim(),
        correction: 'Review the correct syntax and best practices for this concept.'
      };
    }

    // Otherwise, treat the whole point as the mistake
    return {
      mistake: point.trim(),
      consequence: 'This is a common error that can cause problems.',
      correction: 'Review the lesson content and examples carefully.'
    };
  });
}

/**
 * Create test cases from expectedOutputPatterns
 */
function createTestCases(patterns?: string[]): TestCase[] {
  if (!patterns || patterns.length === 0) {
    return [];
  }

  return patterns.map((pattern, index) => ({
    id: `test-${index + 1}`,
    description: `Output should contain "${pattern}"`,
    expectedOutput: pattern,
    isVisible: true,
    testType: 'contains' as const,
    customMessage: `Expected output to contain "${pattern}"`
  }));
}

/**
 * Create hints array from legacy hint and commonStickingPoints
 */
function createHints(legacyHint?: string, stickingPoints?: string[]): Hint[] {
  const hints: Hint[] = [];

  // Add the main hint as level 1
  if (legacyHint) {
    hints.push({
      level: 1,
      text: legacyHint
    });
  }

  // Add common sticking points as additional hints (levels 2-5)
  if (stickingPoints && stickingPoints.length > 0) {
    stickingPoints.slice(0, 4).forEach((point, index) => {
      hints.push({
        level: index + 2,
        text: point
      });
    });
  }

  return hints;
}

/**
 * Convert legacy lesson to interactive platform format
 */
function convertLesson(
  legacyLesson: LegacyLesson,
  moduleNumber: number
): InteractiveLesson {
  const contentSections: ContentSection[] = [];

  // Add ANALOGY section (simplifierConcept)
  if (legacyLesson.simplifierConcept) {
    contentSections.push({
      type: 'ANALOGY',
      title: 'Understanding the Concept',
      content: legacyLesson.simplifierConcept
    });
  }

  // Add EXAMPLE section (coderExample)
  if (legacyLesson.coderExample) {
    const example = extractCodeFromExample(legacyLesson.coderExample);
    if (example) {
      contentSections.push({
        type: 'EXAMPLE',
        title: 'Code Example',
        content: example.explanation,
        code: example.code,
        language: 'csharp'
      });
    }
  }

  // Add THEORY section with syntax breakdown
  if (legacyLesson.syntaxBreakdown && legacyLesson.syntaxBreakdown.length > 0) {
    let theoryContent = '## Breaking Down the Syntax\n\n';
    for (const item of legacyLesson.syntaxBreakdown) {
      theoryContent += `**\`${item.codeSnippet}\`**: ${item.explanation}\n\n`;
    }

    contentSections.push({
      type: 'THEORY',
      title: 'Syntax Breakdown',
      content: theoryContent.trim()
    });
  }

  // Determine difficulty
  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (moduleNumber > 10) difficulty = 'advanced';
  else if (moduleNumber > 5) difficulty = 'intermediate';

  // Create the interactive lesson
  const lesson: InteractiveLesson = {
    id: `lesson-${String(moduleNumber).padStart(2, '0')}-${String(legacyLesson.lessonNumber).padStart(2, '0')}`,
    title: legacyLesson.title,
    moduleId: `module-${String(moduleNumber).padStart(2, '0')}`,
    order: legacyLesson.lessonNumber,
    estimatedMinutes: 15,
    difficulty,
    contentSections
  };

  // Add challenge if it exists
  if (legacyLesson.challenge) {
    const challenge: FreeCodingChallenge = {
      type: 'FREE_CODING',
      id: `${lesson.id}-challenge-01`,
      title: 'Practice Challenge',
      description: 'Apply what you\'ve learned in this interactive coding challenge.',
      instructions: legacyLesson.challenge.instructions,
      starterCode: legacyLesson.challenge.starterCode,
      solution: legacyLesson.challenge.solutionCode,
      language: 'csharp',
      testCases: createTestCases(legacyLesson.challenge.expectedOutputPatterns),
      difficulty,
      estimatedMinutes: 10
    };

    // Add hints
    const hints = createHints(
      legacyLesson.challenge.hint,
      legacyLesson.challenge.commonStickingPoints
    );
    if (hints.length > 0) {
      challenge.hints = hints;
    }

    // Add common mistakes
    if (legacyLesson.challenge.commonStickingPoints) {
      challenge.commonMistakes = parseCommonMistakes(
        legacyLesson.challenge.commonStickingPoints
      );
    }

    lesson.challenges = [challenge];
  }

  return lesson;
}

/**
 * Main conversion function
 */
async function convertCSharpCourse(sourceDir: string, outputPath: string) {
  console.log('🔄 Converting C# course with enhanced interactive content...\n');

  // Find all module directories
  const moduleDirs = await fs.readdir(sourceDir);
  const moduleNumbers = moduleDirs
    .filter(d => d.startsWith('Module'))
    .map(d => parseInt(d.replace('Module', '')))
    .sort((a, b) => a - b);

  console.log(`📁 Found ${moduleNumbers.length} modules\n`);

  // Convert each module
  const modules: Module[] = [];
  let totalLessons = 0;
  let totalChallenges = 0;
  let skippedLessons = 0;

  for (const moduleNum of moduleNumbers) {
    const moduleDirName = `Module${String(moduleNum).padStart(2, '0')}`;
    const modulePath = path.join(sourceDir, moduleDirName);

    // Find all lesson files
    const lessonFiles = (await fs.readdir(modulePath))
      .filter(f => f.startsWith('Lesson') && f.endsWith('.json'))
      .sort();

    console.log(`📖 Module ${moduleNum}: ${lessonFiles.length} lessons`);

    const lessons: InteractiveLesson[] = [];

    for (const file of lessonFiles) {
      const filePath = path.join(modulePath, file);
      try {
        const content = await fs.readFile(filePath, 'utf-8');
        const legacyLesson: LegacyLesson = JSON.parse(content);

        const lesson = convertLesson(legacyLesson, moduleNum);
        lessons.push(lesson);

        // Count challenges
        if (lesson.challenges && lesson.challenges.length > 0) {
          totalChallenges += lesson.challenges.length;
        }
      } catch (error: any) {
        console.warn(`   ⚠️  Skipping ${file}: ${error.message}`);
        skippedLessons++;
        continue;
      }
    }

    // Get module metadata
    const moduleInfo = MODULE_DESCRIPTIONS[moduleNum] || {
      title: `Module ${moduleNum}`,
      description: `Module ${moduleNum} content`
    };

    // Determine module difficulty
    let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
    if (moduleNum > 10) difficulty = 'advanced';
    else if (moduleNum > 5) difficulty = 'intermediate';

    // Calculate module hours
    const totalMinutes = lessons.reduce((sum, l) => sum + l.estimatedMinutes, 0);
    const estimatedHours = Math.ceil(totalMinutes / 60);

    modules.push({
      id: `module-${String(moduleNum).padStart(2, '0')}`,
      title: moduleInfo.title,
      description: moduleInfo.description,
      order: moduleNum,
      estimatedHours,
      prerequisites: moduleNum > 1 ? [`module-${String(moduleNum - 1).padStart(2, '0')}`] : [],
      lessons
    });

    totalLessons += lessons.length;
  }

  // Create course metadata
  const courseMetadata: CourseMetadata = {
    id: 'csharp',
    language: 'csharp',
    version: '2.0.0',
    displayName: 'C# Programming',
    description: 'Master C# and .NET development from fundamentals to advanced concepts, including object-oriented programming, LINQ, async/await, and modern C# features.',
    totalModules: modules.length,
    estimatedHours: modules.reduce((sum, m) => sum + m.estimatedHours, 0),
    difficulty: 'beginner-to-advanced',
    prerequisites: [],
    learningOutcomes: [
      'Write clean, efficient C# code following best practices',
      'Build console applications and understand the .NET runtime',
      'Master object-oriented programming with classes and inheritance',
      'Use LINQ for powerful data querying and transformation',
      'Implement asynchronous programming with async/await',
      'Handle errors gracefully with exception handling',
      'Work with files, collections, and modern C# features'
    ],
    icon: 'csharp',
    color: '#512BD4'
  };

  // Create language configuration
  const languageConfig: LanguageConfig = {
    executionEngine: 'dotnet',
    compilerOptions: {
      version: '8.0',
      flags: ['--optimize', '--nullable', 'enable']
    },
    editorSettings: {
      defaultTemplate: 'using System;\n\n',
      fileExtension: '.cs',
      monacoLanguageId: 'csharp',
      tabSize: 4,
      insertSpaces: true
    },
    sandboxConstraints: {
      maxExecutionTimeMs: 5000,
      maxMemoryMB: 256,
      maxOutputChars: 10000,
      allowedPackages: [
        'System',
        'System.Collections.Generic',
        'System.Linq',
        'System.Threading.Tasks',
        'System.IO',
        'System.Text'
      ],
      blockedPackages: [
        'System.Diagnostics.Process',
        'System.Reflection',
        'System.Runtime.InteropServices'
      ]
    }
  };

  // Create course
  const course: Course = {
    courseMetadata,
    modules,
    languageConfig
  };

  // Ensure output directory exists
  const outputDir = path.dirname(outputPath);
  await fs.mkdir(outputDir, { recursive: true });

  // Write output
  await fs.writeFile(outputPath, JSON.stringify(course, null, 2), 'utf-8');

  console.log('\n✅ Conversion complete!\n');
  console.log('📊 Statistics:');
  console.log(`   📚 ${modules.length} modules`);
  console.log(`   📄 ${totalLessons} lessons imported`);
  console.log(`   💪 ${totalChallenges} interactive challenges`);
  console.log(`   ⚠️  ${skippedLessons} lessons skipped (malformed JSON)`);
  console.log(`   ⏱️  ~${courseMetadata.estimatedHours} hours of content`);
  console.log(`   💾 Output: ${outputPath}\n`);

  // Summary statistics
  const lessonsWithChallenges = totalLessons > 0 ? (totalChallenges / totalLessons * 100).toFixed(1) : '0.0';
  console.log('📈 Content Analysis:');
  console.log(`   • ${lessonsWithChallenges}% of lessons have challenges`);
  console.log(`   • Average ${(totalChallenges / modules.length).toFixed(1)} challenges per module`);
  console.log(`   • ${courseMetadata.learningOutcomes.length} learning outcomes defined`);
}

// ============================================================================
// Main Execution
// ============================================================================

const sourceDir = process.argv[2] || '/tmp/CSharp-Training-Course/CSharpLearningPlatform/Content/Lessons';
const outputPath = process.argv[3] || path.join(__dirname, '../content/courses/csharp/course.json');

convertCSharpCourse(sourceDir, outputPath)
  .then(() => {
    console.log('✨ C# course with interactive content ready!');
    console.log('🚀 Import complete - all challenges, hints, and test cases extracted!\n');
    process.exit(0);
  })
  .catch((error) => {
    console.error('❌ Conversion failed:', error);
    process.exit(1);
  });
