export interface CourseMetadata {
  id: string
  language: string
  version: string
  displayName: string
  description: string
  totalModules: number
  estimatedHours: number
  difficulty: 'beginner' | 'intermediate' | 'advanced' | 'beginner-to-advanced'
  prerequisites: string[]
  learningOutcomes: string[]
  icon: string
  color: string
}

export interface CodeExample {
  id: string
  language: string
  code: string
  explanation: string
  runnable: boolean
  highlightLines?: number[]
}

export interface TestCase {
  id: string
  input: string | null
  expectedOutput: string
  description: string
}

export interface ValidationRules {
  mustContain?: string[]
  mustNotContain?: string[]
  maxLines?: number
  allowedPackages?: string[]
  customValidator?: string | null
}

export interface Exercise {
  id: string
  type: 'coding' | 'quiz' | 'project'
  title: string
  instructions: string
  difficulty: 'beginner' | 'intermediate' | 'advanced'
  estimatedMinutes: number
  starterCode: string
  solution: string
  hints: string[]
  testCases: TestCase[]
  validationRules: ValidationRules
}

export interface QuizQuestion {
  id: string
  type: 'multiple-choice' | 'true-false' | 'fill-in-blank'
  question: string
  points: number
  options?: string[]
  correctAnswer: number | boolean | string
  explanation: string
}

export interface Quiz {
  id: string
  passingScore: number
  questions: QuizQuestion[]
}

export interface LessonContent {
  format: 'markdown'
  body: string
  bodyFile?: string
  codeExamples: CodeExample[]
}

export interface Lesson {
  id: string
  title: string
  type: 'reading' | 'interactive' | 'project'
  order: number
  estimatedMinutes: number
  difficulty: 'beginner' | 'intermediate' | 'advanced'
  tags: string[]
  content: LessonContent
  exercises: Exercise[]
  quiz?: Quiz
}

export interface Module {
  id: string
  title: string
  description: string
  order: number
  estimatedHours: number
  prerequisites: string[]
  lessons: Lesson[]
}

export interface LanguageConfig {
  executionEngine: string
  compilerOptions: {
    version: string
    flags: string[]
  }
  editorSettings: {
    defaultTemplate: string
    fileExtension: string
    monacoLanguageId: string
    tabSize: number
    insertSpaces: boolean
  }
  sandboxConstraints: {
    maxExecutionTimeMs: number
    maxMemoryMB: number
    maxOutputChars: number
    allowedPackages: string[]
    blockedPackages: string[]
  }
}

export interface Course {
  courseMetadata: CourseMetadata
  modules: Module[]
  languageConfig: LanguageConfig
}

export interface ExecutionResult {
  success: boolean
  output: string
  error?: string
  executionTime: number
  testResults?: {
    passed: number
    failed: number
    details: Array<{
      testId: string
      passed: boolean
      expected: string
      actual: string
      message?: string
    }>
  }
}
