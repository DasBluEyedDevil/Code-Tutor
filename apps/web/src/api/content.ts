import axios from 'axios'
import { Course } from '../types/content'

const API_BASE_URL = import.meta.env.VITE_API_URL || '/api'

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
})

export async function fetchCourse(language: string): Promise<Course> {
  const response = await api.get(`/courses/${language}`)
  return response.data
}

export async function executeCode(
  language: string,
  code: string,
  testCases?: any[]
): Promise<any> {
  const response = await api.post('/execute', {
    language,
    code,
    testCases,
  })
  return response.data
}

export async function saveProgress(
  courseId: string,
  moduleId: string,
  lessonId: string,
  data: any
): Promise<void> {
  await api.post('/progress', {
    courseId,
    moduleId,
    lessonId,
    ...data,
  })
}
