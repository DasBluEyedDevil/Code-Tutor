---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// TypeScript Generics - Flexible, Reusable Types

// PROBLEM: Writing the same function for different types
function getFirstNumber(arr: number[]): number | undefined {
  return arr[0];
}

function getFirstString(arr: string[]): string | undefined {
  return arr[0];
}

// We need a new function for every type! That's a lot of repetition.

// SOLUTION: Generics - One function that works with ANY type
function getFirst<T>(arr: T[]): T | undefined {
  return arr[0];
}

// Now it works with any type!
console.log(getFirst<number>([1, 2, 3]));           // 1
console.log(getFirst<string>(['a', 'b', 'c']));    // 'a'
console.log(getFirst<boolean>([true, false]));     // true

// TypeScript can also INFER the type automatically!
console.log(getFirst([10, 20, 30]));               // 10 (infers number)
console.log(getFirst(['hello', 'world']));         // 'hello' (infers string)

// GENERIC INTERFACES - Create flexible data structures
interface Box<T> {
  contents: T;
  label: string;
}

let numberBox: Box<number> = { contents: 42, label: 'Answer' };
let stringBox: Box<string> = { contents: 'Hello', label: 'Greeting' };

console.log('Number box:', numberBox.contents);    // 42
console.log('String box:', stringBox.contents);    // 'Hello'

// MULTIPLE TYPE PARAMETERS - Use more than one generic type
function makePair<K, V>(key: K, value: V): { key: K; value: V } {
  return { key, value };
}

let pair1 = makePair<string, number>('age', 25);
let pair2 = makePair<number, boolean>(1, true);
let pair3 = makePair('name', 'Alice');  // Types inferred automatically

console.log('Pair 1:', pair1);  // { key: 'age', value: 25 }
console.log('Pair 2:', pair2);  // { key: 1, value: true }
console.log('Pair 3:', pair3);  // { key: 'name', value: 'Alice' }

// GENERIC CONSTRAINTS - Limit what types are allowed
interface Lengthable {
  length: number;
}

function logLength<T extends Lengthable>(item: T): void {
  console.log('Length:', item.length);
}

logLength('Hello');           // 5 (strings have length)
logLength([1, 2, 3, 4]);      // 4 (arrays have length)
logLength({ length: 10 });    // 10 (object with length property)
// logLength(42);             // ERROR: number doesn't have length!

// GENERIC CLASSES - Reusable data structures
class Stack<T> {
  private items: T[] = [];
  
  push(item: T): void {
    this.items.push(item);
  }
  
  pop(): T | undefined {
    return this.items.pop();
  }
  
  peek(): T | undefined {
    return this.items[this.items.length - 1];
  }
}

let numberStack = new Stack<number>();
numberStack.push(1);
numberStack.push(2);
numberStack.push(3);
console.log('Top of stack:', numberStack.peek());  // 3
console.log('Popped:', numberStack.pop());         // 3

let stringStack = new Stack<string>();
stringStack.push('first');
stringStack.push('second');
console.log('String stack top:', stringStack.peek());  // 'second'

// REAL-WORLD EXAMPLE: API Response wrapper
interface ApiResponse<T> {
  data: T;
  status: number;
  message: string;
}

interface User {
  id: number;
  name: string;
}

interface Product {
  sku: string;
  price: number;
}

let userResponse: ApiResponse<User> = {
  data: { id: 1, name: 'Alice' },
  status: 200,
  message: 'Success'
};

let productResponse: ApiResponse<Product> = {
  data: { sku: 'ABC123', price: 29.99 },
  status: 200,
  message: 'Success'
};

console.log('User:', userResponse.data.name);       // 'Alice'
console.log('Product:', productResponse.data.sku);  // 'ABC123'
```
