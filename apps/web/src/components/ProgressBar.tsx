interface ProgressBarProps {
  value: number
  max?: number
  showLabel?: boolean
  size?: 'sm' | 'md' | 'lg'
  variant?: 'default' | 'success' | 'warning'
}

export function ProgressBar({
  value,
  max = 100,
  showLabel = false,
  size = 'md',
  variant = 'default',
}: ProgressBarProps) {
  const percentage = Math.min(Math.round((value / max) * 100), 100)

  const heights = {
    sm: 'h-1',
    md: 'h-2',
    lg: 'h-3',
  }

  const colors = {
    default: 'bg-primary',
    success: 'bg-green-500',
    warning: 'bg-yellow-500',
  }

  return (
    <div className="w-full">
      <div className={`relative w-full ${heights[size]} bg-secondary rounded-full overflow-hidden`}>
        <div
          className={`${colors[variant]} ${heights[size]} rounded-full transition-all duration-500 ease-out`}
          style={{ width: `${percentage}%` }}
        ></div>
      </div>
      {showLabel && (
        <div className="mt-1 text-sm text-muted-foreground text-right">{percentage}%</div>
      )}
    </div>
  )
}
