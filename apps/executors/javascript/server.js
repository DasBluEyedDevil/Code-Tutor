import express from 'express'
import cors from 'cors'
import { VM } from 'vm2'

const app = express()
const PORT = process.env.PORT || 4005

app.use(cors())
app.use(express.json())

// Security: Timeout for code execution
const EXECUTION_TIMEOUT = 5000 // 5 seconds
const MAX_OUTPUT_LENGTH = 10000

function executeJavaScriptCode(code, testCases = null) {
  const startTime = Date.now()
  let output = ''
  let error = null
  let success = true

  // Custom console.log that captures output
  const capturedLogs = []
  const customConsole = {
    log: (...args) => {
      capturedLogs.push(args.map(arg => String(arg)).join(' '))
    },
    error: (...args) => {
      capturedLogs.push('ERROR: ' + args.map(arg => String(arg)).join(' '))
    },
    warn: (...args) => {
      capturedLogs.push('WARN: ' + args.map(arg => String(arg)).join(' '))
    }
  }

  try {
    // Create sandboxed VM
    const vm = new VM({
      timeout: EXECUTION_TIMEOUT,
      sandbox: {
        console: customConsole,
        // Provide safe built-ins
        Math,
        Date,
        JSON,
        Array,
        Object,
        String,
        Number,
        Boolean,
      },
      eval: false,
      wasm: false,
    })

    // Execute code
    vm.run(code)

    output = capturedLogs.join('\n')

    // Truncate if too long
    if (output.length > MAX_OUTPUT_LENGTH) {
      output = output.substring(0, MAX_OUTPUT_LENGTH) + '\n... (output truncated)'
    }

    // Run test cases if provided
    let testResults = null
    if (testCases && Array.isArray(testCases)) {
      testResults = runTestCases(code, testCases)
    }

    return {
      success: true,
      output: output || '(No output)',
      error: null,
      executionTime: Date.now() - startTime,
      testResults,
    }

  } catch (err) {
    success = false
    error = err.message

    // Check for timeout
    if (err.message && err.message.includes('Script execution timed out')) {
      error = 'Code execution timed out after 5 seconds'
    }

    return {
      success: false,
      output: capturedLogs.join('\n'),
      error,
      executionTime: Date.now() - startTime,
    }
  }
}

function runTestCases(code, testCases) {
  let passed = 0
  let failed = 0
  const details = []

  for (const test of testCases) {
    try {
      const capturedLogs = []
      const customConsole = {
        log: (...args) => {
          capturedLogs.push(args.map(arg => String(arg)).join(' '))
        }
      }

      const vm = new VM({
        timeout: EXECUTION_TIMEOUT,
        sandbox: {
          console: customConsole,
          Math,
          Date,
          JSON,
          Array,
          Object,
          String,
          Number,
          Boolean,
        },
        eval: false,
        wasm: false,
      })

      vm.run(code)

      const actualOutput = capturedLogs.join('\n').trim()
      const expectedOutput = test.expectedOutput?.trim() || ''

      if (actualOutput === expectedOutput) {
        passed++
        details.push({
          testId: test.id,
          passed: true,
          expected: expectedOutput,
          actual: actualOutput,
        })
      } else {
        failed++
        details.push({
          testId: test.id,
          passed: false,
          expected: expectedOutput,
          actual: actualOutput,
          message: `Expected '${expectedOutput}' but got '${actualOutput}'`,
        })
      }

    } catch (err) {
      failed++
      details.push({
        testId: test.id,
        passed: false,
        expected: test.expectedOutput,
        actual: '',
        message: `Error: ${err.message}`,
      })
    }
  }

  return {
    passed,
    failed,
    details,
  }
}

function executeTypeScriptCode(code, testCases = null) {
  // For now, TypeScript execution would require compilation
  // We'll implement proper TypeScript support later
  // For basic cases, treat it as JavaScript
  return executeJavaScriptCode(code, testCases)
}

app.get('/health', (req, res) => {
  res.json({ status: 'ok', service: 'javascript-executor' })
})

app.post('/execute', (req, res) => {
  try {
    const { code, testCases, language } = req.body

    if (!code) {
      return res.status(400).json({
        success: false,
        error: 'No code provided',
      })
    }

    let result
    if (language === 'typescript') {
      result = executeTypeScriptCode(code, testCases)
    } else {
      result = executeJavaScriptCode(code, testCases)
    }

    res.json(result)

  } catch (error) {
    res.status(500).json({
      success: false,
      error: `Server error: ${error.message}`,
    })
  }
})

app.listen(PORT, () => {
  console.log(`🟨 JavaScript/TypeScript executor service running on port ${PORT}...`)
})
