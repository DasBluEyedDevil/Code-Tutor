import React, { Component, ReactNode } from 'react'
import { AlertTriangle } from 'lucide-react'
import { Button } from './Button'
import { Card, CardContent } from './Card'

interface Props {
  children: ReactNode
}

interface State {
  hasError: boolean
  error?: Error
}

export class ErrorBoundary extends Component<Props, State> {
  constructor(props: Props) {
    super(props)
    this.state = { hasError: false }
  }

  static getDerivedStateFromError(error: Error): State {
    return { hasError: true, error }
  }

  componentDidCatch(error: Error, errorInfo: React.ErrorInfo) {
    console.error('ErrorBoundary caught an error:', error, errorInfo)
  }

  render() {
    if (this.state.hasError) {
      return (
        <div className="min-h-screen flex items-center justify-center bg-background p-4">
          <Card className="max-w-lg w-full">
            <CardContent className="pt-6 text-center">
              <div className="inline-flex items-center justify-center w-16 h-16 rounded-full bg-red-100 dark:bg-red-900/20 mb-4">
                <AlertTriangle className="w-8 h-8 text-red-600 dark:text-red-400" />
              </div>
              <h2 className="text-2xl font-bold mb-2">Something went wrong</h2>
              <p className="text-muted-foreground mb-4">
                An unexpected error occurred. Please try refreshing the page.
              </p>
              {this.state.error && (
                <details className="text-left mb-6">
                  <summary className="cursor-pointer text-sm text-muted-foreground hover:text-foreground mb-2">
                    Error details
                  </summary>
                  <pre className="text-xs bg-secondary p-3 rounded overflow-auto max-h-32">
                    {this.state.error.toString()}
                  </pre>
                </details>
              )}
              <Button onClick={() => window.location.reload()}>
                Reload Page
              </Button>
            </CardContent>
          </Card>
        </div>
      )
    }

    return this.props.children
  }
}
