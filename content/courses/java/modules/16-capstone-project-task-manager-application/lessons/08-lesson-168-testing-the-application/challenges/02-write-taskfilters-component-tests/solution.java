import { render, screen, fireEvent } from '@testing-library/react';
import { describe, it, expect, vi } from 'vitest';
import TaskFilters from '../TaskFilters';

describe('TaskFilters', () => {
  const defaultFilters = {
    status: 'ALL',
    priority: 'ALL',
    search: '',
  };

  it('renders status and priority dropdowns', () => {
    render(<TaskFilters filters={defaultFilters} onFilterChange={() => {}} />);
    
    expect(screen.getByLabelText(/status/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/priority/i)).toBeInTheDocument();
  });

  it('renders search input', () => {
    render(<TaskFilters filters={defaultFilters} onFilterChange={() => {}} />);
    
    expect(screen.getByPlaceholderText(/search/i)).toBeInTheDocument();
  });

  it('calls onFilterChange when status changes', () => {
    const handleChange = vi.fn();
    render(<TaskFilters filters={defaultFilters} onFilterChange={handleChange} />);
    
    fireEvent.change(screen.getByLabelText(/status/i), {
      target: { value: 'COMPLETED' },
    });
    
    expect(handleChange).toHaveBeenCalledWith({
      ...defaultFilters,
      status: 'COMPLETED',
    });
  });

  it('calls onFilterChange when priority changes', () => {
    const handleChange = vi.fn();
    render(<TaskFilters filters={defaultFilters} onFilterChange={handleChange} />);
    
    fireEvent.change(screen.getByLabelText(/priority/i), {
      target: { value: 'HIGH' },
    });
    
    expect(handleChange).toHaveBeenCalledWith({
      ...defaultFilters,
      priority: 'HIGH',
    });
  });

  it('calls onFilterChange when search input changes', () => {
    const handleChange = vi.fn();
    render(<TaskFilters filters={defaultFilters} onFilterChange={handleChange} />);
    
    fireEvent.change(screen.getByPlaceholderText(/search/i), {
      target: { value: 'urgent' },
    });
    
    expect(handleChange).toHaveBeenCalledWith({
      ...defaultFilters,
      search: 'urgent',
    });
  });

  it('displays current filter values', () => {
    const activeFilters = {
      status: 'PENDING',
      priority: 'HIGH',
      search: 'meeting',
    };
    render(<TaskFilters filters={activeFilters} onFilterChange={() => {}} />);
    
    expect(screen.getByLabelText(/status/i)).toHaveValue('PENDING');
    expect(screen.getByLabelText(/priority/i)).toHaveValue('HIGH');
    expect(screen.getByPlaceholderText(/search/i)).toHaveValue('meeting');
  });
});