#!/usr/bin/env tsx

/**
 * Convert C# Training Course to platform format
 * Source: JSON lesson files from CSharp-Training-Course repository
 * Output: content/courses/csharp/course.json
 */

import * as fs from 'fs/promises';
import * as path from 'path';

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
    testCases?: any[];
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

  if (legacyLesson.simplifierConcept) {
    body += `## Understanding the Concept\n\n${legacyLesson.simplifierConcept}\n\n`;
  }

  if (legacyLesson.coderExample) {
    body += `## Code Example\n\n\`\`\`csharp\n${legacyLesson.coderExample}\n\`\`\`\n\n`;
  }

  if (legacyLesson.syntaxBreakdown && legacyLesson.syntaxBreakdown.length > 0) {
    body += `## Breaking Down the Syntax\n\n`;
    for (const item of legacyLesson.syntaxBreakdown) {
      body += `**\`${item.codeSnippet}\`**: ${item.explanation}\n\n`;
    }
  }

  if (legacyLesson.challenge?.commonStickingPoints && legacyLesson.challenge.commonStickingPoints.length > 0) {
    body += `## Common Mistakes & Tips\n\n`;
    for (const point of legacyLesson.challenge.commonStickingPoints) {
      body += `- ${point}\n`;
    }
    body += `\n`;
  }

  // Determine difficulty
  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (moduleNumber > 10) difficulty = 'advanced';
  else if (moduleNumber > 5) difficulty = 'intermediate';

  // Estimate time
  const estimatedMinutes = 15;

  const platformLesson: PlatformLesson = {
    id: `lesson-${String(moduleNumber).padStart(2, '0')}-${String(legacyLesson.lessonNumber).padStart(2, '0')}`,
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
    const hints = [];
    if (legacyLesson.challenge.hint) {
      hints.push(legacyLesson.challenge.hint);
    }
    if (legacyLesson.challenge.commonStickingPoints) {
      hints.push(...legacyLesson.challenge.commonStickingPoints);
    }

    platformLesson.exercises = [{
      type: 'coding',
      title: 'Practice Exercise',
      instructions: legacyLesson.challenge.instructions || '',
      starterCode: legacyLesson.challenge.starterCode || '',
      solution: legacyLesson.challenge.solutionCode || '',
      hints: hints.slice(0, 5) // Limit to 5 hints
    }];
  }

  return platformLesson;
}

async function convertCSharpCourse(sourceDir: string, outputPath: string) {
  console.log('🔄 Converting C# course...');

  // Find all module directories
  const moduleDirs = await fs.readdir(sourceDir);
  const moduleNumbers = moduleDirs
    .filter(d => d.startsWith('Module'))
    .map(d => parseInt(d.replace('Module', '')))
    .sort((a, b) => a - b);

  console.log(`📁 Found ${moduleNumbers.length} modules`);

  // Convert each module
  const modules: PlatformModule[] = [];
  let totalLessons = 0;

  for (const moduleNum of moduleNumbers) {
    const moduleDirName = `Module${String(moduleNum).padStart(2, '0')}`;
    const modulePath = path.join(sourceDir, moduleDirName);

    // Find all lesson files
    const lessonFiles = (await fs.readdir(modulePath))
      .filter(f => f.startsWith('Lesson') && f.endsWith('.json'))
      .sort();

    console.log(`   📖 Module ${moduleNum}: ${lessonFiles.length} lessons`);

    const lessons: PlatformLesson[] = [];

    for (const file of lessonFiles) {
      const filePath = path.join(modulePath, file);
      try {
        const content = await fs.readFile(filePath, 'utf-8');
        const legacyLesson: LegacyLesson = JSON.parse(content);

        const lesson = convertLesson(legacyLesson, moduleNum);
        lessons.push(lesson);
      } catch (error: any) {
        console.warn(`   ⚠️  Skipping ${file}: ${error.message}`);
        continue;
      }
    }

    // Determine module difficulty
    let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
    if (moduleNum > 10) difficulty = 'advanced';
    else if (moduleNum > 5) difficulty = 'intermediate';

    // Calculate module hours
    const totalMinutes = lessons.reduce((sum, l) => sum + l.estimatedMinutes, 0);
    const estimatedHours = Math.ceil(totalMinutes / 60);

    modules.push({
      id: `module-${String(moduleNum).padStart(2, '0')}`,
      title: `Module ${moduleNum}`,
      description: `Module ${moduleNum}`,
      difficulty,
      estimatedHours,
      lessons
    });

    totalLessons += lessons.length;
  }

  // Create course
  const course: PlatformCourse = {
    id: 'csharp',
    language: 'csharp',
    title: 'C# Programming',
    description: 'Master C# and .NET development from fundamentals to advanced concepts, including object-oriented programming, LINQ, async/await, and modern C# features.',
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
const sourceDir = process.argv[2] || '/tmp/CSharp-Training-Course/CSharpLearningPlatform/Content/Lessons';
const outputPath = process.argv[3] || path.join(__dirname, '../content/courses/csharp/course.json');

convertCSharpCourse(sourceDir, outputPath)
  .then(() => {
    console.log('\n✨ C# course ready!');
    process.exit(0);
  })
  .catch((error) => {
    console.error('❌ Conversion failed:', error);
    process.exit(1);
  });
