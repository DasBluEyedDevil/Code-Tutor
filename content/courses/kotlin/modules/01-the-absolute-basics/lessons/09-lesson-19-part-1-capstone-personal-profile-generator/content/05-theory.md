---
type: "THEORY"
title: "Step-by-Step Implementation"
---


Let's build this project step by step!

### Step 1: Create Display Helper Functions

These functions will make our output look professional:


**Why these functions?**
- **Reusability**: Call them whenever you need formatting
- **Consistency**: All section headers look the same
- **Easy to change**: Want different borders? Change once, affects everywhere!

---

### Step 2: Create Calculation Functions

These functions process user data:


**Key points**:
- Each function has a single, clear purpose
- Descriptive names explain what they do
- Parameters and return types are explicit

---

### Step 3: Create Data Input Function

Let's gather all the user information:


This function provides a welcoming introduction. We'll collect the actual data in `main()`.

---

### Step 4: Build the Main Program

Now let's put it all together:


---

### Step 5: Create the Profile Display Function

This function creates the beautiful output:


---



```kotlin
fun displayProfile(
    name: String,
    age: Int,
    birthYear: Int,
    heightMeters: Double,
    heightFeet: Double,
    hobby: String,
    favoriteNumber: Int,
    dreamJob: String,
    ageIn10Years: Int,
    ageIn20Years: Int,
    birthDecade: String,
    doubledNumber: Int,
    tripledNumber: Int
) {
    // Header
    printSectionHeader("YOUR PERSONAL PROFILE")

    // Basic Information
    println("👤 BASIC INFORMATION")
    printSimpleLine()
    println("Name: $name")
    println("Current Age: $age years old")
    println("Birth Year: $birthYear")
    println("Birth Decade: $birthDecade")
    println("Height: ${String.format("%.2f", heightMeters)}m (${String.format("%.2f", heightFeet)} feet)")
    println()

    // Future Projections
    println("🔮 FUTURE PROJECTIONS")
    printSimpleLine()
    println("In 10 years (${2024 + 10}), you will be: $ageIn10Years years old")
    println("In 20 years (${2024 + 20}), you will be: $ageIn20Years years old")
    println()

    // Interests & Dreams
    println("⭐ INTERESTS & DREAMS")
    printSimpleLine()
    println("Favorite Hobby: $hobby")
    println("Dream Job: $dreamJob")
    println()

    // Fun Facts
    println("🎲 FUN NUMBER FACTS")
    printSimpleLine()
    println("Your favorite number: $favoriteNumber")
    println("Doubled: $doubledNumber")
    println("Tripled: $tripledNumber")
    println()

    // Footer
    printDecorativeLine()
    println("     Thank you for using Profile Generator!")
    println("           Keep dreaming big, $name!")
    printDecorativeLine()
}
```
