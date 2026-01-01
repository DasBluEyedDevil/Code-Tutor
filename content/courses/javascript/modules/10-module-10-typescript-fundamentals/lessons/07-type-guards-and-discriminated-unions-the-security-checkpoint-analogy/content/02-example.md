---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Type Guards and Type Narrowing in TypeScript

// BASIC TYPE GUARDS - Using typeof
function formatValue(value: string | number): string {
  // TypeScript doesn't know if value is string or number here
  
  if (typeof value === 'string') {
    // Inside this block, TypeScript KNOWS value is a string!
    return value.toUpperCase();  // String methods available
  } else {
    // Here, TypeScript KNOWS value is a number!
    return value.toFixed(2);  // Number methods available
  }
}

console.log(formatValue('hello'));  // 'HELLO'
console.log(formatValue(42.567));   // '42.57'

// TYPE GUARDS WITH ARRAYS - Using Array.isArray
function processData(data: string | string[]): void {
  if (Array.isArray(data)) {
    // TypeScript knows: data is string[]
    console.log('Array with', data.length, 'items');
    data.forEach(item => console.log('  -', item));
  } else {
    // TypeScript knows: data is string
    console.log('Single string:', data);
  }
}

processData('hello');                    // Single string: hello
processData(['apple', 'banana', 'cherry']); // Array with 3 items

// DISCRIMINATED UNIONS - The 'kind' pattern
interface Circle {
  kind: 'circle';  // Literal type - always exactly 'circle'
  radius: number;
}

interface Rectangle {
  kind: 'rectangle';  // Literal type - always exactly 'rectangle'
  width: number;
  height: number;
}

interface Triangle {
  kind: 'triangle';  // Literal type
  base: number;
  height: number;
}

// Union of all shapes - the 'kind' property discriminates between them
type Shape = Circle | Rectangle | Triangle;

function calculateArea(shape: Shape): number {
  // Switch on the discriminant property 'kind'
  switch (shape.kind) {
    case 'circle':
      // TypeScript knows: shape is Circle
      return Math.PI * shape.radius ** 2;
      
    case 'rectangle':
      // TypeScript knows: shape is Rectangle
      return shape.width * shape.height;
      
    case 'triangle':
      // TypeScript knows: shape is Triangle
      return 0.5 * shape.base * shape.height;
  }
}

let circle: Circle = { kind: 'circle', radius: 5 };
let rect: Rectangle = { kind: 'rectangle', width: 10, height: 4 };
let tri: Triangle = { kind: 'triangle', base: 6, height: 8 };

console.log('Circle area:', calculateArea(circle).toFixed(2));   // 78.54
console.log('Rectangle area:', calculateArea(rect));              // 40
console.log('Triangle area:', calculateArea(tri));                // 24

// CUSTOM TYPE GUARDS - Using 'is' keyword
interface Dog {
  breed: string;
  bark(): void;
}

interface Cat {
  breed: string;
  meow(): void;
}

type Pet = Dog | Cat;

// Custom type guard function - returns 'pet is Dog'
function isDog(pet: Pet): pet is Dog {
  return 'bark' in pet;  // Dogs have a bark method
}

function isCat(pet: Pet): pet is Cat {
  return 'meow' in pet;  // Cats have a meow method
}

function makeNoise(pet: Pet): void {
  if (isDog(pet)) {
    // TypeScript knows: pet is Dog
    pet.bark();
  } else {
    // TypeScript knows: pet is Cat
    pet.meow();
  }
}

let myDog: Dog = {
  breed: 'Golden Retriever',
  bark() { console.log('Woof!'); }
};

let myCat: Cat = {
  breed: 'Siamese',
  meow() { console.log('Meow!'); }
};

makeNoise(myDog);  // Woof!
makeNoise(myCat);  // Meow!

// 'IN' OPERATOR TYPE GUARD
interface Admin {
  name: string;
  privileges: string[];
}

interface Employee {
  name: string;
  startDate: Date;
}

type Staff = Admin | Employee;

function printStaffInfo(staff: Staff): void {
  console.log('Name:', staff.name);  // Both types have 'name'
  
  if ('privileges' in staff) {
    // TypeScript knows: staff is Admin
    console.log('Privileges:', staff.privileges.join(', '));
  }
  
  if ('startDate' in staff) {
    // TypeScript knows: staff is Employee
    console.log('Start date:', staff.startDate.toDateString());
  }
}

let admin: Admin = { name: 'Alice', privileges: ['create', 'delete'] };
let employee: Employee = { name: 'Bob', startDate: new Date('2024-01-15') };

printStaffInfo(admin);     // Name: Alice, Privileges: create, delete
printStaffInfo(employee);  // Name: Bob, Start date: Mon Jan 15 2024

// NULLISH TYPE GUARDS - Handling null and undefined
function greet(name: string | null | undefined): string {
  if (name === null || name === undefined) {
    return 'Hello, stranger!';
  }
  // TypeScript knows: name is string
  return `Hello, ${name.toUpperCase()}!`;
}

console.log(greet('alice'));     // Hello, ALICE!
console.log(greet(null));        // Hello, stranger!
console.log(greet(undefined));   // Hello, stranger!

// EXHAUSTIVENESS CHECKING - Catch missing cases
type Color = 'red' | 'green' | 'blue';

function getColorHex(color: Color): string {
  switch (color) {
    case 'red':
      return '#FF0000';
    case 'green':
      return '#00FF00';
    case 'blue':
      return '#0000FF';
    default:
      // This should never happen if all cases are handled
      const _exhaustiveCheck: never = color;
      return _exhaustiveCheck;
  }
}

console.log('Red hex:', getColorHex('red'));    // #FF0000
console.log('Blue hex:', getColorHex('blue'));  // #0000FF
```
