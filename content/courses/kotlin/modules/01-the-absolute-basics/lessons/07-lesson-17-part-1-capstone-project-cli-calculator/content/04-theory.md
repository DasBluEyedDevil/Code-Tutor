---
type: "THEORY"
title: "Project Architecture"
---


We'll structure our calculator with several components to keep the code organized and maintainable. This follows the **Single Responsibility Principle** you learned in the functions lesson.

1. **Data Models**: We'll create a simple way to store each calculation so we can show a history later.
2. **Core Functions**: Separate functions for each math operation.
3. **UI Functions**: Functions that handle printing menus and results to the screen.
4. **Input Functions**: Specialized functions that handle reading user input and validating it (handling nulls and non-numbers).
5. **Main Program**: The "orchestrator" that ties everything together in a loop.

---



```kotlin
1. Data Models
   - Calculation (stores a single calculation)

2. Core Functions
   - add(), subtract(), multiply(), divide(), modulus()
   - formatResult()

3. UI Functions
   - displayMenu()
   - displayHistory()
   - clearHistory()

4. Input Functions
   - getNumber()
   - getMenuChoice()

5. Main Program
   - Main loop
   - Menu handling
   - Operation execution
```
