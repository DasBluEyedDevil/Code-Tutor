#!/usr/bin/env tsx

/**
 * Enhanced Kotlin Course Converter - Extracts ALL Interactive Content
 * Source: /tmp/Kotlin-Training-Course/src/main/resources/
 * - 63 markdown lessons across 7 parts
 * - 7 quiz JSON files (part1-quiz.json - part7-quiz.json)
 * - 7 challenge JSON files (part1-challenges.json - part7-challenges.json)
 * Output: Complete interactive course with content, challenges, and quizzes
 */

import * as fs from 'fs/promises';
import * as path from 'path';

// ============================================================================
// Type Definitions
// ============================================================================

interface LegacyQuizFile {
  partNumber: number;
  partTitle: string;
  quizzes: LegacyQuiz[];
}

interface LegacyQuiz {
  lessonId: string;
  title: string;
  questions: LegacyQuizQuestion[];
}

interface LegacyQuizQuestion {
  id: string;
  type: 'multiple_choice' | 'true_false' | 'code_output';
  question: string;
  options?: string[];
  correctAnswer: number | boolean;
  explanation: string;
}

interface LegacyChallengeFile {
  challenges: LegacyChallenge[];
}

interface LegacyChallenge {
  id: string;
  lessonId: string;
  title: string;
  description: string;
  difficulty: number;
  starterCode: string;
  solution: string;
  hints?: string[];
  testCases?: LegacyTestCase[];
}

interface LegacyTestCase {
  description: string;
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
  difficulty?: 'beginner' | 'intermediate' | 'advanced';
}

interface TestCase {
  description: string;
  expectedOutput: any;
  isVisible: boolean;
}

interface Hint {
  level: number;
  text: string;
}

interface QuizQuestion {
  type: 'MULTIPLE_CHOICE' | 'TRUE_FALSE' | 'CODE_OUTPUT';
  id: string;
  title: string;
  description: string;
  question?: string;
  options?: string[];
  correctAnswer: number | boolean;
  explanation: string;
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
// Markdown Parser
// ============================================================================

function parseMarkdownLesson(markdown: string, lessonId: string): ContentSection[] {
  const sections: ContentSection[] = [];
  const lines = markdown.split('\n');

  let currentSection: ContentSection | null = null;
  let inCodeBlock = false;
  let codeContent: string[] = [];
  let codeLang = 'kotlin';

  for (let i = 0; i < lines.length; i++) {
    const line = lines[i];

    // Detect code blocks
    if (line.startsWith('```')) {
      if (!inCodeBlock) {
        inCodeBlock = true;
        codeLang = line.replace('```', '').trim() || 'kotlin';
        codeContent = [];
      } else {
        inCodeBlock = false;
        // Add code to current section or create new example section
        if (currentSection && codeContent.length > 0) {
          currentSection.code = codeContent.join('\n');
          currentSection.language = codeLang;
        }
        codeContent = [];
      }
      continue;
    }

    if (inCodeBlock) {
      codeContent.push(line);
      continue;
    }

    // Detect headings
    if (line.startsWith('## ')) {
      // Save previous section
      if (currentSection && currentSection.content.trim()) {
        sections.push(currentSection);
      }

      const title = line.replace('## ', '').trim();
      let sectionType: ContentSection['type'] = 'THEORY';

      // Determine section type by title
      if (title.toLowerCase().includes('concept') || title.toLowerCase().includes('analogy') || title.toLowerCase().includes('think of')) {
        sectionType = 'ANALOGY';
      } else if (title.toLowerCase().includes('example') || title.toLowerCase().includes('code')) {
        sectionType = 'EXAMPLE';
      } else if (title.toLowerCase().includes('key') || title.toLowerCase().includes('takeaway') || title.toLowerCase().includes('remember')) {
        sectionType = 'KEY_POINT';
      } else if (title.toLowerCase().includes('warning') || title.toLowerCase().includes('pitfall') || title.toLowerCase().includes('mistake')) {
        sectionType = 'WARNING';
      } else if (title.toLowerCase().includes('experiment') || title.toLowerCase().includes('try')) {
        sectionType = 'EXPERIMENT';
      }

      currentSection = {
        type: sectionType,
        title,
        content: ''
      };
    } else if (line.startsWith('# ')) {
      // Main title - skip or use as first section
      continue;
    } else {
      // Regular content
      if (currentSection) {
        currentSection.content += line + '\n';
      } else if (line.trim()) {
        // Create initial section if none exists
        currentSection = {
          type: 'THEORY',
          title: 'Introduction',
          content: line + '\n'
        };
      }
    }
  }

  // Save last section
  if (currentSection && currentSection.content.trim()) {
    sections.push(currentSection);
  }

  return sections.filter(s => s.content.trim().length > 0);
}

function extractEstimatedTime(markdown: string): number {
  const match = markdown.match(/\*\*Estimated Time\*\*:\s*(\d+)\s*minutes?/i);
  if (match) {
    return parseInt(match[1]);
  }
  return 30; // default
}

function extractTitle(markdown: string): string {
  const match = markdown.match(/^#\s+(.+)$/m);
  return match ? match[1] : 'Untitled Lesson';
}

// ============================================================================
// Converters
// ============================================================================

function convertQuizQuestion(q: LegacyQuizQuestion): QuizQuestion {
  if (q.type === 'multiple_choice') {
    return {
      type: 'MULTIPLE_CHOICE',
      id: q.id,
      title: q.question,
      description: q.question,
      options: q.options || [],
      correctAnswer: q.correctAnswer as number,
      explanation: q.explanation
    };
  } else if (q.type === 'true_false') {
    return {
      type: 'TRUE_FALSE',
      id: q.id,
      title: q.question,
      description: q.question,
      question: q.question,
      correctAnswer: q.correctAnswer as boolean,
      explanation: q.explanation
    };
  } else {
    return {
      type: 'MULTIPLE_CHOICE',
      id: q.id,
      title: q.question,
      description: q.question,
      options: [],
      correctAnswer: 0,
      explanation: q.explanation
    };
  }
}

function convertChallenge(c: LegacyChallenge): FreeCodingChallenge {
  const testCases: TestCase[] = [];

  if (c.testCases && c.testCases.length > 0) {
    c.testCases.forEach(tc => {
      testCases.push({
        description: tc.description,
        expectedOutput: tc.expectedOutput,
        isVisible: true
      });
    });
  } else {
    testCases.push({
      description: 'Code compiles and runs successfully',
      expectedOutput: '',
      isVisible: true
    });
  }

  const hints: Hint[] = [];
  if (c.hints && c.hints.length > 0) {
    c.hints.forEach((hint, i) => {
      hints.push({
        level: i + 1,
        text: hint
      });
    });
  }

  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (c.difficulty > 3) difficulty = 'advanced';
  else if (c.difficulty > 1) difficulty = 'intermediate';

  return {
    type: 'FREE_CODING',
    id: c.id,
    title: c.title,
    description: c.description,
    instructions: c.description,
    starterCode: c.starterCode,
    solution: c.solution,
    language: 'kotlin',
    testCases,
    hints: hints.length > 0 ? hints : undefined,
    difficulty
  };
}

// ============================================================================
// Main Conversion
// ============================================================================

async function convertKotlinInteractive(sourceDir: string, outputPath: string) {
  console.log('🔄 Converting Kotlin course with ALL interactive content...\n');

  const resourcesDir = path.join(sourceDir, 'src', 'main', 'resources');
  const lessonsDir = path.join(resourcesDir, 'lessons');
  const quizzesDir = path.join(resourcesDir, 'quizzes');
  const challengesDir = path.join(resourcesDir, 'challenges');

  // Load parts (modules)
  const parts = ['part1', 'part2', 'part3', 'part4', 'part5', 'part6', 'part7'];
  const modules: Module[] = [];
  let totalLessons = 0;
  let totalChallenges = 0;
  let totalQuizzes = 0;
  let totalQuestions = 0;

  for (let partIndex = 0; partIndex < parts.length; partIndex++) {
    const partName = parts[partIndex];
    const partNumber = partIndex + 1;
    const lessonPartDir = path.join(lessonsDir, partName);

    console.log(`📁 Processing ${partName}...`);

    // Load lessons
    const lessons: InteractiveLesson[] = [];
    try {
      const lessonFiles = (await fs.readdir(lessonPartDir))
        .filter(f => f.endsWith('.md'))
        .sort();

      for (let i = 0; i < lessonFiles.length; i++) {
        const file = lessonFiles[i];
        const content = await fs.readFile(path.join(lessonPartDir, file), 'utf-8');

        const lessonId = file.replace('-expanded.md', '').replace('lesson-', '');
        const title = extractTitle(content);
        const estimatedMinutes = extractEstimatedTime(content);
        const contentSections = parseMarkdownLesson(content, lessonId);

        lessons.push({
          id: lessonId,
          title,
          moduleId: `module-${String(partNumber).padStart(2, '0')}`,
          order: i + 1,
          estimatedMinutes,
          difficulty: partNumber > 5 ? 'advanced' : partNumber > 3 ? 'intermediate' : 'beginner',
          contentSections,
          challenges: []
        });
      }
    } catch (error: any) {
      console.warn(`   ⚠️  No lessons found for ${partName}`);
    }

    // Load challenges
    try {
      const challengeFile = path.join(challengesDir, `${partName}-challenges.json`);
      const content = await fs.readFile(challengeFile, 'utf-8');
      const data: LegacyChallengeFile = JSON.parse(content);

      data.challenges.forEach(challenge => {
        const convertedChallenge = convertChallenge(challenge);

        // Find corresponding lesson and add challenge
        const lesson = lessons.find(l => l.id === challenge.lessonId || l.id.includes(challenge.lessonId));
        if (lesson) {
          lesson.challenges.push(convertedChallenge);
          totalChallenges++;
        }
      });
    } catch (error: any) {
      console.warn(`   ⚠️  No challenges found for ${partName}`);
    }

    // Load quizzes
    const quizzes: Quiz[] = [];
    try {
      const quizFile = path.join(quizzesDir, `${partName}-quiz.json`);
      const content = await fs.readFile(quizFile, 'utf-8');
      const data: LegacyQuizFile = JSON.parse(content);

      data.quizzes.forEach(quiz => {
        const questions = quiz.questions.map(convertQuizQuestion);
        totalQuestions += questions.length;

        quizzes.push({
          id: `${partName}-quiz-${quiz.lessonId}`,
          title: quiz.title,
          description: `Knowledge check for ${quiz.title}`,
          moduleId: `module-${String(partNumber).padStart(2, '0')}`,
          passingScore: 70,
          estimatedMinutes: Math.ceil(questions.length * 2),
          questions
        });
      });
      totalQuizzes += quizzes.length;
    } catch (error: any) {
      console.warn(`   ⚠️  No quizzes found for ${partName}`);
    }

    // Create module
    const totalMinutes = lessons.reduce((sum, l) => sum + l.estimatedMinutes, 0);
    const estimatedHours = Math.ceil(totalMinutes / 60);

    let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
    if (partNumber > 5) difficulty = 'advanced';
    else if (partNumber > 3) difficulty = 'intermediate';

    modules.push({
      id: `module-${String(partNumber).padStart(2, '0')}`,
      title: `Part ${partNumber}: Kotlin Fundamentals`,
      description: `Learn Kotlin programming - Part ${partNumber}`,
      difficulty,
      estimatedHours,
      lessons,
      quizzes
    });

    const lessonChallenges = lessons.reduce((sum, l) => sum + l.challenges.length, 0);
    console.log(`   ✅ ${lessons.length} lessons, ${lessonChallenges} challenges, ${quizzes.length} quizzes`);
    totalLessons += lessons.length;
  }

  const course: Course = {
    id: 'kotlin',
    language: 'kotlin',
    title: 'Kotlin Programming Complete Course',
    description: 'Master Kotlin from basics to advanced with 63+ interactive lessons, coding challenges, and comprehensive quizzes.',
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
        totalQuizzes,
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
  console.log(`   📋 ${totalQuizzes} quizzes`);
  console.log(`   ❓ ${totalQuestions} quiz questions`);
  console.log(`   ⏱️  ~${course.estimatedHours} hours of content`);
  console.log(`   💾 ${outputPath}\n`);
}

// ============================================================================
// Execute
// ============================================================================

const sourceDir = process.argv[2] || '/tmp/Kotlin-Training-Course';
const outputPath = process.argv[3] || path.join(__dirname, '../content/courses/kotlin/course.json');

convertKotlinInteractive(sourceDir, outputPath)
  .then(() => {
    console.log('✨ Kotlin interactive course ready!');
    process.exit(0);
  })
  .catch((error) => {
    console.error('❌ Conversion failed:', error);
    process.exit(1);
  });
