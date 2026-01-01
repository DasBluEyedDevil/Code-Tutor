---
type: "THEORY"
title: "Enum Types for Type Safety"
---

We use enums to ensure only valid values are stored in the database. Create these in the model/enums package.

```java
// com/taskmanager/model/enums/Role.java
package com.taskmanager.model.enums;

public enum Role {
    USER,
    ADMIN
}

// com/taskmanager/model/enums/TaskStatus.java
package com.taskmanager.model.enums;

public enum TaskStatus {
    PENDING,
    IN_PROGRESS,
    COMPLETED,
    CANCELLED
}

// com/taskmanager/model/enums/Priority.java
package com.taskmanager.model.enums;

public enum Priority {
    LOW,
    MEDIUM,
    HIGH,
    URGENT
}
```

Why Enums Instead of Strings?

1. Type Safety: The compiler prevents invalid values. You cannot set status to "DONE" - it must be a TaskStatus value.

2. Refactoring: If you rename PENDING to TODO, your IDE updates all usages. With strings, you would miss some.

3. Documentation: Enums are self-documenting. Anyone reading the code knows exactly what values are valid.

4. No Magic Strings: Strings like "pending" are error-prone (typos, case sensitivity). Enums eliminate this.

5. Database Storage: With @Enumerated(EnumType.STRING), the database stores "PENDING" not 0. This is readable and safer if you reorder enums.