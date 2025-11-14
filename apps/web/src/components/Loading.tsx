export function LoadingSpinner({ size = 'md' }: { size?: 'sm' | 'md' | 'lg' }) {
  const sizeClasses = {
    sm: 'h-4 w-4',
    md: 'h-8 w-8',
    lg: 'h-12 w-12',
  }

  return (
    <div className="flex items-center justify-center">
      <svg
        className={`animate-spin ${sizeClasses[size]} text-primary`}
        xmlns="http://www.w3.org/2000/svg"
        fill="none"
        viewBox="0 0 24 24"
      >
        <circle
          className="opacity-25"
          cx="12"
          cy="12"
          r="10"
          stroke="currentColor"
          strokeWidth="4"
        ></circle>
        <path
          className="opacity-75"
          fill="currentColor"
          d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
        ></path>
      </svg>
    </div>
  )
}

export function SkeletonLine({ className }: { className?: string }) {
  return <div className={`animate-pulse bg-secondary/50 rounded ${className}`}></div>
}

export function SkeletonCard() {
  return (
    <div className="bg-card rounded-xl border p-6 animate-pulse">
      <SkeletonLine className="h-6 w-3/4 mb-4" />
      <SkeletonLine className="h-4 w-full mb-2" />
      <SkeletonLine className="h-4 w-5/6" />
    </div>
  )
}

export function SkeletonLesson() {
  return (
    <div className="flex items-center gap-4 p-4 border-b animate-pulse">
      <div className="w-5 h-5 rounded-full bg-secondary/50"></div>
      <div className="flex-1">
        <SkeletonLine className="h-5 w-2/3 mb-2" />
        <SkeletonLine className="h-3 w-1/2" />
      </div>
      <div className="w-5 h-5 bg-secondary/50 rounded"></div>
    </div>
  )
}
