#!/usr/bin/env ts-node

import * as fs from 'fs/promises'
import * as path from 'path'

interface Course {
  courseMetadata: {
    id: string
    language: string
    [key: string]: any
  }
  modules: Array<{
    id: string
    lessons: Array<{
      id: string
      content: {
        bodyFile?: string
        body: string
        [key: string]: any
      }
      [key: string]: any
    }>
    [key: string]: any
  }>
  languageConfig: any
}

async function processCourse(courseName: string) {
  console.log(`\n📚 Processing ${courseName} course...`)

  const coursePath = path.join(process.cwd(), 'content/courses', courseName)
  const courseJsonPath = path.join(coursePath, 'course.json')

  // Check if course.json exists
  try {
    await fs.access(courseJsonPath)
  } catch (error) {
    console.log(`  ⚠️  No course.json found for ${courseName}, skipping...`)
    return null
  }

  // Read course.json
  const courseData = await fs.readFile(courseJsonPath, 'utf-8')
  const course: Course = JSON.parse(courseData)

  let lessonsProcessed = 0
  let markdownFilesRead = 0

  // Process each module and lesson
  for (const module of course.modules) {
    for (const lesson of module.lessons) {
      if (lesson.content.bodyFile) {
        const markdownPath = path.join(
          coursePath,
          'modules',
          module.id,
          'lessons',
          lesson.content.bodyFile
        )

        try {
          const markdownContent = await fs.readFile(markdownPath, 'utf-8')
          lesson.content.body = markdownContent
          markdownFilesRead++
        } catch (error) {
          console.log(`  ⚠️  Could not read ${lesson.content.bodyFile}`)
        }
      }
      lessonsProcessed++
    }
  }

  // Save to apps/api/content/
  const outputDir = path.join(process.cwd(), 'apps/api/content')
  await fs.mkdir(outputDir, { recursive: true })

  const outputPath = path.join(outputDir, `${course.courseMetadata.id}.json`)
  await fs.writeFile(outputPath, JSON.stringify(course, null, 2), 'utf-8')

  console.log(`  ✅ Processed ${lessonsProcessed} lessons`)
  console.log(`  📝 Read ${markdownFilesRead} markdown files`)
  console.log(`  💾 Saved to apps/api/content/${course.courseMetadata.id}.json`)

  return {
    courseName,
    lessonsProcessed,
    markdownFilesRead
  }
}

async function main() {
  console.log('🚀 Starting course processing...\n')
  console.log('=' .repeat(50))

  const courses = [
    'python',
    'java',
    'kotlin',
    'rust',
    'csharp',
    'flutter',
    'javascript-typescript'
  ]

  const results = []

  for (const course of courses) {
    try {
      const result = await processCourse(course)
      if (result) {
        results.push(result)
      }
    } catch (error) {
      console.log(`  ❌ Error processing ${course}:`, error instanceof Error ? error.message : error)
    }
  }

  console.log('\n' + '='.repeat(50))
  console.log('📊 Summary:')
  console.log('='.repeat(50))
  console.log(`Total courses processed: ${results.length}`)
  console.log(`Total lessons processed: ${results.reduce((sum, r) => sum + r.lessonsProcessed, 0)}`)
  console.log(`Total markdown files read: ${results.reduce((sum, r) => sum + r.markdownFilesRead, 0)}`)
  console.log('\n✨ Course processing complete!')
}

main().catch(error => {
  console.error('Fatal error:', error)
  process.exit(1)
})
