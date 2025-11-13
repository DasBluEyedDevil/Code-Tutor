import { Router } from 'express'
import axios from 'axios'

const router = Router()

// Map language IDs to executor service URLs
const EXECUTOR_URLS: Record<string, string> = {
  python: process.env.PYTHON_EXECUTOR_URL || 'http://localhost:4000',
  java: process.env.JAVA_EXECUTOR_URL || 'http://localhost:4001',
  kotlin: process.env.KOTLIN_EXECUTOR_URL || 'http://localhost:4002',
  rust: process.env.RUST_EXECUTOR_URL || 'http://localhost:4003',
  csharp: process.env.CSHARP_EXECUTOR_URL || 'http://localhost:4004',
  javascript: process.env.JAVASCRIPT_EXECUTOR_URL || 'http://localhost:4005',
  'javascript-typescript': process.env.JAVASCRIPT_EXECUTOR_URL || 'http://localhost:4005',
  typescript: process.env.TYPESCRIPT_EXECUTOR_URL || 'http://localhost:4006',
  flutter: process.env.FLUTTER_EXECUTOR_URL || 'http://localhost:4007',
  dart: process.env.FLUTTER_EXECUTOR_URL || 'http://localhost:4007',
}

interface ExecuteRequest {
  language: string
  code: string
  testCases?: Array<{
    id: string
    input: string | null
    expectedOutput: string
    description: string
  }>
}

router.post('/', async (req, res, next) => {
  try {
    const { language, code, testCases }: ExecuteRequest = req.body

    if (!language || !code) {
      return res.status(400).json({ error: 'Language and code are required' })
    }

    const executorUrl = EXECUTOR_URLS[language.toLowerCase()]
    if (!executorUrl) {
      return res.status(400).json({ error: `Unsupported language: ${language}` })
    }

    const startTime = Date.now()

    try {
      // Call the executor service
      const response = await axios.post(
        `${executorUrl}/execute`,
        {
          code,
          testCases,
        },
        {
          timeout: 10000, // 10 second timeout
        }
      )

      const executionTime = Date.now() - startTime

      res.json({
        ...response.data,
        executionTime,
      })
    } catch (error: any) {
      if (error.code === 'ECONNREFUSED') {
        return res.status(503).json({
          success: false,
          error: `Executor service for ${language} is not available. Please ensure the executor is running.`,
          executionTime: Date.now() - startTime,
        })
      }

      if (error.response) {
        return res.status(error.response.status).json(error.response.data)
      }

      throw error
    }
  } catch (error) {
    next(error)
  }
})

export default router
