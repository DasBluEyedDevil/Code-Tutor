---
type: "THEORY"
title: "Project Structure: Components, Pages, Services, Context"
---

A well-organized React project follows a clear structure that separates concerns and makes code easy to navigate.

```
frontend/
  src/
    components/           # Reusable UI components
      common/
        Button.jsx
        Input.jsx
        Modal.jsx
        LoadingSpinner.jsx
      layout/
        Header.jsx
        Sidebar.jsx
        Footer.jsx
      tasks/
        TaskCard.jsx
        TaskList.jsx
        TaskForm.jsx
        TaskFilters.jsx
      categories/
        CategoryBadge.jsx
        CategorySelect.jsx
    
    pages/                # Page-level components (routes)
      LoginPage.jsx
      RegisterPage.jsx
      DashboardPage.jsx
      TasksPage.jsx
      SettingsPage.jsx
    
    services/             # API communication layer
      api.js              # Axios instance with interceptors
      authService.js      # Login, register, token management
      taskService.js      # Task CRUD operations
      categoryService.js  # Category CRUD operations
    
    context/              # React Context for global state
      AuthContext.jsx     # Authentication state
      TaskContext.jsx     # Task state (optional)
    
    hooks/                # Custom React hooks
      useAuth.js          # Hook to access auth context
      useTasks.js         # Hook for task operations
    
    utils/                # Helper functions
      formatDate.js
      priorityColors.js
    
    App.jsx               # Root component with routing
    main.jsx              # Entry point
    index.css             # Global styles
```

Component Organization Rules:
1. Components are organized by feature (tasks/, categories/) not by type
2. Pages are top-level route components that compose other components
3. Services handle all API calls - components never call fetch/axios directly
4. Context provides global state without prop drilling
5. Hooks encapsulate reusable stateful logic