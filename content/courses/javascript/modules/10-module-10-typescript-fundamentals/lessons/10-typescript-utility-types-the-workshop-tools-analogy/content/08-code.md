---
type: "CODE"
title: "NoInfer<T> (TypeScript 5.4+)"
---

NoInfer<T> is a newer utility type that prevents TypeScript from inferring a type from a specific position. It's an advanced pattern primarily useful for library authors who need precise control over which arguments drive type inference and which should follow along. Without NoInfer, TypeScript might infer an overly-wide type from the wrong argument.

```typescript
// NOINFER<T> - Control type inference in generics (TypeScript 5.4+)
// Prevents a type from being used in inference

// The problem NoInfer solves:
function createState<T>(initial: T, defaultValue: T): { value: T; default: T } {
  return { value: initial, default: defaultValue };
}

// Without NoInfer, TypeScript infers from BOTH arguments:
const state1 = createState('hello', 'default');
// T is inferred as string - OK!

const state2 = createState('specific', 'fallback' as string);
// T might be inferred as string instead of the literal

// WITH NoInfer - control which argument drives inference:
function createStateFixed<T>(initial: T, defaultValue: NoInfer<T>): { value: T; default: T } {
  return { value: initial, default: defaultValue };
}

// Now ONLY 'initial' drives inference, 'defaultValue' must match:
const state3 = createStateFixed('hello', 'fallback');
// T inferred from 'initial' only -> 'hello' literal type!
// 'fallback' must be assignable to T

// Real use case: Default values in generic functions
function getOrDefault<T>(value: T | null, defaultValue: NoInfer<T>): T {
  return value ?? defaultValue;
}

// The type of 'colors' is inferred from the first argument
const colors = ['red', 'green', 'blue'] as const;
const color = getOrDefault(colors[0] ?? null, 'unknown');
// Without NoInfer: T might widen to string
// With NoInfer: T is 'red' | 'green' | 'blue', and 'unknown' must match

// Use case: Event handlers with specific event types
type EventHandler<T extends string> = {
  type: T;
  handler: (event: { type: NoInfer<T>; data: any }) => void;
};

function on<T extends string>(config: EventHandler<T>): void {
  console.log(`Registered handler for: ${config.type}`);
}

// T is inferred from 'type' property only
on({
  type: 'click',
  handler: (event) => {
    // event.type is 'click' (literal), not just string
    console.log(`Handling ${event.type}`);
  }
});

// Use case: Validators with inferred output types
type Validator<T> = {
  validate: (input: unknown) => input is T;
  defaultValue: NoInfer<T>;
};

function createValidator<T>(validator: Validator<T>): (input: unknown) => T {
  return (input: unknown): T => {
    if (validator.validate(input)) {
      return input;
    }
    return validator.defaultValue;
  };
}

const stringValidator = createValidator({
  validate: (x): x is string => typeof x === 'string',
  defaultValue: ''  // Must match the validated type
});

console.log(stringValidator('hello'));  // 'hello'
console.log(stringValidator(123));      // ''

// Use case: Preventing unwanted type widening in options
interface Options<T extends string> {
  mode: T;
  fallbackMode: NoInfer<T>;
}

function configure<T extends string>(options: Options<T>): void {
  console.log(`Mode: ${options.mode}, Fallback: ${options.fallbackMode}`);
}

// T is inferred from 'mode' only
configure({
  mode: 'strict',
  fallbackMode: 'strict'  // Must be same as mode
});

// configure({
//   mode: 'strict',
//   fallbackMode: 'lenient'  // ERROR: 'lenient' not assignable to 'strict'
// });

// Note: NoInfer is most useful for library authors
// In application code, explicit type annotations usually suffice
// But when you need precise inference control, NoInfer is invaluable
```
