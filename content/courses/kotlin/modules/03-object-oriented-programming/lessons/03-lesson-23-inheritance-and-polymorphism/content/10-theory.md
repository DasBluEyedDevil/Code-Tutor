---
type: "THEORY"
title: "Exercise 1: Employee Hierarchy"
---


**Goal**: Create an employee management system with inheritance.

**Requirements**:
1. Abstract class `Employee` with properties: `name`, `id`, `baseSalary`
2. Abstract method: `calculateSalary(): Double`
3. Method: `displayInfo()`
4. Class `FullTimeEmployee` extends `Employee`:
   - Adds `bonus` property
   - Implements `calculateSalary()` as baseSalary + bonus
5. Class `Contractor` extends `Employee`:
   - Adds `hourlyRate` and `hoursWorked` properties
   - Implements `calculateSalary()` as hourlyRate * hoursWorked
6. Class `Intern` extends `Employee`:
   - Adds `stipend` property
   - Implements `calculateSalary()` as stipend (fixed amount)
7. Create a list of mixed employees and calculate total payroll

---

