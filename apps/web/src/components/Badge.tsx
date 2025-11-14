import { clsx } from 'clsx'

interface BadgeProps {
  children: React.ReactNode
  variant?: 'default' | 'success' | 'warning' | 'error' | 'info'
  className?: string
}

export function Badge({ children, variant = 'default', className }: BadgeProps) {
  return (
    <span
      className={clsx(
        'inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium',
        {
          'bg-secondary text-secondary-foreground': variant === 'default',
          'bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-200': variant === 'success',
          'bg-yellow-100 text-yellow-800 dark:bg-yellow-900 dark:text-yellow-200':
            variant === 'warning',
          'bg-red-100 text-red-800 dark:bg-red-900 dark:text-red-200': variant === 'error',
          'bg-blue-100 text-blue-800 dark:bg-blue-900 dark:text-blue-200': variant === 'info',
        },
        className
      )}
    >
      {children}
    </span>
  )
}
