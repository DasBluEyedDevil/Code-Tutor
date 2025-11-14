import { ReactNode } from 'react'
import { clsx } from 'clsx'

interface CardProps {
  children: ReactNode
  className?: string
  hover?: boolean
  onClick?: () => void
}

export function Card({ children, className, hover, onClick }: CardProps) {
  return (
    <div
      className={clsx(
        'bg-card rounded-xl border shadow-sm overflow-hidden transition-all duration-200',
        {
          'hover:shadow-lg hover:scale-[1.02] cursor-pointer': hover,
        },
        className
      )}
      onClick={onClick}
    >
      {children}
    </div>
  )
}

export function CardHeader({ children, className }: { children: ReactNode; className?: string }) {
  return <div className={clsx('px-6 py-4 border-b bg-secondary/50', className)}>{children}</div>
}

export function CardContent({ children, className }: { children: ReactNode; className?: string }) {
  return <div className={clsx('px-6 py-4', className)}>{children}</div>
}

export function CardFooter({ children, className }: { children: ReactNode; className?: string }) {
  return <div className={clsx('px-6 py-4 border-t bg-secondary/20', className)}>{children}</div>
}
