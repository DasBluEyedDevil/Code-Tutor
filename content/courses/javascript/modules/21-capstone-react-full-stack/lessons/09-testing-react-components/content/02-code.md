---
type: "EXAMPLE"
title: "Test Rendering"
---

Verify components render correctly:

```typescript
import { render, screen } from '@testing-library/react';
import TaskForm from '../components/TaskForm';

describe('TaskForm', () => {
  it('renders form fields', () => {
    render(<TaskForm />);
    expect(screen.getByLabelText(/title/i)).toBeInTheDocument();
  });
});
```
