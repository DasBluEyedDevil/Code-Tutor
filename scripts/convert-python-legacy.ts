#!/usr/bin/env ts-node

import * as fs from 'fs/promises'
import * as path from 'path'

interface LegacyLesson {
  title: string
  estimated_time?: string
  concept?: string
  code_example?: {
    language: string
    code: string
    output?: string
  }
  syntax_breakdown?: string
  exercise?: {
    instructions: string
    starter_code: string
    hint?: string
  }
  solution?: {
    code: string
    explanation?: string
    common_mistakes?: string
  }
  key_takeaways?: string
}

interface ModuleInfo {
  title: string
  description: string
  lessons: number
}

const MODULE_INFO: Record<string, ModuleInfo> = {
  module_01: { title: "The Absolute Basics", description: "Start your programming journey with Python fundamentals", lessons: 5 },
  module_02: { title: "Storing & Using Information", description: "Learn how to work with variables and data", lessons: 5 },
  module_03: { title: "Making Decisions", description: "Control program flow with conditional statements", lessons: 6 },
  module_04: { title: "Repeating Actions", description: "Master loops and iteration", lessons: 5 },
  module_05: { title: "Grouping Information", description: "Work with lists, tuples, and dictionaries", lessons: 6 },
  module_06: { title: "Creating Reusable Tools", description: "Build and use functions effectively", lessons: 6 },
  module_07: { title: "Handling Mistakes", description: "Error handling and exception management", lessons: 6 },
  module_08: { title: "Blueprints for Code", description: "Introduction to object-oriented programming", lessons: 6 },
  module_09: { title: "Working with the Real World", description: "File I/O and data persistence", lessons: 6 },
  module_10: { title: "Modules & Packages", description: "Organize and share your code", lessons: 6 },
  module_11: { title: "Object-Oriented Programming", description: "Advanced OOP concepts", lessons: 6 },
  module_12: { title: "Advanced Topics", description: "Decorators, generators, and more", lessons: 6 },
  module_13: { title: "Web Development & APIs", description: "Build web applications with Python", lessons: 6 },
  module_14: { title: "Sharing Your Work", description: "Git, testing, and deployment", lessons: 5 },
}

async function convertPythonCourse() {
  const pythonRepoPath = '/tmp/Python-Training-Course'
  const contentPath = path.join(pythonRepoPath, 'content', 'modules')

  console.log('🐍 Converting Python Training Course...\n')

  const modules = []
  let totalLessons = 0

  // Process each module
  for (let modNum = 1; modNum <= 14; modNum++) {
    const modId = `module_${String(modNum).padStart(2, '0')}`
    const modPath = path.join(contentPath, modId)
    const modInfo = MODULE_INFO[modId]

    console.log(`📚 Processing ${modId}: ${modInfo.title}`)

    const lessons = []

    // Process each lesson in the module
    for (let lessonNum = 1; lessonNum <= modInfo.lessons; lessonNum++) {
      const lessonFile = `lesson_${String(lessonNum).padStart(2, '0')}.json`
      const lessonPath = path.join(modPath, lessonFile)

      try {
        const lessonData = await fs.readFile(lessonPath, 'utf-8')
        const legacy: LegacyLesson = JSON.parse(lessonData)

        // Build markdown content from legacy format
        let markdown = `# ${legacy.title}\n\n`

        if (legacy.estimated_time) {
          markdown += `*Estimated time: ${legacy.estimated_time}*\n\n---\n\n`
        }

        // Add concept section
        if (legacy.concept) {
          markdown += `## The Concept\n\n${legacy.concept}\n\n---\n\n`
        }

        // Add code example
        if (legacy.code_example) {
          markdown += `## Code Example\n\n`
          markdown += `\`\`\`${legacy.code_example.language.toLowerCase()}\n`
          markdown += `${legacy.code_example.code}\n`
          markdown += `\`\`\`\n\n`
          if (legacy.code_example.output) {
            markdown += `**Output:**\n\`\`\`\n${legacy.code_example.output}\n\`\`\`\n\n`
          }
          markdown += `---\n\n`
        }

        // Add syntax breakdown
        if (legacy.syntax_breakdown) {
          markdown += `## Syntax Breakdown\n\n${legacy.syntax_breakdown}\n\n---\n\n`
        }

        // Add key takeaways
        if (legacy.key_takeaways) {
          markdown += `## Key Takeaways\n\n${legacy.key_takeaways}\n\n`
        }

        // Create exercise if available
        const exercises = []
        if (legacy.exercise && legacy.solution) {
          exercises.push({
            id: `exercise-${modNum}-${lessonNum}`,
            type: 'coding',
            title: `Practice: ${legacy.title}`,
            instructions: legacy.exercise.instructions || "Complete the code exercise.",
            difficulty: 'beginner',
            estimatedMinutes: 15,
            starterCode: legacy.exercise.starter_code || "# Write your code here\n",
            solution: legacy.solution.code || "",
            hints: legacy.exercise.hint ? [legacy.exercise.hint] : [],
            testCases: [],
            validationRules: {
              mustContain: [],
              mustNotContain: [],
              maxLines: 100,
              allowedPackages: [],
              customValidator: null
            }
          })

          // Add solution explanation to markdown
          if (legacy.solution.explanation) {
            markdown += `\n---\n\n## Solution Explanation\n\n${legacy.solution.explanation}\n\n`
          }

          if (legacy.solution.common_mistakes) {
            markdown += `## Common Mistakes\n\n${legacy.solution.common_mistakes}\n\n`
          }
        }

        // Create lesson object
        const lesson = {
          id: `lesson-${modNum}-${lessonNum}`,
          title: legacy.title,
          type: exercises.length > 0 ? 'interactive' : 'reading',
          order: lessonNum,
          estimatedMinutes: parseInt(legacy.estimated_time || '15'),
          difficulty: modNum <= 4 ? 'beginner' : modNum <= 10 ? 'intermediate' : 'advanced',
          tags: [modInfo.title.toLowerCase(), 'python'],
          content: {
            format: 'markdown',
            body: markdown,
            codeExamples: legacy.code_example ? [{
              id: `example-${modNum}-${lessonNum}`,
              language: 'python',
              code: legacy.code_example.code,
              explanation: `Example for ${legacy.title}`,
              runnable: true,
              highlightLines: []
            }] : []
          },
          exercises
        }

        lessons.push(lesson)
        totalLessons++
        console.log(`  ✓ Lesson ${lessonNum}: ${legacy.title}`)

      } catch (error) {
        console.log(`  ⚠️  Could not read ${lessonFile}`)
      }
    }

    // Create module object
    const module = {
      id: modId,
      title: modInfo.title,
      description: modInfo.description,
      order: modNum - 1,
      estimatedHours: Math.ceil(modInfo.lessons * 0.5),
      prerequisites: modNum > 1 ? [`module_${String(modNum - 1).padStart(2, '0')}`] : [],
      lessons
    }

    modules.push(module)
  }

  // Create full course structure
  const course = {
    courseMetadata: {
      id: 'python',
      language: 'Python',
      version: '1.0',
      displayName: 'Python Full-Stack Development',
      description: 'From fundamentals to full-stack Python development - 73 comprehensive lessons',
      totalModules: 14,
      estimatedHours: 120,
      difficulty: 'beginner-to-advanced',
      prerequisites: [],
      learningOutcomes: [
        'Master Python fundamentals and syntax',
        'Build web applications with Flask',
        'Work with databases and APIs',
        'Deploy production-ready applications',
        'Apply object-oriented programming principles',
        'Handle errors and debug effectively'
      ],
      icon: 'python-icon.svg',
      color: '#3776ab'
    },
    modules,
    languageConfig: {
      executionEngine: 'python-3.11',
      compilerOptions: {
        version: '3.11',
        flags: []
      },
      editorSettings: {
        defaultTemplate: '# Write your Python code here\n',
        fileExtension: '.py',
        monacoLanguageId: 'python',
        tabSize: 4,
        insertSpaces: true
      },
      sandboxConstraints: {
        maxExecutionTimeMs: 5000,
        maxMemoryMB: 256,
        maxOutputChars: 10000,
        allowedPackages: [],
        blockedPackages: ['os', 'sys', 'subprocess']
      }
    }
  }

  // Save to apps/api/content/
  const outputDir = path.join(process.cwd(), 'apps/api/content')
  await fs.mkdir(outputDir, { recursive: true })

  const outputPath = path.join(outputDir, 'python.json')
  await fs.writeFile(outputPath, JSON.stringify(course, null, 2), 'utf-8')

  console.log(`\n✨ Conversion Complete!`)
  console.log(`📊 Statistics:`)
  console.log(`  - Modules: ${modules.length}`)
  console.log(`  - Lessons: ${totalLessons}`)
  console.log(`  - Output: apps/api/content/python.json`)
  console.log(`  - File size: ${(JSON.stringify(course).length / 1024).toFixed(2)} KB`)
}

// Run conversion
convertPythonCourse().catch(error => {
  console.error('❌ Error:', error)
  process.exit(1)
})
