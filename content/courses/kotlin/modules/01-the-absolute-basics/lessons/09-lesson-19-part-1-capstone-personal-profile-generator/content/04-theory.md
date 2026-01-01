---
type: "THEORY"
title: "Project Architecture"
---


Before coding, let's plan the structure:


This modular approach makes code easier to write, test, and maintain!

---



```kotlin
Personal Profile Generator
│
├── Data Collection Functions
│   └── getUserInput() - Gets all user data
│
├── Calculation Functions
│   ├── calculateFutureAge(currentAge, years)
│   ├── calculateBirthDecade(birthYear)
│   ├── metersToFeet(meters)
│   └── multiplyNumber(number, multiplier)
│
├── Display Functions
│   ├── printSectionHeader(title)
│   ├── printDecorativeLine()
│   └── displayProfile(userData)
│
└── Main Program
    └── main() - Orchestrates everything
```
