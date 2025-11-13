#!/usr/bin/env node
import { readFile, writeFile, mkdir } from 'fs/promises'
import { join, dirname } from 'path'
import { Command } from 'commander'
import MarkdownIt from 'markdown-it'

const md = new MarkdownIt()

interface LessonTemplate {
  id: string
  title: string
  type: 'reading' | 'interactive' | 'project'
  order: number
  estimatedMinutes: number
  difficulty: 'beginner' | 'intermediate' | 'advanced'
  tags: string[]
  content: {
    format: 'markdown'
    bodyFile: string
    body: string
    codeExamples: any[]
  }
  exercises: any[]
  quiz?: any
}

async function ensureDir(path: string) {
  try {
    await mkdir(path, { recursive: true })
  } catch (err: any) {
    if (err.code !== 'EEXIST') throw err
  }
}

async function createLesson(
  courseId: string,
  moduleId: string,
  lessonId: string,
  title: string,
  markdownContent: string
) {
  const coursePath = join(process.cwd(), 'content/courses', courseId)
  const modulePath = join(coursePath, 'modules', moduleId)
  const lessonsPath = join(modulePath, 'lessons')

  await ensureDir(lessonsPath)

  // Create markdown file
  const markdownFile = `${lessonId}.md`
  const markdownPath = join(lessonsPath, markdownFile)
  await writeFile(markdownPath, markdownContent, 'utf-8')

  console.log(`✅ Created markdown: ${markdownPath}`)

  // Extract code examples from markdown
  const codeExamples = extractCodeExamples(markdownContent)

  const lesson: LessonTemplate = {
    id: lessonId,
    title,
    type: 'reading',
    order: parseInt(lessonId.split('-')[2]) || 1,
    estimatedMinutes: 30,
    difficulty: 'beginner',
    tags: [],
    content: {
      format: 'markdown',
      bodyFile: markdownFile,
      body: '',
      codeExamples,
    },
    exercises: [],
  }

  return lesson
}

function extractCodeExamples(markdown: string): any[] {
  const examples: any[] = []
  const codeBlockRegex = /```(\w+)\n([\s\S]*?)```/g
  let match
  let index = 0

  while ((match = codeBlockRegex.exec(markdown)) !== null) {
    const language = match[1]
    const code = match[2].trim()

    // Try to find explanation (paragraph before code block)
    const beforeCode = markdown.substring(0, match.index)
    const lines = beforeCode.split('\n')
    const explanation = lines[lines.length - 1]?.trim() || 'Code example'

    examples.push({
      id: `example-${index + 1}`,
      language,
      code,
      explanation,
      runnable: true,
      highlightLines: [],
    })

    index++
  }

  return examples
}

async function updateCourseFile(courseId: string, moduleId: string, lesson: LessonTemplate) {
  const coursePath = join(process.cwd(), 'content/courses', courseId, 'course.json')
  const courseData = JSON.parse(await readFile(coursePath, 'utf-8'))

  // Find or create module
  let module = courseData.modules.find((m: any) => m.id === moduleId)

  if (!module) {
    console.log(`⚠️  Module ${moduleId} not found, creating it...`)
    module = {
      id: moduleId,
      title: `Module ${moduleId.split('-')[1]}`,
      description: 'Module description',
      order: courseData.modules.length,
      estimatedHours: 8,
      prerequisites: [],
      lessons: [],
    }
    courseData.modules.push(module)
  }

  // Add lesson if not exists
  const existingLesson = module.lessons.find((l: any) => l.id === lesson.id)
  if (!existingLesson) {
    module.lessons.push(lesson)
    module.lessons.sort((a: any, b: any) => a.order - b.order)

    await writeFile(coursePath, JSON.stringify(courseData, null, 2), 'utf-8')
    console.log(`✅ Updated course file: ${coursePath}`)
  } else {
    console.log(`ℹ️  Lesson ${lesson.id} already exists in course file`)
  }
}

async function addLesson(options: any) {
  const { course, module, lesson, title, markdown } = options

  console.log(`\n📝 Adding lesson to ${course}/${module}/${lesson}`)

  let markdownContent = markdown

  // If markdown is a file path, read it
  try {
    markdownContent = await readFile(markdown, 'utf-8')
  } catch {
    // markdown is content, not a file
  }

  const lessonData = await createLesson(course, module, lesson, title, markdownContent)
  await updateCourseFile(course, module, lessonData)

  console.log('✅ Lesson added successfully!\n')
}

async function generateTemplate(options: any) {
  const { output } = options

  const template = `# Lesson Title

## Introduction

Brief introduction to the topic.

## Main Concept

Explain the main concept in simple terms.

## Example

Here's a code example:

\`\`\`python
print("Hello, World!")
\`\`\`

## Key Points

- Point 1
- Point 2
- Point 3

## Practice

Try this yourself:

1. Step 1
2. Step 2
3. Step 3

## Summary

Summarize what was learned.
`

  if (output) {
    await writeFile(output, template, 'utf-8')
    console.log(`✅ Template written to: ${output}`)
  } else {
    console.log(template)
  }
}

const program = new Command()

program
  .name('migrate')
  .description('Content migration tool for Code Tutor')
  .version('1.0.0')

program
  .command('add-lesson')
  .description('Add a new lesson to a course')
  .requiredOption('-c, --course <course>', 'Course ID (e.g., python)')
  .requiredOption('-m, --module <module>', 'Module ID (e.g., module-00)')
  .requiredOption('-l, --lesson <lesson>', 'Lesson ID (e.g., lesson-00-04)')
  .requiredOption('-t, --title <title>', 'Lesson title')
  .requiredOption('--markdown <markdown>', 'Markdown content or file path')
  .action(addLesson)

program
  .command('template')
  .description('Generate a lesson template')
  .option('-o, --output <file>', 'Output file path')
  .action(generateTemplate)

program.parse()
