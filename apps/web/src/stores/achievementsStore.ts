import { create } from 'zustand'
import { persist } from 'zustand/middleware'

export interface Achievement {
  id: string
  title: string
  description: string
  icon: string
  requirement: {
    type: 'lessons_completed' | 'course_completed' | 'streak_days' | 'tests_passed' | 'code_runs'
    target: number
    courseId?: string
  }
  earned: boolean
  earnedAt?: string
  progress: number
}

export interface Bookmark {
  id: string
  courseId: string
  moduleId: string
  lessonId: string
  title: string
  createdAt: string
  note?: string
}

interface AchievementsStore {
  achievements: Achievement[]
  bookmarks: Bookmark[]
  stats: {
    totalLessonsCompleted: number
    totalCoursesCompleted: number
    currentStreak: number
    longestStreak: number
    totalTestsPassed: number
    totalCodeRuns: number
    lastActiveDate: string
  }

  // Achievements
  initializeAchievements: () => void
  checkAndUnlockAchievements: () => void
  getEarnedAchievements: () => Achievement[]
  getUnlockedCount: () => number

  // Bookmarks
  addBookmark: (bookmark: Omit<Bookmark, 'id' | 'createdAt'>) => void
  removeBookmark: (id: string) => void
  getBookmarks: () => Bookmark[]

  // Stats
  incrementLessonsCompleted: () => void
  incrementCoursesCompleted: () => void
  incrementTestsPassed: (count: number) => void
  incrementCodeRuns: () => void
  updateStreak: () => void
}

const defaultAchievements: Omit<Achievement, 'earned' | 'earnedAt' | 'progress'>[] = [
  {
    id: 'first-lesson',
    title: 'First Steps',
    description: 'Complete your first lesson',
    icon: '🎯',
    requirement: { type: 'lessons_completed', target: 1 },
  },
  {
    id: 'ten-lessons',
    title: 'Getting Started',
    description: 'Complete 10 lessons',
    icon: '📚',
    requirement: { type: 'lessons_completed', target: 10 },
  },
  {
    id: 'fifty-lessons',
    title: 'Dedicated Learner',
    description: 'Complete 50 lessons',
    icon: '🌟',
    requirement: { type: 'lessons_completed', target: 50 },
  },
  {
    id: 'hundred-lessons',
    title: 'Century',
    description: 'Complete 100 lessons',
    icon: '💯',
    requirement: { type: 'lessons_completed', target: 100 },
  },
  {
    id: 'first-course',
    title: 'Course Graduate',
    description: 'Complete your first course',
    icon: '🎓',
    requirement: { type: 'course_completed', target: 1 },
  },
  {
    id: 'three-courses',
    title: 'Multi-linguist',
    description: 'Complete 3 different courses',
    icon: '🗣️',
    requirement: { type: 'course_completed', target: 3 },
  },
  {
    id: 'week-streak',
    title: 'Week Warrior',
    description: 'Maintain a 7-day learning streak',
    icon: '🔥',
    requirement: { type: 'streak_days', target: 7 },
  },
  {
    id: 'month-streak',
    title: 'Consistency Champion',
    description: 'Maintain a 30-day learning streak',
    icon: '⚡',
    requirement: { type: 'streak_days', target: 30 },
  },
  {
    id: 'hundred-tests',
    title: 'Test Master',
    description: 'Pass 100 test cases',
    icon: '✅',
    requirement: { type: 'tests_passed', target: 100 },
  },
  {
    id: 'hundred-runs',
    title: 'Code Runner',
    description: 'Run code 100 times',
    icon: '▶️',
    requirement: { type: 'code_runs', target: 100 },
  },
]

export const useAchievementsStore = create<AchievementsStore>()(
  persist(
    (set, get) => ({
      achievements: [],
      bookmarks: [],
      stats: {
        totalLessonsCompleted: 0,
        totalCoursesCompleted: 0,
        currentStreak: 0,
        longestStreak: 0,
        totalTestsPassed: 0,
        totalCodeRuns: 0,
        lastActiveDate: new Date().toISOString().split('T')[0],
      },

      initializeAchievements: () => {
        const { achievements } = get()
        if (achievements.length === 0) {
          set({
            achievements: defaultAchievements.map((a) => ({
              ...a,
              earned: false,
              progress: 0,
            })),
          })
        }
      },

      checkAndUnlockAchievements: () => {
        const { achievements, stats } = get()
        const updated = achievements.map((achievement) => {
          if (achievement.earned) return achievement

          let progress = 0
          let shouldUnlock = false

          switch (achievement.requirement.type) {
            case 'lessons_completed':
              progress = Math.min(
                (stats.totalLessonsCompleted / achievement.requirement.target) * 100,
                100
              )
              shouldUnlock = stats.totalLessonsCompleted >= achievement.requirement.target
              break
            case 'course_completed':
              progress = Math.min(
                (stats.totalCoursesCompleted / achievement.requirement.target) * 100,
                100
              )
              shouldUnlock = stats.totalCoursesCompleted >= achievement.requirement.target
              break
            case 'streak_days':
              progress = Math.min((stats.currentStreak / achievement.requirement.target) * 100, 100)
              shouldUnlock = stats.currentStreak >= achievement.requirement.target
              break
            case 'tests_passed':
              progress = Math.min(
                (stats.totalTestsPassed / achievement.requirement.target) * 100,
                100
              )
              shouldUnlock = stats.totalTestsPassed >= achievement.requirement.target
              break
            case 'code_runs':
              progress = Math.min((stats.totalCodeRuns / achievement.requirement.target) * 100, 100)
              shouldUnlock = stats.totalCodeRuns >= achievement.requirement.target
              break
          }

          return {
            ...achievement,
            progress,
            earned: shouldUnlock || achievement.earned,
            earnedAt: shouldUnlock && !achievement.earned ? new Date().toISOString() : achievement.earnedAt,
          }
        })

        set({ achievements: updated })
      },

      getEarnedAchievements: () => {
        return get().achievements.filter((a) => a.earned)
      },

      getUnlockedCount: () => {
        return get().achievements.filter((a) => a.earned).length
      },

      addBookmark: (bookmark) => {
        const { bookmarks } = get()
        const newBookmark: Bookmark = {
          ...bookmark,
          id: `bookmark-${Date.now()}-${Math.random().toString(36).substr(2, 9)}`,
          createdAt: new Date().toISOString(),
        }
        set({ bookmarks: [...bookmarks, newBookmark] })
      },

      removeBookmark: (id) => {
        set((state) => ({
          bookmarks: state.bookmarks.filter((b) => b.id !== id),
        }))
      },

      getBookmarks: () => {
        return get().bookmarks.sort(
          (a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
        )
      },

      incrementLessonsCompleted: () => {
        set((state) => ({
          stats: {
            ...state.stats,
            totalLessonsCompleted: state.stats.totalLessonsCompleted + 1,
          },
        }))
        get().checkAndUnlockAchievements()
      },

      incrementCoursesCompleted: () => {
        set((state) => ({
          stats: {
            ...state.stats,
            totalCoursesCompleted: state.stats.totalCoursesCompleted + 1,
          },
        }))
        get().checkAndUnlockAchievements()
      },

      incrementTestsPassed: (count) => {
        set((state) => ({
          stats: {
            ...state.stats,
            totalTestsPassed: state.stats.totalTestsPassed + count,
          },
        }))
        get().checkAndUnlockAchievements()
      },

      incrementCodeRuns: () => {
        set((state) => ({
          stats: {
            ...state.stats,
            totalCodeRuns: state.stats.totalCodeRuns + 1,
          },
        }))
        get().checkAndUnlockAchievements()
      },

      updateStreak: () => {
        const { stats } = get()
        const today = new Date().toISOString().split('T')[0]
        const lastActive = stats.lastActiveDate

        if (lastActive === today) {
          // Already updated today
          return
        }

        const lastActiveDate = new Date(lastActive)
        const todayDate = new Date(today)
        const diffDays = Math.floor(
          (todayDate.getTime() - lastActiveDate.getTime()) / (1000 * 60 * 60 * 24)
        )

        let newStreak = stats.currentStreak

        if (diffDays === 1) {
          // Consecutive day
          newStreak = stats.currentStreak + 1
        } else if (diffDays > 1) {
          // Streak broken
          newStreak = 1
        }

        set((state) => ({
          stats: {
            ...state.stats,
            currentStreak: newStreak,
            longestStreak: Math.max(newStreak, state.stats.longestStreak),
            lastActiveDate: today,
          },
        }))

        get().checkAndUnlockAchievements()
      },
    }),
    {
      name: 'achievements-storage',
    }
  )
)
