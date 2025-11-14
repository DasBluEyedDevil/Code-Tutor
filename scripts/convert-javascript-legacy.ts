#!/usr/bin/env tsx

/**
 * Convert JavaScript/TypeScript Training Course to platform format
 *
 * Source: module*.json files from JavaScript-TypeScript-Training-Course repository
 * Output: content/courses/javascript/course.json
 */

import * as fs from 'fs/promises';
import * as path from 'path';

interface LegacyChallenge {
  instructions: string;
  starterCode: string;
  testCases?: Array<{
    description: string;
    input: string;
    expectedOutput: string;
  }>;
  hint?: string;
}

interface LegacyLesson {
  id: string;
  title: string;
  conceptAnalogy?: string;
  codeExample?: string;
  syntaxBreakdown?: string;
  challenge?: LegacyChallenge;
  solution?: string;
  commonStickingPoints?: string;
}

interface LegacyModule {
  id: number;
  title: string;
  description: string;
  goal?: string;
  lessons: LegacyLesson[];
}

interface PlatformLesson {
  id: string;
  title: string;
  type: 'reading' | 'exercise' | 'project';
  estimatedMinutes: number;
  difficulty: 'beginner' | 'intermediate' | 'advanced';
  content: {
    format: 'markdown';
    body: string;
  };
  exercises?: Array<{
    type: 'coding';
    title: string;
    instructions: string;
    starterCode: string;
    solution: string;
    hints: string[];
    testCases?: Array<{
      description: string;
      input: string;
      expectedOutput: string;
    }>;
  }>;
}

interface PlatformModule {
  id: string;
  title: string;
  description: string;
  difficulty: 'beginner' | 'intermediate' | 'advanced';
  estimatedHours: number;
  lessons: PlatformLesson[];
}

interface PlatformCourse {
  id: string;
  language: string;
  title: string;
  description: string;
  difficulty: 'beginner';
  estimatedHours: number;
  prerequisites: string[];
  modules: PlatformModule[];
  metadata: {
    version: string;
    lastUpdated: string;
    author: string;
  };
}

// Convert legacy lesson to platform format
function convertLesson(legacyLesson: LegacyLesson, moduleNumber: number): PlatformLesson {
  // Build markdown content
  let body = `# ${legacyLesson.title}\n\n`;

  if (legacyLesson.conceptAnalogy) {
    body += `## Understanding the Concept\n\n${legacyLesson.conceptAnalogy}\n\n`;
  }

  if (legacyLesson.codeExample) {
    body += `## Code Example\n\n\`\`\`javascript\n${legacyLesson.codeExample}\n\`\`\`\n\n`;
  }

  if (legacyLesson.syntaxBreakdown) {
    body += `## Breaking Down the Syntax\n\n${legacyLesson.syntaxBreakdown}\n\n`;
  }

  if (legacyLesson.commonStickingPoints) {
    body += `## Common Mistakes & Tips\n\n${legacyLesson.commonStickingPoints}\n\n`;
  }

  // Determine difficulty based on module number
  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (moduleNumber > 10) difficulty = 'advanced';
  else if (moduleNumber > 5) difficulty = 'intermediate';

  // Estimate time (10-20 minutes per lesson)
  const estimatedMinutes = 15;

  const platformLesson: PlatformLesson = {
    id: `lesson-${String(moduleNumber).padStart(2, '0')}-${legacyLesson.id.replace('.', '-')}`,
    title: legacyLesson.title,
    type: legacyLesson.challenge ? 'exercise' : 'reading',
    estimatedMinutes,
    difficulty,
    content: {
      format: 'markdown',
      body: body.trim()
    }
  };

  // Add exercise if challenge exists
  if (legacyLesson.challenge) {
    platformLesson.exercises = [{
      type: 'coding',
      title: 'Practice Exercise',
      instructions: legacyLesson.challenge.instructions || '',
      starterCode: legacyLesson.challenge.starterCode || '',
      solution: legacyLesson.solution || '',
      hints: legacyLesson.challenge.hint ? [legacyLesson.challenge.hint] : [],
      testCases: legacyLesson.challenge.testCases
    }];
  }

  return platformLesson;
}

// Convert legacy module to platform format
function convertModule(legacyModule: LegacyModule): PlatformModule {
  const moduleNumber = legacyModule.id;
  const lessons = legacyModule.lessons.map(lesson => convertLesson(lesson, moduleNumber));

  // Determine difficulty
  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (moduleNumber > 10) difficulty = 'advanced';
  else if (moduleNumber > 5) difficulty = 'intermediate';

  // Estimate hours (sum of lesson minutes / 60)
  const totalMinutes = lessons.reduce((sum, lesson) => sum + lesson.estimatedMinutes, 0);
  const estimatedHours = Math.ceil(totalMinutes / 60);

  return {
    id: `module-${String(moduleNumber).padStart(2, '0')}`,
    title: legacyModule.title,
    description: legacyModule.description,
    difficulty,
    estimatedHours,
    lessons
  };
}

async function convertJavaScriptCourse(sourceDir: string, outputPath: string) {
  console.log('🔄 Converting JavaScript/TypeScript course...');

  // Find all module files
  const files = await fs.readdir(sourceDir);
  const moduleFiles = files
    .filter(f => f.match(/^module\d+\.json$/))
    .sort((a, b) => {
      const numA = parseInt(a.match(/\d+/)![0]);
      const numB = parseInt(b.match(/\d+/)![0]);
      return numA - numB;
    });

  console.log(`📁 Found ${moduleFiles.length} modules`);

  // Load and convert modules
  const modules: PlatformModule[] = [];
  let totalLessons = 0;

  for (const file of moduleFiles) {
    const filePath = path.join(sourceDir, file);
    const content = await fs.readFile(filePath, 'utf-8');
    const legacyModule: LegacyModule = JSON.parse(content);

    console.log(`   📖 Module ${legacyModule.id}: ${legacyModule.title} (${legacyModule.lessons.length} lessons)`);

    const platformModule = convertModule(legacyModule);
    modules.push(platformModule);
    totalLessons += legacyModule.lessons.length;
  }

  // Create course
  const course: PlatformCourse = {
    id: 'javascript-typescript',
    language: 'javascript',
    title: 'JavaScript & TypeScript Full-Stack Development',
    description: 'Master JavaScript and TypeScript from fundamentals to advanced concepts, including modern ES6+ features, async programming, and full-stack development.',
    difficulty: 'beginner',
    estimatedHours: modules.reduce((sum, m) => sum + m.estimatedHours, 0),
    prerequisites: [],
    modules,
    metadata: {
      version: '1.0.0',
      lastUpdated: new Date().toISOString().split('T')[0],
      author: 'Code Tutor'
    }
  };

  // Ensure output directory exists
  const outputDir = path.dirname(outputPath);
  await fs.mkdir(outputDir, { recursive: true });

  // Write output
  await fs.writeFile(outputPath, JSON.stringify(course, null, 2), 'utf-8');

  console.log(`✅ Conversion complete!`);
  console.log(`   📚 ${modules.length} modules`);
  console.log(`   📄 ${totalLessons} lessons`);
  console.log(`   ⏱️  ~${course.estimatedHours} hours of content`);
  console.log(`   💾 Output: ${outputPath}`);
}

// Main execution
const sourceDir = process.argv[2] || '/tmp/JavaScript-TypeScript-Training-Course/src/main/resources/content';
const outputPath = process.argv[3] || path.join(__dirname, '../content/courses/javascript/course.json');

convertJavaScriptCourse(sourceDir, outputPath)
  .then(() => {
    console.log('\n✨ JavaScript/TypeScript course ready!');
    process.exit(0);
  })
  .catch((error) => {
    console.error('❌ Conversion failed:', error);
    process.exit(1);
  });
