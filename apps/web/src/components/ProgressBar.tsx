import { memo, useMemo } from 'react'

interface ProgressBarProps {
  value: number
  max?: number
  showLabel?: boolean
  size?: 'sm' | 'md' | 'lg'
  variant?: 'default' | 'success' | 'warning'
  label?: string
}

export const ProgressBar = memo(function ProgressBar({
  value,
  max = 100,
  showLabel = false,
  size = 'md',
  variant = 'default',
  label,
}: ProgressBarProps) {
  const percentage = useMemo(() =>
    Math.min(Math.round((value / max) * 100), 100),
    [value, max]
  )

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
      <div
        className={`relative w-full ${heights[size]} bg-secondary rounded-full overflow-hidden`}
        role="progressbar"
        aria-valuenow={value}
        aria-valuemin={0}
        aria-valuemax={max}
        aria-label={label || `Progress: ${percentage}%`}
      >
        <div
          className={`${colors[variant]} ${heights[size]} rounded-full transition-all duration-500 ease-out`}
          style={{ width: `${percentage}%` }}
          aria-hidden="true"
        ></div>
      </div>
      {showLabel && (
        <div className="mt-1 text-sm text-muted-foreground text-right" aria-hidden="true">
          {percentage}%
        </div>
      )}
    </div>
  )
})
