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
    const coursePath = path.join(__dirname, '../../../../content/courses', language, 'course.json')

    try {
      const courseData = await fs.readFile(coursePath, 'utf-8')
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
  } catch (error) {
    next(error)
  }
})

// List all available courses
router.get('/', async (req, res, next) => {
  try {
    const coursesDir = path.join(__dirname, '../../../../content/courses')
    const languages = await fs.readdir(coursesDir)

    const courses = await Promise.all(
      languages.map(async (language) => {
        try {
          const coursePath = path.join(coursesDir, language, 'course.json')
          const courseData = await fs.readFile(coursePath, 'utf-8')
          const course = JSON.parse(courseData)
          return course.courseMetadata
        } catch {
          return null
        }
      })
    )

    res.json(courses.filter(Boolean))
  } catch (error) {
    next(error)
  }
})

export default router
