#!/usr/bin/env tsx

/**
 * Convert Python Training Course to platform format (Complete version)
 * Source: JSON lesson files from Python-Training-Course repository
 * Output: content/courses/python/course.json
 */

import * as fs from 'fs/promises';
import * as path from 'path';

interface LegacyLesson {
  title: string;
  estimated_time: string;
  concept: string;
  code_example: {
    language: string;
    code: string;
    output?: string;
  };
  syntax_breakdown: string;
  exercise: {
    instructions: string;
    starter_code: string;
    hint: string;
  };
  solution: {
    code: string;
    explanation: string;
    common_mistakes: string;
  };
  key_takeaways: string;
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

// Convert HTML to markdown (simple conversion)
function htmlToMarkdown(html: string): string {
  let md = html;

  // Convert HTML tags to markdown
  md = md.replace(/<h1>(.*?)<\/h1>/g, '# $1\n');
  md = md.replace(/<h2>(.*?)<\/h2>/g, '## $1\n');
  md = md.replace(/<h3>(.*?)<\/h3>/g, '### $1\n');
  md = md.replace(/<h4>(.*?)<\/h4>/g, '#### $1\n');
  md = md.replace(/<p>(.*?)<\/p>/g, '$1\n\n');
  md = md.replace(/<strong>(.*?)<\/strong>/g, '**$1**');
  md = md.replace(/<code>(.*?)<\/code>/g, '`$1`');
  md = md.replace(/<pre><code>(.*?)<\/code><\/pre>/gs, '```\n$1\n```\n');
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

// Convert legacy lesson to platform format
function convertLesson(legacyLesson: LegacyLesson, moduleNumber: number, lessonNumber: number): PlatformLesson {
  // Build markdown content
  let body = `# ${legacyLesson.title}\n\n`;

  // Add concept section
  if (legacyLesson.concept) {
    body += `## Understanding the Concept\n\n${htmlToMarkdown(legacyLesson.concept)}\n\n`;
  }

  // Add code example
  if (legacyLesson.code_example) {
    body += `## Code Example\n\n\`\`\`${legacyLesson.code_example.language.toLowerCase()}\n${legacyLesson.code_example.code}\n\`\`\`\n\n`;

    if (legacyLesson.code_example.output) {
      body += `**Output:**\n\`\`\`\n${legacyLesson.code_example.output}\n\`\`\`\n\n`;
    }
  }

  // Add syntax breakdown
  if (legacyLesson.syntax_breakdown) {
    body += `## Breaking Down the Syntax\n\n${htmlToMarkdown(legacyLesson.syntax_breakdown)}\n\n`;
  }

  // Add key takeaways
  if (legacyLesson.key_takeaways) {
    body += `## Key Takeaways\n\n${htmlToMarkdown(legacyLesson.key_takeaways)}\n\n`;
  }

  // Add common mistakes if available
  if (legacyLesson.solution?.common_mistakes) {
    body += `## Common Mistakes\n\n${htmlToMarkdown(legacyLesson.solution.common_mistakes)}\n\n`;
  }

  // Determine difficulty
  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (moduleNumber > 10) difficulty = 'advanced';
  else if (moduleNumber > 5) difficulty = 'intermediate';

  // Parse estimated time
  const timeMatch = legacyLesson.estimated_time.match(/(\d+)/);
  const estimatedMinutes = timeMatch ? parseInt(timeMatch[1]) : 15;

  const platformLesson: PlatformLesson = {
    id: `lesson-${String(moduleNumber).padStart(2, '0')}-${String(lessonNumber).padStart(2, '0')}`,
    title: legacyLesson.title,
    type: legacyLesson.exercise ? 'exercise' : 'reading',
    estimatedMinutes,
    difficulty,
    content: {
      format: 'markdown',
      body: body.trim()
    }
  };

  // Add exercise if present
  if (legacyLesson.exercise) {
    const hints = [];
    if (legacyLesson.exercise.hint) {
      const hintText = htmlToMarkdown(legacyLesson.exercise.hint);
      // Split hints by bullet points or line breaks
      const hintLines = hintText.split(/\n-\s*/).filter(h => h.trim());
      hints.push(...hintLines.slice(0, 5)); // Limit to 5 hints
    }

    platformLesson.exercises = [{
      type: 'coding',
      title: 'Practice Exercise',
      instructions: htmlToMarkdown(legacyLesson.exercise.instructions),
      starterCode: legacyLesson.exercise.starter_code || '',
      solution: legacyLesson.solution?.code || '',
      hints
    }];
  }

  return platformLesson;
}

async function convertPythonCourse(sourceDir: string, outputPath: string) {
  console.log('🔄 Converting Python course...');

  // Find all module directories
  const modulesDir = path.join(sourceDir, 'modules');
  const moduleDirs = (await fs.readdir(modulesDir))
    .filter(d => d.startsWith('module_'))
    .sort();

  console.log(`📁 Found ${moduleDirs.length} modules`);

  // Convert each module
  const modules: PlatformModule[] = [];
  let totalLessons = 0;

  for (const moduleDir of moduleDirs) {
    const moduleNumber = parseInt(moduleDir.replace('module_', ''));
    const modulePath = path.join(modulesDir, moduleDir);

    // Find all lesson files
    const lessonFiles = (await fs.readdir(modulePath))
      .filter(f => f.startsWith('lesson_') && f.endsWith('.json'))
      .sort();

    console.log(`   📖 Module ${moduleNumber}: ${lessonFiles.length} lessons`);

    const lessons: PlatformLesson[] = [];

    for (let i = 0; i < lessonFiles.length; i++) {
      const file = lessonFiles[i];
      const filePath = path.join(modulePath, file);

      try {
        const content = await fs.readFile(filePath, 'utf-8');
        const legacyLesson: LegacyLesson = JSON.parse(content);

        const lesson = convertLesson(legacyLesson, moduleNumber, i + 1);
        lessons.push(lesson);
      } catch (error: any) {
        console.warn(`   ⚠️  Skipping ${file}: ${error.message}`);
        continue;
      }
    }

    // Determine module difficulty
    let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
    if (moduleNumber > 10) difficulty = 'advanced';
    else if (moduleNumber > 5) difficulty = 'intermediate';

    // Calculate module hours
    const totalMinutes = lessons.reduce((sum, l) => sum + l.estimatedMinutes, 0);
    const estimatedHours = Math.ceil(totalMinutes / 60);

    modules.push({
      id: `module-${String(moduleNumber).padStart(2, '0')}`,
      title: `Module ${moduleNumber}`,
      description: `Module ${moduleNumber}`,
      difficulty,
      estimatedHours,
      lessons
    });

    totalLessons += lessons.length;
  }

  // Create course
  const course: PlatformCourse = {
    id: 'python',
    language: 'python',
    title: 'Python Full-Stack Development',
    description: 'Master Python from fundamentals to full-stack development, including web applications with Flask, databases, APIs, and deployment.',
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
const sourceDir = process.argv[2] || '/tmp/Python-Training-Course/content';
const outputPath = process.argv[3] || path.join(__dirname, '../content/courses/python/course.json');

convertPythonCourse(sourceDir, outputPath)
  .then(() => {
    console.log('\n✨ Python course ready!');
    process.exit(0);
  })
  .catch((error) => {
    console.error('❌ Conversion failed:', error);
    process.exit(1);
  });
