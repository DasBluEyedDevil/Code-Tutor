import express from 'express'
import cors from 'cors'
import dotenv from 'dotenv'
import coursesRouter from './routes/courses.js'
import executeRouter from './routes/execute.js'
import progressRouter from './routes/progress.js'
import authRouter from './routes/auth.js'

dotenv.config()

const app = express()
const PORT = process.env.PORT || 3001

// Middleware
app.use(cors())
app.use(express.json())

// Request logging
app.use((req, res, next) => {
  console.log(`${new Date().toISOString()} - ${req.method} ${req.path}`)
  next()
})

// Routes
app.use('/api/courses', coursesRouter)
app.use('/api/execute', executeRouter)
app.use('/api/progress', progressRouter)
app.use('/api/auth', authRouter)

// Health check
app.get('/health', (req, res) => {
  res.json({ status: 'ok', timestamp: new Date().toISOString() })
})

// Error handling
app.use((err: any, req: express.Request, res: express.Response, next: express.NextFunction) => {
  console.error('Error:', err)
  res.status(err.status || 500).json({
    error: err.message || 'Internal server error',
  })
})

app.listen(PORT, () => {
  console.log(`🚀 API server running on http://localhost:${PORT}`)
  console.log(`📚 Courses endpoint: http://localhost:${PORT}/api/courses`)
  console.log(`⚡ Execute endpoint: http://localhost:${PORT}/api/execute`)
})
