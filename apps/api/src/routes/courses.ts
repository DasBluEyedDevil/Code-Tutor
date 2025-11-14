import { Router } from 'express'
import { promises as fs } from 'fs'
import path from 'path'
import { fileURLToPath } from 'url'

const router = Router()
const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)

// Get course by language
router.get('/:language', async (req, res, next) => {
  try {
    const { language } = req.params

    // Try new content location first (with embedded markdown)
    const newCoursePath = path.join(__dirname, '../../../content', `${language}.json`)

    try {
      const courseData = await fs.readFile(newCoursePath, 'utf-8')
      const course = JSON.parse(courseData)
      res.json(course)
      return
    } catch (err) {
      // Fall back to old structure
      const oldCoursePath = path.join(__dirname, '../../../../content/courses', language, 'course.json')

      try {
        const courseData = await fs.readFile(oldCoursePath, 'utf-8')
        const course = JSON.parse(courseData)

        // Load lesson content from markdown files
        for (const module of course.modules) {
          for (const lesson of module.lessons) {
            if (lesson.content.bodyFile) {
              const lessonPath = path.join(
                __dirname,
                '../../../../content/courses',
                language,
                'modules',
                module.id,
                'lessons',
                lesson.content.bodyFile
              )
              try {
                const markdownContent = await fs.readFile(lessonPath, 'utf-8')
                lesson.content.body = markdownContent
              } catch (err) {
                // If file doesn't exist, use placeholder
                lesson.content.body = `# ${lesson.title}\n\nLesson content coming soon...`
              }
            }
          }
        }

        res.json(course)
      } catch (err) {
        res.status(404).json({ error: `Course not found for language: ${language}` })
      }
    }
  } catch (error) {
    next(error)
  }
})

// List all available courses
router.get('/', async (req, res, next) => {
  try {
    const newContentDir = path.join(__dirname, '../../../content')
    const courses = []

    // Try new content location first
    try {
      const files = await fs.readdir(newContentDir)
      const jsonFiles = files.filter(f => f.endsWith('.json'))

      for (const file of jsonFiles) {
        try {
          const coursePath = path.join(newContentDir, file)
          const courseData = await fs.readFile(coursePath, 'utf-8')
          const course = JSON.parse(courseData)
          courses.push(course.courseMetadata)
        } catch {
          // Skip invalid files
        }
      }
    } catch {
      // New content dir doesn't exist, fall back to old structure
    }

    // Fall back to old structure if no courses found
    if (courses.length === 0) {
      const oldCoursesDir = path.join(__dirname, '../../../../content/courses')
      try {
        const languages = await fs.readdir(oldCoursesDir)

        const oldCourses = await Promise.all(
          languages.map(async (language) => {
            try {
              const coursePath = path.join(oldCoursesDir, language, 'course.json')
              const courseData = await fs.readFile(coursePath, 'utf-8')
              const course = JSON.parse(courseData)
              return course.courseMetadata
            } catch {
              return null
            }
          })
        )

        courses.push(...oldCourses.filter(Boolean))
      } catch {
        // Old structure also doesn't exist
      }
    }

    res.json(courses)
  } catch (error) {
    next(error)
  }
})

export default router
