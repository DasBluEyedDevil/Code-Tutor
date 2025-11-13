import { Router } from 'express'
import { promises as fs } from 'fs'
import path from 'path'
import { fileURLToPath } from 'url'

const router = Router()
const __filename = fileURLToPath(import.meta.url)
const __dirname = path.dirname(__filename)

// For now, we'll store progress in a JSON file
// In production, this would use PostgreSQL
const PROGRESS_FILE = path.join(__dirname, '../../../../data/progress.json')

async function ensureDataDir() {
  const dataDir = path.dirname(PROGRESS_FILE)
  try {
    await fs.access(dataDir)
  } catch {
    await fs.mkdir(dataDir, { recursive: true })
  }
}

async function readProgress(): Promise<any> {
  try {
    const data = await fs.readFile(PROGRESS_FILE, 'utf-8')
    return JSON.parse(data)
  } catch {
    return {}
  }
}

async function writeProgress(data: any): Promise<void> {
  await ensureDataDir()
  await fs.writeFile(PROGRESS_FILE, JSON.stringify(data, null, 2))
}

// Save progress
router.post('/', async (req, res, next) => {
  try {
    const { courseId, moduleId, lessonId, userId = 'default', ...progressData } = req.body

    if (!courseId || !moduleId || !lessonId) {
      return res.status(400).json({ error: 'courseId, moduleId, and lessonId are required' })
    }

    const allProgress = await readProgress()

    if (!allProgress[userId]) {
      allProgress[userId] = {}
    }
    if (!allProgress[userId][courseId]) {
      allProgress[userId][courseId] = {}
    }
    if (!allProgress[userId][courseId][moduleId]) {
      allProgress[userId][courseId][moduleId] = {}
    }

    allProgress[userId][courseId][moduleId][lessonId] = {
      ...allProgress[userId][courseId][moduleId][lessonId],
      ...progressData,
      lastUpdated: new Date().toISOString(),
    }

    await writeProgress(allProgress)

    res.json({ success: true })
  } catch (error) {
    next(error)
  }
})

// Get progress
router.get('/:userId?', async (req, res, next) => {
  try {
    const userId = req.params.userId || 'default'
    const allProgress = await readProgress()
    const userProgress = allProgress[userId] || {}

    res.json(userProgress)
  } catch (error) {
    next(error)
  }
})

export default router
