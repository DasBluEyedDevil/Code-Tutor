---
type: "THEORY"
title: "The Protocol Folder Structure"
---

Serverpod projects have a specific folder structure. The most important folder for models is the **protocol/** folder.

**Project Structure Overview:**

```
my_project/
├── my_project_server/           # Server-side Dart code
│   ├── lib/
│   │   └── src/
│   │       ├── endpoints/       # API endpoint classes
│   │       └── generated/       # Auto-generated server code
│   └── protocol/                # YOUR MODEL DEFINITIONS GO HERE
│       ├── user.yaml
│       ├── post.yaml
│       └── comment.yaml
│
├── my_project_client/           # Generated client library
│   └── lib/
│       └── src/
│           └── protocol/        # Auto-generated client models
│
└── my_project_flutter/          # Your Flutter app
    └── lib/
        └── ...                  # Uses my_project_client
```

**Key Points:**

1. **protocol/ folder**: This is where you write your YAML model definitions. Every .yaml file here becomes a model.

2. **generated/ folders**: Never edit these! They are recreated every time you run serverpod generate.

3. **my_project_client/**: This entire package is generated. Your Flutter app imports it to get type-safe access to your models and endpoints.

4. **Naming Convention**: The project name (my_project) becomes the prefix for all generated packages.

