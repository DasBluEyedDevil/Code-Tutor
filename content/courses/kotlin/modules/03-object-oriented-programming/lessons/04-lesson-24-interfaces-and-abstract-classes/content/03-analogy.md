---
type: "ANALOGY"
title: "The Concept"
---


### What is an Interface?

An **interface** is a contract that defines what a class can do, without specifying how it does it.

**Real-World Analogy: Power Outlets**

A power outlet is an interface:
- **Contract**: "I provide electricity through these two/three holes"
- **Devices** (implementations): Phone chargers, laptops, lamps all plug into the same outlet
- **Different implementations**: Each device uses the electricity differently, but all follow the outlet interface


### Why Interfaces?

**Problems interfaces solve**:
1. **Multiple inheritance**: A class can implement multiple interfaces
2. **Loose coupling**: Code depends on contracts, not implementations
3. **Testability**: Easy to create mock implementations for testing
4. **Flexibility**: Swap implementations without changing client code

---



```kotlin
  Interface: PowerSource
       ↓
  ┌───────────────────────────┐
  │ fun provideElectricity()  │
  └───────────────────────────┘
           ↓         ↓         ↓
     PhoneCharger  Laptop   Lamp
```
