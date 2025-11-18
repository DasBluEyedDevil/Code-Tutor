import { Course } from '../types/content'

// Type definitions for Electron API
declare global {
  interface Window {
    electronAPI?: {
      getCourses: () => Promise<any>
      getCourse: (language: string) => Promise<any>
      executeCode: (language: string, code: string) => Promise<any>
      validateChallenge: (challenge: any, userSubmission: any) => Promise<any>
      validateVisibleTests: (code: string, language: string, testCases: any[]) => Promise<any>
      getProgress: (userId?: string) => Promise<any>
      saveProgress: (courseId: string, moduleId: string, lessonId: string, progressData: any, userId?: string) => Promise<any>
      register: (email: string, password: string, name: string) => Promise<any>
      login: (email: string, password: string) => Promise<any>
      verifyAuth: () => Promise<any>
      checkRuntimes: () => Promise<any>
    }
    isElectron?: boolean
  }
}

// Check if running in Electron
const isElectron = typeof window !== 'undefined' && window.isElectron === true

// Helper to ensure Electron API response format
async function invokeElectron<T>(fn: () => Promise<any>): Promise<T> {
  const response = await fn()
  if (response.success === false) {
    throw new Error(response.error || 'Operation failed')
  }
  // For responses that have a nested result/data field, unwrap it
  if (response.result !== undefined) {
    return response.result
  }
  if (response.course !== undefined) {
    return response.course
  }
  if (response.courses !== undefined) {
    return response.courses
  }
  if (response.progress !== undefined) {
    return response.progress
  }
  if (response.runtimes !== undefined) {
    return response.runtimes
  }
  return response
}

export async function fetchCourses(): Promise<any[]> {
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
): Promise<any> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.executeCode(language, code))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function validateChallenge(challenge: any, userSubmission: any): Promise<any> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.validateChallenge(challenge, userSubmission))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function validateVisibleTests(code: string, language: string, testCases: any[]): Promise<any> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.validateVisibleTests(code, language, testCases))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function getProgress(userId?: string): Promise<any> {
  if (isElectron && window.electronAPI) {
    return invokeElectron(() => window.electronAPI!.getProgress(userId))
  }
  throw new Error('This application only runs in Electron desktop mode')
}

export async function saveProgress(
  courseId: string,
  moduleId: string,
  lessonId: string,
  data: any,
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
