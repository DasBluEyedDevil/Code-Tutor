import { X, Award, BookMarked, TrendingUp, Flame } from 'lucide-react'
import { Button } from './Button'
import { Card, CardContent } from './Card'
import { Badge } from './Badge'
import { ProgressBar } from './ProgressBar'
import { useFocusTrap } from '../hooks/useFocusTrap'
import { useFocusReturn } from '../hooks/useFocusReturn'
import { useAchievementsStore } from '../stores/achievementsStore'
import { useNavigate } from 'react-router-dom'
import { clsx } from 'clsx'

interface AchievementsProps {
  isOpen: boolean
  onClose: () => void
}

export function Achievements({ isOpen, onClose }: AchievementsProps) {
  const dialogRef = useFocusTrap<HTMLDivElement>(isOpen)
  useFocusReturn()
  const navigate = useNavigate()

  const {
    achievements,
    bookmarks,
    stats,
    removeBookmark,
  } = useAchievementsStore()

  if (!isOpen) return null

  const earnedAchievements = achievements.filter((a) => a.earned)
  const inProgressAchievements = achievements.filter((a) => !a.earned && a.progress > 0)
  const lockedAchievements = achievements.filter((a) => !a.earned && a.progress === 0)

  return (
    <>
      {/* Backdrop */}
      <div
        className="fixed inset-0 bg-black/50 backdrop-blur-sm z-50 animate-fade-in"
        onClick={onClose}
        aria-hidden="true"
      />

      {/* Modal */}
      <div
        ref={dialogRef}
        role="dialog"
        aria-modal="true"
        aria-labelledby="achievements-title"
        className="fixed inset-0 z-50 flex items-center justify-center p-4"
      >
        <div className="bg-background border border-border rounded-lg shadow-2xl max-w-4xl w-full max-h-[85vh] overflow-hidden animate-scale-in">
          {/* Header */}
          <div className="flex items-center justify-between p-6 border-b border-border bg-secondary/20">
            <div className="flex items-center gap-3">
              <div className="bg-gradient-to-br from-yellow-500/20 to-yellow-500/5 p-2 rounded-lg">
                <Award className="w-5 h-5 text-yellow-500" />
              </div>
              <div>
                <h2 id="achievements-title" className="text-2xl font-bold">
                  Achievements & Progress
                </h2>
                <p className="text-sm text-muted-foreground">
                  {earnedAchievements.length} of {achievements.length} unlocked
                </p>
              </div>
            </div>
            <Button
              variant="ghost"
              size="sm"
              onClick={onClose}
              aria-label="Close achievements"
              className="rounded-full"
            >
              <X className="w-5 h-5" />
            </Button>
          </div>

          {/* Content */}
          <div className="overflow-y-auto max-h-[calc(85vh-160px)]">
            {/* Stats Overview */}
            <div className="p-6 border-b border-border bg-secondary/10">
              <h3 className="text-lg font-semibold mb-4 flex items-center gap-2">
                <TrendingUp className="w-5 h-5 text-primary" />
                Your Stats
              </h3>
              <div className="grid grid-cols-2 md:grid-cols-4 gap-4">
                <div className="text-center p-4 bg-background border border-border rounded-lg">
                  <div className="text-3xl font-bold text-blue-500">{stats.totalLessonsCompleted}</div>
                  <div className="text-xs text-muted-foreground mt-1">Lessons Completed</div>
                </div>
                <div className="text-center p-4 bg-background border border-border rounded-lg">
                  <div className="text-3xl font-bold text-green-500">{stats.totalCoursesCompleted}</div>
                  <div className="text-xs text-muted-foreground mt-1">Courses Completed</div>
                </div>
                <div className="text-center p-4 bg-background border border-border rounded-lg">
                  <div className="text-3xl font-bold text-orange-500 flex items-center justify-center gap-1">
                    {stats.currentStreak}
                    <Flame className="w-6 h-6" />
                  </div>
                  <div className="text-xs text-muted-foreground mt-1">Day Streak</div>
                </div>
                <div className="text-center p-4 bg-background border border-border rounded-lg">
                  <div className="text-3xl font-bold text-purple-500">{stats.totalTestsPassed}</div>
                  <div className="text-xs text-muted-foreground mt-1">Tests Passed</div>
                </div>
              </div>
            </div>

            {/* Bookmarks */}
            {bookmarks.length > 0 && (
              <div className="p-6 border-b border-border">
                <h3 className="text-lg font-semibold mb-4 flex items-center gap-2">
                  <BookMarked className="w-5 h-5 text-primary" />
                  Bookmarked Lessons ({bookmarks.length})
                </h3>
                <div className="space-y-2">
                  {bookmarks.slice(0, 5).map((bookmark) => (
                    <div
                      key={bookmark.id}
                      className="flex items-center justify-between p-3 bg-background border border-border rounded-lg hover:border-primary/50 transition-colors"
                    >
                      <div className="flex-1 min-w-0">
                        <div className="font-medium truncate">{bookmark.title}</div>
                        {bookmark.note && (
                          <div className="text-xs text-muted-foreground truncate">{bookmark.note}</div>
                        )}
                      </div>
                      <div className="flex items-center gap-2">
                        <Button
                          variant="ghost"
                          size="sm"
                          onClick={() => {
                            navigate(`/course/${bookmark.courseId}/module/${bookmark.moduleId}/lesson/${bookmark.lessonId}`)
                            onClose()
                          }}
                        >
                          Open
                        </Button>
                        <Button
                          variant="ghost"
                          size="sm"
                          onClick={() => removeBookmark(bookmark.id)}
                        >
                          <X className="w-4 h-4" />
                        </Button>
                      </div>
                    </div>
                  ))}
                </div>
              </div>
            )}

            {/* Earned Achievements */}
            {earnedAchievements.length > 0 && (
              <div className="p-6 border-b border-border">
                <h3 className="text-lg font-semibold mb-4">Unlocked ({earnedAchievements.length})</h3>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  {earnedAchievements.map((achievement) => (
                    <Card key={achievement.id} className="border-green-500/20 bg-green-500/5">
                      <CardContent className="pt-4">
                        <div className="flex items-start gap-3">
                          <div className="text-4xl">{achievement.icon}</div>
                          <div className="flex-1 min-w-0">
                            <div className="font-semibold flex items-center gap-2">
                              {achievement.title}
                              <Badge variant="success" className="text-xs">
                                Unlocked
                              </Badge>
                            </div>
                            <div className="text-sm text-muted-foreground">{achievement.description}</div>
                            {achievement.earnedAt && (
                              <div className="text-xs text-muted-foreground mt-1">
                                Earned {new Date(achievement.earnedAt).toLocaleDateString()}
                              </div>
                            )}
                          </div>
                        </div>
                      </CardContent>
                    </Card>
                  ))}
                </div>
              </div>
            )}

            {/* In Progress Achievements */}
            {inProgressAchievements.length > 0 && (
              <div className="p-6 border-b border-border">
                <h3 className="text-lg font-semibold mb-4">In Progress ({inProgressAchievements.length})</h3>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  {inProgressAchievements.map((achievement) => (
                    <Card key={achievement.id} className="border-blue-500/20">
                      <CardContent className="pt-4">
                        <div className="flex items-start gap-3">
                          <div className="text-4xl opacity-70">{achievement.icon}</div>
                          <div className="flex-1 min-w-0">
                            <div className="font-semibold">{achievement.title}</div>
                            <div className="text-sm text-muted-foreground mb-2">
                              {achievement.description}
                            </div>
                            <ProgressBar
                              value={achievement.progress}
                              variant="default"
                              size="sm"
                              showLabel
                            />
                          </div>
                        </div>
                      </CardContent>
                    </Card>
                  ))}
                </div>
              </div>
            )}

            {/* Locked Achievements */}
            {lockedAchievements.length > 0 && (
              <div className="p-6">
                <h3 className="text-lg font-semibold mb-4">Locked ({lockedAchievements.length})</h3>
                <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                  {lockedAchievements.map((achievement) => (
                    <Card key={achievement.id} className="opacity-50">
                      <CardContent className="pt-4">
                        <div className="flex items-start gap-3">
                          <div className="text-4xl grayscale">{achievement.icon}</div>
                          <div className="flex-1 min-w-0">
                            <div className="font-semibold">{achievement.title}</div>
                            <div className="text-sm text-muted-foreground">
                              {achievement.description}
                            </div>
                          </div>
                        </div>
                      </CardContent>
                    </Card>
                  ))}
                </div>
              </div>
            )}
          </div>
        </div>
      </div>
    </>
  )
}
