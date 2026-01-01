---
type: "THEORY"
title: "Complete Solution"
---


Here's the full, working program:


---



```kotlin
// ========================================
// DISPLAY HELPER FUNCTIONS
// ========================================

fun printDecorativeLine() {
    println("═══════════════════════════════════════════════════")
}

fun printSectionHeader(title: String) {
    println()
    printDecorativeLine()
    println("  $title")
    printDecorativeLine()
    println()
}

fun printSimpleLine() {
    println("---")
}

// ========================================
// CALCULATION FUNCTIONS
// ========================================

fun calculateFutureAge(currentAge: Int, yearsInFuture: Int): Int {
    return currentAge + yearsInFuture
}

fun calculateBirthDecade(birthYear: Int): String {
    val decade = (birthYear / 10) * 10
    return "${decade}s"
}

fun metersToFeet(meters: Double): Double {
    return meters * 3.28084
}

fun multiplyNumber(number: Int, multiplier: Int): Int {
    return number * multiplier
}

// ========================================
// INPUT FUNCTION
// ========================================

fun displayWelcome() {
    println("╔════════════════════════════════════════╗")
    println("║  PERSONAL PROFILE GENERATOR            ║")
    println("╚════════════════════════════════════════╝")
    println()
    println("Let's create your profile!")
    println("Please answer the following questions:")
    println()
}

// ========================================
// PROFILE DISPLAY FUNCTION
// ========================================

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

// ========================================
// MAIN PROGRAM
// ========================================

fun main() {
    // Display welcome message
    displayWelcome()

    // Collect user data
    print("What is your name? ")
    val name = readln()

    print("How old are you? ")
    val age = readln().toInt()

    print("What year were you born? ")
    val birthYear = readln().toInt()

    print("What is your height in meters? (e.g., 1.75) ")
    val heightMeters = readln().toDouble()

    print("What is your favorite hobby? ")
    val hobby = readln()

    print("What is your favorite number? ")
    val favoriteNumber = readln().toInt()

    print("What is your dream job? ")
    val dreamJob = readln()

    // Perform calculations
    val ageIn10Years = calculateFutureAge(age, 10)
    val ageIn20Years = calculateFutureAge(age, 20)
    val birthDecade = calculateBirthDecade(birthYear)
    val heightFeet = metersToFeet(heightMeters)
    val doubledNumber = multiplyNumber(favoriteNumber, 2)
    val tripledNumber = multiplyNumber(favoriteNumber, 3)

    // Display beautiful profile
    displayProfile(
        name, age, birthYear, heightMeters, heightFeet,
        hobby, favoriteNumber, dreamJob,
        ageIn10Years, ageIn20Years, birthDecade,
        doubledNumber, tripledNumber
    )
}
```
