---
type: "CONCEPT"
title: "Props Typing with TypeScript"
---

TypeScript props ensure components are used correctly. Define an interface for each component's props:

**Basic Props**
```typescript
interface ButtonProps {
  label: string;
  onClick: () => void;
  disabled?: boolean;  // Optional with ?
}
```

**Common Patterns**
- **Optional props**: Use `?` to make a prop optional
- **Union types**: `status: 'loading' | 'success' | 'error'`
- **Event handlers**: `onChange: (value: string) => void`
- **Children**: `children: React.ReactNode`
- **Ref prop**: `ref: React.Ref<HTMLInputElement>`

**Example with multiple prop types**
```typescript
interface TaskCardProps {
  id: string;
  title: string;
  completed: boolean;
  onToggle: (id: string) => void;
  onDelete: (id: string) => void;
  priority?: 'low' | 'medium' | 'high';
}

const TaskCard: React.FC<TaskCardProps> = ({
  id,
  title,
  completed,
  onToggle,
  onDelete,
  priority = 'medium'
}) => {
  return (
    <div className={`task ${completed ? 'done' : ''}`}>
      <h3>{title}</h3>
      {priority && <span className="priority">{priority}</span>}
      <button onClick={() => onToggle(id)}>Toggle</button>
      <button onClick={() => onDelete(id)}>Delete</button>
    </div>
  );
};
```