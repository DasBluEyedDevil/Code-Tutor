#!/usr/bin/env tsx

/**
 * Enhanced Java Training Course Converter - Extracts ALL Interactive Content
 * Source: Java class files from socratic-java-mentor
 * Output: content/courses/java/course.json
 *
 * Features:
 * - Extracts all content sections (theory, analogy, example, key points, warnings)
 * - Extracts ALL challenge types (MULTIPLE_CHOICE, FREE_CODING, CODE_COMPLETION, CONCEPTUAL)
 * - Extracts test cases with inputs, expectedOutput, isVisible
 * - Parses Java string literals with multi-line concatenation
 * - Converts to platform format with proper typing
 */

import * as fs from 'fs/promises';
import * as path from 'path';

// ============================================================================
// Type Definitions
// ============================================================================

interface ContentSection {
  type: 'THEORY' | 'ANALOGY' | 'EXAMPLE' | 'KEY_POINT' | 'WARNING';
  title: string;
  content: string;
}

interface TestCase {
  id: string;
  input: string | null;
  expectedOutput: string;
  description: string;
  isVisible?: boolean;
}

interface ValidationRules {
  mustContain?: string[];
  mustNotContain?: string[];
  maxLines?: number;
  allowedPackages?: string[];
}

interface Exercise {
  id: string;
  type: 'coding' | 'quiz' | 'project';
  title: string;
  instructions: string;
  difficulty: 'beginner' | 'intermediate' | 'advanced';
  estimatedMinutes: number;
  starterCode: string;
  solution: string;
  hints: string[];
  testCases: TestCase[];
  validationRules: ValidationRules;
  methodSignature?: string;
  challengeType?: string;
}

interface QuizQuestion {
  id: string;
  type: 'multiple-choice' | 'true-false' | 'fill-in-blank';
  question: string;
  points: number;
  options?: string[];
  correctAnswer: number | boolean | string;
  explanation: string;
}

interface Lesson {
  id: string;
  title: string;
  type: 'reading' | 'interactive' | 'project';
  order: number;
  estimatedMinutes: number;
  difficulty: 'beginner' | 'intermediate' | 'advanced';
  tags: string[];
  content: {
    format: 'markdown';
    body: string;
    bodyFile?: string;
    codeExamples: any[];
  };
  exercises: Exercise[];
  quiz?: {
    id: string;
    passingScore: number;
    questions: QuizQuestion[];
  };
}

interface Module {
  id: string;
  title: string;
  description: string;
  order: number;
  estimatedHours: number;
  prerequisites: string[];
  lessons: Lesson[];
}

interface Course {
  courseMetadata: {
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
  };
  modules: Module[];
  languageConfig: {
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
  };
}

// ============================================================================
// Java String Parser
// ============================================================================

/**
 * Extract a Java string literal, handling multi-line concatenation and escapes
 */
function extractJavaString(javaCode: string, startPos: number): { content: string; endPos: number } {
  let content = '';
  let i = startPos;
  let inString = false;
  let escaped = false;

  // Skip whitespace to find opening quote
  while (i < javaCode.length && /\s/.test(javaCode[i])) {
    i++;
  }

  if (javaCode[i] !== '"') {
    return { content: '', endPos: i };
  }

  i++; // Skip opening quote
  inString = true;

  while (i < javaCode.length) {
    const char = javaCode[i];

    if (escaped) {
      // Handle escape sequences
      if (char === 'n') content += '\n';
      else if (char === 't') content += '\t';
      else if (char === 'r') content += '\r';
      else if (char === '"') content += '"';
      else if (char === '\\') content += '\\';
      else content += char;
      escaped = false;
      i++;
      continue;
    }

    if (char === '\\') {
      escaped = true;
      i++;
      continue;
    }

    if (char === '"') {
      inString = false;
      i++;

      // Check for string concatenation (+)
      let j = i;
      while (j < javaCode.length && /\s/.test(javaCode[j])) {
        j++;
      }

      if (javaCode[j] === '+') {
        // Continue to next string
        j++;
        while (j < javaCode.length && /\s/.test(javaCode[j])) {
          j++;
        }
        if (javaCode[j] === '"') {
          i = j + 1;
          inString = true;
          continue;
        }
      }

      // End of concatenated string
      return { content, endPos: i };
    }

    content += char;
    i++;
  }

  return { content, endPos: i };
}

/**
 * Extract method parameter string (e.g., .description("text here"))
 */
function extractMethodParam(javaCode: string, methodName: string, startIndex: number = 0): string | null {
  const pattern = new RegExp(`\\.${methodName}\\s*\\(\\s*"`, 'g');
  pattern.lastIndex = startIndex;

  const match = pattern.exec(javaCode);
  if (!match) return null;

  const result = extractJavaString(javaCode, match.index + match[0].length - 1);
  return result.content;
}

/**
 * Extract all method parameters with title and content pairs
 */
function extractMethodParamPairs(javaCode: string, methodName: string): Array<{ title: string; content: string }> {
  const results: Array<{ title: string; content: string }> = [];
  const pattern = new RegExp(`\\.${methodName}\\s*\\(\\s*"`, 'g');
  let match;

  while ((match = pattern.exec(javaCode)) !== null) {
    // Extract first parameter (title)
    const titleResult = extractJavaString(javaCode, match.index + match[0].length - 1);

    // Find comma and extract second parameter (content)
    let i = titleResult.endPos;
    while (i < javaCode.length && javaCode[i] !== ',') {
      i++;
    }

    if (i < javaCode.length) {
      i++; // Skip comma
      const contentResult = extractJavaString(javaCode, i);
      results.push({
        title: titleResult.content,
        content: contentResult.content
      });
    }
  }

  return results;
}

/**
 * Extract all multiple choice options
 */
function extractMultipleChoiceOptions(javaCode: string, challengeStartPos: number, challengeEndPos: number): string[] {
  const options: string[] = [];
  const challengeCode = javaCode.substring(challengeStartPos, challengeEndPos);
  const pattern = /\.addMultipleChoiceOption\s*\(\s*"/g;
  let match;

  while ((match = pattern.exec(challengeCode)) !== null) {
    const result = extractJavaString(challengeCode, match.index + match[0].length - 1);
    options.push(result.content);
  }

  return options;
}

// ============================================================================
// Content Section Extraction
// ============================================================================

function extractContentSections(javaCode: string): ContentSection[] {
  const sectionsWithPos: Array<ContentSection & { position: number }> = [];

  const sectionTypes = [
    { method: 'addTheory', type: 'THEORY' as const },
    { method: 'addAnalogy', type: 'ANALOGY' as const },
    { method: 'addExample', type: 'EXAMPLE' as const },
    { method: 'addKeyPoint', type: 'KEY_POINT' as const },
    { method: 'addWarning', type: 'WARNING' as const }
  ];

  for (const { method, type } of sectionTypes) {
    const pattern = new RegExp(`\\.${method}\\s*\\(\\s*"`, 'g');
    let match;

    while ((match = pattern.exec(javaCode)) !== null) {
      const position = match.index;

      // Extract first parameter (title)
      const titleResult = extractJavaString(javaCode, match.index + match[0].length - 1);

      // Find comma and extract second parameter (content)
      let i = titleResult.endPos;
      while (i < javaCode.length && javaCode[i] !== ',') {
        i++;
      }

      if (i < javaCode.length) {
        i++; // Skip comma
        const contentResult = extractJavaString(javaCode, i);
        sectionsWithPos.push({
          type,
          title: titleResult.content,
          content: contentResult.content,
          position
        });
      }
    }
  }

  // Sort by position in file
  sectionsWithPos.sort((a, b) => a.position - b.position);

  // Remove position property
  return sectionsWithPos.map(({ type, title, content }) => ({ type, title, content }));
}

// ============================================================================
// Challenge Extraction
// ============================================================================

function findChallengeBlocks(javaCode: string): Array<{ start: number; end: number; name: string }> {
  const blocks: Array<{ start: number; end: number; name: string }> = [];

  // Find all "private static Challenge create...Challenge()" methods
  const challengePattern = /private\s+static\s+Challenge\s+(\w+)\s*\(\s*\)\s*\{/g;
  let match;

  while ((match = challengePattern.exec(javaCode)) !== null) {
    const startPos = match.index;
    const methodName = match[1];

    // Find the matching closing brace
    let braceCount = 1;
    let i = match.index + match[0].length;

    while (i < javaCode.length && braceCount > 0) {
      if (javaCode[i] === '{') braceCount++;
      else if (javaCode[i] === '}') braceCount--;
      i++;
    }

    blocks.push({ start: startPos, end: i, name: methodName });
  }

  return blocks;
}

function extractChallengeType(challengeCode: string): string {
  const typeMatch = challengeCode.match(/ChallengeType\.(\w+)/);
  return typeMatch ? typeMatch[1] : 'MULTIPLE_CHOICE';
}

function extractTestCases(challengeCode: string, challengeId: string): TestCase[] {
  const testCases: TestCase[] = [];
  const pattern = /new\s+TestCase\s*\(/g;
  let match;
  let testIndex = 0;

  while ((match = pattern.exec(challengeCode)) !== null) {
    let i = match.index + match[0].length;

    // Extract parameters manually by parsing
    const params: string[] = [];
    let currentParam = '';
    let parenDepth = 0;
    let braceDepth = 0;
    let inString = false;
    let escaped = false;

    while (i < challengeCode.length) {
      const char = challengeCode[i];

      if (escaped) {
        currentParam += char;
        escaped = false;
        i++;
        continue;
      }

      if (char === '\\' && inString) {
        escaped = true;
        currentParam += char;
        i++;
        continue;
      }

      if (char === '"' && !escaped) {
        inString = !inString;
        currentParam += char;
        i++;
        continue;
      }

      if (inString) {
        currentParam += char;
        i++;
        continue;
      }

      if (char === '(') parenDepth++;
      else if (char === ')') {
        if (parenDepth === 0) {
          // End of TestCase constructor
          if (currentParam.trim()) {
            params.push(currentParam.trim());
          }
          break;
        }
        parenDepth--;
      }
      else if (char === '{') braceDepth++;
      else if (char === '}') braceDepth--;
      else if (char === ',' && parenDepth === 0 && braceDepth === 0) {
        params.push(currentParam.trim());
        currentParam = '';
        i++;
        continue;
      }

      currentParam += char;
      i++;
    }

    // Parse the parameters
    if (params.length >= 2) {
      // Extract string values
      const description = params[0].match(/"((?:[^"\\]|\\.)*)"/)?.[1] || '';

      let input: string | null = null;
      let expectedOutput = '';
      let isVisible = true;

      if (params.length === 2) {
        // (description, expectedOutput)
        expectedOutput = params[1].match(/"((?:[^"\\]|\\.)*)"/)?.[1] || params[1];
      } else if (params.length === 3) {
        // Could be (description, expectedOutput, isVisible) or (description, inputs[], expectedOutput)
        if (params[1].includes('new Object') || params[1].includes('{}')) {
          // (description, inputs[], expectedOutput)
          expectedOutput = params[2].match(/"((?:[^"\\]|\\.)*)"/)?.[1] || params[2];
        } else {
          // (description, expectedOutput, isVisible)
          expectedOutput = params[1].match(/"((?:[^"\\]|\\.)*)"/)?.[1] || params[1];
          if (params[2].trim() === 'true' || params[2].trim() === 'false') {
            isVisible = params[2].trim() === 'true';
          }
        }
      } else if (params.length >= 4) {
        // Multiple possibilities:
        // (description, inputs[], expectedOutput, isVisible)
        // (description, expectedOutput, isVisible, message)
        // (description, inputs[], expectedOutput, isVisible, message)

        if (params[1].includes('new Object') || params[1].includes('{}')) {
          // Has inputs array
          input = params[1];
          expectedOutput = params[2].match(/"((?:[^"\\]|\\.)*)"/)?.[1] || params[2];
          if (params.length >= 4 && (params[3].trim() === 'true' || params[3].trim() === 'false')) {
            isVisible = params[3].trim() === 'true';
          }
        } else {
          // No inputs array: (description, expectedOutput, isVisible, message?)
          expectedOutput = params[1].match(/"((?:[^"\\]|\\.)*)"/)?.[1] || params[1];
          if (params[2].trim() === 'true' || params[2].trim() === 'false') {
            isVisible = params[2].trim() === 'true';
          }
          // param[3] is optional message, we ignore it
        }
      }

      testCases.push({
        id: `${challengeId}-test-${testIndex + 1}`,
        input,
        expectedOutput: expectedOutput.replace(/\\n/g, '\n').replace(/\\"/g, '"'),
        description: description.replace(/\\n/g, '\n').replace(/\\"/g, '"'),
        isVisible
      });

      testIndex++;
    }
  }

  return testCases;
}

function extractChallenge(challengeCode: string, challengeMethodName: string, lessonId: string, index: number): Exercise | QuizQuestion | null {
  // Extract basic info
  const builderMatch = challengeCode.match(/new\s+Challenge\.Builder\s*\(\s*"([^"]+)"\s*,\s*"([^"]+)"\s*,\s*ChallengeType\.(\w+)\)/);
  if (!builderMatch) return null;

  const challengeId = builderMatch[1];
  const challengeTitle = builderMatch[2];
  const challengeType = builderMatch[3];

  const description = extractMethodParam(challengeCode, 'description') || '';
  const starterCode = extractMethodParam(challengeCode, 'starterCode') || '';
  const methodSignature = extractMethodParam(challengeCode, 'methodSignature') || '';

  if (challengeType === 'MULTIPLE_CHOICE') {
    const options = extractMultipleChoiceOptions(challengeCode, 0, challengeCode.length);
    const correctAnswerStr = extractMethodParam(challengeCode, 'correctAnswer') || '';

    // Convert A, B, C, D to index
    let correctAnswer: number = 0;
    if (correctAnswerStr.match(/^[A-Z]$/)) {
      correctAnswer = correctAnswerStr.charCodeAt(0) - 'A'.charCodeAt(0);
    }

    return {
      id: challengeId,
      type: 'multiple-choice',
      question: description,
      points: 1,
      options,
      correctAnswer,
      explanation: ''
    };
  } else {
    // Coding challenge
    const testCases = extractTestCases(challengeCode, challengeId);

    return {
      id: challengeId,
      type: 'coding',
      title: challengeTitle,
      instructions: description,
      difficulty: 'beginner',
      estimatedMinutes: 10,
      starterCode,
      solution: '',
      hints: [],
      testCases,
      validationRules: {},
      methodSignature,
      challengeType
    };
  }
}

// ============================================================================
// Lesson Parsing
// ============================================================================

async function parseJavaLesson(filePath: string, epochNumber: number, lessonNumber: number): Promise<Lesson> {
  const javaCode = await fs.readFile(filePath, 'utf-8');

  // Extract basic info
  const builderMatch = javaCode.match(/new\s+Lesson\.Builder\s*\(\s*"([^"]+)"\s*,\s*"([^"]+)"\)/);
  const lessonId = builderMatch ? builderMatch[1] : `epoch-${epochNumber}-lesson-${lessonNumber}`;
  const lessonTitle = builderMatch ? builderMatch[2] : 'Untitled Lesson';

  const estimatedMinutesMatch = javaCode.match(/\.estimatedMinutes\s*\(\s*(\d+)\s*\)/);
  const estimatedMinutes = estimatedMinutesMatch ? parseInt(estimatedMinutesMatch[1]) : 30;

  // Extract content sections
  const sections = extractContentSections(javaCode);

  // Build markdown content
  let body = `# ${lessonTitle}\n\n`;

  for (const section of sections) {
    let heading = section.title;
    let content = section.content;

    switch (section.type) {
      case 'THEORY':
        body += `## ${heading}\n\n${content}\n\n`;
        break;
      case 'ANALOGY':
        body += `## 💡 ${heading}\n\n${content}\n\n`;
        break;
      case 'KEY_POINT':
        body += `## 🎯 ${heading}\n\n${content}\n\n`;
        break;
      case 'EXAMPLE':
        body += `## 💻 ${heading}\n\n\`\`\`java\n${content}\n\`\`\`\n\n`;
        break;
      case 'WARNING':
        body += `## ⚠️ ${heading}\n\n${content}\n\n`;
        break;
    }
  }

  // Extract challenges
  const challengeBlocks = findChallengeBlocks(javaCode);
  const exercises: Exercise[] = [];
  const quizQuestions: QuizQuestion[] = [];

  for (let i = 0; i < challengeBlocks.length; i++) {
    const block = challengeBlocks[i];
    const challengeCode = javaCode.substring(block.start, block.end);
    const challenge = extractChallenge(challengeCode, block.name, lessonId, i);

    if (challenge) {
      if ('question' in challenge) {
        quizQuestions.push(challenge);
      } else {
        exercises.push(challenge);
      }
    }
  }

  // Determine difficulty
  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (epochNumber >= 8) difficulty = 'advanced';
  else if (epochNumber >= 4) difficulty = 'intermediate';

  // Determine type
  let type: 'reading' | 'interactive' | 'project' = 'reading';
  if (exercises.length > 0 || quizQuestions.length > 0) {
    type = 'interactive';
  }

  const lesson: Lesson = {
    id: lessonId,
    title: lessonTitle,
    type,
    order: lessonNumber,
    estimatedMinutes,
    difficulty,
    tags: [`epoch-${epochNumber}`, difficulty],
    content: {
      format: 'markdown',
      body: body.trim(),
      codeExamples: []
    },
    exercises
  };

  // Add quiz if there are quiz questions
  if (quizQuestions.length > 0) {
    lesson.quiz = {
      id: `${lessonId}-quiz`,
      passingScore: 70,
      questions: quizQuestions
    };
  }

  return lesson;
}

// ============================================================================
// Course Conversion
// ============================================================================

async function convertJavaCourse(sourceDir: string, outputPath: string) {
  console.log('🔄 Converting Java course with full interactive content...');

  const contentDir = path.join(sourceDir, 'src/main/java/com/socraticjava/content');
  const entries = await fs.readdir(contentDir, { withFileTypes: true });
  const epochDirs = entries
    .filter(e => e.isDirectory() && e.name.match(/^epoch\d+$/))
    .sort((a, b) => {
      const numA = parseInt(a.name.replace('epoch', ''));
      const numB = parseInt(b.name.replace('epoch', ''));
      return numA - numB;
    });

  console.log(`📁 Found ${epochDirs.length} epochs`);

  const modules: Module[] = [];
  let totalLessons = 0;
  let totalChallenges = 0;
  let totalTestCases = 0;
  let challengesByType = {
    MULTIPLE_CHOICE: 0,
    FREE_CODING: 0,
    CODE_COMPLETION: 0,
    CONCEPTUAL: 0
  };

  for (const epochDir of epochDirs) {
    const epochNumber = parseInt(epochDir.name.replace('epoch', ''));
    const epochPath = path.join(contentDir, epochDir.name);

    const lessonFiles = (await fs.readdir(epochPath))
      .filter(f => f.match(/^Lesson\d+Content\.java$/))
      .sort((a, b) => {
        const numA = parseInt(a.match(/\d+/)![0]);
        const numB = parseInt(b.match(/\d+/)![0]);
        return numA - numB;
      });

    if (lessonFiles.length === 0) continue;

    console.log(`   📖 Epoch ${epochNumber}: Processing ${lessonFiles.length} lessons...`);

    const lessons: Lesson[] = [];

    for (let i = 0; i < lessonFiles.length; i++) {
      const file = lessonFiles[i];
      const filePath = path.join(epochPath, file);

      try {
        const lesson = await parseJavaLesson(filePath, epochNumber, i + 1);
        lessons.push(lesson);

        // Count challenges
        totalChallenges += lesson.exercises.length;
        if (lesson.quiz) {
          totalChallenges += lesson.quiz.questions.length;
        }

        // Count by type
        for (const exercise of lesson.exercises) {
          const type = exercise.challengeType as keyof typeof challengesByType;
          if (type && type in challengesByType) {
            challengesByType[type]++;
          }
          totalTestCases += exercise.testCases.length;
        }

        if (lesson.quiz) {
          challengesByType.MULTIPLE_CHOICE += lesson.quiz.questions.length;
        }

      } catch (error: any) {
        console.warn(`   ⚠️  Skipping ${file}: ${error.message}`);
        continue;
      }
    }

    let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
    if (epochNumber >= 8) difficulty = 'advanced';
    else if (epochNumber >= 4) difficulty = 'intermediate';

    const totalMinutes = lessons.reduce((sum, l) => sum + l.estimatedMinutes, 0);
    const estimatedHours = Math.ceil(totalMinutes / 60 * 10) / 10;

    modules.push({
      id: `epoch-${epochNumber}`,
      title: `Epoch ${epochNumber}`,
      description: `Epoch ${epochNumber} - ${lessons[0]?.title.split(':')[0] || 'Java Fundamentals'}`,
      order: epochNumber,
      estimatedHours,
      prerequisites: epochNumber === 1 ? [] : [`epoch-${epochNumber - 1}`],
      lessons
    });

    totalLessons += lessons.length;
    console.log(`   ✓ Extracted ${lessons.length} lessons, ${lessons.reduce((sum, l) => sum + l.exercises.length + (l.quiz?.questions.length || 0), 0)} challenges`);
  }

  const totalHours = modules.reduce((sum, m) => sum + m.estimatedHours, 0);

  const course: Course = {
    courseMetadata: {
      id: 'java',
      language: 'java',
      version: '1.0.0',
      displayName: 'Java From First Principles to Full-Stack',
      description: 'Master Java from absolute beginner to job-ready full-stack developer through Socratic mentorship and hands-on projects.',
      totalModules: modules.length,
      estimatedHours: Math.round(totalHours * 10) / 10,
      difficulty: 'beginner-to-advanced',
      prerequisites: [],
      learningOutcomes: [
        'Master Java fundamentals including variables, control flow, and object-oriented programming',
        'Build real-world applications using Java collections, file I/O, and exception handling',
        'Develop full-stack web applications with Spring Boot and REST APIs',
        'Write clean, maintainable code following industry best practices',
        'Deploy production-ready applications to the cloud'
      ],
      icon: '☕',
      color: '#007396'
    },
    modules,
    languageConfig: {
      executionEngine: 'java-compiler',
      compilerOptions: {
        version: '17',
        flags: ['-encoding', 'UTF-8', '-Xlint:unchecked']
      },
      editorSettings: {
        defaultTemplate: 'public class Main {\n    public static void main(String[] args) {\n        // Your code here\n    }\n}',
        fileExtension: '.java',
        monacoLanguageId: 'java',
        tabSize: 4,
        insertSpaces: true
      },
      sandboxConstraints: {
        maxExecutionTimeMs: 10000,
        maxMemoryMB: 256,
        maxOutputChars: 10000,
        allowedPackages: ['java.lang', 'java.util', 'java.io', 'java.math', 'java.time'],
        blockedPackages: ['java.net', 'java.nio.file', 'java.lang.reflect', 'javax.swing']
      }
    }
  };

  const outputDir = path.dirname(outputPath);
  await fs.mkdir(outputDir, { recursive: true });
  await fs.writeFile(outputPath, JSON.stringify(course, null, 2), 'utf-8');

  console.log(`\n✅ Conversion complete!`);
  console.log(`   📚 ${modules.length} epochs (modules)`);
  console.log(`   📄 ${totalLessons} lessons`);
  console.log(`   🎯 ${totalChallenges} challenges total:`);
  console.log(`      - ${challengesByType.MULTIPLE_CHOICE} multiple choice`);
  console.log(`      - ${challengesByType.FREE_CODING} free coding`);
  console.log(`      - ${challengesByType.CODE_COMPLETION} code completion`);
  console.log(`      - ${challengesByType.CONCEPTUAL} conceptual`);
  console.log(`   🧪 ${totalTestCases} test cases`);
  console.log(`   ⏱️  ~${totalHours.toFixed(1)} hours of content`);
  console.log(`   💾 Output: ${outputPath}`);
}

// ============================================================================
// Main Execution
// ============================================================================

const sourceDir = process.argv[2] || '/tmp/Java-Training-Course/socratic-java-mentor';
const outputPath = process.argv[3] || path.join(__dirname, '../content/courses/java/course.json');

convertJavaCourse(sourceDir, outputPath)
  .then(() => {
    console.log('\n✨ Java course with full interactive content ready!');
    process.exit(0);
  })
  .catch((error) => {
    console.error('❌ Conversion failed:', error);
    console.error(error.stack);
    process.exit(1);
  });
