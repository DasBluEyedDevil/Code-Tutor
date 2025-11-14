#!/usr/bin/env tsx

/**
 * Enhanced JavaScript Course Converter - Extracts ALL Interactive Content
 * Source: /tmp/JavaScript-TypeScript-Training-Course/src/main/resources/content/
 * - 14 module JSON files with embedded challenges
 * - ~42 lessons total with conceptAnalogy, codeExample, syntaxBreakdown, challenge
 * Output: Complete interactive course with challenges
 */

import * as fs from 'fs/promises';
import * as path from 'path';

// ============================================================================
// Type Definitions
// ============================================================================

interface LegacyJSModule {
  id: number;
  title: string;
  description: string;
  goal: string;
  lessons: LegacyJSLesson[];
}

interface LegacyJSLesson {
  id: string;
  title: string;
  conceptAnalogy: string;
  codeExample: string;
  syntaxBreakdown: string;
  challenge?: LegacyChallenge;
  solution?: string;
  commonStickingPoints?: string;
}

interface LegacyChallenge {
  instructions: string;
  starterCode: string;
  testCases?: LegacyTestCase[];
  hint?: string;
}

interface LegacyTestCase {
  description: string;
  input: string;
  expectedOutput: string;
}

// Platform types
interface ContentSection {
  type: 'THEORY' | 'ANALOGY' | 'EXAMPLE' | 'KEY_POINT' | 'WARNING' | 'EXPERIMENT';
  title: string;
  content: string;
  code?: string;
  language?: string;
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
  difficulty?: 'beginner' | 'intermediate' | 'advanced';
}

interface TestCase {
  description: string;
  expectedOutput: any;
  isVisible: boolean;
  inputs?: any[];
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
}

interface InteractiveLesson {
  id: string;
  title: string;
  moduleId: string;
  order: number;
  estimatedMinutes: number;
  difficulty: 'beginner' | 'intermediate' | 'advanced';
  contentSections: ContentSection[];
  challenges: FreeCodingChallenge[];
  learningObjectives?: string[];
}

interface Module {
  id: string;
  title: string;
  description: string;
  difficulty: 'beginner' | 'intermediate' | 'advanced';
  estimatedHours: number;
  lessons: InteractiveLesson[];
}

interface Course {
  id: string;
  language: string;
  title: string;
  description: string;
  difficulty: 'beginner';
  estimatedHours: number;
  prerequisites: string[];
  modules: Module[];
  metadata: {
    version: string;
    lastUpdated: string;
    author: string;
    interactiveElements: {
      totalLessons: number;
      totalChallenges: number;
    };
  };
}

// ============================================================================
// Converters
// ============================================================================

function extractCodeFromMarkdown(text: string): { code: string; language: string } | null {
  const match = text.match(/```(\w+)?\n([\s\S]*?)```/);
  if (match) {
    return {
      language: match[1] || 'javascript',
      code: match[2].trim()
    };
  }
  return null;
}

function stripCodeBlocks(text: string): string {
  return text.replace(/```[\s\S]*?```/g, '').trim();
}

function convertLesson(legacyLesson: LegacyJSLesson, moduleId: string, order: number): InteractiveLesson {
  const sections: ContentSection[] = [];

  // Add analogy section
  if (legacyLesson.conceptAnalogy) {
    sections.push({
      type: 'ANALOGY',
      title: 'Understanding the Concept',
      content: legacyLesson.conceptAnalogy.trim()
    });
  }

  // Add code example section
  if (legacyLesson.codeExample) {
    const codeBlock = extractCodeFromMarkdown(legacyLesson.codeExample);
    if (codeBlock) {
      sections.push({
        type: 'EXAMPLE',
        title: 'Code Example',
        content: stripCodeBlocks(legacyLesson.codeExample),
        code: codeBlock.code,
        language: codeBlock.language
      });
    } else {
      sections.push({
        type: 'EXAMPLE',
        title: 'Code Example',
        content: legacyLesson.codeExample.trim(),
        language: 'javascript'
      });
    }
  }

  // Add syntax breakdown
  if (legacyLesson.syntaxBreakdown) {
    sections.push({
      type: 'THEORY',
      title: 'Breaking Down the Syntax',
      content: legacyLesson.syntaxBreakdown.trim()
    });
  }

  // Add common mistakes as warning
  if (legacyLesson.commonStickingPoints) {
    sections.push({
      type: 'WARNING',
      title: 'Common Pitfalls',
      content: legacyLesson.commonStickingPoints.trim()
    });
  }

  // Convert challenge
  const challenges: FreeCodingChallenge[] = [];
  if (legacyLesson.challenge) {
    const testCases: TestCase[] = [];

    if (legacyLesson.challenge.testCases) {
      legacyLesson.challenge.testCases.forEach(tc => {
        testCases.push({
          description: tc.description,
          expectedOutput: tc.expectedOutput,
          isVisible: true,
          inputs: tc.input ? [tc.input] : undefined
        });
      });
    } else {
      testCases.push({
        description: 'Code runs without errors',
        expectedOutput: '',
        isVisible: true
      });
    }

    const hints: Hint[] = [];
    if (legacyLesson.challenge.hint) {
      hints.push({
        level: 1,
        text: legacyLesson.challenge.hint
      });
    }

    const commonMistakes: CommonMistake[] = [];
    if (legacyLesson.commonStickingPoints) {
      // Parse common mistakes
      const mistakes = legacyLesson.commonStickingPoints.split(/\n\d+\.\s+/).filter(m => m.trim());
      mistakes.forEach(mistake => {
        commonMistakes.push({
          mistake: mistake.split('\n')[0].trim(),
          consequence: 'This can lead to errors or unexpected behavior.',
          correction: mistake.trim()
        });
      });
    }

    challenges.push({
      type: 'FREE_CODING',
      id: `${legacyLesson.id}-challenge`,
      title: 'Practice Challenge',
      description: legacyLesson.challenge.instructions,
      instructions: legacyLesson.challenge.instructions,
      starterCode: legacyLesson.challenge.starterCode || '// Write your code here\n',
      solution: legacyLesson.solution || '',
      language: 'javascript',
      testCases,
      hints: hints.length > 0 ? hints : undefined,
      commonMistakes: commonMistakes.length > 0 ? commonMistakes.slice(0, 3) : undefined,
      difficulty: 'beginner'
    });
  }

  // Determine difficulty based on module
  const moduleNum = parseInt(moduleId.replace('module-', ''));
  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (moduleNum > 10) difficulty = 'advanced';
  else if (moduleNum > 5) difficulty = 'intermediate';

  return {
    id: legacyLesson.id,
    title: legacyLesson.title,
    moduleId,
    order,
    estimatedMinutes: 25,
    difficulty,
    contentSections: sections,
    challenges
  };
}

// ============================================================================
// Main Conversion
// ============================================================================

async function convertJavaScriptInteractive(sourceDir: string, outputPath: string) {
  console.log('🔄 Converting JavaScript course with ALL interactive content...\n');

  const contentDir = path.join(sourceDir, 'src', 'main', 'resources', 'content');

  // Find all module files
  const files = await fs.readdir(contentDir);
  const moduleFiles = files
    .filter(f => f.startsWith('module') && f.endsWith('.json'))
    .sort((a, b) => {
      const numA = parseInt(a.match(/\d+/)?.[0] || '0');
      const numB = parseInt(b.match(/\d+/)?.[0] || '0');
      return numA - numB;
    });

  console.log(`📁 Found ${moduleFiles.length} module files\n`);

  const modules: Module[] = [];
  let totalLessons = 0;
  let totalChallenges = 0;

  for (const file of moduleFiles) {
    const moduleNum = parseInt(file.match(/\d+/)?.[0] || '0');
    const content = await fs.readFile(path.join(contentDir, file), 'utf-8');
    const legacyModule: LegacyJSModule = JSON.parse(content);

    const lessons: InteractiveLesson[] = [];
    let moduleChallenges = 0;

    legacyModule.lessons.forEach((legacyLesson, index) => {
      const moduleId = `module-${String(moduleNum).padStart(2, '0')}`;
      const lesson = convertLesson(legacyLesson, moduleId, index + 1);
      lessons.push(lesson);
      moduleChallenges += lesson.challenges.length;
    });

    let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
    if (moduleNum > 10) difficulty = 'advanced';
    else if (moduleNum > 5) difficulty = 'intermediate';

    const totalMinutes = lessons.reduce((sum, l) => sum + l.estimatedMinutes, 0);
    const estimatedHours = Math.ceil(totalMinutes / 60);

    modules.push({
      id: `module-${String(moduleNum).padStart(2, '0')}`,
      title: legacyModule.title,
      description: legacyModule.description || legacyModule.goal,
      difficulty,
      estimatedHours,
      lessons
    });

    console.log(`   ✅ Module ${moduleNum}: ${lessons.length} lessons, ${moduleChallenges} challenges`);
    totalLessons += lessons.length;
    totalChallenges += moduleChallenges;
  }

  const course: Course = {
    id: 'javascript',
    language: 'javascript',
    title: 'JavaScript & TypeScript Full Course',
    description: 'Master JavaScript from basics to advanced with TypeScript, covering 42+ interactive lessons with real-world challenges.',
    difficulty: 'beginner',
    estimatedHours: modules.reduce((sum, m) => sum + m.estimatedHours, 0),
    prerequisites: [],
    modules,
    metadata: {
      version: '2.0.0',
      lastUpdated: new Date().toISOString().split('T')[0],
      author: 'Code Tutor',
      interactiveElements: {
        totalLessons,
        totalChallenges
      }
    }
  };

  // Write output
  const outputDir = path.dirname(outputPath);
  await fs.mkdir(outputDir, { recursive: true });
  await fs.writeFile(outputPath, JSON.stringify(course, null, 2), 'utf-8');

  console.log('\n✅ Conversion complete!\n');
  console.log('📊 Statistics:');
  console.log(`   📚 ${modules.length} modules`);
  console.log(`   📄 ${totalLessons} lessons`);
  console.log(`   💻 ${totalChallenges} coding challenges`);
  console.log(`   ⏱️  ~${course.estimatedHours} hours of content`);
  console.log(`   💾 ${outputPath}\n`);
}

// ============================================================================
// Execute
// ============================================================================

const sourceDir = process.argv[2] || '/tmp/JavaScript-TypeScript-Training-Course';
const outputPath = process.argv[3] || path.join(__dirname, '../content/courses/javascript/course.json');

convertJavaScriptInteractive(sourceDir, outputPath)
  .then(() => {
    console.log('✨ JavaScript interactive course ready!');
    process.exit(0);
  })
  .catch((error) => {
    console.error('❌ Conversion failed:', error);
    process.exit(1);
  });
