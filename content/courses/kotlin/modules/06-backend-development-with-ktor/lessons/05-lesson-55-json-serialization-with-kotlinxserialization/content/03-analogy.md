---
type: "ANALOGY"
title: "💡 The Concept: What Is Serialization?"
---


### The Translation Analogy

Imagine you have a letter written in English, and you need to send it to someone who only reads Spanish.

**Serialization** = Translating English → Spanish

**Deserialization** = Translating Spanish → English

### Why Do We Need It?

**Problem**: Kotlin objects only exist in memory on your server. How do you send them over the internet?

**Solution**: Convert them to a **text format** (JSON) that any programming language can understand.


### JSON Basics Refresher

**JSON** (JavaScript Object Notation) is a text format for data:


**Supported types:**
- **Numbers**: `42`, `3.14`
- **Strings**: `"hello"`
- **Booleans**: `true`, `false`
- **null**: `null`
- **Arrays**: `[1, 2, 3]`
- **Objects**: `{"key": "value"}`

---



```json
{
  "id": 1,
  "title": "1984",
  "author": "George Orwell",
  "year": 1949,
  "inStock": true,
  "price": 12.99,
  "tags": ["fiction", "dystopia"],
  "publisher": null
}
```
