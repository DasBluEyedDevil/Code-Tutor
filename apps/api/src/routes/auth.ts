import { Router } from 'express'
import jwt from 'jsonwebtoken'
import bcrypt from 'bcrypt'

const router = Router()

// Placeholder for authentication
// In production, this would use PostgreSQL and proper user management

const JWT_SECRET = process.env.JWT_SECRET || 'your-secret-key-change-in-production'

// Register (placeholder)
router.post('/register', async (req, res, next) => {
  try {
    const { email, password, name } = req.body

    if (!email || !password || !name) {
      return res.status(400).json({ error: 'Email, password, and name are required' })
    }

    // Hash password
    const hashedPassword = await bcrypt.hash(password, 10)

    // In production, save to database
    // For now, just return a token
    const token = jwt.sign({ email, name }, JWT_SECRET, { expiresIn: '7d' })

    res.json({
      user: { id: '1', email, name },
      token,
    })
  } catch (error) {
    next(error)
  }
})

// Login (placeholder)
router.post('/login', async (req, res, next) => {
  try {
    const { email, password } = req.body

    if (!email || !password) {
      return res.status(400).json({ error: 'Email and password are required' })
    }

    // In production, verify against database
    // For now, just return a token
    const token = jwt.sign({ email, name: 'Demo User' }, JWT_SECRET, { expiresIn: '7d' })

    res.json({
      user: { id: '1', email, name: 'Demo User' },
      token,
    })
  } catch (error) {
    next(error)
  }
})

// Verify token
router.get('/verify', async (req, res, next) => {
  try {
    const token = req.headers.authorization?.replace('Bearer ', '')

    if (!token) {
      return res.status(401).json({ error: 'No token provided' })
    }

    const decoded = jwt.verify(token, JWT_SECRET) as any

    res.json({
      valid: true,
      user: decoded,
    })
  } catch (error) {
    res.status(401).json({ error: 'Invalid token' })
  }
})

export default router
