---
type: "THEORY"
title: "Solution: Employee Hierarchy"
---



---



```kotlin
abstract class Employee(val name: String, val id: String, val baseSalary: Double) {
    abstract fun calculateSalary(): Double

    open fun displayInfo() {
        println("ID: $id")
        println("Name: $name")
        println("Salary: $${calculateSalary()}")
    }
}

class FullTimeEmployee(
    name: String,
    id: String,
    baseSalary: Double,
    val bonus: Double
) : Employee(name, id, baseSalary) {

    override fun calculateSalary(): Double {
        return baseSalary + bonus
    }

    override fun displayInfo() {
        println("=== Full-Time Employee ===")
        super.displayInfo()
        println("Base Salary: $$baseSalary")
        println("Bonus: $$bonus")
    }
}

class Contractor(
    name: String,
    id: String,
    val hourlyRate: Double,
    val hoursWorked: Double
) : Employee(name, id, 0.0) {

    override fun calculateSalary(): Double {
        return hourlyRate * hoursWorked
    }

    override fun displayInfo() {
        println("=== Contractor ===")
        super.displayInfo()
        println("Hourly Rate: $$hourlyRate")
        println("Hours Worked: $hoursWorked")
    }
}

class Intern(
    name: String,
    id: String,
    val stipend: Double
) : Employee(name, id, 0.0) {

    override fun calculateSalary(): Double {
        return stipend
    }

    override fun displayInfo() {
        println("=== Intern ===")
        super.displayInfo()
        println("Monthly Stipend: $$stipend")
    }
}

fun main() {
    val employees: List<Employee> = listOf(
        FullTimeEmployee("Alice Johnson", "FT001", 80000.0, 10000.0),
        FullTimeEmployee("Bob Smith", "FT002", 75000.0, 8000.0),
        Contractor("Carol Davis", "CT001", 50.0, 160.0),
        Contractor("David Wilson", "CT002", 60.0, 120.0),
        Intern("Eve Brown", "IN001", 2000.0),
        Intern("Frank Miller", "IN002", 1800.0)
    )

    employees.forEach { employee ->
        employee.displayInfo()
        println()
    }

    val totalPayroll = employees.sumOf { it.calculateSalary() }
    println("=== Payroll Summary ===")
    println("Total Employees: ${employees.size}")
    println("Total Payroll: $$totalPayroll")
}
```
