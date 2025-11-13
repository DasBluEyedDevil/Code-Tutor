#!/usr/bin/env node
import { readFile, readdir } from 'fs/promises'
import { join, basename } from 'path'
import { glob } from 'glob'

interface ValidationError {
  file: string
  field: string
  message: string
}

const errors: ValidationError[] = []
const warnings: ValidationError[] = []

function addError(file: string, field: string, message: string) {
  errors.push({ file, field, message })
}

function addWarning(file: string, field: string, message: string) {
  warnings.push({ file, field, message })
}

async function validateCourse(coursePath: string) {
  const courseFile = join(coursePath, 'course.json')
  console.log(`\n📚 Validating course: ${coursePath}`)

  try {
    const courseData = JSON.parse(await readFile(courseFile, 'utf-8'))
    const courseId = basename(coursePath)

    // Validate metadata
    if (!courseData.courseMetadata) {
      addError(courseFile, 'courseMetadata', 'Missing courseMetadata')
      return
    }

    const meta = courseData.courseMetadata
    const requiredMetaFields = ['id', 'language', 'displayName', 'description']
    for (const field of requiredMetaFields) {
      if (!meta[field]) {
        addError(courseFile, `courseMetadata.${field}`, `Missing ${field}`)
      }
    }

    if (meta.id !== courseId) {
      addWarning(courseFile, 'courseMetadata.id', `ID '${meta.id}' doesn't match directory name '${courseId}'`)
    }

    // Validate modules
    if (!courseData.modules || !Array.isArray(courseData.modules)) {
      addError(courseFile, 'modules', 'Missing or invalid modules array')
      return
    }

    console.log(`  Found ${courseData.modules.length} modules`)

    for (const module of courseData.modules) {
      await validateModule(coursePath, courseFile, module)
    }

    // Validate language config
    if (!courseData.languageConfig) {
      addError(courseFile, 'languageConfig', 'Missing languageConfig')
    } else {
      validateLanguageConfig(courseFile, courseData.languageConfig)
    }

  } catch (error: any) {
    addError(courseFile, 'parse', `Failed to parse: ${error.message}`)
  }
}

async function validateModule(coursePath: string, courseFile: string, module: any) {
  const requiredFields = ['id', 'title', 'description', 'order']
  for (const field of requiredFields) {
    if (module[field] === undefined) {
      addError(courseFile, `module.${module.id || 'unknown'}.${field}`, `Missing ${field}`)
    }
  }

  if (!module.lessons || !Array.isArray(module.lessons)) {
    addError(courseFile, `module.${module.id}`, 'Missing or invalid lessons array')
    return
  }

  console.log(`    Module ${module.id}: ${module.lessons.length} lessons`)

  for (const lesson of module.lessons) {
    await validateLesson(coursePath, courseFile, module.id, lesson)
  }
}

async function validateLesson(coursePath: string, courseFile: string, moduleId: string, lesson: any) {
  const requiredFields = ['id', 'title', 'type', 'order', 'estimatedMinutes', 'difficulty']
  for (const field of requiredFields) {
    if (lesson[field] === undefined) {
      addError(courseFile, `lesson.${lesson.id || 'unknown'}.${field}`, `Missing ${field}`)
    }
  }

  // Validate content
  if (!lesson.content) {
    addError(courseFile, `lesson.${lesson.id}.content`, 'Missing content')
  } else {
    // Check if markdown file exists
    if (lesson.content.bodyFile) {
      const markdownPath = join(coursePath, 'modules', moduleId, 'lessons', lesson.content.bodyFile)
      try {
        await readFile(markdownPath, 'utf-8')
      } catch {
        addError(markdownPath, 'file', `Markdown file not found: ${lesson.content.bodyFile}`)
      }
    } else if (!lesson.content.body) {
      addWarning(courseFile, `lesson.${lesson.id}.content`, 'No body or bodyFile specified')
    }
  }

  // Validate exercises
  if (lesson.exercises && Array.isArray(lesson.exercises)) {
    for (const exercise of lesson.exercises) {
      validateExercise(courseFile, lesson.id, exercise)
    }
  }
}

function validateExercise(courseFile: string, lessonId: string, exercise: any) {
  const requiredFields = ['id', 'type', 'title', 'instructions', 'starterCode']
  for (const field of requiredFields) {
    if (!exercise[field]) {
      addWarning(courseFile, `exercise.${exercise.id || 'unknown'}.${field}`, `Missing ${field}`)
    }
  }

  if (!exercise.hints || !Array.isArray(exercise.hints) || exercise.hints.length === 0) {
    addWarning(courseFile, `exercise.${exercise.id}`, 'No hints provided')
  }
}

function validateLanguageConfig(courseFile: string, config: any) {
  if (!config.executionEngine) {
    addError(courseFile, 'languageConfig.executionEngine', 'Missing executionEngine')
  }

  if (!config.editorSettings) {
    addError(courseFile, 'languageConfig.editorSettings', 'Missing editorSettings')
  } else {
    const required = ['defaultTemplate', 'fileExtension', 'monacoLanguageId']
    for (const field of required) {
      if (!config.editorSettings[field]) {
        addError(courseFile, `languageConfig.editorSettings.${field}`, `Missing ${field}`)
      }
    }
  }

  if (!config.sandboxConstraints) {
    addWarning(courseFile, 'languageConfig.sandboxConstraints', 'Missing sandboxConstraints')
  }
}

async function main() {
  console.log('🔍 Code Tutor Content Validator\n')

  const contentDir = join(process.cwd(), 'content/courses')

  try {
    const courses = await readdir(contentDir)

    for (const course of courses) {
      const coursePath = join(contentDir, course)
      await validateCourse(coursePath)
    }

    // Print summary
    console.log('\n' + '='.repeat(60))
    console.log('📊 Validation Summary')
    console.log('='.repeat(60))

    if (errors.length === 0 && warnings.length === 0) {
      console.log('✅ All courses are valid!')
    } else {
      if (errors.length > 0) {
        console.log(`\n❌ ${errors.length} Errors:\n`)
        for (const error of errors) {
          console.log(`  ${error.file}`)
          console.log(`    Field: ${error.field}`)
          console.log(`    Message: ${error.message}\n`)
        }
      }

      if (warnings.length > 0) {
        console.log(`\n⚠️  ${warnings.length} Warnings:\n`)
        for (const warning of warnings) {
          console.log(`  ${warning.file}`)
          console.log(`    Field: ${warning.field}`)
          console.log(`    Message: ${warning.message}\n`)
        }
      }
    }

    process.exit(errors.length > 0 ? 1 : 0)

  } catch (error: any) {
    console.error('❌ Validation failed:', error.message)
    process.exit(1)
  }
}

main()
