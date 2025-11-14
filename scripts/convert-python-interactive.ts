#!/usr/bin/env tsx

/**
 * Enhanced Python Course Converter - Extracts ALL Interactive Content
 * Source: /tmp/Python-Training-Course/
 * - 80 JSON lesson files with exercises
 * - 14 separate quiz JSON files
 * Output: Complete interactive course with challenges and quizzes
 */

import * as fs from 'fs/promises';
import * as path from 'path';

// ============================================================================
// Type Definitions
// ============================================================================

// OLD FORMAT (Modules 1-4)
interface OldFormatLesson {
  title: string;
  estimated_time: string;
  concept: string;
  code_example?: {
    language: string;
    code: string;
    output: string;
  };
  syntax_breakdown?: string;
  exercise: {
    instructions: string;
    starter_code: string;
    hint: string;
  };
  solution: {
    code: string;
    explanation: string;
    common_mistakes?: string;
  };
  key_takeaways: string;
}

// NEW FORMAT (Modules 5-14)
interface LegacyPythonLesson {
  lesson_id: string;
  title: string;
  module_id: number;
  order_index: number;
  description: string;
  estimated_minutes: number;
  content_blocks: ContentBlock[];
  checkpoint_quiz?: CheckpointQuiz;
}

interface ContentBlock {
  type: string;
  title?: string;
  content?: string;
  code?: string;
  explanation?: string;
  output?: string;
  instruction?: string;
  starter_code?: string;
  hint?: string;
  solution_code?: string;
  common_mistakes?: string[];
  takeaways?: string[];
}

interface CheckpointQuiz {
  questions: QuizQuestionLegacy[];
}

interface QuizQuestionLegacy {
  id: number;
  type: 'multiple_choice' | 'true_false' | 'code_output';
  question: string;
  options?: string[];
  correct_answer: number | string | boolean;
  explanation: string;
  code?: string;
}

interface LegacyQuizFile {
  title: string;
  description: string;
  estimated_time: string;
  passing_score: number;
  questions: QuizQuestionLegacy[];
}

// Platform types (from interactive.ts)
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

interface QuizQuestion {
  type: 'MULTIPLE_CHOICE' | 'TRUE_FALSE' | 'CODE_OUTPUT';
  id: string;
  title: string;
  description: string;
  question?: string;
  options?: string[];
  correctAnswer: number | string | boolean;
  explanation: string;
  code?: string;
  language?: string;
  correctOutput?: string;
}

interface Quiz {
  id: string;
  title: string;
  description: string;
  moduleId: string;
  passingScore: number;
  estimatedMinutes: number;
  questions: QuizQuestion[];
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
  quizzes: Quiz[];
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
      totalQuizzes: number;
      totalQuestions: number;
    };
  };
}

// ============================================================================
// HTML to Markdown Converter
// ============================================================================

function htmlToMarkdown(html: string | undefined): string {
  if (!html) return '';

  let md = html;

  // Convert HTML tags to markdown
  md = md.replace(/<h1>(.*?)<\/h1>/g, '# $1\n');
  md = md.replace(/<h2>(.*?)<\/h2>/g, '## $1\n');
  md = md.replace(/<h3>(.*?)<\/h3>/g, '### $1\n');
  md = md.replace(/<h4>(.*?)<\/h4>/g, '#### $1\n');
  md = md.replace(/<p>(.*?)<\/p>/g, '$1\n\n');
  md = md.replace(/<strong>(.*?)<\/strong>/g, '**$1**');
  md = md.replace(/<code>(.*?)<\/code>/g, '`$1`');
  md = md.replace(/<pre><code>([\s\S]*?)<\/code><\/pre>/g, '```\n$1\n```\n');
  md = md.replace(/<li>(.*?)<\/li>/g, '- $1\n');
  md = md.replace(/<ul>/g, '\n');
  md = md.replace(/<\/ul>/g, '\n');
  md = md.replace(/<ol>/g, '\n');
  md = md.replace(/<\/ol>/g, '\n');
  md = md.replace(/<br\s*\/?>/g, '\n');

  // Clean up extra whitespace
  md = md.replace(/\n{3,}/g, '\n\n');

  return md.trim();
}

// ============================================================================
// Converters
// ============================================================================

function convertContentBlocks(blocks: ContentBlock[]): ContentSection[] {
  const sections: ContentSection[] = [];

  for (const block of blocks) {
    switch (block.type) {
      case 'the_simplifier':
        sections.push({
          type: 'ANALOGY',
          title: block.title || 'Understanding the Concept',
          content: htmlToMarkdown(block.content)
        });
        break;

      case 'the_coder':
        if (block.code) {
          sections.push({
            type: 'EXAMPLE',
            title: block.title || 'Code Example',
            content: htmlToMarkdown(block.explanation || ''),
            code: block.code,
            language: 'python'
          });
        }
        break;

      case 'key_takeaways':
        if (block.takeaways && block.takeaways.length > 0) {
          sections.push({
            type: 'KEY_POINT',
            title: 'Key Takeaways',
            content: block.takeaways.map(t => `- ${htmlToMarkdown(t)}`).join('\n')
          });
        }
        break;

      default:
        // Generic theory section
        if (block.content) {
          sections.push({
            type: 'THEORY',
            title: block.title || 'Concept',
            content: htmlToMarkdown(block.content),
            code: block.code,
            language: block.code ? 'python' : undefined
          });
        }
    }
  }

  return sections;
}

function convertExerciseToChallenge(
  block: ContentBlock,
  lessonId: string,
  index: number
): FreeCodingChallenge | null {
  if (!block.instruction && block.type !== 'the_coder') return null;

  const hints: Hint[] = [];
  if (block.hint) {
    hints.push({
      level: 1,
      text: htmlToMarkdown(block.hint)
    });
  }

  const commonMistakes: CommonMistake[] = [];
  if (block.common_mistakes && Array.isArray(block.common_mistakes)) {
    block.common_mistakes.forEach((mistake, i) => {
      commonMistakes.push({
        mistake: htmlToMarkdown(mistake),
        consequence: 'This is a common error that beginners make.',
        correction: 'Review the syntax and examples carefully.'
      });
    });
  }

  return {
    type: 'FREE_CODING',
    id: `${lessonId}-challenge-${index}`,
    title: block.title || 'Practice Exercise',
    description: htmlToMarkdown(block.instruction || ''),
    instructions: htmlToMarkdown(block.instruction || ''),
    starterCode: block.starter_code || '# Write your code here\n',
    solution: block.solution_code || '',
    language: 'python',
    testCases: [
      {
        description: 'Code runs without errors',
        expectedOutput: '',
        isVisible: true
      }
    ],
    hints: hints.length > 0 ? hints : undefined,
    commonMistakes: commonMistakes.length > 0 ? commonMistakes : undefined,
    difficulty: 'beginner'
  };
}

function convertQuizQuestion(q: QuizQuestionLegacy, moduleId: string, quizIndex: number): QuizQuestion {
  const id = `quiz-${moduleId}-q${q.id || quizIndex}`;

  if (q.type === 'multiple_choice') {
    return {
      type: 'MULTIPLE_CHOICE',
      id,
      title: htmlToMarkdown(q.question),
      description: htmlToMarkdown(q.question),
      options: q.options?.map(htmlToMarkdown) || [],
      correctAnswer: typeof q.correct_answer === 'number' ? q.correct_answer : 0,
      explanation: htmlToMarkdown(q.explanation)
    };
  } else if (q.type === 'true_false') {
    return {
      type: 'TRUE_FALSE',
      id,
      title: htmlToMarkdown(q.question),
      description: htmlToMarkdown(q.question),
      question: htmlToMarkdown(q.question),
      correctAnswer: typeof q.correct_answer === 'boolean' ? q.correct_answer : q.correct_answer === 'true',
      explanation: htmlToMarkdown(q.explanation)
    };
  } else if (q.type === 'code_output') {
    return {
      type: 'CODE_OUTPUT',
      id,
      title: htmlToMarkdown(q.question),
      description: htmlToMarkdown(q.question),
      code: q.code || '',
      language: 'python',
      correctAnswer: String(q.correct_answer),
      correctOutput: String(q.correct_answer),
      explanation: htmlToMarkdown(q.explanation)
    };
  }

  // Default to multiple choice
  return {
    type: 'MULTIPLE_CHOICE',
    id,
    title: htmlToMarkdown(q.question),
    description: htmlToMarkdown(q.question),
    options: [],
    correctAnswer: 0,
    explanation: htmlToMarkdown(q.explanation)
  };
}

function convertOldFormatLesson(
  lesson: OldFormatLesson,
  moduleNumber: number,
  lessonNumber: number
): InteractiveLesson {
  const contentSections: ContentSection[] = [];

  // Add concept section
  if (lesson.concept) {
    contentSections.push({
      type: 'THEORY',
      title: 'Understanding the Concept',
      content: htmlToMarkdown(lesson.concept)
    });
  }

  // Add code example
  if (lesson.code_example) {
    contentSections.push({
      type: 'EXAMPLE',
      title: 'Code Example',
      content: `**Expected Output:**\n\`\`\`\n${lesson.code_example.output}\n\`\`\``,
      code: lesson.code_example.code,
      language: 'python'
    });
  }

  // Add syntax breakdown
  if (lesson.syntax_breakdown) {
    contentSections.push({
      type: 'THEORY',
      title: 'Syntax Breakdown',
      content: htmlToMarkdown(lesson.syntax_breakdown)
    });
  }

  // Add key takeaways
  if (lesson.key_takeaways) {
    contentSections.push({
      type: 'KEY_POINT',
      title: 'Key Takeaways',
      content: htmlToMarkdown(lesson.key_takeaways)
    });
  }

  // Convert exercise to challenge
  const challenges: FreeCodingChallenge[] = [];
  if (lesson.exercise) {
    const hints: Hint[] = [];
    if (lesson.exercise.hint) {
      hints.push({
        level: 1,
        text: htmlToMarkdown(lesson.exercise.hint)
      });
    }

    // Parse common mistakes from solution
    const commonMistakes: CommonMistake[] = [];
    if (lesson.solution.common_mistakes) {
      // Extract list items from HTML
      const mistakeText = htmlToMarkdown(lesson.solution.common_mistakes);
      const lines = mistakeText.split('\n').filter(l => l.trim());
      lines.forEach(line => {
        if (line.includes('❌') || line.includes('Wrong')) {
          commonMistakes.push({
            mistake: line.replace(/^-\s*/, '').trim(),
            consequence: 'This will cause an error',
            correction: 'Review the correct syntax in the examples'
          });
        }
      });
    }

    challenges.push({
      type: 'FREE_CODING',
      id: `module-${String(moduleNumber).padStart(2, '0')}-lesson-${String(lessonNumber).padStart(2, '0')}-challenge-1`,
      title: 'Practice Exercise',
      description: htmlToMarkdown(lesson.exercise.instructions),
      instructions: htmlToMarkdown(lesson.exercise.instructions),
      starterCode: lesson.exercise.starter_code || '# Write your code here\n',
      solution: lesson.solution.code || '',
      language: 'python',
      testCases: [
        {
          description: 'Code runs without errors',
          expectedOutput: '',
          isVisible: true
        }
      ],
      hints: hints.length > 0 ? hints : undefined,
      commonMistakes: commonMistakes.length > 0 ? commonMistakes : undefined,
      difficulty: 'beginner'
    });
  }

  const estimatedMinutes = parseInt(lesson.estimated_time) || 30;

  return {
    id: `module-${String(moduleNumber).padStart(2, '0')}-lesson-${String(lessonNumber).padStart(2, '0')}`,
    title: lesson.title,
    moduleId: `module-${String(moduleNumber).padStart(2, '0')}`,
    order: lessonNumber,
    estimatedMinutes,
    difficulty: 'beginner',
    contentSections,
    challenges
  };
}

function convertLesson(lesson: LegacyPythonLesson): InteractiveLesson {
  const contentSections = convertContentBlocks(lesson.content_blocks);

  const challenges: FreeCodingChallenge[] = [];
  lesson.content_blocks.forEach((block, index) => {
    if (block.instruction || block.starter_code) {
      const challenge = convertExerciseToChallenge(block, lesson.lesson_id, index);
      if (challenge) {
        challenges.push(challenge);
      }
    }
  });

  const moduleNumber = lesson.module_id;
  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (moduleNumber > 10) difficulty = 'advanced';
  else if (moduleNumber > 5) difficulty = 'intermediate';

  return {
    id: lesson.lesson_id,
    title: lesson.title,
    moduleId: `module-${String(moduleNumber).padStart(2, '0')}`,
    order: lesson.order_index,
    estimatedMinutes: lesson.estimated_minutes || 30,
    difficulty,
    contentSections,
    challenges
  };
}

async function loadQuizzes(quizzesDir: string): Promise<Map<number, Quiz>> {
  const quizMap = new Map<number, Quiz>();

  try {
    const files = await fs.readdir(quizzesDir);
    const quizFiles = files.filter(f => f.startsWith('quiz_') && f.endsWith('.json'));

    for (const file of quizFiles) {
      const match = file.match(/quiz_(\d+)\.json/);
      if (!match) continue;

      const quizNumber = parseInt(match[1]);
      const content = await fs.readFile(path.join(quizzesDir, file), 'utf-8');
      const quizData: LegacyQuizFile = JSON.parse(content);

      const questions = quizData.questions.map((q, i) =>
        convertQuizQuestion(q, String(quizNumber).padStart(2, '0'), i)
      );

      const estimatedMinutes = parseInt(quizData.estimated_time) || 15;

      quizMap.set(quizNumber, {
        id: `quiz-${String(quizNumber).padStart(2, '0')}`,
        title: quizData.title,
        description: quizData.description,
        moduleId: `module-${String(quizNumber).padStart(2, '0')}`,
        passingScore: quizData.passing_score || 70,
        estimatedMinutes,
        questions
      });
    }
  } catch (error: any) {
    console.warn(`⚠️  Could not load quizzes: ${error.message}`);
  }

  return quizMap;
}

// ============================================================================
// Main Conversion
// ============================================================================

async function convertPythonInteractive(sourceDir: string, outputPath: string) {
  console.log('🔄 Converting Python course with ALL interactive content...\n');

  const modulesDir = path.join(sourceDir, 'content', 'modules');
  const quizzesDir = path.join(sourceDir, 'content', 'quizzes');

  // Load quizzes
  console.log('📋 Loading quizzes...');
  const quizzes = await loadQuizzes(quizzesDir);
  console.log(`   Found ${quizzes.size} quiz files\n`);

  // Load modules
  const moduleDirs = (await fs.readdir(modulesDir))
    .filter(d => d.startsWith('module_'))
    .sort();

  console.log(`📁 Found ${moduleDirs.length} modules\n`);

  const modules: Module[] = [];
  let totalLessons = 0;
  let totalChallenges = 0;
  let totalQuestions = 0;

  for (const moduleDir of moduleDirs) {
    const moduleNumber = parseInt(moduleDir.replace('module_', ''));
    const modulePath = path.join(modulesDir, moduleDir);

    const lessonFiles = (await fs.readdir(modulePath))
      .filter(f => f.startsWith('lesson_') && f.endsWith('.json'))
      .sort();

    const lessons: InteractiveLesson[] = [];
    let moduleChallenges = 0;

    for (const file of lessonFiles) {
      try {
        const content = await fs.readFile(path.join(modulePath, file), 'utf-8');
        const rawLesson = JSON.parse(content);

        let lesson: InteractiveLesson;

        // Detect format: new format has content_blocks, old format has concept/exercise
        if (rawLesson.content_blocks) {
          // New format (modules 5-14)
          lesson = convertLesson(rawLesson as LegacyPythonLesson);
        } else {
          // Old format (modules 1-4)
          const lessonNumber = parseInt(file.match(/lesson_(\d+)\.json/)?.[1] || '1');
          lesson = convertOldFormatLesson(rawLesson as OldFormatLesson, moduleNumber, lessonNumber);
        }

        lessons.push(lesson);
        moduleChallenges += lesson.challenges.length;
      } catch (error: any) {
        console.warn(`   ⚠️  Skipping ${file}: ${error.message}`);
      }
    }

    let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
    if (moduleNumber > 10) difficulty = 'advanced';
    else if (moduleNumber > 5) difficulty = 'intermediate';

    const totalMinutes = lessons.reduce((sum, l) => sum + l.estimatedMinutes, 0);
    const estimatedHours = Math.ceil(totalMinutes / 60);

    const moduleQuizzes: Quiz[] = [];
    const quiz = quizzes.get(moduleNumber);
    if (quiz) {
      moduleQuizzes.push(quiz);
      totalQuestions += quiz.questions.length;
    }

    // Module titles based on actual content
    const moduleTitles: Record<number, { title: string; description: string }> = {
      1: { title: 'The Absolute Basics', description: 'Introduction to programming and Python fundamentals' },
      2: { title: 'Variables', description: 'Understanding variables and data storage' },
      3: { title: 'Boolean Logic', description: 'Making decisions with True and False' },
      4: { title: 'Loops', description: 'Repeating actions with while and for loops' },
      5: { title: 'Lists & Tuples', description: 'Working with ordered collections' },
      6: { title: 'Functions', description: 'Creating reusable code blocks' },
      7: { title: 'Dictionaries', description: 'Managing key-value pairs' },
      8: { title: 'Exception Handling', description: 'Handling errors gracefully' },
      9: { title: 'File I/O', description: 'Reading and writing files' },
      10: { title: 'Modules & Packages', description: 'Organizing and reusing code' },
      11: { title: 'Classes & Objects (OOP)', description: 'Object-oriented programming fundamentals' },
      12: { title: 'Decorators', description: 'Advanced function modification techniques' },
      13: { title: 'HTTP & Web APIs', description: 'Working with web services' },
      14: { title: 'Sharing Your Work', description: 'Project planning and deployment' }
    };

    const moduleInfo = moduleTitles[moduleNumber] || {
      title: `Module ${moduleNumber}`,
      description: `Python programming fundamentals - Module ${moduleNumber}`
    };

    modules.push({
      id: `module-${String(moduleNumber).padStart(2, '0')}`,
      title: moduleInfo.title,
      description: moduleInfo.description,
      difficulty,
      estimatedHours,
      lessons,
      quizzes: moduleQuizzes
    });

    console.log(`   ✅ Module ${moduleNumber}: ${lessons.length} lessons, ${moduleChallenges} challenges, ${moduleQuizzes.length} quiz`);
    totalLessons += lessons.length;
    totalChallenges += moduleChallenges;
  }

  const course: Course = {
    id: 'python',
    language: 'python',
    title: 'Python Full-Stack Development',
    description: 'Master Python from fundamentals to full-stack development with 80+ interactive lessons, coding challenges, and quizzes.',
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
        totalChallenges,
        totalQuizzes: quizzes.size,
        totalQuestions
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
  console.log(`   📋 ${quizzes.size} quizzes`);
  console.log(`   ❓ ${totalQuestions} quiz questions`);
  console.log(`   ⏱️  ~${course.estimatedHours} hours of content`);
  console.log(`   💾 ${outputPath}\n`);
}

// ============================================================================
// Execute
// ============================================================================

const sourceDir = process.argv[2] || '/tmp/Python-Training-Course';
const outputPath = process.argv[3] || path.join(__dirname, '../content/courses/python/course.json');

convertPythonInteractive(sourceDir, outputPath)
  .then(() => {
    console.log('✨ Python interactive course ready!');
    process.exit(0);
  })
  .catch((error) => {
    console.error('❌ Conversion failed:', error);
    process.exit(1);
  });
