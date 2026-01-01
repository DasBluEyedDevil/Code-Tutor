---
type: "CODE"
title: "TaskCard Component"
---

Create a reusable TaskCard component for individual task display:

```typescript
import React from 'react';

interface TaskCardProps {
  id: string;
  title: string;
  description: string;
  completed: boolean;
  createdAt: string;
  onToggle: (id: string) => void;
  onDelete: (id: string) => void;
}

const TaskCard: React.FC<TaskCardProps> = ({
  id,
  title,
  description,
  completed,
  createdAt,
  onToggle,
  onDelete
}) => {
  const formattedDate = new Date(createdAt).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric'
  });

  return (
    <div className={`task-card ${completed ? 'completed' : ''}`}>
      <div className="task-card-header">
        <input
          type="checkbox"
          checked={completed}
          onChange={() => onToggle(id)}
          className="task-card-checkbox"
          aria-label={`Toggle task: ${title}`}
        />
        <h3 className="task-card-title">{title}</h3>
      </div>
      
      <p className="task-card-description">{description}</p>
      
      <div className="task-card-footer">
        <span className="task-card-date">Created: {formattedDate}</span>
        <button
          onClick={() => onDelete(id)}
          className="task-card-delete-btn"
          aria-label={`Delete task: ${title}`}
        >
          Delete
        </button>
      </div>
    </div>
  );
};

export default TaskCard;
```
