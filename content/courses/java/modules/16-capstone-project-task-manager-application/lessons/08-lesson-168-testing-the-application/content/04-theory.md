---
type: "THEORY"
title: "React Component Tests with Vitest and React Testing Library"
---

For the React frontend, we use Vitest (a fast test runner compatible with Vite) and React Testing Library (for component testing that focuses on user behavior).

Setup Vitest:
```bash
npm install -D vitest @testing-library/react @testing-library/jest-dom jsdom
```

Configure Vitest (vite.config.js):
```javascript
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  test: {
    globals: true,
    environment: 'jsdom',
    setupFiles: './src/test/setup.js',
  },
})
```

Setup File (src/test/setup.js):
```javascript
import '@testing-library/jest-dom';
```

Testing Components:

```jsx
// src/components/tasks/__tests__/TaskCard.test.jsx
import { render, screen, fireEvent } from '@testing-library/react';
import { describe, it, expect, vi } from 'vitest';
import TaskCard from '../TaskCard';

describe('TaskCard', () => {
  const mockTask = {
    id: 1,
    title: 'Test Task',
    description: 'Task description',
    status: 'PENDING',
    priority: 'HIGH',
    dueDate: '2024-12-31',
  };

  it('renders task title and status', () => {
    render(<TaskCard task={mockTask} onEdit={() => {}} onDelete={() => {}} />);
    
    expect(screen.getByText('Test Task')).toBeInTheDocument();
    expect(screen.getByText(/pending/i)).toBeInTheDocument();
  });

  it('applies priority-based styling', () => {
    render(<TaskCard task={mockTask} onEdit={() => {}} onDelete={() => {}} />);
    
    const card = screen.getByText('Test Task').closest('div');
    expect(card).toHaveClass('border-orange-500'); // HIGH priority
  });

  it('shows overdue indicator for past due date', () => {
    const overdueTask = {
      ...mockTask,
      dueDate: '2020-01-01', // Past date
    };
    render(<TaskCard task={overdueTask} onEdit={() => {}} onDelete={() => {}} />);
    
    expect(screen.getByText(/overdue/i)).toBeInTheDocument();
  });

  it('calls onEdit when edit button clicked', () => {
    const handleEdit = vi.fn();
    render(<TaskCard task={mockTask} onEdit={handleEdit} onDelete={() => {}} />);
    
    fireEvent.click(screen.getByText('Edit'));
    expect(handleEdit).toHaveBeenCalledWith(1);
  });

  it('calls onDelete when delete button clicked', () => {
    const handleDelete = vi.fn();
    render(<TaskCard task={mockTask} onEdit={() => {}} onDelete={handleDelete} />);
    
    fireEvent.click(screen.getByText('Delete'));
    expect(handleDelete).toHaveBeenCalledWith(1);
  });
});

// src/components/tasks/__tests__/TaskForm.test.jsx
import { render, screen, fireEvent, waitFor } from '@testing-library/react';
import { describe, it, expect, vi, beforeEach } from 'vitest';
import { BrowserRouter } from 'react-router-dom';
import TaskForm from '../TaskForm';
import { taskService } from '../../../services/taskService';

vi.mock('../../../services/taskService');

const renderWithRouter = (component) => {
  return render(<BrowserRouter>{component}</BrowserRouter>);
};

describe('TaskForm', () => {
  beforeEach(() => {
    vi.clearAllMocks();
  });

  it('renders form fields', () => {
    renderWithRouter(<TaskForm />);
    
    expect(screen.getByLabelText(/title/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/description/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/status/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/priority/i)).toBeInTheDocument();
  });

  it('shows validation error for empty title', async () => {
    renderWithRouter(<TaskForm />);
    
    fireEvent.click(screen.getByText(/create task/i));
    
    await waitFor(() => {
      expect(screen.getByText(/title is required/i)).toBeInTheDocument();
    });
  });

  it('submits form with valid data', async () => {
    taskService.createTask.mockResolvedValue({ id: 1 });
    renderWithRouter(<TaskForm />);
    
    fireEvent.change(screen.getByLabelText(/title/i), {
      target: { value: 'New Task' },
    });
    fireEvent.click(screen.getByText(/create task/i));
    
    await waitFor(() => {
      expect(taskService.createTask).toHaveBeenCalledWith(
        expect.objectContaining({ title: 'New Task' })
      );
    });
  });

  it('shows loading state during submission', async () => {
    taskService.createTask.mockImplementation(
      () => new Promise(resolve => setTimeout(resolve, 100))
    );
    renderWithRouter(<TaskForm />);
    
    fireEvent.change(screen.getByLabelText(/title/i), {
      target: { value: 'New Task' },
    });
    fireEvent.click(screen.getByText(/create task/i));
    
    expect(screen.getByText(/saving/i)).toBeInTheDocument();
  });
});
```

Run frontend tests: npm test

Testing philosophy:
- Test behavior, not implementation details
- Query by accessible elements (role, label, text)
- Use fireEvent or userEvent for interactions
- Mock API calls with vi.mock()