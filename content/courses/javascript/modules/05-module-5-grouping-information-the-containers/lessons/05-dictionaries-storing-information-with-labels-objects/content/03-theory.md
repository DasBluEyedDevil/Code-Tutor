---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Object syntax:

// Creating an object
let objectName = {
  key1: value1,
  key2: value2,
  key3: value3
};

Key points:

1. Surrounded by curly braces { }
2. Key-value pairs separated by commas
3. Key and value separated by colon :
4. Keys are usually unquoted (unless they have spaces)
5. Values can be any type

Accessing properties:

// Dot notation (most common)
object.propertyName

// Bracket notation (for special cases)
object['property name']  // Property has space
object[variableName]     // Property name is in a variable

When to use brackets:
- Property name has spaces/special chars: obj['first name']
- Property name is in a variable: let prop = 'age'; obj[prop]
- Property name is computed: obj['item' + i]

Modifying objects:

// Change existing property
obj.name = 'new value';

// Add new property
obj.newProperty = 'value';

// Delete property
delete obj.property;

// Check if property exists
if (obj.property !== undefined) { }
// OR
if ('property' in obj) { }

Nested structures:
let user = {
  name: 'Alice',
  address: {
    street: '123 Main St',
    city: 'NYC'
  },
  hobbies: ['reading', 'coding']
};

user.address.city  // NYC
user.hobbies[0]    // reading