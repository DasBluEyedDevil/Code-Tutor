---
type: "THEORY"
title: "Method Reference"
---

**Transformation methods (return new iterator):**
| Method | Description | Example |
|--------|-------------|--------|
| `.map(fn)` | Transform each value | `iter.map(x => x * 2)` |
| `.filter(fn)` | Keep matching values | `iter.filter(x => x > 0)` |
| `.take(n)` | First n values | `iter.take(5)` |
| `.drop(n)` | Skip first n values | `iter.drop(10)` |
| `.flatMap(fn)` | Map and flatten | `iter.flatMap(x => [x, x])` |

**Consumption methods (return value, exhaust iterator):**
| Method | Description | Example |
|--------|-------------|--------|
| `.toArray()` | Collect all values | `iter.toArray()` |
| `.forEach(fn)` | Execute for each | `iter.forEach(console.log)` |
| `.some(fn)` | Any match? | `iter.some(x => x > 10)` |
| `.every(fn)` | All match? | `iter.every(x => x > 0)` |
| `.find(fn)` | First match | `iter.find(x => x > 10)` |
| `.reduce(fn, init)` | Accumulate | `iter.reduce((a, x) => a + x, 0)` |