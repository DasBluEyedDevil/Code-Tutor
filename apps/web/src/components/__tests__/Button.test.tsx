import { describe, it, expect, vi } from 'vitest'
import { render, screen, fireEvent } from '@testing-library/react'
import { Button } from '../Button'

describe('Button', () => {
  it('renders button with children', () => {
    render(<Button>Click me</Button>)
    expect(screen.getByText('Click me')).toBeInTheDocument()
  })

  it('calls onClick when clicked', () => {
    const handleClick = vi.fn()
    render(<Button onClick={handleClick}>Click me</Button>)

    fireEvent.click(screen.getByText('Click me'))
    expect(handleClick).toHaveBeenCalledTimes(1)
  })

  it('is disabled when disabled prop is true', () => {
    render(<Button disabled>Disabled</Button>)
    const button = screen.getByText('Disabled')
    expect(button).toBeDisabled()
  })

  it('shows loading state when isLoading is true', () => {
    render(<Button isLoading>Loading</Button>)
    const button = screen.getByRole('button')
    expect(button).toHaveAttribute('aria-busy', 'true')
    expect(button).toBeDisabled()
  })

  it('does not call onClick when disabled', () => {
    const handleClick = vi.fn()
    render(<Button disabled onClick={handleClick}>Disabled</Button>)

    fireEvent.click(screen.getByText('Disabled'))
    expect(handleClick).not.toHaveBeenCalled()
  })

  it('applies variant styles correctly', () => {
    const { rerender } = render(<Button variant="primary">Primary</Button>)
    let button = screen.getByText('Primary')
    expect(button.className).toContain('bg-primary')

    rerender(<Button variant="secondary">Secondary</Button>)
    button = screen.getByText('Secondary')
    expect(button.className).toContain('bg-secondary')

    rerender(<Button variant="ghost">Ghost</Button>)
    button = screen.getByText('Ghost')
    expect(button.className).toContain('hover:bg-secondary')
  })

  it('applies size styles correctly', () => {
    const { rerender } = render(<Button size="sm">Small</Button>)
    let button = screen.getByText('Small')
    expect(button.className).toContain('text-sm')

    rerender(<Button size="md">Medium</Button>)
    button = screen.getByText('Medium')
    expect(button.className).toContain('px-4')

    rerender(<Button size="lg">Large</Button>)
    button = screen.getByText('Large')
    expect(button.className).toContain('text-lg')
  })

  it('has proper accessibility attributes', () => {
    render(<Button isLoading>Loading</Button>)
    const button = screen.getByRole('button')

    expect(button).toHaveAttribute('aria-busy', 'true')
    expect(button).toHaveAttribute('aria-disabled', 'true')
  })

  it('forwards ref correctly', () => {
    const ref = vi.fn()
    render(<Button ref={ref}>With Ref</Button>)
    expect(ref).toHaveBeenCalled()
  })
})
