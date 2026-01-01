---
type: "THEORY"
title: "Annotation Targets"
---


Specify where an annotation can be used:


**Common Targets**:
- `CLASS` - classes, interfaces, objects
- `FUNCTION` - functions
- `PROPERTY` - properties
- `FIELD` - backing fields
- `VALUE_PARAMETER` - function parameters
- `CONSTRUCTOR` - constructors
- `EXPRESSION` - expressions
- `FILE` - file

### Use-Site Targets

Specify exactly which part to annotate:


---



```kotlin
class Example(
    @field:Required val name: String,  // Annotate the backing field
    @get:Required val age: Int,        // Annotate the getter
    @param:NotBlank val email: String  // Annotate constructor parameter
)
```
