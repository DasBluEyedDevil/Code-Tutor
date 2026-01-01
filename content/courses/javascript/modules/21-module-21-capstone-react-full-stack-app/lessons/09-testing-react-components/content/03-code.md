---
type: "CODE"
title: "Test User Interactions"
---

Simulate user actions:

```typescript
import { userEvent } from '@testing-library/user-event';
import { waitFor } from '@testing-library/react';

it('submits form', async () => {
  const user = userEvent.setup();
  const onSuccess = vi.fn();
  render(<TaskForm onSuccess={onSuccess} />);

  await user.type(screen.getByLabelText(/title/i), 'My Task');
  await user.click(screen.getByRole('button'));

  await waitFor(() => {
    expect(onSuccess).toHaveBeenCalled();
  });
});
```
