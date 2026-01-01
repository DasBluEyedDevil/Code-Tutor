---
type: "CONCEPT"
title: "Functional Components with TypeScript"
---

Modern React uses functional components with hooks instead of class components. Functional components are:

- **Simpler**: Less boilerplate than class components
- **Composable**: Easier to share stateful logic via custom hooks
- **Testable**: Easier to test pure functions than classes

With TypeScript, we add strong typing to our components, props, and state, catching errors at compile time rather than runtime.

**Component Signature**
A typed React component is a function that takes props and returns JSX:
```typescript
interface MyComponentProps {
  title: string;
  onAction: () => void;
}

const MyComponent: React.FC<MyComponentProps> = ({ title, onAction }) => {
  return <div onClick={onAction}>{title}</div>;
};
```

**Common Hook Patterns**
- `useState`: Manage local component state
- `useEffect`: Handle side effects (data fetching, subscriptions)
- `useContext`: Access values from context without prop drilling