import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { renderHook, waitFor } from '@testing-library/react'
import { useAutoSave } from '../useAutoSave'

describe('useAutoSave', () => {
  beforeEach(() => {
    vi.useFakeTimers()
  })

  afterEach(() => {
    vi.restoreAllMocks()
  })

  it('calls save function after delay when enabled', async () => {
    const saveFn = vi.fn()
    const { rerender } = renderHook(
      ({ data }) => useAutoSave(data, saveFn, { enabled: true, delay: 1000 }),
      { initialProps: { data: 'initial' } }
    )

    // Change data
    rerender({ data: 'changed' })

    // Should not call immediately
    expect(saveFn).not.toHaveBeenCalled()

    // Fast-forward time
    vi.advanceTimersByTime(1000)

    await waitFor(() => {
      expect(saveFn).toHaveBeenCalledWith('changed')
      expect(saveFn).toHaveBeenCalledTimes(1)
    })
  })

  it('does not call save function when disabled', async () => {
    const saveFn = vi.fn()
    const { rerender } = renderHook(
      ({ data }) => useAutoSave(data, saveFn, { enabled: false, delay: 1000 }),
      { initialProps: { data: 'initial' } }
    )

    rerender({ data: 'changed' })
    vi.advanceTimersByTime(1000)

    expect(saveFn).not.toHaveBeenCalled()
  })

  it('debounces multiple rapid changes', async () => {
    const saveFn = vi.fn()
    const { rerender } = renderHook(
      ({ data }) => useAutoSave(data, saveFn, { enabled: true, delay: 1000 }),
      { initialProps: { data: 'initial' } }
    )

    // Make multiple rapid changes
    rerender({ data: 'change1' })
    vi.advanceTimersByTime(500)
    rerender({ data: 'change2' })
    vi.advanceTimersByTime(500)
    rerender({ data: 'change3' })

    // Fast-forward past the delay
    vi.advanceTimersByTime(1000)

    await waitFor(() => {
      // Should only save once with the last value
      expect(saveFn).toHaveBeenCalledWith('change3')
      expect(saveFn).toHaveBeenCalledTimes(1)
    })
  })

  it('respects custom delay', async () => {
    const saveFn = vi.fn()
    const { rerender } = renderHook(
      ({ data }) => useAutoSave(data, saveFn, { enabled: true, delay: 2000 }),
      { initialProps: { data: 'initial' } }
    )

    rerender({ data: 'changed' })

    // Should not save at 1000ms
    vi.advanceTimersByTime(1000)
    expect(saveFn).not.toHaveBeenCalled()

    // Should save at 2000ms
    vi.advanceTimersByTime(1000)
    await waitFor(() => {
      expect(saveFn).toHaveBeenCalledWith('changed')
    })
  })

  it('cleans up timeout on unmount', () => {
    const saveFn = vi.fn()
    const { unmount, rerender } = renderHook(
      ({ data }) => useAutoSave(data, saveFn, { enabled: true, delay: 1000 }),
      { initialProps: { data: 'initial' } }
    )

    rerender({ data: 'changed' })
    unmount()

    vi.advanceTimersByTime(1000)
    expect(saveFn).not.toHaveBeenCalled()
  })

  it('does not save when data has not changed', async () => {
    const saveFn = vi.fn()
    renderHook(() => useAutoSave('unchanged', saveFn, { enabled: true, delay: 1000 }))

    vi.advanceTimersByTime(1000)
    expect(saveFn).not.toHaveBeenCalled()
  })
})
