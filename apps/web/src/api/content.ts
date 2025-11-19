import { Course } from '../types/content'
import type { Challenge, TestCase, ValidationResponse } from '../types/interactive'

// Electron API response types
interface ElectronResponse<T> {
  success: boolean
  data?: T
  error?: string
}

interface ExecutionResult {
  success: boolean
  output?: string
  error?: string
  exitCode?: number
}

interface ProgressData {
  [courseId: string]: {
    [moduleId: string]: {
      [lessonId: string]: {
        completed: boolean
        lastAccessed: string
        score?: number
      }
    }
  }
}

interface AuthResult {
  success: boolean
  user?: {
    id: string
    email: string
    name: string
  }
  token?: string
}

interface RuntimeCheckResult {
  python: boolean
  node: boolean
  java?: boolean
  rust?: boolean
}

// Type definitions for Electron API
declare global {
  interface Window {
    electronAPI?: {
      getCourses: () => Promise<ElectronResponse<Course[]>>
      getCourse: (language: string) => Promise<ElectronResponse<Course>>
      executeCode: (language: string, code: string) => Promise<ElectronResponse<ExecutionResult>>
      validateChallenge: (challenge: Challenge, userSubmission: unknown) => Promise<ElectronResponse<ValidationResponse>>
      validateVisibleTests: (code: string, language: string, testCases: TestCase[]) => Promise<ElectronResponse<ValidationResponse>>
      getProgress: (userId?: string) => Promise<ElectronResponse<ProgressData>>
      saveProgress: (courseId: string, moduleId: string, lessonId: string, progressData: Record<string, unknown>, userId?: string) => Promise<ElectronResponse<void>>
      register: (email: string, password: string, name: string) => Promise<ElectronResponse<AuthResult>>
      login: (email: string, password: string) => Promise<ElectronResponse<AuthResult>>
      verifyAuth: () => Promise<ElectronResponse<AuthResult>>
      checkRuntimes: () => Promise<ElectronResponse<RuntimeCheckResult>>
    }
    isElectron?: boolean
  }
}

// Check if running in Electron
const isElectron = typeof window !== 'undefined' && window.isElectron === true

// Helper to ensure Electron API response format
async function invokeElectron<T>(fn: () => Promise<ElectronResponse<T>>): Promise<T> {
  const response = await fn()
  if (response.success === false) {
    throw new Error(response.error || 'Operation failed')
  }
  // For responses that have a data field, unwrap it
  if (response.data !== undefined) {
    return response.data
  }
  // Fallback: return response as-is (for legacy format)
  return response as unknown as T
}

export async function fetchCourses(): Promise<Course[]> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.getCourses())
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function fetchCourse(language: string): Promise<Course> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.getCourse(language))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function executeCode(
  language: string,
  code: string
): Promise<ExecutionResult> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.executeCode(language, code))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function validateChallenge(challenge: Challenge, userSubmission: unknown): Promise<ValidationResponse> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.validateChallenge(challenge, userSubmission))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function validateVisibleTests(code: string, language: string, testCases: TestCase[]): Promise<ValidationResponse> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.validateVisibleTests(code, language, testCases))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function getProgress(userId?: string): Promise<ProgressData> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.getProgress(userId))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function saveProgress(
  courseId: string,
  moduleId: string,
  lessonId: string,
  data: Record<string, unknown>,
  userId?: string
): Promise<void> {
  if (isElectron && window.electronAPI) {
    await invokeElectron(() => window.electronAPI!.saveProgress(courseId, moduleId, lessonId, data, userId))
    return
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function register(email: string, password: string, name: string): Promise<any> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.register(email, password, name))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function login(email: string, password: string): Promise<any> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.login(email, password))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function verifyAuth(): Promise<any> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.verifyAuth())
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function checkRuntimes(): Promise<any> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.checkRuntimes())
  }
  throw new Error('This application only runs in Electron desktop mode')
}
