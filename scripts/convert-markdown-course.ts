#!/usr/bin/env tsx

/**
 * Universal Markdown Course Importer
 * Converts markdown-based courses (Rust, Kotlin, Flutter, Java, C#) to platform format
 */

import * as fs from 'fs/promises';
import * as path from 'path';

interface ConversionOptions {
  courseId: string;
  language: string;
  title: string;
  description: string;
  sourceDir: string;
  outputPath: string;
  lessonPattern?: RegExp;
  modulePattern?: RegExp;
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

// Extract title from markdown filename or content
function extractTitle(filename: string, content: string): string {
  // Try to get title from first H1 in content
  const h1Match = content.match(/^#\s+(.+)$/m);
  if (h1Match) {
    return h1Match[1].trim();
  }

  // Fall back to filename
  return filename
    .replace(/\.md$/, '')
    .replace(/-/g, ' ')
    .replace(/_/g, ' ')
    .split(' ')
    .map(word => word.charAt(0).toUpperCase() + word.slice(1))
    .join(' ');
}

// Convert markdown file to platform lesson
async function convertMarkdownLesson(
  filePath: string,
  lessonId: string,
  moduleNumber: number
): Promise<PlatformLesson> {
  const content = await fs.readFile(filePath, 'utf-8');
  const filename = path.basename(filePath);
  const title = extractTitle(filename, content);

  // Determine difficulty based on module number
  let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
  if (moduleNumber > 6) difficulty = 'advanced';
  else if (moduleNumber > 3) difficulty = 'intermediate';

  // Estimate reading time (200 words per minute)
  const wordCount = content.split(/\s+/).length;
  const estimatedMinutes = Math.max(5, Math.ceil(wordCount / 200));

  // Determine lesson type
  let type: 'reading' | 'exercise' | 'project' = 'reading';
  if (content.toLowerCase().includes('exercise') || content.toLowerCase().includes('practice')) {
    type = 'exercise';
  }
  if (content.toLowerCase().includes('project') || content.toLowerCase().includes('capstone')) {
    type = 'project';
  }

  return {
    id: lessonId,
    title,
    type,
    estimatedMinutes,
    difficulty,
    content: {
      format: 'markdown',
      body: content
    }
  };
}

// Find all lesson files recursively
async function findLessonFiles(dir: string, pattern?: RegExp): Promise<string[]> {
  const files: string[] = [];

  async function walk(currentDir: string) {
    const entries = await fs.readdir(currentDir, { withFileTypes: true });

    for (const entry of entries) {
      const fullPath = path.join(currentDir, entry.name);

      if (entry.isDirectory()) {
        await walk(fullPath);
      } else if (entry.isFile() && entry.name.endsWith('.md')) {
        if (!pattern || pattern.test(entry.name)) {
          files.push(fullPath);
        }
      }
    }
  }

  await walk(dir);
  return files.sort();
}

// Group lessons by module
function groupLessonsByModule(lessonFiles: string[]): Map<string, string[]> {
  const modules = new Map<string, string[]>();

  for (const file of lessonFiles) {
    // Extract module from path or filename
    const parts = file.split('/');
    let moduleName = 'module-00';

    // Try to find module in path (e.g., "module_01", "module-01", "part1")
    for (const part of parts) {
      if (/module[_-]?\d+/i.test(part)) {
        const match = part.match(/\d+/);
        if (match) {
          moduleName = `module-${match[0].padStart(2, '0')}`;
          break;
        }
      } else if (/part[_-]?\d+/i.test(part)) {
        const match = part.match(/\d+/);
        if (match) {
          moduleName = `module-${match[0].padStart(2, '0')}`;
          break;
        }
      }
    }

    if (!modules.has(moduleName)) {
      modules.set(moduleName, []);
    }
    modules.get(moduleName)!.push(file);
  }

  return modules;
}

// Convert entire course
export async function convertMarkdownCourse(options: ConversionOptions): Promise<void> {
  console.log(`🔄 Converting ${options.title}...`);

  // Find all lesson files
  const lessonFiles = await findLessonFiles(options.sourceDir, options.lessonPattern);
  console.log(`📁 Found ${lessonFiles.length} lesson files`);

  // Group by module
  const moduleMap = groupLessonsByModule(lessonFiles);
  console.log(`📚 Organized into ${moduleMap.size} modules`);

  // Convert modules
  const modules: PlatformModule[] = [];
  let totalLessons = 0;

  for (const [moduleName, files] of Array.from(moduleMap.entries()).sort()) {
    const moduleNumber = parseInt(moduleName.match(/\d+/)![0]);
    const lessons: PlatformLesson[] = [];

    console.log(`   📖 ${moduleName}: ${files.length} lessons`);

    for (let i = 0; i < files.length; i++) {
      const file = files[i];
      const lessonId = `lesson-${String(moduleNumber).padStart(2, '0')}-${String(i + 1).padStart(2, '0')}`;
      const lesson = await convertMarkdownLesson(file, lessonId, moduleNumber);
      lessons.push(lesson);
    }

    // Determine module difficulty
    let difficulty: 'beginner' | 'intermediate' | 'advanced' = 'beginner';
    if (moduleNumber > 6) difficulty = 'advanced';
    else if (moduleNumber > 3) difficulty = 'intermediate';

    // Calculate module hours
    const totalMinutes = lessons.reduce((sum, l) => sum + l.estimatedMinutes, 0);
    const estimatedHours = Math.ceil(totalMinutes / 60);

    modules.push({
      id: moduleName,
      title: moduleName.replace(/-/g, ' ').toUpperCase(),
      description: `Module ${moduleNumber}`,
      difficulty,
      estimatedHours,
      lessons
    });

    totalLessons += lessons.length;
  }

  // Create course
  const course: PlatformCourse = {
    id: options.courseId,
    language: options.language,
    title: options.title,
    description: options.description,
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
  const outputDir = path.dirname(options.outputPath);
  await fs.mkdir(outputDir, { recursive: true });

  // Write output
  await fs.writeFile(options.outputPath, JSON.stringify(course, null, 2), 'utf-8');

  console.log(`✅ Conversion complete!`);
  console.log(`   📚 ${modules.length} modules`);
  console.log(`   📄 ${totalLessons} lessons`);
  console.log(`   ⏱️  ~${course.estimatedHours} hours of content`);
  console.log(`   💾 Output: ${options.outputPath}`);
}

// CLI execution
if (require.main === module) {
  const args = process.argv.slice(2);
  const command = args[0];

  if (!command) {
    console.log('Usage: convert-markdown-course.ts <language> [source-dir]');
    console.log('Languages: rust, kotlin, flutter, java, csharp');
    process.exit(1);
  }

  const courseConfigs: Record<string, ConversionOptions> = {
    rust: {
      courseId: 'rust',
      language: 'rust',
      title: 'Rust Programming',
      description: 'Learn Rust from fundamentals to advanced concepts including ownership, borrowing, and systems programming.',
      sourceDir: args[1] || '/tmp/Rust-Training-Course/course_content',
      outputPath: path.join(__dirname, '../content/courses/rust/course.json'),
      lessonPattern: /lesson.*\.md$/
    },
    kotlin: {
      courseId: 'kotlin',
      language: 'kotlin',
      title: 'Kotlin Programming',
      description: 'Master Kotlin programming from basics to advanced features including coroutines and Android development.',
      sourceDir: args[1] || '/tmp/Kotlin-Training-Course/src/main/resources/lessons',
      outputPath: path.join(__dirname, '../content/courses/kotlin/course.json'),
      lessonPattern: /lesson.*\.md$/
    },
    flutter: {
      courseId: 'flutter',
      language: 'dart',
      title: 'Flutter & Dart Development',
      description: 'Build beautiful cross-platform mobile apps with Flutter and Dart.',
      sourceDir: args[1] || '/tmp/Flutter-Training-Course/lessons',
      outputPath: path.join(__dirname, '../content/courses/flutter/course.json'),
      lessonPattern: /lesson.*\.md$/
    },
    java: {
      courseId: 'java',
      language: 'java',
      title: 'Java Programming',
      description: 'Learn Java programming from fundamentals to enterprise development.',
      sourceDir: args[1] || '/tmp/Java-Training-Course',
      outputPath: path.join(__dirname, '../content/courses/java/course.json'),
      lessonPattern: /lesson.*\.md$/i
    },
    csharp: {
      courseId: 'csharp',
      language: 'csharp',
      title: 'C# Programming',
      description: 'Master C# and .NET development from basics to advanced concepts.',
      sourceDir: args[1] || '/tmp/CSharp-Training-Course',
      outputPath: path.join(__dirname, '../content/courses/csharp/course.json'),
      lessonPattern: /lesson.*\.md$/i
    }
  };

  const config = courseConfigs[command.toLowerCase()];
  if (!config) {
    console.error(`Unknown language: ${command}`);
    console.log('Available: rust, kotlin, flutter, java, csharp');
    process.exit(1);
  }

  convertMarkdownCourse(config)
    .then(() => {
      console.log(`\n✨ ${config.title} course ready!`);
      process.exit(0);
    })
    .catch((error) => {
      console.error('❌ Conversion failed:', error);
      process.exit(1);
    });
}
