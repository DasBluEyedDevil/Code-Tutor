import { create } from 'zustand'
import { persist } from 'zustand/middleware'

interface LessonProgress {
  lessonId: string
  completed: boolean
  lastAccessed: string
  codeSubmissions: number
  timeSpent: number
}

interface CourseProgress {
  [courseId: string]: {
    [moduleId: string]: {
      [lessonId: string]: LessonProgress
    }
  }
}

interface ProgressStore {
  progress: CourseProgress
  markLessonComplete: (courseId: string, moduleId: string, lessonId: string) => void
  updateLessonProgress: (courseId: string, moduleId: string, lessonId: string, data: Partial<LessonProgress>) => void
  getLessonProgress: (courseId: string, moduleId: string, lessonId: string) => LessonProgress | undefined
  getCourseProgress: (courseId: string) => number
}

export const useProgressStore = create<ProgressStore>()(
  persist(
    (set, get) => ({
      progress: {},
      markLessonComplete: (courseId, moduleId, lessonId) => {
        set((state) => {
          const newProgress = { ...state.progress }
          if (!newProgress[courseId]) newProgress[courseId] = {}
          if (!newProgress[courseId][moduleId]) newProgress[courseId][moduleId] = {}

          newProgress[courseId][moduleId][lessonId] = {
            ...newProgress[courseId][moduleId][lessonId],
            lessonId,
            completed: true,
            lastAccessed: new Date().toISOString(),
            codeSubmissions: (newProgress[courseId][moduleId][lessonId]?.codeSubmissions || 0) + 1,
            timeSpent: newProgress[courseId][moduleId][lessonId]?.timeSpent || 0,
          }

          return { progress: newProgress }
        })
      },
      updateLessonProgress: (courseId, moduleId, lessonId, data) => {
        set((state) => {
          const newProgress = { ...state.progress }
          if (!newProgress[courseId]) newProgress[courseId] = {}
          if (!newProgress[courseId][moduleId]) newProgress[courseId][moduleId] = {}

          newProgress[courseId][moduleId][lessonId] = {
            ...newProgress[courseId][moduleId][lessonId],
            lessonId,
            lastAccessed: new Date().toISOString(),
            completed: false,
            codeSubmissions: 0,
            timeSpent: 0,
            ...data,
          }

          return { progress: newProgress }
        })
      },
      getLessonProgress: (courseId, moduleId, lessonId) => {
        const state = get()
        return state.progress[courseId]?.[moduleId]?.[lessonId]
      },
      getCourseProgress: (courseId) => {
        const state = get()
        const courseData = state.progress[courseId]
        if (!courseData) return 0

        let totalLessons = 0
        let completedLessons = 0

        Object.values(courseData).forEach((module) => {
          Object.values(module).forEach((lesson) => {
            totalLessons++
            if (lesson.completed) completedLessons++
          })
        })

        return totalLessons > 0 ? Math.round((completedLessons / totalLessons) * 100) : 0
      },
    }),
    {
      name: 'progress-storage',
    }
  )
)
