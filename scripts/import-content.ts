import fs from 'fs/promises'
import path from 'path'
import { Course, Module, Lesson, Exercise } from '../apps/web/src/types/content'

/**
 * Content Import and Migration Tool
 *
 * This tool helps import course content from various formats into the Code Tutor format.
 * Supports: Markdown, JSON, YAML, and custom formats from legacy repositories.
 */

export interface ImportConfig {
  sourceDir: string
  targetDir: string
  language: string
  format: 'markdown' | 'json' | 'yaml' | 'custom'
  options?: {
    includeExercises?: boolean
    includeTests?: boolean
    validateContent?: boolean
  }
}

export interface MarkdownLesson {
  title: string
  content: string
  filePath: string
  metadata?: Record<string, any>
}

/**
 * Parse markdown file with frontmatter
 */
export async function parseMarkdownLesson(filePath: string): Promise<MarkdownLesson> {
  const content = await fs.readFile(filePath, 'utf-8')

  // Extract frontmatter if present (between --- markers)
  const frontmatterRegex = /^---\n([\s\S]*?)\n---\n([\s\S]*)$/
  const match = content.match(frontmatterRegex)

  let metadata: Record<string, any> = {}
  let body = content

  if (match) {
    const frontmatter = match[1]
    body = match[2]

    // Parse YAML-like frontmatter
    frontmatter.split('\n').forEach(line => {
      const [key, ...valueParts] = line.split(':')
      if (key && valueParts.length > 0) {
        const value = valueParts.join(':').trim()
        metadata[key.trim()] = value.replace(/^["']|["']$/g, '')
      }
    })
  }

  // Extract title from first # heading if not in metadata
  const titleMatch = body.match(/^#\s+(.+)$/m)
  const title = metadata.title || (titleMatch ? titleMatch[1] : path.basename(filePath, '.md'))

  return {
    title,
    content: body,
    filePath,
    metadata
  }
}

/**
 * Convert markdown lesson to Course format
 */
export function markdownLessonToCourseLesson(
  markdown: MarkdownLesson,
  index: number
): Lesson {
  // Extract code examples from markdown (```language code blocks)
  const codeExamples: Lesson['content']['codeExamples'] = []
  const codeBlockRegex = /```(\w+)\n([\s\S]*?)```/g
  let match
  let exampleId = 0

  while ((match = codeBlockRegex.exec(markdown.content)) !== null) {
    const [, language, code] = match
    codeExamples.push({
      id: `example-${exampleId++}`,
      language,
      code: code.trim(),
      explanation: `Code example in ${language}`,
      runnable: true
    })
  }

  // Remove code blocks from content to get clean body
  const body = markdown.content.replace(/```[\s\S]*?```/g, '').trim()

  // Extract exercises from special markers (if present)
  const exercises: Exercise[] = []
  const exerciseRegex = /<!-- EXERCISE\s+([\s\S]*?)-->/g
  let exerciseMatch
  let exerciseId = 0

  while ((exerciseMatch = exerciseRegex.exec(markdown.content)) !== null) {
    try {
      const exerciseData = JSON.parse(exerciseMatch[1])
      exercises.push({
        id: `exercise-${exerciseId++}`,
        type: exerciseData.type || 'coding',
        title: exerciseData.title || 'Practice Exercise',
        instructions: exerciseData.instructions || '',
        difficulty: exerciseData.difficulty || 'beginner',
        estimatedMinutes: exerciseData.estimatedMinutes || 10,
        starterCode: exerciseData.starterCode || '',
        solution: exerciseData.solution || '',
        hints: exerciseData.hints || [],
        testCases: exerciseData.testCases || [],
        validationRules: exerciseData.validationRules || {}
      })
    } catch (e) {
      console.warn(`Failed to parse exercise in ${markdown.filePath}:`, e)
    }
  }

  const metadata = markdown.metadata || {}

  return {
    id: `lesson-${index + 1}`,
    title: markdown.title,
    type: (metadata.type as any) || 'reading',
    order: index,
    estimatedMinutes: parseInt(metadata.estimatedMinutes || '15'),
    difficulty: (metadata.difficulty as any) || 'beginner',
    tags: metadata.tags ? metadata.tags.split(',').map((s: string) => s.trim()) : [],
    content: {
      format: 'markdown' as const,
      body,
      codeExamples
    },
    exercises: exercises.length > 0 ? exercises : [{
      id: 'default-exercise',
      type: 'coding' as const,
      title: 'Practice Exercise',
      instructions: 'Try the concepts you learned in this lesson.',
      difficulty: 'beginner' as const,
      estimatedMinutes: 10,
      starterCode: '',
      solution: '',
      hints: [],
      testCases: [],
      validationRules: {}
    }]
  }
}

/**
 * Scan directory for markdown files
 */
export async function scanMarkdownFiles(dir: string): Promise<MarkdownLesson[]> {
  const lessons: MarkdownLesson[] = []

  async function scan(currentDir: string) {
    const entries = await fs.readdir(currentDir, { withFileTypes: true })

    for (const entry of entries) {
      const fullPath = path.join(currentDir, entry.name)

      if (entry.isDirectory()) {
        await scan(fullPath)
      } else if (entry.isFile() && entry.name.endsWith('.md')) {
        const lesson = await parseMarkdownLesson(fullPath)
        lessons.push(lesson)
      }
    }
  }

  await scan(dir)
  return lessons.sort((a, b) => a.filePath.localeCompare(b.filePath))
}

/**
 * Import course from markdown files
 */
export async function importFromMarkdown(config: ImportConfig): Promise<Course> {
  console.log(`Importing ${config.language} course from ${config.sourceDir}...`)

  const markdownLessons = await scanMarkdownFiles(config.sourceDir)
  console.log(`Found ${markdownLessons.length} markdown files`)

  // Group lessons into modules based on directory structure
  const moduleMap = new Map<string, MarkdownLesson[]>()

  for (const lesson of markdownLessons) {
    const relativePath = path.relative(config.sourceDir, lesson.filePath)
    const parts = relativePath.split(path.sep)
    const moduleName = parts.length > 1 ? parts[0] : 'Introduction'

    if (!moduleMap.has(moduleName)) {
      moduleMap.set(moduleName, [])
    }
    moduleMap.get(moduleName)!.push(lesson)
  }

  // Convert to modules
  const modules: Module[] = []
  let moduleIndex = 0

  for (const [moduleName, lessons] of moduleMap) {
    const moduleLessons = lessons.map((lesson, index) => markdownLessonToCourseLesson(lesson, index))
    const totalMinutes = moduleLessons.reduce((sum, lesson) => sum + lesson.estimatedMinutes, 0)

    const module: Module = {
      id: `module-${moduleIndex + 1}`,
      title: moduleName.replace(/^\d+-/, '').replace(/-/g, ' '),
      description: `${moduleName} module`,
      order: moduleIndex,
      estimatedHours: Math.ceil(totalMinutes / 60),
      prerequisites: [],
      lessons: moduleLessons
    }

    modules.push(module)
    moduleIndex++
  }

  // Calculate total hours and outcomes
  const totalMinutes = modules.reduce((sum, mod) => sum + (mod.estimatedHours * 60), 0)
  const totalHours = Math.ceil(totalMinutes / 60)

  // Create course
  return {
    courseMetadata: {
      id: config.language,
      language: config.language,
      version: '1.0.0',
      displayName: config.language.charAt(0).toUpperCase() + config.language.slice(1),
      description: `Complete ${config.language} course`,
      totalModules: modules.length,
      estimatedHours: totalHours,
      difficulty: 'beginner-to-advanced' as const,
      prerequisites: [],
      learningOutcomes: [
        `Master ${config.language} fundamentals`,
        'Build real-world projects',
        'Write clean, efficient code'
      ],
      icon: '📚',
      color: '#3B82F6'
    },
    languageConfig: {
      executionEngine: config.language,
      compilerOptions: {
        version: '1.0.0',
        flags: []
      },
      editorSettings: {
        defaultTemplate: getDefaultTemplate(config.language),
        fileExtension: getFileExtension(config.language),
        monacoLanguageId: getMonacoLanguageId(config.language),
        tabSize: 4,
        insertSpaces: true
      },
      sandboxConstraints: {
        maxExecutionTimeMs: 5000,
        maxMemoryMB: 128,
        maxOutputChars: 10000,
        allowedPackages: [],
        blockedPackages: []
      }
    },
    modules
  }
}

/**
 * Get default code template for language
 */
function getDefaultTemplate(language: string): string {
  const templates: Record<string, string> = {
    python: '# Write your Python code here\n\nprint("Hello, World!")\n',
    java: 'public class Main {\n    public static void main(String[] args) {\n        System.out.println("Hello, World!");\n    }\n}\n',
    javascript: '// Write your JavaScript code here\n\nconsole.log("Hello, World!");\n',
    typescript: '// Write your TypeScript code here\n\nconsole.log("Hello, World!");\n',
    kotlin: 'fun main() {\n    println("Hello, World!")\n}\n',
    rust: 'fn main() {\n    println!("Hello, World!");\n}\n',
    csharp: 'using System;\n\nclass Program {\n    static void Main() {\n        Console.WriteLine("Hello, World!");\n    }\n}\n',
    flutter: 'void main() {\n  print("Hello, World!");\n}\n'
  }

  return templates[language] || '// Write your code here\n'
}

/**
 * Get Monaco editor language ID
 */
function getMonacoLanguageId(language: string): string {
  const mapping: Record<string, string> = {
    python: 'python',
    java: 'java',
    javascript: 'javascript',
    typescript: 'typescript',
    kotlin: 'kotlin',
    rust: 'rust',
    csharp: 'csharp',
    flutter: 'dart'
  }

  return mapping[language] || 'plaintext'
}

/**
 * Get file extension for language
 */
function getFileExtension(language: string): string {
  const extensions: Record<string, string> = {
    python: '.py',
    java: '.java',
    javascript: '.js',
    typescript: '.ts',
    kotlin: '.kt',
    rust: '.rs',
    csharp: '.cs',
    flutter: '.dart'
  }

  return extensions[language] || '.txt'
}

/**
 * Save course to JSON file
 */
export async function saveCourse(course: Course, outputPath: string): Promise<void> {
  const dir = path.dirname(outputPath)
  await fs.mkdir(dir, { recursive: true })
  await fs.writeFile(outputPath, JSON.stringify(course, null, 2), 'utf-8')
  console.log(`✅ Course saved to ${outputPath}`)
  console.log(`   - ${course.modules.length} modules`)
  console.log(`   - ${course.modules.reduce((sum, m) => sum + m.lessons.length, 0)} lessons`)
}

/**
 * Validate course structure
 */
export function validateCourse(course: Course): { valid: boolean; errors: string[] } {
  const errors: string[] = []

  if (!course.courseMetadata.id) {
    errors.push('Course metadata missing id')
  }

  if (!course.modules || course.modules.length === 0) {
    errors.push('Course has no modules')
  }

  course.modules.forEach((module, moduleIndex) => {
    if (!module.id) {
      errors.push(`Module ${moduleIndex} missing id`)
    }

    if (!module.lessons || module.lessons.length === 0) {
      errors.push(`Module ${module.id} has no lessons`)
    }

    module.lessons.forEach((lesson, lessonIndex) => {
      if (!lesson.id) {
        errors.push(`Lesson ${lessonIndex} in module ${module.id} missing id`)
      }

      if (!lesson.content?.body) {
        errors.push(`Lesson ${lesson.id} missing content body`)
      }
    })
  })

  return {
    valid: errors.length === 0,
    errors
  }
}
