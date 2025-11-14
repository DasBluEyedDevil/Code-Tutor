#!/usr/bin/env node
import { importFromMarkdown, saveCourse, validateCourse, ImportConfig } from './import-content'
import path from 'path'

/**
 * Command-line interface for importing course content
 *
 * Usage:
 *   npm run import -- --source ./path/to/python-course --language python --format markdown
 *   npm run import -- --source ./path/to/java-course --language java --format markdown
 */

interface CLIArgs {
  source?: string
  language?: string
  format?: 'markdown' | 'json' | 'yaml' | 'custom'
  output?: string
  validate?: boolean
  help?: boolean
}

function parseArgs(args: string[]): CLIArgs {
  const parsed: CLIArgs = {}

  for (let i = 0; i < args.length; i++) {
    const arg = args[i]

    if (arg === '--help' || arg === '-h') {
      parsed.help = true
    } else if (arg === '--source' || arg === '-s') {
      parsed.source = args[++i]
    } else if (arg === '--language' || arg === '-l') {
      parsed.language = args[++i]
    } else if (arg === '--format' || arg === '-f') {
      parsed.format = args[++i] as any
    } else if (arg === '--output' || arg === '-o') {
      parsed.output = args[++i]
    } else if (arg === '--validate' || arg === '-v') {
      parsed.validate = true
    }
  }

  return parsed
}

function printHelp() {
  console.log(`
Code Tutor - Content Import Tool

Usage:
  npm run import -- [options]

Options:
  --source, -s      Source directory containing course content (required)
  --language, -l    Programming language (required)
                    Options: python, java, javascript, typescript, kotlin, rust, csharp, flutter
  --format, -f      Content format (default: markdown)
                    Options: markdown, json, yaml, custom
  --output, -o      Output file path (default: apps/api/content/<language>.json)
  --validate, -v    Validate course structure after import
  --help, -h        Show this help message

Examples:
  # Import Python course from markdown files
  npm run import -- --source ~/Python-Course --language python

  # Import Java course with custom output
  npm run import -- -s ~/Java-Course -l java -o ./courses/java.json

  # Import and validate
  npm run import -- -s ~/Kotlin-Course -l kotlin --validate

Markdown File Structure:
  Your source directory can have any structure. Files will be grouped into modules
  based on their directory structure:

  python-course/
  ├── 01-basics/
  │   ├── 01-hello-world.md
  │   ├── 02-variables.md
  │   └── 03-data-types.md
  ├── 02-control-flow/
  │   ├── 01-if-statements.md
  │   └── 02-loops.md
  └── README.md

Markdown Format with Frontmatter:
  ---
  title: Hello World
  description: Your first Python program
  type: lesson
  difficulty: beginner
  estimatedMinutes: 10
  keyTakeaways: print function, strings, comments
  ---

  # Hello World

  In this lesson, you'll write your first Python program.

  ## The Print Function

  \`\`\`python
  print("Hello, World!")
  \`\`\`

  <!-- EXERCISE {"title": "Print Your Name", "instructions": "Modify the code to print your name", "starterCode": "print('...')", "solution": "print('Your Name')", "hints": ["Use the print() function"], "testCases": []} -->

For more information, see: docs/CONTENT_IMPORT.md
`)
}

async function main() {
  const args = parseArgs(process.argv.slice(2))

  if (args.help) {
    printHelp()
    process.exit(0)
  }

  if (!args.source) {
    console.error('❌ Error: --source is required')
    console.log('Run with --help for usage information')
    process.exit(1)
  }

  if (!args.language) {
    console.error('❌ Error: --language is required')
    console.log('Run with --help for usage information')
    process.exit(1)
  }

  const validLanguages = ['python', 'java', 'javascript', 'typescript', 'kotlin', 'rust', 'csharp', 'flutter']
  if (!validLanguages.includes(args.language)) {
    console.error(`❌ Error: Invalid language "${args.language}"`)
    console.error(`   Valid options: ${validLanguages.join(', ')}`)
    process.exit(1)
  }

  const config: ImportConfig = {
    sourceDir: path.resolve(args.source),
    targetDir: path.resolve('apps/api/content'),
    language: args.language,
    format: args.format || 'markdown',
    options: {
      validateContent: args.validate !== false
    }
  }

  const outputPath = args.output || path.join(config.targetDir, `${args.language}.json`)

  try {
    console.log('\n📦 Starting content import...\n')
    console.log(`Source: ${config.sourceDir}`)
    console.log(`Language: ${config.language}`)
    console.log(`Format: ${config.format}`)
    console.log(`Output: ${outputPath}\n`)

    let course

    switch (config.format) {
      case 'markdown':
        course = await importFromMarkdown(config)
        break
      case 'json':
        throw new Error('JSON import not yet implemented - coming soon!')
      case 'yaml':
        throw new Error('YAML import not yet implemented - coming soon!')
      case 'custom':
        throw new Error('Custom import not yet implemented - coming soon!')
      default:
        throw new Error(`Unknown format: ${config.format}`)
    }

    if (args.validate) {
      console.log('\n🔍 Validating course structure...')
      const validation = validateCourse(course)

      if (!validation.valid) {
        console.error('\n❌ Validation failed:')
        validation.errors.forEach(error => console.error(`   - ${error}`))
        process.exit(1)
      }

      console.log('✅ Validation passed')
    }

    await saveCourse(course, outputPath)

    console.log('\n✨ Import complete!\n')
    console.log('Next steps:')
    console.log('  1. Review the generated JSON file')
    console.log('  2. Test the course in the application')
    console.log('  3. Make any necessary adjustments')
    console.log('')

  } catch (error: any) {
    console.error('\n❌ Import failed:', error.message)
    if (error.stack) {
      console.error('\nStack trace:')
      console.error(error.stack)
    }
    process.exit(1)
  }
}

main()
