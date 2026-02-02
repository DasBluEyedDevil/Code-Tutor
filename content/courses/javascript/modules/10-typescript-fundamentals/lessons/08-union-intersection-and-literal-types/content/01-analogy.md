---
type: "ANALOGY"
title: "Beyond Basic Types - The Power of Combination"
---

Imagine a restaurant menu. Basic types are like single ingredients: chicken, rice, vegetables. But real dishes are combinations. A 'stir-fry' is chicken AND vegetables AND sauce (intersection - must have all). A 'protein option' could be chicken OR beef OR tofu (union - pick one). And 'spice level: mild | medium | hot' are literal choices - exactly those three options, nothing else.

TypeScript's type system works the same way. You're not limited to primitive types like string or number. You can combine types in powerful ways: unions let a value be one of several types, intersections merge multiple types into one, and literal types restrict values to specific options. This is where TypeScript's type system becomes truly expressive - you can model exactly what your data looks like, not just 'it's an object' but 'it's an object with these specific fields that can only have these specific values.'