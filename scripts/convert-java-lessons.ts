#!/usr/bin/env tsx

/**
 * Convert Java Training Course to platform format
 * Source: Java class files from socratic-java-mentor
 * Output: content/courses/java/course.json
 */

import * as fs from 'fs/promises';
import * as path from 'path';

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
    type: 'multiple-choice' | 'coding';
    title: string;
    instructions: string;
    options?: string[];
    correctAnswer?: string;
    starterCode?: string;
    solution?: string;
    hints?: string[];
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

// Extract string content from Java file
function extractStrings(javaCode: string, methodName: string): string[] {
  const results: string[] = [];

  // Match method calls like .addTheory("title", "content")
  const methodRegex = new RegExp(`\\.${methodName}\\([^)]*"([^"]*)"[^)]*,\\s*"`, 'g');
  let match;

  while ((match = methodRegex.exec(javaCode)) !== null) {
    const startIndex = match.index + match[0].length;

    // Extract multi-line string content
    let content = '';
    let quoteCount = 0;
    let escaped = false;

    for (let i = startIndex; i < javaCode.length; i++) {
      const char = javaCode[i];

      if (escaped) {
        content += char;
        escaped = false;
        continue;
      }

      if (char === '\\') {
        escaped = true;
        content += char;
        continue;
      }

      if (char === '"') {
        quoteCount++;
        if (quoteCount % 2 === 0) {
          // Even number of quotes - might be end of string
          const next = javaCode.substring(i + 1, i + 10);
          if (next.match(/^\s*\)/)) {
            // End of method call
            break;
          } else if (next.match(/^\s*\+/)) {
            // String concatenation continues
            continue;
          }
        }
      } else if (char !== '+' || quoteCount % 2 === 1) {
        content += char;
      }
    }

    results.push(content.trim());
  }

  return results;
}

// Extract lesson title from Java file
function extractTitle(javaCode: string): string {
  const match = javaCode.match(/new Lesson\.Builder\([^,]*,\s*"([^"]*)"/);
  return match ? match[1] : 'Untitled Lesson';
}

// Extract estimated minutes
function extractEstimatedMinutes(javaCode: string): number {
  const match = javaCode.match(/\.estimatedMinutes\((\d+)\)/);
  return match ? parseInt(match[1]) : 15;
}

// Parse a Java lesson file
async function parseJavaLesson(filePath: string, epochNumber: number, lessonNumber: number): Promise<PlatformLesson> {
  const javaCode = await fs.readFile(filePath, 'utf-8');

  const title = extractTitle(javaCode);
  const estimatedMinutes = extractEstimatedMinutes(javaCode);

  // Extract content sections
  const theories = extractStrings(javaCode, 'addTheory');
  const analogies = extractStrings(javaCode, 'addAnalogy');
  const keyPoints = extractStrings(javaCode, 'addKeyPoint');
  const examples = extractStrings(javaCode, 'addExample');

  // Build markdown content
  let body = `# ${title}\n\n`;

  // Add theories
  for (let i = 0; i < theories.length; i += 2) {
    const heading = theories[i];
    const content = theories[i + 1];
    if (heading && content) {
      body += `## ${heading}\n\n${content.replace(/\\n/g, '\n').replace(/\\"/g, '"')}\n\n`;
    }
  }

  // Add analogies
  for (let i = 0; i < analogies.length; i += 2) {
    const heading = analogies[i];
    const content = analogies[i + 1];
    if (heading && content) {
      body += `## 💡 ${heading}\n\n${content.replace(/\\n/g, '\n').replace(/\\"/g, '"')}\n\n`;
    }
  }

  // Add key points
  for (let i = 0; i < keyPoints.length; i += 2) {
    const heading = keyPoints[i];
    const content = keyPoints[i + 1];
    if (heading && content) {
      body += `## 🎯 ${heading}\n\n${content.replace(/\\n/g, '\n').replace(/\\"/g, '"')}\n\n`;
    }
  }

  // Add examples
  for (let i = 0; i < examples.length; i += 2) {
    const heading = examples[i];
    const content = examples[i + 1];
    if (heading && content) {
      body += `## 💻 ${heading}\n\n\`\`\`java\n${content.replace(/\\n/g, '\n').replace(/\\"/g, '"')}\n\`\`\`\n\n`;
    }
  }

  // Determine difficulty
  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (epochNumber >= 8) difficulty = 'advanced';
  else if (epochNumber >= 4) difficulty = 'intermediate';

  const lesson: PlatformLesson = {
    id: `lesson-${String(epochNumber).padStart(2, '0')}-${String(lessonNumber).padStart(2, '0')}`,
    title,
    type: javaCode.includes('addChallenge') ? 'exercise' : 'reading',
    estimatedMinutes,
    difficulty,
    content: {
      format: 'markdown',
      body: body.trim()
    }
  };

  // Extract challenge if present
  if (javaCode.includes('MULTIPLE_CHOICE')) {
    const descMatch = javaCode.match(/\.description\("([^"]*)"\)/);
    const optionsMatches = javaCode.matchAll(/\.addMultipleChoiceOption\("([^"]*)"\)/g);
    const correctMatch = javaCode.match(/\.correctAnswer\("([^"]*)"\)/);

    if (descMatch) {
      const options = Array.from(optionsMatches).map(m => m[1]);

      lesson.exercises = [{
        type: 'multiple-choice',
        title: 'Quiz',
        instructions: descMatch[1],
        options,
        correctAnswer: correctMatch ? correctMatch[1] : undefined
      }];
    }
  }

  return lesson;
}

async function convertJavaCourse(sourceDir: string, outputPath: string) {
  console.log('🔄 Converting Java course...');

  // Find all epoch directories
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

  // Convert each epoch
  const modules: PlatformModule[] = [];
  let totalLessons = 0;

  for (const epochDir of epochDirs) {
    const epochNumber = parseInt(epochDir.name.replace('epoch', ''));
    const epochPath = path.join(contentDir, epochDir.name);

    // Find all lesson files
    const lessonFiles = (await fs.readdir(epochPath))
      .filter(f => f.match(/^Lesson\d+Content\.java$/))
      .sort((a, b) => {
        const numA = parseInt(a.match(/\d+/)![0]);
        const numB = parseInt(b.match(/\d+/)![0]);
        return numA - numB;
      });

    if (lessonFiles.length === 0) continue;

    console.log(`   📖 Epoch ${epochNumber}: ${lessonFiles.length} lessons`);

    const lessons: PlatformLesson[] = [];

    for (let i = 0; i < lessonFiles.length; i++) {
      const file = lessonFiles[i];
      const filePath = path.join(epochPath, file);

      try {
        const lesson = await parseJavaLesson(filePath, epochNumber, i + 1);
        lessons.push(lesson);
      } catch (error: any) {
        console.warn(`   ⚠️  Skipping ${file}: ${error.message}`);
        continue;
      }
    }

    // Determine module difficulty
    let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
    if (epochNumber >= 8) difficulty = 'advanced';
    else if (epochNumber >= 4) difficulty = 'intermediate';

    // Calculate module hours
    const totalMinutes = lessons.reduce((sum, l) => sum + l.estimatedMinutes, 0);
    const estimatedHours = Math.ceil(totalMinutes / 60);

    modules.push({
      id: `module-${String(epochNumber).padStart(2, '0')}`,
      title: `Epoch ${epochNumber}`,
      description: `Epoch ${epochNumber}`,
      difficulty,
      estimatedHours,
      lessons
    });

    totalLessons += lessons.length;
  }

  // Create course
  const course: PlatformCourse = {
    id: 'java',
    language: 'java',
    title: 'Java From First Principles to Full-Stack',
    description: 'Master Java from absolute beginner to job-ready full-stack developer through Socratic mentorship and hands-on projects.',
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
  console.log(`   📚 ${modules.length} epochs (modules)`);
  console.log(`   📄 ${totalLessons} lessons`);
  console.log(`   ⏱️  ~${course.estimatedHours} hours of content`);
  console.log(`   💾 Output: ${outputPath}`);
}

// Main execution
const sourceDir = process.argv[2] || '/tmp/Java-Training-Course/socratic-java-mentor';
const outputPath = process.argv[3] || path.join(__dirname, '../content/courses/java/course.json');

convertJavaCourse(sourceDir, outputPath)
  .then(() => {
    console.log('\n✨ Java course ready!');
    process.exit(0);
  })
  .catch((error) => {
    console.error('❌ Conversion failed:', error);
    process.exit(1);
  });
